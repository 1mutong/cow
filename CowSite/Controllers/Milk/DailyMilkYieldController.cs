using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Web.Mvc;

namespace CowSite.Controllers.Milk
{
    public class DailyMilkYieldController : Controller
    {
        MilkRecordBLL bllMilkRecord = new MilkRecordBLL();

        public ActionResult Add()
        {
            return View("~/Views/Milk/DailyMilkYield/Add.cshtml");
        }

        public JsonResult SaveMilkShipping()
        {
            MilkSale milkSale = new MilkSale();
            milkSale.PastureID = UserBLL.Instance.CurrentUser.Pasture.ID;     
            milkSale.MilkDate = Convert.ToDateTime(Request.Form["MilkDate"]);
            milkSale.TankerNum = Request.Form["TankerNum"];
            milkSale.Decoding = Request.Form["Decoding"];
            milkSale.ShipCode = Request.Form["ShipCode"];
            milkSale.TruckNum = Request.Form["TruckNum"];
            if (!string.IsNullOrWhiteSpace(Request.Form["MilkWeight"]))
            {
                milkSale.MilkWeight = (float)Convert.ToDouble(Request.Form["MilkWeight"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Fat"]))
            {
                milkSale.Fat = (float)Convert.ToDouble(Request.Form["Fat"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Protein"]))
            {
                milkSale.Protein = (float)Convert.ToDouble(Request.Form["Protein"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["NonFatSolid"]))
            {
                milkSale.NonFatSolid = (float)Convert.ToDouble(Request.Form["NonFatSolid"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Lactose"]))
            {
                milkSale.Lactose = (float)Convert.ToDouble(Request.Form["Lactose"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Microbe"]))
            {
                milkSale.Microbe = (float)Convert.ToDouble(Request.Form["Microbe"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["IcePoint"]))
            {
                milkSale.IcePoint = (float)Convert.ToDouble(Request.Form["IcePoint"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Acidity"]))
            {
                milkSale.Acidity = (float)Convert.ToDouble(Request.Form["Acidity"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Amount"]))
            {
                milkSale.Amount = (float)Convert.ToDouble(Request.Form["Amount"]);
            }
            milkSale.Company = Request.Form["Company"];
            int result = bllMilkRecord.InsertMilkSale(milkSale);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveOtherMilk()
        {
            OtherMilk milkSale = new OtherMilk();
            milkSale.PastureID = UserBLL.Instance.CurrentUser.Pasture.ID;
            milkSale.MilkDate = Convert.ToDateTime(Request.Form["OtherMilkDate"]);
            if (!string.IsNullOrWhiteSpace(Request.Form["MilkForCalf"]))
            {
                milkSale.MilkForCalf = (float)Convert.ToDouble(Request.Form["MilkForCalf"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["AbnormalSaleMilk"]))
            {
                milkSale.AbnormalSaleMilk = (float)Convert.ToDouble(Request.Form["AbnormalSaleMilk"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["BadMilk"]))
            {
                milkSale.BadMilk = (float)Convert.ToDouble(Request.Form["BadMilk"]);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["LeftMilk"]))
            {
                milkSale.LeftMilk = (float)Convert.ToDouble(Request.Form["LeftMilk"]);
            }
            int result = bllMilkRecord.InsertOtherMilkRecord(milkSale);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
    }
}