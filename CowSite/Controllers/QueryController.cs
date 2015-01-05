using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers
{
    public class QueryController : Controller
    {
        public ActionResult CowGroup()
        {
            return View("~/Views/CowGroup/Query.cshtml");
        }
	}
}