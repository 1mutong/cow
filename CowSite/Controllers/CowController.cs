using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers
{
    public class CowController : Controller
    {
        CowBLL bllCow = new CowBLL();

        public ActionResult AssignGroup()
        {
            return View();
        }

        /// <summary>
        /// 牛档案卡Controller
        /// </summary>
        /// <returns></returns>
        public ActionResult CowDetail()
        {
            ViewBag.DisplayEarNum = Request.QueryString["displayEarNum"];
            return View("~/Views/Cow/Detail.cshtml");
        }

        public JsonResult GetCowInfo(string displayEarNum)
        {
            int earNum = CowBLL.ConvertDislayEarNumToEarNum(displayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            CowInfo myCow = new CowInfo(earNum);
            return Json(myCow, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CowList()
        {
            ViewBag.CowType = Request.QueryString["cowType"];
            return View("~/Views/Cow/List.cshtml");
        }

        public JsonResult GetCowList(string cowType)
        {
            FarmInfo farm = new FarmInfo(UserBLL.Instance.CurrentUser.Pasture.ID);

            List<CowInfo> cowList = new List<CowInfo>();

            switch (cowType)
            {
                case "经产牛":
                    cowList = farm.MultiParityCows;
                    break;
                case "青年牛":
                    cowList = farm.NullParityCows;
                    break;
                case "育成牛":
                    cowList = farm.BredCattleCows;
                    break;
                case "犊牛":
                    cowList = farm.Calfs;
                    break;
                case "泌乳牛":
                    cowList = farm.MilkCows;
                    break;
                case "干奶牛":
                    cowList = farm.DryMilkCows;
                    break;
                case "经产牛未配牛":
                    cowList = farm.UninseminatedMultiParity;
                    break;
                case "经产牛已配未检牛":
                    cowList = farm.InseminatedMultiParity;
                    break;
                case "经产牛已孕牛":
                    cowList = farm.PregnantMultiParity;
                    break;
                case "青年牛未配牛":
                    cowList = farm.UninseminatedNullParity;
                    break;
                case "青年牛已配未检牛":
                    cowList = farm.InseminatedNullParity;
                    break;
                case "青年牛已孕牛":
                    cowList = farm.PregnantNullParity;
                    break;
                default:
                    break;
            }

            return Json(new { Rows = cowList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证显示耳号是否存在
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckDisplayEarNum(string displayEarNum)
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(displayEarNum))
            {
                int nID = CowBLL.ConvertDislayEarNumToEarNum(displayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
                if (nID != -1)
                {
                    result = true;
                }
            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过显示耳号取得受孕天数
        /// </summary>
        /// <param name="displayEarNum"></param>
        /// <returns></returns>
        public JsonResult GetDaysOfPregnant(string displayEarNum)
        {
            int earNum = CowBLL.ConvertDislayEarNumToEarNum(displayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            CowInfo cow = new CowInfo(earNum);
            int daysOfPregnant = cow.DaysOfPregnant;
            return Json(new { DaysOfPregnant = daysOfPregnant }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCowImage(string displayEarNum)
        {
            List<string> lstImage = new List<string>();

            int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            string rootPath = Server.MapPath("~/CowImage");

            for (int i = 1; i <= 3; i++)
            {
                string cowImageName = string.Format("{0}\\{1}-{2}.jpg", pastureID, displayEarNum, i);
                string cowImagePath = Path.Combine(rootPath, cowImageName);
                if (!System.IO.File.Exists(cowImagePath))
                {
                    cowImageName = "Default-" + i.ToString() + ".png";
                }
                else
                {
                    cowImageName = string.Format("{0}/{1}-{2}.jpg", pastureID, displayEarNum, i);
                }
                lstImage.Add(cowImageName);
            }

            return Json(lstImage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadCowImage()
        {
            string cowImageUrl = string.Empty;

            string displayEarNum = Request.Form["DisplayEarNum"];
            string imageIndex = Request.Form["ImgIndex"];
            Stream imgStream = Request.Files["ImgFile"].InputStream;
            if (imgStream != null && imgStream.Length > 0)
            {
                byte[] bytes = new byte[imgStream.Length];
                imgStream.Read(bytes, 0, bytes.Length);

                int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
                string rootPath = Server.MapPath("~/CowImage");
                cowImageUrl = string.Format("{0}/{1}-{2}.jpg", pastureID, displayEarNum, imageIndex);
                string cowImageName = string.Format("{0}\\{1}-{2}.jpg", pastureID, displayEarNum, imageIndex);
                string cowImagePath = Path.Combine(rootPath, cowImageName);
                if (System.IO.File.Exists(cowImagePath))
                {
                    System.IO.File.Delete(cowImagePath);
                }
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(cowImagePath)))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(cowImagePath));
                }
                System.IO.File.WriteAllBytes(cowImagePath, bytes);
            }
            return Json(new { ImgUrl = cowImageUrl }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCowInFarm(string displayEarNum)
        {
            int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            bool result = this.bllCow.CheckCowInFarm(displayEarNum, pastureID);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 取得配种天数/怀孕天数>210天的牛
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCowForInseminatedDayGT210()
        {
            FarmInfo farm = new FarmInfo();
            List<CowInfo> lstCow = farm.CowInfoList.FindAll(cow => cow.DaysOfPregnant > 210);
            return Json(new { Rows = lstCow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCowForPregnantDayGT225()
        {
            FarmInfo farm = new FarmInfo();
            List<CowInfo> lstCow = farm.CowInfoList.FindAll(cow => cow.DaysOfPregnant > 210);
            return Json(new { Rows = lstCow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCowLiteInfo(string displayEarNum)
        {
            CowLite cow = this.bllCow.GetCowLiteInfo(displayEarNum);
            return Json(cow, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 插入转群任务
        /// </summary>
        /// <returns></returns>
        public JsonResult InsertChangeGroupTask()
        {
            bool bResult = false;
            int earNum = CowBLL.ConvertDislayEarNumToEarNum(Request["DisplayEarNum"].ToString(), UserBLL.Instance.CurrentUser.Pasture.ID);
            int pastureID=UserBLL.Instance.CurrentUser.Pasture.ID;
            TaskBLL bllTask = new TaskBLL();
            int operatorID = bllTask.AssignTask(pastureID, earNum, Role.FARM_FEEDER_ID);
            if (earNum != 0)
            {
                DairyTask t = new DairyTask
                {
                    EarNum =earNum,
                    TaskType = TaskType.GroupingTask,
                    Status = DairyTaskStatus.Initial,
                    PastureID = pastureID,
                    OperatorID = operatorID,
                    ArrivalTime = DateTime.Now, //转群当天要做
                    InputTime = DateTime.Now,
                    DeadLine = DateTime.Now.AddDays(15)
                };
                
                bResult = bllTask.AddTask(t);

                GroupingRecord gRecord = new GroupingRecord
                {
                    TaskID = t.ID,
                    EarNum = t.EarNum,
                    TargetGroupID = Convert.ToInt32(Request["TargetGroupID"]),
                    TargetHouseID = Convert.ToInt32(Request["TargetHouseID"]),
                    OriginalGroupID = Convert.ToInt32(Request["OriginalGroupID"]),
                    OriginalHouseID = Convert.ToInt32(Request["OriginalHouseID"])
                };

                GroupingRecordBLL bllGroupingRecord = new GroupingRecordBLL();
                bllGroupingRecord.InsertGroupingRecord(gRecord);
            }

            return Json(bResult, JsonRequestBehavior.AllowGet);
        }
    }
}