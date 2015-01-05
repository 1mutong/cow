using DairyCow.BLL;
using DairyCow.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Medical
{
    /// <summary>
    /// 修蹄
    /// </summary>
    public class RepairHoofController : Controller
    {
        RepairHoofBLL bllRepairHoof = new RepairHoofBLL();
        //
        // GET: /RepairHoof/
        public ActionResult Add()
        {
            return View("~/Views/Medical/FootParing/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Save()
        {
            RepairHoof repairHoof = new RepairHoof();
            UpdateModel<RepairHoof>(repairHoof);
            bllRepairHoof.InsertRepairHoofInfo(repairHoof);
            return RedirectToAction("../Index/List");
        }
    }
}