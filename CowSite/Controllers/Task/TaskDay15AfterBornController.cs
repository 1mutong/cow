using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Task
{
    public class TaskDay15AfterBornController : Controller
    {
        public JsonResult LoadTask(string taskID)
        {
            TaskBLL bll = new TaskBLL();
            DairyTask v;
            v = bll.GetTaskByID(Convert.ToInt32(taskID));
            string displayEarNum = CowBLL.ConvertEarNumToDisplayEarNum(v.EarNum);

            return Json(new
            {
                EarNum = v.EarNum,
                DisplayEarNum = displayEarNum,
                ArrivalTime = v.ArrivalTime.ToString("yyyy-MM-dd"),
                Operator = v.OperatorID
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveTask()
        {
            try
            {
                TaskBLL bll = new TaskBLL();
                DairyTask v = bll.GetTaskByID(Convert.ToInt32(Request.Form["id"]));
                v.ArrivalTime = DateTime.Parse(Request.Form["start"]);
                v.CompleteTime = DateTime.Parse(Request.Form["end"]);
                v.OperatorID = Convert.ToInt32(Request.Form["operatorName"]);

                bll.CompleteDay15AfterBorn(v);
                return View("~/Views/Task/Index.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Task/TaskError.cshtml");
            }
        }
    }

}