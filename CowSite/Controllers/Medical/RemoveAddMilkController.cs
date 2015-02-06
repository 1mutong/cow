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
    /// 去附乳
    /// </summary>
    public class RemoveAddMilkController : Controller
    {
        RemoveAddMilkBLL bllRemoveAddMilk = new RemoveAddMilkBLL();

        public ActionResult Add()
        {
            return View("~/Views/Medical/MilkBreast/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Save()
        {
            RemoveAddMilk removeAddMilk = new RemoveAddMilk();
            UpdateModel<RemoveAddMilk>(removeAddMilk);
            bllRemoveAddMilk.InsertRemoveAddMilkInfo(removeAddMilk);
            return View();
        }

        public JsonResult GetRemoveAddMilkList()
        {

            List<RemoveAddMilk> RemoveAddMilkList = bllRemoveAddMilk.GetRemoveAddMilkList();

            var gridData = new { Rows = RemoveAddMilkList, Total = RemoveAddMilkList.Count };
            return Json(gridData, JsonRequestBehavior.AllowGet);
        }
    }
}