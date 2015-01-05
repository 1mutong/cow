using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyCow.BLL;
using DairyCow.Model;

namespace CowSite.Controllers.Task
{
    public class TaskRegroupingController : Controller
    {
        public JsonResult LoadTask(string taskID)
        {
            TaskBLL bll = new TaskBLL();
            DairyTask v;
            v = bll.GetTaskByID(Int32.Parse(taskID));
            string displayEarNum = CowBLL.ConvertEarNumToDisplayEarNum(v.EarNum);

            GroupingRecordBLL grbBLL = new GroupingRecordBLL();
            GroupingRecord gr = grbBLL.GetGroupingRecordByTaskID(Int32.Parse(taskID));
            CowGroupBLL cgBLL = new CowGroupBLL();
            HouseBLL hBLL = new HouseBLL();
            CowGroup oldgroup = cgBLL.GetCowGroupInfo(gr.OriginalGroupID);
            CowGroup newgroup = cgBLL.GetCowGroupInfo(gr.TargetGroupID);
            House oldhouse = hBLL.GetHouseByID(UserBLL.Instance.CurrentUser.Pasture.ID, gr.OriginalHouseID);
            House newhouse = hBLL.GetHouseByID(UserBLL.Instance.CurrentUser.Pasture.ID, gr.TargetHouseID);

            return Json(new
            {
                EarNum = v.EarNum,
                DisplayEarNum = displayEarNum,
                ArrivalTime = v.ArrivalTime.ToString("yyyy-MM-dd"),
                Operator = v.OperatorID,
                OldGroup = oldgroup.Name,
                OldHouse = oldhouse.Name,
                NewGroup = newgroup.Name,
                NewHouse = newhouse.Name
            }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult LoadRegroupingInfo(string taskID)
        //{
        //    GroupingRecordBLL grbBLL = new GroupingRecordBLL();
        //    GroupingRecord gr = grbBLL.GetGroupingRecordByTaskID(Int32.Parse(taskID));
        //    CowGroupBLL cgBLL = new CowGroupBLL();
        //    HouseBLL hBLL = new HouseBLL();
        //    CowGroup oldgroup = cgBLL.GetCowGroupInfo(gr.OriginalGroupID);
        //    CowGroup newgroup = cgBLL.GetCowGroupInfo(gr.TargetGroupID);
        //    House oldhouse = hBLL.GetHouseByID(UserBLL.Instance.CurrentUser.Pasture.ID, gr.OriginalHouseID);
        //    House newhouse = hBLL.GetHouseByID(UserBLL.Instance.CurrentUser.Pasture.ID, gr.TargetHouseID);

        //    return Json(new
        //    {
        //        OldGroup =oldgroup.Name,
        //        OldHouse = oldhouse.Name,
        //        NewGroup = newgroup.Name,
        //        NewHouse = newhouse.Name
        //    }, JsonRequestBehavior.AllowGet);

        //}

        [EMuTongAuthorizeAttribute]
        public ActionResult SaveTask()
        {
            try
            {
                TaskBLL bll = new TaskBLL();
                DairyTask v = bll.GetTaskByID(Convert.ToInt32(Request.Form["id"]));
                v.ArrivalTime = DateTime.Parse(Request.Form["start"]);
                v.CompleteTime = DateTime.Parse(Request.Form["end"]);
                v.OperatorID = Convert.ToInt32(Request.Form["operatorName"]);
                bll.CompleteGrouping(v);
                return View("~/Views/Task/Index.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Task/TaskError.cshtml");
            }
        }
	}
}