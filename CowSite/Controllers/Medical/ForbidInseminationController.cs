using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Medical
{
    public class ForbidInseminationController : Controller
    {
        InseminationBLL bllInsemination = new InseminationBLL();
        ForbidInseminationBLL bllForbidInsemination = new ForbidInseminationBLL();

        public ActionResult Edit()
        {
            //ViewBag.EarNum = id;
            return View("~/Views/Medical/ForbidInsemination/Edit.cshtml");
        }


        public JsonResult Forbid(string EarNum, string Date, string Operator, string Description)
        {
            int earNum = CowBLL.ConvertDislayEarNumToEarNum(EarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            // 更新牛的禁配状态
            bllInsemination.ForbidInsemination(earNum);
            ForbidInsemination forbidInsemination = new ForbidInsemination();
            forbidInsemination.EarNum = earNum;
            forbidInsemination.Operator = Convert.ToInt32(Operator);
            forbidInsemination.OperateDate = Convert.ToDateTime(Date);
            forbidInsemination.Description = Description;
            // 插入牛的禁配信息
            bllForbidInsemination.InsertForbidInseminationInfo(forbidInsemination);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}