using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyCow.Model;
using DairyCow.BLL;

namespace CowSite.Controllers.Feed
{
    /// <summary>
    /// 干奶
    /// </summary>
    public class DryMilkController : Controller
    {
        
        //
        // GET: /DryMilk/
        public ActionResult Add()
        {
            return View("~/Views/Feed/DryMilk/Add.cshtml");
        }

        public JsonResult InsertDryMilk(string earNum, string dryDate, string drySitudation, string dryReason, string operatorID)
        {
            DryMilk dry = new DryMilk();
            dry.EarNum = CowBLL.ConvertDislayEarNumToEarNum(earNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            dry.DryDate = DateTime.Parse(dryDate);
            dry.DrySituation = Int32.Parse(drySitudation);
            dry.OperatorID = Int32.Parse(operatorID);
            dry.DryReason = dryReason;
            int temp=CowInfo.InsertDryMilk(dry);
            var result = new
            {
                count = temp
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}