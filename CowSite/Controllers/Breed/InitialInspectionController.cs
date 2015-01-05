using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Breed
{
    public class InitialInspectionController : Controller
    {
        InitialInspectionBLL bllInitialInspection = new InitialInspectionBLL();
        InseminationBLL bllInsemination = new InseminationBLL();
        CowBLL bllCow = new CowBLL();

        //
        // GET: /InitialInspection/
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                ViewBag.InseminationID = 0;
                ViewBag.EarNum = "0";
                ViewBag.OperateDate = DateTime.Today.ToShortDateString().ToString();
                ViewBag.InspectResult = "-1";
                ViewBag.Operator = "";
                ViewBag.HelpOperator = "";
                ViewBag.InspectWay = null;
                ViewBag.AfterInsemDays = null;
                ViewBag.Description = "";
                return View("~/Views/Breed/InitialInspection/Edit.cshtml");
            }
            else
            {
                InitialInspection initialInspection = bllInitialInspection.GetInitialInspectionInfo(Convert.ToInt32(id));
                if (initialInspection == null)
                {
                    ViewBag.InseminationID = 0;
                    ViewBag.EarNum = "0";
                    ViewBag.OperateDate = DateTime.Today.ToShortDateString().ToString();
                    ViewBag.InspectResult = "-1";
                    ViewBag.Operator = "";
                    ViewBag.HelpOperator = "";
                    ViewBag.InspectWay = null;
                    ViewBag.AfterInsemDays = null;
                    ViewBag.Description = "";
                }
                else
                {
                    ViewBag.EarNum = id;
                    ViewBag.InseminationID = initialInspection.InseminationID;
                    ViewBag.OperateDate = initialInspection.OperateDate;
                    ViewBag.InspectResult = initialInspection.InspectResult;
                    ViewBag.Operator = initialInspection.Operator;
                    ViewBag.HelpOperator = initialInspection.HelpOperator;
                    ViewBag.InspectWay = initialInspection.InspectWay;
                    ViewBag.AfterInsemDays = initialInspection.AfterInsemDays;
                    ViewBag.Description = initialInspection.Description;
                }
                return View("~/Views/Breed/InitialInspection/Edit.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Save(string id)
        {
            try
            {
                InitialInspection initialInspection = new InitialInspection();
                UpdateModel<InitialInspection>(initialInspection);
                Cow cow = bllCow.GetCowInfo(initialInspection.EarNum);
                if (cow.FarmCode == null)
                {
                    //弹框提示此牛不存在
                    return RedirectToAction("../Index/List");
                }
                else
                {
                    int inseminationID = bllInsemination.GetLatestInseminationID(initialInspection.EarNum);
                    initialInspection.InseminationID = inseminationID;
                    bool IsInitialInspectionExist = bllInitialInspection.IsInitialInspectionExist(initialInspection.EarNum, initialInspection.InseminationID);
                    if (IsInitialInspectionExist == true)
                    {
                        bllInitialInspection.UpdateInitialInspection(initialInspection);
                    }
                    else
                    {
                        bllInitialInspection.InsertInitialInspection(initialInspection);
                    }
                    return RedirectToAction("../Index/List");
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}