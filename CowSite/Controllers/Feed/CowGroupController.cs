using Common;
using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers.Feed
{
    public class CowGroupController : Controller
    {
        CowGroupBLL bllCowGroup = new CowGroupBLL();
        HouseBLL bllHouse = new HouseBLL();
        int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;

        public JsonResult GetCowGroupInfo()
        {
            List<CowGroup> lstCowGroup = bllCowGroup.GetCowGroupList(pastureID);
            var cowGroupData = new
            {
                Rows = lstCowGroup
            };
            return Json(cowGroupData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用于select绑定
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCowGroupList()
        {
            List<CowGroup> lstCowGroup = bllCowGroup.GetCowGroupList(pastureID);
            return Json(lstCowGroup, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用于select绑定
        /// </summary>
        /// <returns></returns>
        public JsonResult GetHouses()
        {
            List<House> lstHouse = bllHouse.GetHouseList(pastureID);
            return Json(lstHouse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCowGroup()
        {
            List<CowGroup> lstCowGroup = bllCowGroup.GetCowGroupList(pastureID);
            return Json(new { Rows = lstCowGroup }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 分群提示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRemindCows()
        {
            FarmInfo fi = new FarmInfo(UserBLL.Instance.CurrentUser.Pasture.ID);
            List<CowInfo>  cows = fi.GetNeedRegroupingCows(UserBLL.Instance.CurrentUser.Pasture.ID);
            return Json(new { Rows = cows }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            ViewBag.CowGroupID = Request.QueryString["id"];
            return View("~/Views/CowGroup/List.cshtml");
        }

        /// <summary>
        /// 弹出新增牛群详细对话框
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCowGroup()
        {
            return View("~/Views/CowGroup/Add.cshtml");
        }

        public ActionResult Remind()
        {
            return View("~/Views/CowGroup/Remind.cshtml");
        }

        public ActionResult Assign()
        {
            return View("~/Views/CowGroup/Assign.cshtml");
        }

        public ActionResult ChangeGroup()
        {
            return View("~/Views/CowGroup/ChangeGroup.cshtml");
        }

        //assign 相关 json
        public JsonResult GetPastureInsemOperators()
        {
            UserBLL uBLL = new UserBLL();
            List<User> list = uBLL.GetInseminationOperatorList(UserBLL.Instance.CurrentUser.Pasture.ID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPastureDoctors()
        {
            UserBLL uBLL = new UserBLL();
            List<User> list = uBLL.GetDoctorList(UserBLL.Instance.CurrentUser.Pasture.ID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPastureFeeders()
        {
            UserBLL uBLL = new UserBLL();
            List<User> list = uBLL.GetFeederList(UserBLL.Instance.CurrentUser.Pasture.ID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateOperators(string cowGroupID,string insemID, string feederID, string doctorID)
        {
            CowGroupBLL cBLL = new CowGroupBLL();
            int gID=Int32.Parse(cowGroupID);
            int inID=Int32.Parse(insemID);
            int fID=Int32.Parse(feederID);
            int dID=Int32.Parse(doctorID);
            int i1 = cBLL.UpdateCowGroupInsemOperator(gID, inID);
            int i2 = cBLL.UpdateCowGroupFeeder(gID, fID);
            int i3 = cBLL.UpdateCowGroupDoctor(gID, dID);
            var result = new { Count = i1 + i2 + i3 };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCowGroupItem(string id)
        {
            return Json(this.bllCowGroup.GetCowGroupInfo(Convert.ToInt32(id)), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除牛群
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(string id)
        {
            int i = bllCowGroup.DeleteCowGroup(Convert.ToInt32(id));
            if (i == 0)
            {
                var msg = "牛群存在牛不能删除！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var msg = "牛群删除成功！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 增加牛群
        /// </summary>
        /// <returns></returns>
        public JsonResult Add()
        {
            CowGroup group = new CowGroup();
            group.PastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            group.Name = Request.Form["Name"];
            group.TypeNum = Convert.ToInt32(Request.Form["Type"]);
            group.Description = Request.Form["Description"];
            bllCowGroup.AddCowGroupWithBasicInfo(group);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}