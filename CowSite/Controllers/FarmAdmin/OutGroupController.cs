using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyCow.Model;
using DairyCow.BLL;

namespace CowSite.Controllers.FarmAdmin
{
    public class OutGroupController : Controller
    {
        public ActionResult List()
        {
            return View("~/Views/FarmAdmin/OutGroup/List.cshtml");
        }

        public JsonResult OutGroup()
        {
            Cow myCow = new Cow();
            myCow.DisplayEarNum = Request.Form["displayEarNum"].ToString();
            myCow.FarmCode=UserBLL.Instance.CurrentUser.Pasture.ID;
            myCow.EarNum = CowBLL.ConvertDislayEarNumToEarNum(myCow.DisplayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            
            Stray stray = new Stray();
            stray.EarNum = myCow.EarNum;
            stray.PastureID = myCow.FarmCode;
            stray.Reason = Request.Form["strayReason"].ToString();
            stray.StrayDate = Convert.ToDateTime(Request.Form["strayDate"]);
            stray.StrayType = Convert.ToInt32(Request.Form["strayType"]);
            stray.IsSold = Convert.ToInt32(Request.Form["isSold"]);
            if (stray.IsSold ==1)
            {
                stray.Price = Convert.ToSingle(Request.Form["price"]);
            }
            else
            {
                stray.Price = 0.0f;
            }
            StrayBLL strayBLL = new StrayBLL();
            strayBLL.StrayCow(stray);

            return Json(new { result = 1 });
        }
	}
}