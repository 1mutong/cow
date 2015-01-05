using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Web.Mvc;

namespace CowSite.Controllers.Breed
{
    /// <summary>
    /// 产犊控制器
    /// </summary>
    public class CalfController : Controller
    {
        CalvingBLL bllCalving = new CalvingBLL();

        public ActionResult Add()
        {
            return View("~/Views/Breed/Calf.cshtml");
        }

        public JsonResult InsertCalving()
        {
            Calving c = new Calving();
            string[] birthDate = Request.Form["Birthday"].Split('-');
            string[] birthTime = Request.Form["BirthTime"].Split(':');
            c.Birthday = new DateTime(Convert.ToInt32(birthDate[0]), Convert.ToInt32(birthDate[1]), Convert.ToInt32(birthDate[2]), Convert.ToInt32(birthTime[0]), Convert.ToInt32(birthTime[1]), Convert.ToInt32(birthTime[2]));
            c.EarNum = CowBLL.ConvertDislayEarNumToEarNum(Request.Form["EarNum"], UserBLL.Instance.CurrentUser.Pasture.ID);
            c.BirthType = (BirthType)Convert.ToInt32(Request.Form["BirthType"]);
            c.Difficulty = Request.Form["Difficulty"];
            c.PositionOfFetus = Request.Form["PositionOfFetus"];
            c.OperatorName = Request.Form["OperatorName"];
            c.NumberOfMale = Convert.ToInt32(Request.Form["NumberOfMale"]);
            c.NumberOfFemale = Convert.ToInt32(Request.Form["NumberOfFemale"]);
            c.Comment = Request.Form["Comment"];

            int result = bllCalving.InsertCalving(c);

            //产生产后任务
            TaskBLL tBLL = new TaskBLL();
            tBLL.CreateAfterBornTasks(c);
            //如有正常母犊，重定向到犊牛入群

            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }
	}
}