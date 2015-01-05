using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Breed
{
    public class ReInspectionController : Controller
    {
        CowBLL bllCow = new CowBLL();
        InseminationBLL bllInsemination = new InseminationBLL();
        ReInspectionBLL bllReInspection = new ReInspectionBLL();
        //
        // GET: /ReInspection/
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                ViewBag.InseminationID = 0;
                ViewBag.EarNum = "0";
                ViewBag.OperateDate = DateTime.Today.ToShortDateString().ToString();
                ViewBag.ReInspectResult = null;
                ViewBag.Operator = "";
                ViewBag.HelpOperator = "";
                ViewBag.AfterInsemDays = null;
                ViewBag.AfterInitInspectDays = null;
                ViewBag.Description = "";
                return View("~/Views/Breed/ReInspection/Edit.cshtml");
            }
            else
            {
                ReInspection reInspection = bllReInspection.GetReInspectionInfo(Convert.ToInt32(id));
                if (reInspection == null)
                {
                    ViewBag.InseminationID = 0;
                    ViewBag.EarNum = "0";
                    ViewBag.OperateDate = DateTime.Today.ToShortDateString().ToString();
                    ViewBag.ReInspectResult = null;
                    ViewBag.Operator = "";
                    ViewBag.HelpOperator = "";
                    ViewBag.AfterInsemDays = null;
                    ViewBag.AfterInitInspectDays = null;
                    ViewBag.Description = "";
                }
                else
                {
                    ViewBag.EarNum = id;
                    ViewBag.InseminationID = reInspection.InseminationID;
                    ViewBag.OperateDate = reInspection.OperateDate;
                    ViewBag.ReInspectResult = reInspection.ReInspectResult;
                    ViewBag.Operator = reInspection.Operator;
                    ViewBag.HelpOperator = reInspection.HelpOperator;
                    ViewBag.AfterInsemDays = reInspection.AfterInsemDays;
                    ViewBag.AfterInitInspectDays = reInspection.AfterInitInspectDays;
                    ViewBag.Description = reInspection.Description;
                }
                return View("~/Views/Breed/ReInspection/Edit.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Save(int id)
        {
            try
            {
                ReInspection reInspection = new ReInspection();
                UpdateModel<ReInspection>(reInspection);
                Cow cow = bllCow.GetCowInfo(reInspection.EarNum);
                if (cow.FarmCode == null)
                {
                    //弹框提示此牛不存在
                    return RedirectToAction("../Index/List");
                }
                else
                {
                    int inseminationID = bllInsemination.GetLatestInseminationID(reInspection.EarNum);
                    reInspection.InseminationID = inseminationID;
                    bool IsReInspectionExist = bllReInspection.IsReInspectionExist(reInspection.EarNum, reInspection.InseminationID);
                    if (IsReInspectionExist == true)
                    {
                        bllReInspection.UpdateReInspection(reInspection);
                    }
                    else
                    {
                        bllReInspection.InsertReInspection(reInspection);
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