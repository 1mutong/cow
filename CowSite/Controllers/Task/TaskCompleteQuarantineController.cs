using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Task
{
    public class TaskCompleteQuarantineController : Controller
    {
        /// <summary>
        /// 检疫
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        UserBLL bllUser = new UserBLL();
        TaskBLL bllTask = new TaskBLL();
        CowBLL bllCow = new CowBLL();
        CowGroupBLL bllCowGroup = new CowGroupBLL();
        public JsonResult LoadTask(string taskID)
        {
            TaskBLL bllTask = new TaskBLL();
            DairyTask task = bllTask.GetTaskByID(Convert.ToInt32(taskID));

            //获得对应牛群的兽医
            Cow cow = bllCow.GetCowInfo(task.EarNum);
            CowGroup cowGroup = bllCowGroup.GetCowGroupInfo(cow.GroupID);
            int doctorID = cowGroup.DoctorID;
            User user = bllUser.GetDefaultDoctor(doctorID);

            var taskData = new
            {
                doctor = user.Role.Name,
                doctorID = user.Role.ID,
                earNum = CowBLL.ConvertEarNumToDisplayEarNum(task.EarNum)
            };
            return Json(taskData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveTask(string id)
        {
            try
            {
                Quarantine quarantine = new Quarantine();
                int PastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
                string QuarantineType = Request.Form["quarantineType"];
                string QuarantineDate = Request.Form["quarantineDate"];
                string QuarantineMethod = Request.Form["quarantineMethod"];
                string DoctorID = Request.Form["doctorID"];
                string Result = Request.Form["result"];
                string EarNum = Request.Form["DisplayEarNum"];

                quarantine.PastureID = PastureID;
                quarantine.QuarantineType = QuarantineType;
                quarantine.QuarantineDate = Convert.ToDateTime(QuarantineDate);
                quarantine.QuarantineMethod = QuarantineMethod;
                quarantine.DoctorID = Convert.ToInt32(DoctorID);
                quarantine.Result = Convert.ToInt32(Result);
                quarantine.EarNum = CowBLL.ConvertDislayEarNumToEarNum(EarNum, PastureID);

                //增加检疫记录
                bllTask.AddQuarantineRecord(quarantine);

                //更新检疫记录
                DairyTask task = bllTask.GetTaskByID(Convert.ToInt32(id));//To-do please get taskID 
                bllTask.CompleteQuarantine(task);

                return View("~/Views/Task/Index.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Task/TaskError.cshtml");
            }
        }
    }
}