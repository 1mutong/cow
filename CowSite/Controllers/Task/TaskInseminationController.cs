using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Web.Mvc;

namespace CowSite.Controllers.Task
{
    /// <summary>
    /// 发情配种任务处理
    /// </summary>
    public class TaskInseminationController : Controller
    {
        TaskBLL bllTask = new TaskBLL();
        InseminationBLL bllInsemination = new InseminationBLL();

        public JsonResult LoadTask()
        {
            var taskData = new
            {
                op = UserBLL.Instance.CurrentUser.ID,
                startTime = DateTime.Now.ToString("yyyy-MM-dd")
            };
            return Json(taskData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveTask(string id)
        {
            try
            {

                string displayEarNum = Request.Form["DisplayEarNum"];
                string startDate = Request.Form["start"];
                string endDate = Request.Form["end"];
                string operatorID = Request.Form["operatorName"]; //@德华 怀疑有错
                string knownWay = Request.Form["knownWay"];
                string semenNum = Request.Form["semNum"];
                string semenCount = Request.Form["semCount"];
                string desc = Request.Form["description"];

                Insemination i = new Insemination();
                i.EstrusDate = DateTime.Parse(startDate);
                i.OperateDate = DateTime.Parse(endDate);
                i.EarNum = CowBLL.ConvertDislayEarNumToEarNum(displayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
                i.operatorID = Convert.ToInt32(operatorID);
                i.EstrusFindType = Convert.ToInt32(knownWay);
                i.SemenNum = semenNum;
                i.InseminationNum = Convert.ToInt32(semenCount);
                i.Description = desc;

                DairyTask task = new DairyTask();
                task.TaskType = TaskType.InseminationTask;
                task.EarNum = CowBLL.ConvertDislayEarNumToEarNum(displayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
                task.ArrivalTime = DateTime.Parse(startDate);
                task.CompleteTime = DateTime.Parse(endDate);
                task.DeadLine = DateTime.Parse(endDate);
                task.RoleID = UserBLL.Instance.CurrentUser.Role.ID;
                task.PastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
                task.OperatorID = Convert.ToInt32(operatorID);

                bllTask.CompleteInsemination(task, i);

                return View("~/Views/Task/Index.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Task/TaskError.cshtml");
            }
        }
    }
}