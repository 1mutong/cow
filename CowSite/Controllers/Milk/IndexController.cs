using DairyCow.BLL;
using DairyCow.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace CowSite.Controllers.Milk
{
    public class IndexController : Controller
    {
        MilkHallBLL bllMilkHall = new MilkHallBLL();
        MilkRecordBLL milkBLL = new MilkRecordBLL();

        public ActionResult Index()
        {
            return View("~/Views/Milk/Index.cshtml");
        }

        public JsonResult GetMilkHallList()
        {
            List<MilkHall> lstMilkHall = bllMilkHall.GetMilkHallList();
            var milkHallData = new
            {
                Rows = lstMilkHall
            };
            return Json(milkHallData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMilkData(string startDate,string endDate)
        {
            DateTime sDate=DateTime.Parse(startDate);
            DateTime eDate=DateTime.Parse(endDate);
            //List<DailyReport> reportList=reportBLL.GetDailyReport(UserBLL.Instance.CurrentUser.Pasture.ID,sDate,eDate);
            List<MilkRecord> list = milkBLL.GetMilkRecords(UserBLL.Instance.CurrentUser.Pasture.ID, sDate, eDate);
            var reportData = new
            {
                Rows = list
            };
            return Json(reportData, JsonRequestBehavior.AllowGet);
        }
    }
}