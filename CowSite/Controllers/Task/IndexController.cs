using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Task
{
    public class IndexController : Controller
    {
        /// <summary>
        /// 任务调整
        /// </summary>
        /// <returns></returns>
        public ActionResult Adjust()
        {
            return View("~/Views/Task/Adjust.cshtml");
        }
        /// <summary>
        /// 任务列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View("~/Views/Task/Index.cshtml");
        }
        /// <summary>
        /// 发情/配种任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskInsemination()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskInsemination.cshtml");
        }
        /// <summary>
        /// 妊检初检任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskInitialInspection()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskInitialInspection.cshtml");
        }
        /// <summary>
        /// 妊检复检任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskReInspection()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskReInspection.cshtml");
        }
        /// <summary>
        /// 产前21天任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskDay21ToBorn()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskDay21ToBorn.cshtml");
        }
        /// <summary>
        /// 产前7天任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskDay7TorBorn()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskDay7ToBorn.cshtml");
        }
        /// <summary>
        /// 产后3天任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskDay3AfterBorn()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskDay3AfterBorn.cshtml");
        }
        /// <summary>
        /// 产后10天任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskDay10AfterBorn()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskDay10AfterBorn.cshtml");
        }
        /// <summary>
        /// 产后15天任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskDay15AfterBorn()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskDay15AfterBorn.cshtml");
        }
        /// <summary>
        /// 免疫任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskCompleteImmune()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskCompleteImmune.cshtml");
        }
        /// <summary>
        /// 检疫任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskCompleteQuarantine()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskCompleteQuarantine.cshtml");
        }
        /// <summary>
        /// 分群任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskCompleteGrouping()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskRegrouping.cshtml");
        }
        /// <summary>
        /// 犊牛饲喂任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskCompleteCalf()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskCompleteCalf.cshtml");
        }
        /// <summary>
        /// 分群任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskRegrouping()
        {
            ViewBag.TaskID = Request.QueryString["taskID"];
            return View("~/Views/Task/TaskRegrouping.cshtml");
        }
    }
}