using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyCow.Model;
using DairyCow.BLL;

namespace CowSite.Controllers.FarmAdmin
{
    public class InGroupController : Controller
    {
        public ActionResult List()
        {
            return View("~/Views/FarmAdmin/InGroup/List.cshtml");
        }

        public JsonResult GetFatherSemon()
        {
            string motherEarNum = Request.Form["motherDisplayEarNum"].ToString();
            int temp=CowBLL.ConvertDislayEarNumToEarNum(motherEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            if (temp==-1)
            {
                return Json(new { SemenNum = "" });
            }
            else
            {
                CowInfo cow = new CowInfo(temp);
                string s = cow.LatestInsemination.SemenNum;
                return Json(new { SemenNum = s });
            }
        }

        public JsonResult InGroup()
        {
            
            Cow myCow = new Cow();
            myCow.DisplayEarNum = Request.Form["displayEarNum"].ToString();
            myCow.Gender = Request.Form["sex"].ToString();
            myCow.BirthDate = Convert.ToDateTime(Request.Form["birthDate"]);
            myCow.Color = Request.Form["color"].ToString();
            myCow.GroupID = Convert.ToInt32(Request.Form["group"]);
            myCow.HouseID = Convert.ToInt32(Request.Form["house"]);
            myCow.FarmCode = UserBLL.Instance.CurrentUser.Pasture.ID;
            myCow.FatherID = Request.Form["fatherSemen"].ToString();
            myCow.MotherID = Request.Form["motherDisplayEarNum"].ToString();
            myCow.IsIll = false;
            myCow.IsStray = false;
            myCow.Status = Request.Form["breedStatus"].ToString();
            //----------------------Modify By LJW-------------------//
            if (Request.Form["PedometerID"] != null && Request.Form["PedometerID"].ToString() != "")
                myCow.Pedometer = Convert.ToInt32(Request.Form["PedometerID"]);
            //----------------------Modify By LJW-------------------//
            CowBLL cowBLL = new CowBLL();
           
            //插入牛基本信息
            cowBLL.InsertCow(myCow);

            
            //取回牛，主要取得ID
            Cow newCow = cowBLL.GetCowInfo(myCow.FarmCode, myCow.DisplayEarNum);

            //如为产后小犊牛，生成犊牛饲喂单（10天之内，最好3天之内）
            double days = DateTime.Now.Date.Subtract(myCow.BirthDate).TotalDays;
            if (days <= 10.0)
            {
                TaskBLL tBLL = new TaskBLL();
                DairyTask t = new DairyTask();
                t.ArrivalTime = DateTime.Now;
                t.DeadLine = DateTime.Now.AddDays(45.0);
                t.Status = DairyTaskStatus.Initial;
                t.TaskType = TaskType.CalfTask;
                t.EarNum = newCow.EarNum;
                tBLL.AddTask(t);
            }

            if (newCow.Status.Equals("已配未检")||newCow.Status.Equals("初检+")||newCow.Status.Equals("复检+"))
            {
                InseminationBLL insemBLL = new InseminationBLL();
                DateTime t = Convert.ToDateTime(Request.Form["inseminationDate"]);
                insemBLL.InsertFakeInsemination(newCow.EarNum, t);
            }
            int parity = Convert.ToInt32(Request.Form["parityNumber"]);
            if (parity>0)
            {
                DateTime t = Convert.ToDateTime(Request.Form["calvingDate"]);
                CalvingBLL calvingBLL = new CalvingBLL();
                //插入假产犊记录
                calvingBLL.InsertFakeCalvings(newCow.EarNum, t, parity);
            }

            return Json(new { Result = 1 });
        }
	}
}