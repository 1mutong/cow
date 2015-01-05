using Common;
using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Feed
{
    public class PastureController : Controller
    {
        private PastureBLL bllPasture = new PastureBLL();

        public JsonResult GetPasture()
        { 
            return Json(this.bllPasture.GetPastures(), JsonRequestBehavior.AllowGet);
        }
	}
}