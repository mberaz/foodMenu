﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodMenu.Web.Controllers
{
    public class ClientsController :Controller
    {
        // GET: Clients
        public ActionResult Index ()
        {
            return View();
        }

        public ActionResult NewClient ()
        {
            return View();
        }

        public ActionResult EditClient (int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}