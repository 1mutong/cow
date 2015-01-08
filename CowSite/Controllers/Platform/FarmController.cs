using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DairyCow.BLL;
using DairyCow.Model;

namespace CowSite.Controllers.Platform
{
    /// <summary>
    /// 平台-牧场管理
    /// </summary>
    public class FarmController : Controller
    {
        private PastureBLL pastureBLL = new PastureBLL();
        private UserBLL userBLL = new UserBLL();
        public ActionResult List()
        {
            return View("~/Views/Platform/Farm/List.cshtml");
        }

        public JsonResult GetPastures()
        {
            List<Pasture> pastureList = pastureBLL.GetPastures();
            var pastureData = new
            {
                Rows = pastureList
            };
            return Json(pastureData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddPasture(string pastureName,string farmAdminAccount,string farmAdminName)
        {
            string msg = "";
            if (pastureName.Equals(string.Empty)||farmAdminAccount.Equals(string.Empty))
            {
                msg = "页面传入牧场名或牧场管理员账号不能为空。";
            }
            else
            {
                int i1=pastureBLL.AddPasture(pastureName);
                
                if(i1==1)
                {
                    
                    Pasture myPasture = pastureBLL.GetPasture(pastureName);
                    //copy pasture parameters
                    DairyParameterBLL.CopyPlatformParameters(myPasture.ID);
                    userBLL.InsertUser(farmAdminName, farmAdminAccount, "123", "8", myPasture.ID.ToString());
                    msg = "创建牧场成功！";
                }
                else
                {
                    msg = "牧场未能创建！";
                }
                
            }
            var Result = new
            {
                Msg = msg
            };
            return Json(Result,JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetPastureStatus(string pastureID,string isActive)
        {
            string msg = "";
            int id;
            bool idGood= Int32.TryParse(pastureID, out id);
            if (!idGood)
            {
                msg = "牧场ID不正确：" + pastureID;
            }
            else
            {
                if (isActive.Equals("1")||isActive.Equals("0"))
                {
                    if (isActive.Equals("1"))
                    {
                        if (pastureBLL.SetPastureActiveStatus(id,true) == 1)
                        {
                            msg = "牧场状态设置成功。";
                        }
                        else
                        {
                            msg = "牧场状态未设置。";
                        }
                    }
                    else
                    {
                        if (pastureBLL.SetPastureActiveStatus(id,false) == 1)
                        {
                            msg = "牧场状态设置成功";
                        }
                        else
                        {
                            msg = "牧场状态未设置。";
                        }
                    }
                    
                }
                else
                {
                    msg = "牧场设置状态不正确：" + isActive;
                }
                
            }
            var Result = new
            {
                Msg = msg
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
            
        }
	}
}