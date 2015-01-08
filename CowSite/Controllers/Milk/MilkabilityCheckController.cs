using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Milk
{
    public class MilkabilityCheckController : Controller
    {
        IndividualProdcutionBLL bllIndividual = new IndividualProdcutionBLL();
        public ActionResult Add()
        {
            return View("~/Views/Milk/MilkabilityCheck/Add.cshtml");
        }
        public JsonResult AddMilkAbility(string earNum, string date, string ability, string round)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                var msg = "请选择日期！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else if (string.IsNullOrWhiteSpace(ability))
            {
                var msg = "请输入奶量！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                IndividualProdcution production = new IndividualProdcution();
                production.EarNum = CowBLL.ConvertDislayEarNumToEarNum(earNum, UserBLL.Instance.CurrentUser.Pasture.ID);
                production.MilkDate = Convert.ToDateTime(date);
                production.MilkWeight = float.Parse(ability);
                production.Round = round;

                bllIndividual.InsertIndividualProdcution(production);
                var msg = "奶量添加成功！";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SelectMilkCheckList()
        {
            List<MilkCheckModel> listobj = bllIndividual.GetMilkCheckList(UserBLL.Instance.CurrentUser.Pasture.ID);
            List<MilkCheckModel> listLiger = new List<MilkCheckModel>();
            foreach (var item in listobj)
            {
                
                listLiger.Add(new MilkCheckModel()
                {
                    EarNum = item.EarNum,
                    ID = item.ID,
                    MilkDate = item.MilkDate,
                    Weight = item.Weight,
                    Round = item.Round
                });

            }

            var gridData = new { Rows = listLiger, Total = listLiger.Count };
            return Json(gridData, JsonRequestBehavior.AllowGet);
        }
    }
}