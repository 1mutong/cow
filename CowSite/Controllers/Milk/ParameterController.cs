using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Milk
{
    public class ParameterController : Controller
    {
        MilkHallBLL bllMilkHall = new MilkHallBLL();

        //
        // GET: /Parameter/
        public ActionResult Edit()
        {
            //ViewBag.ID = id;
            //MilkHall milkHall = bllMilkHall.GetMilkHallByID(id);
            //ViewBag.VacuumPressure = milkHall.VacuumPressure;
            //ViewBag.Pulsation = milkHall.Pulsation;
            //ViewBag.CleanupCount = milkHall.CleanupCount;
            //ViewBag.Name = milkHall.Name;
            MilkHall milkHallExist = bllMilkHall.GetMilkHallByID();
            ViewBag.VacuumPressure = milkHallExist.VacuumPressure;
            ViewBag.Pulsation = milkHallExist.Pulsation;
            ViewBag.CleanupCount = milkHallExist.CleanupCount;
            return View("~/Views/Milk/Hall/Parameters.cshtml");
        }

        public JsonResult LoadTask()
        {
            MilkHall milkHallExist = bllMilkHall.GetMilkHallByID();
            ViewBag.VacuumPressure = milkHallExist.VacuumPressure;
            ViewBag.Pulsation = milkHallExist.Pulsation;
            ViewBag.CleanupCount = milkHallExist.CleanupCount;

            var milkData = new
            {
                vacuumPressure = milkHallExist.VacuumPressure,
                pulsation = milkHallExist.Pulsation,
                cleanupCount = milkHallExist.CleanupCount
            };
            return Json(milkData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(string VacuumPressure, string Pulsation, string CleanupCount)
        {
            //MilkHall milkHall = new MilkHall();
            //UpdateModel<MilkHall>(milkHall);
            //MilkHall milkHallExist = bllMilkHall.GetMilkHallByID(id);
            //milkHallExist.VacuumPressure = milkHall.VacuumPressure;
            //milkHallExist.Pulsation = milkHall.Pulsation;
            //milkHallExist.CleanupCount = milkHall.CleanupCount;
            //milkHallExist.ModifyTime = DateTime.Now;
            //bllMilkHall.UpdateMilkHallInfo(milkHallExist, id);
            //string VacuumPressure = Request.Form["vacPressure"];
            //string Pulsation = Request.Form["pulsat"];
            //string CleanupCount = Request.Form["cleaCount"];

            MilkHall milkHall = new MilkHall();

            milkHall.VacuumPressure = float.Parse(VacuumPressure);
            milkHall.Pulsation = Convert.ToInt32(Pulsation);
            milkHall.CleanupCount = Convert.ToInt32(CleanupCount);
            milkHall.ModifyTime = DateTime.Now;

            bllMilkHall.UpdateMilkHallInfo(milkHall);

            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}