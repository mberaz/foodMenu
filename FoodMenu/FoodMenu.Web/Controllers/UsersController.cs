﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodMenu.Web.Controllers
{
    public class UsersController :Controller
    {
        // GET: Users
        public ActionResult Edit ()
        {
            return View();
        }
    }
}