using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Feed
{
    /// <summary>
    /// 牛舍维护
    /// </summary>
    public class CowHouseController : Controller
    {
        HouseBLL bllHouse = new HouseBLL();
        CowGroupBLL bllCowGroup = new CowGroupBLL();
        int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
        public JsonResult GetCowHouseInfo()
        {
            int pastureID = Convert.ToInt32(UserBLL.Instance.CurrentUser.Pasture.ID);
            List<House> lstHouse = bllHouse.GetHouseList(pastureID);
            var cowHouseData = new
            {
                Rows = lstHouse
            };
            return Json(cowHouseData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return View("~/Views/Feed/CowHouse/List.cshtml");
        }

        //用于select绑定
        public JsonResult GetCowHouseList()
        {
            List<House> lstHouse = bllHouse.GetHouseList(pastureID);
            return Json(lstHouse, JsonRequestBehavior.AllowGet);
        }

        //增加牛舍
        public JsonResult Add(string houseName, string groupID)
        {
            House house = new House();
            house.PastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            if (string.IsNullOrWhiteSpace(houseName))
            {
                var msg = "请输入牛舍名称！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                house.Name = houseName;
                if (Convert.ToInt32(groupID) != 0)
                {
                    house.GroupID = Convert.ToInt32(groupID);
                    //增加分配牛群的牛舍
                    bllHouse.AddHouse(house);
                }
                else
                {
                    //增加未分配牛群的牛舍
                    bllHouse.AddUnusedHouse(house);
                }
                var msg = "牛舍添加成功！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        //删除牛舍（当此牛舍不存在牛时）
        public JsonResult Delete(string name, string groupID, string id)
        {
            House house = new House();
            house.ID = Convert.ToInt32(id);
            house.Name = name;
            house.GroupID = Convert.ToInt32(groupID);
            house.PastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            //删除牛群
            int i = bllHouse.DeleteHouse(house);
            if (i == 0)
            {
                var message = "牛舍中存在牛群不能删除！";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var message = "牛舍删除成功！";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Update(string name, string GroupID, string id)
        {
            House house = new House();
            house.ID = Convert.ToInt32(id);
            bllHouse.UpdateHouseGroup(house, Convert.ToInt32(GroupID));
            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}