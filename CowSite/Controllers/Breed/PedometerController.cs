using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyCow.Model;
using DairyCow.BLL;

namespace CowSite.Controllers.Breed
{
    public class PedometerController : Controller
    {
        CowBLL bllcow = new CowBLL();
        // GET: Pedometer
        public ActionResult Index()
        {
            return View("~/Views/Breed/Pedometer.cshtml");
        }

        public JsonResult GetPedometer()
        {//通过耳号,找计步器号
            int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            
            string DisplayEarNum = Request.Form["FirstDisplayEarNum"];

            int PedometerID = this.bllcow.GetpedometerByEarNum(DisplayEarNum, pastureID);
            if (PedometerID == -2)
            {//该牛不存在
                return Json(new { Result = -1 });
            }
            else if (PedometerID == -1)
            {//该牛是没有计步器
                return Json(new { Result = -2 });
            }
            else
                return Json(new { Result = PedometerID });//该牛的计步器号
        }

        public JsonResult GetEarNum()
        {//通过计步器号,获得耳号
            int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            int temp = 0;
            int PedometerID = Convert.ToInt32(Request.Form["Pedometer"]);
            temp = bllcow.PedometerInTable(PedometerID, pastureID);
            if (temp == 0)//没有该计步器
                return Json(new { Result = 0 });
            else
            {
                string DisplayEarNum = this.bllcow.GetEarNumByPedometer(PedometerID, pastureID);

                return Json(new { Result = DisplayEarNum });//有该计步器,返还对应耳号
            }
        }
        public JsonResult PedometerChange()
        {//修改
            int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            Pedometer MyPedometer = new Pedometer();
            MyPedometer.OldDisplayEarNum = Request.Form["FirstDisplayEarNum"].ToString();
            MyPedometer.NewDisplayEarNum = Request.Form["SecondDisplayEarNum"].ToString();
            MyPedometer.PedometerID = Convert.ToInt32(Request.Form["Pedometer"]);
            int temp = 0;
            if (MyPedometer.OldDisplayEarNum != "")//如果占有计步器号的牛存在
            {//给没有计步器的牛,换上该牛的计步器
                temp = bllcow.UpdatePedometer(MyPedometer.OldDisplayEarNum, MyPedometer.NewDisplayEarNum, MyPedometer.PedometerID, pastureID);

                return Json(new { Result = temp });
            }
            else
            { //如果占有计步器号的牛不存在,给一个没有计步器的牛,装上一个新的计步器
                temp = bllcow.UpdatePedometer(MyPedometer.NewDisplayEarNum, MyPedometer.PedometerID, pastureID);
            return Json(new { Result = temp });
            }



        }
        public JsonResult CheckNewEarNum()
        {//检查该牛在表中的情况
            int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            Pedometer MyPedometer = new Pedometer();
            MyPedometer.NewDisplayEarNum = Request.Form["SecondDisplayEarNum"].ToString();
            bool temp = false;
            temp = bllcow.CheckCowInFarm(MyPedometer.NewDisplayEarNum, pastureID);
            if (temp == false)
                return Json(new { Result = -1 });//不存在该牛    
            else
                return Json(new { Resul = 1 });
           


        }
        public JsonResult CheckCavling()
        {//查看是否是犊牛
            int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            Pedometer MyPedometer = new Pedometer();
            MyPedometer.NewDisplayEarNum = Request.Form["SecondDisplayEarNum"].ToString();
            int temp=0;
            temp = bllcow.CheckCowInCavling(MyPedometer.NewDisplayEarNum, pastureID);
            if (temp == 1)
                return Json(new { Result = -2 });//该牛是牛犊    
            else
                return Json(new { Resul = 1 });

        }
        public JsonResult CheckIfPedometer()
        {//查看是否有计步器
            int pastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
           string  DisplayEarNum=Request.Form["SecondDisplayEarNum"].ToString();
            int PedometerID = this.bllcow.GetpedometerByEarNum(DisplayEarNum, pastureID);


            if (PedometerID == -2)
            {
                return Json(new { Result = 1 });
            }
            else if (PedometerID == -1)
            {
                return Json(new { Result = 1 });
            }
            else
                return Json(new { Result = -3 });//有计步器

        }
        public JsonResult GetPedometerList()
        {//得到有计步器的所有牛
            List<Cow> MyCow=new List<Cow>();
            MyCow = bllcow.GetPedometerList(UserBLL.Instance.CurrentUser.Pasture.ID);
            var PedometerData = new
            {
                Rows = MyCow
            };
            return Json(PedometerData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePedometer(string DisplayEarNum)
        {//摘除该牛的计步器
            bllcow.DeletePedometer(DisplayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        
    }
}