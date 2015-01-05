using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Medical
{
    public class DiagnosesController : Controller
    {
        private MedicalBLL bllMedical = new MedicalBLL();
        private CowBLL bllCow = new CowBLL();

        /// <summary>
        /// 取得所有疾病
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDiseases()
        {
            return Json(bllMedical.GetDiseases(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 取得所有疾病类型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDiseasesType()
        {
            return Json(bllMedical.GetDiseasesType(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View("~/Views/Medical/Diagnoses/Add.cshtml");
        }

        public JsonResult InsertCare()
        {
            Care c = new Care();
            c.DisplayEarNum = Request.Form["DisplayEarNum"];
            c.Disease_Id = Convert.ToInt32(Request.Form["diseases"]);
            c.Prescription = Request.Form["Prescription"];
            c.Date = DateTime.Now;
            c.LeftFront = Convert.ToInt32(Request.Form["pLeftFront"]);
            c.RightFront = Convert.ToInt32(Request.Form["pRightFront"]);
            c.LeftBack = Convert.ToInt32(Request.Form["pLeftBack"]);
            c.RightBack = Convert.ToInt32(Request.Form["pRightBack"]);

            c.EarNum = CowBLL.ConvertDislayEarNumToEarNum(c.DisplayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            c.DoctorID = UserBLL.Instance.CurrentUser.ID;

            bllMedical.InsertCare(c);
            CowBLL bllCow = new CowBLL();
            bllCow.UpdateCowIllStatus(c.EarNum, true);
            return Json(new { Result = 1}, JsonRequestBehavior.AllowGet);
        }
	}
}