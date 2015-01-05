using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers.Breed
{
    /// <summary>
    /// 配种控制器
    /// </summary>
    public class InseminationController : Controller
    {
        InseminationBLL bllInsemination = new InseminationBLL();
        CowGroupBLL bllCowGroup = new CowGroupBLL();
        CowBLL bllCow = new CowBLL();

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                //ViewBag.Insemination = Utility.JsonSerialize(insem);
                ViewBag.EarNum = "0";
                ViewBag.ParityNum = 0;
                ViewBag.InseminationNum = 0;
                ViewBag.SemenNum = "";
                ViewBag.SemenType = 0;
                ViewBag.EstrusFindType = 0;
                ViewBag.OperateDate = DateTime.Today.ToShortDateString().ToString();
                ViewBag.Operator = "";
                ViewBag.Description = "";
                ViewBag.EstrusDate = DateTime.Today.ToShortDateString().ToString();
                ViewBag.EstrusType = 0;
                ViewBag.EstrusFindPerson = "";
                return View("~/Views/Breed/Insemination/Edit.cshtml");
            }
            else
            {
                Insemination insemination = bllInsemination.GetInseminationInfo(Convert.ToInt32(id));
                if (insemination == null)
                {
                    ViewBag.EarNum = "0";
                    ViewBag.InseminationNum = 0;
                    ViewBag.SemenNum = "";
                    ViewBag.SemenType = 0;
                    ViewBag.EstrusFindType = 0;
                    ViewBag.OperateDate = DateTime.Today.ToShortDateString().ToString();
                    ViewBag.Operator = "";
                    ViewBag.Description = "";
                    ViewBag.EstrusDate = DateTime.Today.ToShortDateString().ToString();
                    ViewBag.EstrusType = 0;
                    ViewBag.EstrusFindPerson = "";
                }
                else
                {
                    ViewBag.EarNum = id;
                    ViewBag.InseminationNum = insemination.InseminationNum;
                    ViewBag.SemenNum = insemination.SemenNum;
                    ViewBag.SemenType = insemination.SemenType;
                    ViewBag.EstrusFindType = insemination.EstrusFindType;
                    ViewBag.OperateDate = insemination.OperateDate;
                    ViewBag.Operator = UserBLL.Instance.CurrentUser.Name; // insemination.OperatorID;
                    ViewBag.Description = insemination.Description;
                    ViewBag.EstrusDate = insemination.EstrusDate;
                    ViewBag.EstrusType = insemination.EstrusType;
                    ViewBag.EstrusFindPerson = insemination.EstrusFindPerson;
                }
                return View("~/Views/Breed/Insemination/Edit.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Save(int id)
        {
            try
            {
                Insemination insemination = new Insemination();
                // insemination.EarNum = id;                
                UpdateModel<Insemination>(insemination);
                Cow cow = bllCow.GetCowInfo(insemination.EarNum);
                int s = insemination.EarNum;
                //根据用户输入的配种信息，判断是否存在此头牛的信息，如果不存在弹框提示
                if (cow.FarmCode == null)
                {
                    return RedirectToAction("../Index/List");
                }
                else
                {
                    //ToDo:将insemination对象的数据保存到数据库中
                    bool isInsemExist = bllInsemination.IsInsemExist(insemination.EarNum, insemination.InseminationNum);
                    if (isInsemExist == true)
                    {
                        //bllInsemination.UpdateInseminationInfo(insemination);
                    }
                    else
                    {
                        bllInsemination.InsertInseminationInfo(insemination);
                    }
                    return RedirectToAction("../Index/List");
                }

            }
            catch (Exception e)
            {
                return View();
            }
        }

        //获取牛群配种列表
        public JsonResult GetCowGroupList()
        {
            List<CowGroup> lstCowGroup = bllCowGroup.GetCowGroupList(UserBLL.Instance.CurrentUser.Pasture.ID);
            var cowGroupData = new
            {
                Rows = lstCowGroup
            };
            return Json(cowGroupData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Assign()
        {
            return View("~/Views/Breed/Insemination/Assign.cshtml");
        }

        public JsonResult GetInseminationOperatorList()
        {
            List<User> lstInseminationOperator = UserBLL.Instance.GetInseminationOperatorList();
            return Json(lstInseminationOperator, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveInseminationOperator(int cowGroupID, int insemOperatorID)
        {
            var result = new
            {
                result = bllCowGroup.UpdateCowGroupInsemOperator(cowGroupID, insemOperatorID)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}