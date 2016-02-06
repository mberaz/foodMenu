using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodMenu.Web.Controllers
{
    public class TestingController :Controller
    {
        // GET: Testing
        public ActionResult FileUpload ()
        {
            ViewBag.id = 1;
            return View();
        }
    }
}