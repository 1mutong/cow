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
    /// 复检任务单
    /// </summary>
    public class TaskReInspectionController : Controller
    {
        ReInspectionBLL bllReInspection = new ReInspectionBLL();
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
                string operatorName = Request.Form["operatorName"];
                string pregnantStatus = Request.Form["pregnantStatus"];

                ReInspection i = new ReInspection();
                i.OperateDate = DateTime.Parse(endDate);
                i.ReInspectResult = Convert.ToInt32(pregnantStatus);
                i.Operator = Convert.ToInt32(operatorName);
                i.EarNum = CowBLL.ConvertDislayEarNumToEarNum(earNum, UserBLL.Instance.CurrentUser.Pasture.ID);

                DairyTask task = bllTask.GetTaskByID(Convert.ToInt32(id));

                // 完成复检任务，同时增加复检信息
                task.CompleteTime = i.OperateDate;
                task.Status = DairyTaskStatus.Completed;
                task.OperatorID = i.Operator;
                bllTask.CompleteReInspection(task, i);

                return View("~/Views/Task/Index.cshtml");
            }
            catch (Exception ex)
            {
                return View("~/Views/Task/TaskError.cshtml");
            }
        }
    }
}