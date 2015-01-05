using Common;
using DairyCow.BLL;
using DairyCow.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers.Feed
{
    public class FormulaController : Controller
    {
        private FormulaBLL bllFormula = new FormulaBLL();        

        public ActionResult Select(string id)
        {
            ViewBag.CowGroupID = id;
            return View("~/Views/Feed/Formula/Select.cshtml");
        }

        public ActionResult Modify(string formulaID)
        {
            ViewBag.FormulaID = formulaID;
            return View("~/Views/Feed/Formula/Modify.cshtml");
        }

        public JsonResult UpdateFormulaOfCowGroup(string formulaID, string cowGroupID)
        {
            this.bllFormula.UpdateFormulaOfCowGroup(formulaID, cowGroupID);
            var result = new
            {
                result = 1
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}