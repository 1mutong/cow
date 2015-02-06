using Common;
using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers
{
    public class QueryController : Controller
    {
        CowSelectBLL bll = new CowSelectBLL();
        public ActionResult CowGroup()
        {
            return View("~/Views/CowGroup/Query.cshtml");
        }

        public JsonResult GetSelectCowList()
        {
            List<Condition> ListCondition = new List<Condition>();
            List<CowListModel> CowSelectList = bll.Search(UserBLL.Instance.CurrentUser.Pasture.ID);
            var gridData = new { Rows = CowSelectList, Total = CowSelectList.Count };
            return Json(gridData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BindDropDownList()
        {
            List<SelectListItem> select1 = new List<SelectListItem>
         {
             new SelectListItem { Text = "犊牛群a", Value = "1" },
             new SelectListItem { Text = "泌乳牛a", Value = "2" }
            
         };

            ViewData["select1"] = new SelectList(select1, "Value", "Text", "此处为默认项的值");

            return View();
        }
    }
}