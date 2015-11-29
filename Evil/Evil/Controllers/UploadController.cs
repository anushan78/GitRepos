using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Evil.Service;
using Evil.Service.Dto;

namespace Evil.Controllers
{
    [RoutePrefix("api/upload")]
    public class UploadController : ApiController
    {
        public UploadController(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        private ICustomerService CustomerService { get; set; }

        [ResponseType(typeof (List<CustomerEntry>))]
        public IHttpActionResult GetAll()
        {
            var customers = CustomerService.GetAll();
            var result = (customers != null) ? (IHttpActionResult) Ok(customers) : NotFound();

            return result;
;       }
    }
}
