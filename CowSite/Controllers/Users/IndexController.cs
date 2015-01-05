using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Users
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        public ActionResult List()
        {
            return View("~/Views/Users/List.cshtml");
        }
    }
}