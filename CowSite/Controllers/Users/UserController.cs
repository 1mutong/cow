using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CowSite.Controllers.Users
{
    public class UserController : Controller
    {
        UserBLL bllUser = new UserBLL();
        //
        // GET: /User/
        public ActionResult List()
        {
            return View("~/Views/Users/List.cshtml");
        }

        public JsonResult GetUserList()
        {
            List<User> userList = bllUser.GetUsers();
            var userData = new
            {
                Rows = userList
            };
            return Json(userData, JsonRequestBehavior.AllowGet);
        }
        //用于select
        public JsonResult GetUserItems()
        {
            List<User> userList = bllUser.GetUsers();
            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoleItems()
        {
            List<Role> roleList = bllUser.GetPartRoles();
            return Json(roleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save()
        {
            ViewBag.PastureName = UserBLL.Instance.CurrentUser.Pasture.Name;
            return View("~/Views/Users/AddUser.cshtml");
        }

        [HttpPost]
        public ActionResult SaveAddedUser()
        {

            string name = Request.Form["Name"];
            string account = Request.Form["Account"];
            string roleID = Request.Form["RoleID"];
            string password = Request.Form["Password"];
            string ensurePassword = Request.Form["EnsurePassword"];

            bllUser.InsertUser(name, account, password, roleID, UserBLL.Instance.CurrentUser.Pasture.ID.ToString());
            return RedirectToAction("../User/List");
        }

        public JsonResult Delete(string userID)
        {
            //删除用户            
            bllUser.DeleteUser(Convert.ToInt32(userID));
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePassword()
        {
            ViewBag.UserName = UserBLL.Instance.CurrentUser.Name;
            ViewBag.UserAccount = UserBLL.Instance.CurrentUser.Account;
            return View("~/Views/Users/UpdatePassword.cshtml");
        }
        [HttpPost]
        public ActionResult UpdateUserPassword()
        {
            User user = new User();
            user.ID = UserBLL.Instance.CurrentUser.ID;
            UpdateModel<User>(user);
            bllUser.ChangePassword(user.ID, user.Password);
            return RedirectToAction("../User/List");
        }
    }
}