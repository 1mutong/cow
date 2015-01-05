using DairyCow.BLL;
using System.Web.Mvc;

namespace CowSite.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public JsonResult CurrentUserInfo()
        {
            return Json(UserBLL.Instance.CurrentUser, JsonRequestBehavior.AllowGet);
        }
	}
}