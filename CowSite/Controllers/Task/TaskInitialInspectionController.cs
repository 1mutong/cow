using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Task
{
    /// <summary>
    /// 初检
    /// </summary>
    public class TaskInitialInspectionController : Controller
    {
        InitialInspectionBLL bllInitialInspection = new InitialInspectionBLL();
        TaskBLL bllTask = new TaskBLL();
        public JsonResult LoadTask(string taskID)
        {
            DairyTask task = bllTask.GetTaskByID(Convert.ToInt32(taskID));
            var taskData = new
            {
                startTime = task.ArrivalTime.ToString("yyyy-MM-dd"),
                earNum = CowBLL.ConvertEarNumToDisplayEarNum(task.EarNum),
                op = task.OperatorID
            };
            return Json(taskData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveTask(string id)
        {
            try
            {
                string startDate = Request.Form["start"];
                string endDate = Request.Form["end"];
                string earNum = Request.Form["DisplayEarNum"];
                string operatorID = Request.Form["operatorName"];
                string pregnantStatus = Request.Form["pregnantStatus"];

                InitialInspection i = new InitialInspection();

                i.OperateDate = DateTime.Parse(endDate);
                i.InspectResult = Convert.ToInt32(pregnantStatus);
                i.Operator = Convert.ToInt32(operatorID);
                i.EarNum = CowBLL.ConvertDislayEarNumToEarNum(earNum, UserBLL.Instance.CurrentUser.Pasture.ID);

                //取原任务
                DairyTask task = bllTask.GetTaskByID(Convert.ToInt32(id));
                task.CompleteTime = i.OperateDate;//完成时间
                task.Status = DairyTaskStatus.Completed;
                task.OperatorID = i.Operator;

                //完成初检任务，同时增加初检信息
                bllTask.CompleteInitialInspection(task, i);

                return View("~/Views/Task/Index.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Task/TaskError.cshtml");
            }
        }
    }
}