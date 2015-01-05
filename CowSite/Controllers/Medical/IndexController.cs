using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyCow.BLL;
using DairyCow.Model;

namespace CowSite.Controllers.Medical
{
    /// <summary>
    /// 繁殖首页处理
    /// </summary>
    public class IndexController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Medical/Index.cshtml");
        }   
     
        public JsonResult GetDiseaseCount()
        {
            MedicalBLL mb = new MedicalBLL();
            int diseaseID= Convert.ToInt32(Request.Form["diseases"]);
            DateTime startDate=Convert.ToDateTime( Request.Form["startDate"]);
            DateTime endDate=Convert.ToDateTime( Request.Form["endDate"]);
            int count = mb.GetCareCowsCount(diseaseID, startDate, endDate);
            return Json(new { Count = count }, JsonRequestBehavior.AllowGet);
        }
	}
}