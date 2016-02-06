using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using FoodMenu.Utils;
using FoodMenu.BL;

namespace FoodMenu.WebApi.Controllers
{
    [RoutePrefix("Upload")]
    public class UploadController :ApiController
    {

        UsersBL usersBl;
        public UploadController ()
        {
            usersBl = new UsersBL();
        }
        [Route("Files")]
        [HttpPost]
        public async Task<IHttpActionResult> MyFileUpload ()
        {
            var test = Request.GetQueryNameValuePairs();
            var id = test.First(f=>f.Key=="id").Value.ToInt();

            if(!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach(var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');

                // var imageFolder = Utility.AppSetting("ImageFolderPath");
                var buffer = await file.ReadAsByteArrayAsync();

                // System.IO.File.WriteAllBytes(imageFolder + filename,buffer);

                await usersBl.UpdateImage(id,filename,buffer);

            }

            return Ok();
        }
    }
}
