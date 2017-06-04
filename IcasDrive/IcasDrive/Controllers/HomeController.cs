using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using IcasDrive.Models;

namespace IcasDrive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var examPaperViewModel = new ExamPaperViewModel();
            // Todo: Get Values from Db
            examPaperViewModel.Grades = new List<SelectListItem>()
            {
               new SelectListItem { Text = "Introductory", Value = "1" }
            };

            examPaperViewModel.Subjects = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Science", Value = "1" }
            };

             return View(examPaperViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            //var test = model.SelectedSubject;
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
                AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "ASP.NET MVC Sample"
                });

                Google.Apis.Drive.v2.Data.File file = new Google.Apis.Drive.v2.Data.File();
                file.Title = "anushan78";
                file.Description = "test";
                file.MimeType = "application/pdf";

                byte[] byteArray = System.IO.File.ReadAllBytes(@"F:\Files\Documents\Tickets\Confirmation _ Village Cinemas.pdf");
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

                try
                {
                    FilesResource.InsertMediaUpload request = service.Files.Insert(file, stream, "applicaion/pdf");
                    var awList = await request.UploadAsync();
                }
                catch(Exception ex)
                {
                    return View();
                }

                var list = await service.Files.List().ExecuteAsync();
                //var tempFile = await service.Files.Get("").ExecuteAsync();
                ViewBag.Message = "FILE COUNT IS: " + list.Items.Count();
                return View("Index");
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }
    }
}
