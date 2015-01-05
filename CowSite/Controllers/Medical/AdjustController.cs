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
    /// 康复调群
    /// </summary>
    public class AdjustController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/CowGroup/Adjust.cshtml");
        }

        public JsonResult GetSickCows()
        {
            CowBLL cBLL = new CowBLL();
            List<Cow> list = cBLL.GetCowList().FindAll(p=>p.IsIll);
            var cowData = new
            {
                Rows = list
            };
            return Json(cowData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecoverCows(string cowID)
        {
            CowBLL cBLL = new CowBLL();
            int earN=Int32.Parse(cowID);
            Cow myCow = cBLL.GetCowInfo(earN);
            int temp=cBLL.UpdateCowIllStatus(earN, false);
            if (temp==1)
            {
                //success
                return Json(new {msg="牛状态成功改为健康。"},JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "牛状态未改变。" }, JsonRequestBehavior.AllowGet);
            }

        }
	}
}