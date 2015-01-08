using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Feed
{
    public class GradeController : Controller
    {
        GradeBLL bllGrade = new GradeBLL();
        CowBLL bllCow = new CowBLL();
        //
        // GET: /Grade/
        public ActionResult Add(string id)
        {
            ViewBag.EarNum = id;
            return View("~/Views/Feed/Grade/Add.cshtml");
        }

        public JsonResult Save(string earNum, string date, string height, string weight, string chest, string grade, string description)
        {
            Grade cowGrade = new Grade();

            cowGrade.EarNum = CowBLL.ConvertDislayEarNumToEarNum(earNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            cowGrade.MeasureDate = Convert.ToDateTime(date);
            cowGrade.Height = Convert.ToInt32(height);
            cowGrade.Weight = Convert.ToInt32(weight);
            cowGrade.Chest = Convert.ToInt32(chest);
            cowGrade.Score = Convert.ToInt32(grade);
            cowGrade.Description = description;

            Cow cow = bllCow.GetCowInfo(cowGrade.EarNum);
            if (cow.FarmCode == null)
            {
                var msg = "此牛不存在！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bllGrade.InsertGradeInfo(cowGrade);
                var msg = "保存成功！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetGradeList()
        {
            List<Grade> GradeList = bllGrade.GetGradeList();
            var gridData = new { Rows = GradeList, Total = GradeList.Count };
            return Json(gridData, JsonRequestBehavior.AllowGet);


        }
    }
}