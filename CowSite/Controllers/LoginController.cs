using DairyCow.BLL;
using DairyCow.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers
{
    public class LoginController : Controller
    {
        public JsonResult Login(string userAccount, string password)
        {
            UserBLL bllUser = new UserBLL();
            User user = bllUser.GetUser(userAccount, password);

            if (user != null && !string.IsNullOrWhiteSpace(user.Account))
            {
                // 验证成功，用户名密码正确，构造用户数据（可以添加更多数据，这里只保存用户Id）
                var userData = new EMuTongUserDataPrincipal { UserId = user.Account };
                // 保存Cookie
                EMuTongFormsAuthentication<EMuTongUserDataPrincipal>.SetAuthCookie(userAccount, userData, false);
                return Json(new { login = 1, menu = user.Role.Menus }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Login = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}