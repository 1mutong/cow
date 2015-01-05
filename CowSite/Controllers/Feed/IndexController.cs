using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers.Feed
{
    public class IndexController : Controller
    {
        CowGroupBLL bllCowGroup = new CowGroupBLL();

        //
        // GET: /Index/
        public ActionResult Index()
        {
            return View("~/Views/Feed/Index.cshtml");
        }
	}
}