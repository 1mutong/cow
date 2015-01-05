using Common;
using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers.Platform
{
    /// <summary>
    /// 平台-配方管理
    /// </summary>
    public class FormulaController : Controller
    {
        private FormulaBLL bllFormula = new FormulaBLL();

        public ActionResult List()
        {
            return View("~/Views/Platform/Formula/List.cshtml");
        }

        public ActionResult Assign()
        {
            return View("~/Views/Platform/Formula/Assign.cshtml");
        }

        public ActionResult Modify()
        {
            ViewBag.FormulaID = Request.QueryString["FormulaID"];
            return View("~/Views/Platform/Formula/Modify.cshtml");
        }

        public ActionResult ModifyFodderQuantity()
        {
            ViewBag.FormulaID = Request.QueryString["FormulaID"];
            ViewBag.FodderID = Request.QueryString["FodderID"];
            return View("~/Views/Platform/Formula/ModifyFodderQuantity.cshtml");
        }

        public ActionResult AddFodder()
        {
            ViewBag.FormulaID = Request.QueryString["FormulaID"];
            return View("~/Views/Platform/Formula/AddFodder.cshtml");
        }

        public ActionResult AddFormula()
        {
            return View("~/Views/Platform/Formula/AddFormula.cshtml");
        }

        public JsonResult SaveFodderQuantity(string formulaId, string fodderId, string fodderQuantity)
        {
            var result = new
            {
                result = this.bllFormula.UpdateFodderQuantity(formulaId, fodderId, fodderQuantity)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertFodder(string formulaId, string fodderId, string fodderQuantity)
        {
            var result = new
            {
                result = this.bllFormula.AddFodder(formulaId, fodderId, fodderQuantity)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFodder(string formulaId, string fodderId)
        {
            var result = new
            {
                result = this.bllFormula.DeleteFodder(formulaId, fodderId)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertFormula(string formulaName)
        {
            var result = new
            {
                formulaId = this.bllFormula.AddFormula(formulaName)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFormulaList()
        {
            List<Formula> lstFormula = this.bllFormula.GetFormulaList();
            return Json(lstFormula, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFormulaInfoList(string formulaID)
        {
            Formula formula = this.bllFormula.GetFormulaByIDWithFodders(formulaID);
            var formulaData = new
            {
                ID = formula.ID,
                Name = formula.Name,
                Rows = formula.FodderList
            };
            return Json(formulaData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveFormula(string formulaID, string formulaName, string fodderQuantity)
        {
            List<Fodder> lstFodder = Utility.Deserialize<List<Fodder>>(fodderQuantity);

            var result = new
            {
                result = this.bllFormula.SaveFormula(formulaID, formulaName, lstFodder)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// assign formula related json
        /// </summary>
        /// <param name="areAll"></param>
        /// <returns></returns>
        public JsonResult GetCowGroupInfo(string areAll)
        {
            CowGroupBLL bllCowGroup = new CowGroupBLL();
            List<CowGroup> lstCowGroup = bllCowGroup.GetCowGroupList();
            List<CowGroup> list = lstCowGroup.FindAll(p => p.FormulaID == 0);
            bool all = Int32.Parse(areAll) == 1 ? true : false;
            if (all)
            {
                var cowGroupData = new
                {
                    Rows = lstCowGroup
                };
                return Json(cowGroupData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var cowGroupData = new
                {
                    Rows = list
                };
                return Json(cowGroupData, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFormulas()
        {
            FormulaBLL fBLL = new FormulaBLL();
            List<Formula> list = fBLL.GetFormulaList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateFormula(string cowGroupID, string formulaID)
        {
            CowGroupBLL cBLL = new CowGroupBLL();
            int gID = Int32.Parse(cowGroupID);
            int fID = Int32.Parse(formulaID);
            int i2 = cBLL.UpdateCowGroupFormula(gID, fID);
            var result = new { Count = i2 };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}