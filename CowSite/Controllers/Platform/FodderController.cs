using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Web.Mvc;

namespace CowSite.Controllers.Platform
{
    /// <summary>
    /// 平台-饲料管理
    /// </summary>
    public class FodderController : Controller
    {
        public ActionResult List()
        {
            return View("~/Views/Platform/Fodder/List.cshtml");
        }

        public JsonResult AddFodder()
        {
            FodderBLL fodderBLL = new FodderBLL();
            Fodder f = new Fodder();
            f.Name = Request.Form["fodderName"].ToString();
            f.Description = Request.Form["description"].ToString();
            f.RefPrice = ToDouble(Request.Form["price"]);

            f.DM = ToDouble(Request.Form["DM"]);
            f.NND = ToDouble(Request.Form["NND"]);
            f.Ca = ToDouble(Request.Form["Ca"]);
            f.P = ToDouble(Request.Form["P"]);
            f.CP = ToDouble(Request.Form["CP"]);
            f.CF = ToDouble(Request.Form["CF"]);
            f.Fat = ToDouble(Request.Form["Fat"]);
            f.NFE = ToDouble(Request.Form["NFE"]);
            f.ASH = ToDouble(Request.Form["ASH"]);
            f.NDF = ToDouble(Request.Form["NDF"]);
            f.ADF = ToDouble(Request.Form["ADF"]);
            f.NPP = ToDouble(Request.Form["NPP"]);
            f.Arg = ToDouble(Request.Form["Arg"]);
            f.His = ToDouble(Request.Form["His"]);
            f.Ile = ToDouble(Request.Form["Ile"]);
            f.Leu = ToDouble(Request.Form["Leu"]);
            f.Lys = ToDouble(Request.Form["Lys"]);
            f.Met = ToDouble(Request.Form["Met"]);
            f.Cys = ToDouble(Request.Form["Cys"]);
            f.Phe = ToDouble(Request.Form["Phe"]);
            f.Tyr = ToDouble(Request.Form["Tyr"]);
            f.Thr = ToDouble(Request.Form["Thr"]);
            f.Trp = ToDouble(Request.Form["Trp"]);
            f.Val = ToDouble(Request.Form["Val"]);
            f.Na = ToDouble(Request.Form["Na"]);
            f.Cl = ToDouble(Request.Form["Cl"]);
            f.Mg = ToDouble(Request.Form["Mg"]);
            f.K = ToDouble(Request.Form["K"]);
            f.Fe = ToDouble(Request.Form["Fe"]);
            f.Cu = ToDouble(Request.Form["Cu"]);
            f.Mn = ToDouble(Request.Form["Mn"]);
            f.Zn = ToDouble(Request.Form["Zn"]);
            f.Se = ToDouble(Request.Form["Se"]);

            int temp = fodderBLL.InsertFodder(f);

            return Json(new { Result = temp });

        }

        private double ToDouble(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return 0.0;
            }
            else
            {
                return Convert.ToDouble(s);
            }
        }
	}
}