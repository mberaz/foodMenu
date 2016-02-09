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
    [RoutePrefix("Files")]
    public class FilesController :ApiController
    {

        UsersBL usersBl;
        public FilesController ()
        {
            usersBl = new UsersBL();
        }
        [Route("Upload")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> MyFileUpload ()
        {
            var test = Request.GetQueryNameValuePairs();
            var id = test.First(f => f.Key == "id").Value.ToInt();

            if(!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach(var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                await usersBl.UpdateImage(id,filename,buffer);

            }

            return Ok(true);
        }

        [Route("Logo/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFile (int id)
        {
            var image = await usersBl.GetLogo(id);

            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(image.Result.Bytes);
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = image.Result.Name;

            return result;
        }
    }

}
