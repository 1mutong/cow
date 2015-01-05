using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Web.Mvc;

namespace CowSite.Controllers.Feed
{
    public class EmptyController : Controller
    {
        EmptyRecordBLL bllEmptyRecord = new EmptyRecordBLL();
        CowGroupBLL bllCowGroup = new CowGroupBLL();

        public ActionResult Add()
        {
            return View("~/Views/Feed/Empty/Add.cshtml");
        }



        [HttpPost]
        public ActionResult Save()
        {
            try
            {
                string cowGroupID = Request.Form["cowGroupName"];
                string emptyHour = Request.Form["emHour"];

                EmptyRecord emptyRecord = new EmptyRecord();
                emptyRecord.CowGroupID = Convert.ToInt32(cowGroupID);
                emptyRecord.FormulaID = bllCowGroup.GetFormulaIDByGroupID(Convert.ToInt32(cowGroupID));//需要根据牛群ID获得配方ID
                emptyRecord.RecordUserID = UserBLL.Instance.CurrentUser.ID;
                emptyRecord.RecordTime = DateTime.Now;
                emptyRecord.EmptyHour = Convert.ToInt32(emptyHour);                

                bllEmptyRecord.InsertEmptyRecordInfo(emptyRecord);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("../../Feed/CowGroup/List");
        }
    }
}