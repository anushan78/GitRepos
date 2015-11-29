using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Evil.Core;
using System.Configuration;
using System.IO;
using Evil.Service.Dto;

namespace Evil.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var fileKeys = ConfigurationManager.AppSettings["FileListKeys"].Split(',');

            if (fileKeys.Any())
            {
                var selectItemList = new List<SelectListItem>();
                
                for (int count = 0; count < fileKeys.Length; count++)
                {
                    selectItemList.Add(new SelectListItem { Value = count.ToString(), Text = fileKeys[count] });
                }

                ViewBag.FileList = selectItemList;
            }

            return View();
        }

        public ActionResult Upload(string fileId)
        {
            var successList = new List<Tuple<string, string>>();
            var failList = new List<Tuple<string, string>>(); 
            var internalDataProvider = new HttpDataProvider(ConfigurationManager.AppSettings["InternalApiBaseUrl"]);
            var customers = internalDataProvider.GetData<List<CustomerEntry>>("api/upload");

            if (customers.Any())
            {
                var externalDataProvider = new HttpDataProvider(ConfigurationManager.AppSettings["ExternalApiBaseUrl"]);
                var fileName = Path.GetFileName(ConfigurationManager.AppSettings[fileId]);

                customers.ForEach(delegate(CustomerEntry customer)
                {
                    dynamic customerJson =
                        new
                        {
                            property = "Anushan",
                            customer = customer.FullName,
                            action = "order created",
                            value = customer.Value,
                            file = fileName
                        };

                    var resultJson = externalDataProvider.PostAndReturn<dynamic, dynamic>("upload", customerJson);

                    if (resultJson.added == "true")
                    {
                        var addedCustomerEntry = externalDataProvider.GetData<dynamic>(string.Format("check?hash={0}", resultJson.hash));
                        successList.Add(new Tuple<string, string>(string.Format("Hash: {0}", addedCustomerEntry.hash), string.Format("Customer: {0}", addedCustomerEntry.customer)));
                    }
                    else
                        failList.Add(new Tuple<string, string>(customer.FullName, resultJson.errors));
                });
            }

            ViewBag.SuccessList = successList;
            ViewBag.FailList = failList;
            
            return View();
        }
    }
}