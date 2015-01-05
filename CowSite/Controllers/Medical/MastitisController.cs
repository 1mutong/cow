using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Medical
{
    /// <summary>
    /// 隐乳检测
    /// </summary>
    public class MastitisController : Controller
    {
        MilkMastitisReportBLL bllMilkMastitisReport = new MilkMastitisReportBLL();
        CowBLL bllCow = new CowBLL();

        public ActionResult Add()
        {
            return View("~/Views/Medical/Mastitis/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Save()
        {
            MilkMastitisReport milkMastitisReport = new MilkMastitisReport();
            UpdateModel<MilkMastitisReport>(milkMastitisReport);
            Cow cow = bllCow.GetCowInfo(milkMastitisReport.EarNum);
            if (cow.FarmCode == 0)
            {
                //弹框提示此牛不存在
            }
            else
            {
                bllMilkMastitisReport.InsertMilkMastitisReportInfo(milkMastitisReport);
            }
            return RedirectToAction("../Index/List");
        }
    }
}