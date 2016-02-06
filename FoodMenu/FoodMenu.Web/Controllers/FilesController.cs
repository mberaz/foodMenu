using FoodMenu.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodMenu.Web.Controllers
{
    public class FilesController :Controller
    {
        UsersBL usersBl;
        public FilesController ()
        {
            usersBl = new UsersBL();
        }
        // GET: Files
        public async Task<ActionResult> UserLogo (int id)
        {
            var image = await usersBl.GetLogo(id);
            return new FileStreamResult(new System.IO.MemoryStream(image.Bytes),"image/" + System.IO.Path.GetExtension(image.Name).Replace(".",""));
        }
    }
}