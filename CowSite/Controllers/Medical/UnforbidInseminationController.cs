using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers.Medical
{
    public class UnForbidInseminationController : Controller
    {
        CowBLL bllCow = new CowBLL();
        InseminationBLL bllInsemination = new InseminationBLL();
        UnForbidInseminationBLL bllUnforbidInsemination = new UnForbidInseminationBLL();

        public ActionResult List()
        {
            return View("~/Views/Medical/UnForbidInsemination/List.cshtml");
        }

        public JsonResult UnForbid(string id)
        {
            bllInsemination.UnDoForbidInsemination(Convert.ToInt32(id));
            //UnForbidInsemination unForbidInsemination = new UnForbidInsemination();
            //UpdateModel<UnForbidInsemination>(unForbidInsemination);
            ////插入牛的解禁信息
            //bllUnforbidInsemination.InsertUnForbidInseminationInfo(unForbidInsemination);
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 取得所有禁配状态的牛
        /// </summary>
        /// <returns></returns>
        public JsonResult GetForbidCowList()
        {
            List<Cow> lstCow = bllCow.GetCowList(UserBLL.Instance.CurrentUser.Pasture.ID).FindAll(p => p.Status == "禁配");
            return Json(new { Rows = lstCow }, JsonRequestBehavior.AllowGet);
        }
    }
}