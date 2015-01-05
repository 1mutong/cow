using DairyCow.BLL;
using DairyCow.Model;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers
{
    public class HomeController : Controller
    {
        [EMuTongAuthorizeAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCurrentUserInfo()
        {
            return Json(new
            {
                UserBLL.Instance.CurrentUser
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 牛群结构图
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCowGroupSummary()
        {
            if (UserBLL.Instance.CurrentUser.Pasture == null)
            {
                return Json(null);
            }

            FarmInfo farm = new FarmInfo(UserBLL.Instance.CurrentUser.Pasture.ID);

            // 牛群整体结构
            ArrayList arrCowSummary = new ArrayList();
            arrCowSummary.Add(new { name = "经产牛", value = farm.CountOfMultiParity });
            arrCowSummary.Add(new { name = "青年牛", value = farm.CountOfNullParity });
            arrCowSummary.Add(new { name = "育成牛", value = farm.CountOfBredCattle });
            arrCowSummary.Add(new { name = "犊牛", value = farm.CountOfCalf });
            // 牛群泌乳状态
            ArrayList arrMilkCowSummary = new ArrayList();
            arrMilkCowSummary.Add(new { name = "泌乳牛", value = farm.CountOfMilkCows });
            arrMilkCowSummary.Add(new { name = "干奶牛", value = farm.CountOfDryMilkCows });
            // 经产牛繁殖状况
            ArrayList arrBreedSummary = new ArrayList();
            arrBreedSummary.Add(new { name = "未配牛", value = farm.CountOfUninseminatedMultiParity });
            arrBreedSummary.Add(new { name = "已配未检牛", value = farm.CountOfInseminatedMultiParity });
            arrBreedSummary.Add(new { name = "已孕牛", value = farm.CountOfPregnantMultiParity });
            // 青年牛繁殖状况
            ArrayList arrParitySummary = new ArrayList();
            arrParitySummary.Add(new { name = "未配牛", value = farm.CountOfUninseminatedNullParity });
            arrParitySummary.Add(new { name = "已配未检牛", value = farm.CountOfInseminatedNullParity });
            arrParitySummary.Add(new { name = "已孕牛", value = farm.CountOfPregnantNullParity });

            return Json(new
            {
                CowSummary = arrCowSummary,
                MilkCowSummary = arrMilkCowSummary,
                BreedSummary = arrBreedSummary,
                ParitySummary = arrParitySummary
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetToDoList()
        {
            TaskBLL task = new TaskBLL();
            List<DairyTask> lstTask;
            // 取当前用户的role,如果是场长，需要看到所有用户的未完成任务列表。
            if (UserBLL.Instance.CurrentUser.Role.IsDirector)
            {
                lstTask = task.GetUnfinishedTasks(UserBLL.Instance.CurrentUser.Pasture.ID);
            }
            else
            {
                lstTask = task.GetRecentUnfinishedTaskList(UserBLL.Instance.CurrentUser.ID, UserBLL.Instance.CurrentUser.Pasture.ID);
            }

            return Json(new { Rows = lstTask }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMenuID()
        {
            List<string> lstMenuID = UserBLL.Instance.CurrentUser.Role.Menus;
            return Json(lstMenuID, JsonRequestBehavior.AllowGet);
        }
    }
}