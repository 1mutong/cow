using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Script.Serialization;

namespace CowSite
{
    //存放数据的用户实体
    public class EMuTongUserDataPrincipal : IPrincipal
    {
        //数据源
        //private readonly MingshiEntities mingshiDb = new MingshiEntities();

        public string UserId { get; set; }

        //这里可以定义其他一些属性
        public List<int> RoleId { get; set; }

        //当使用Authorize特性时，会调用改方法验证角色 
        public bool IsInRole(string role)
        {
            return true;
        //    //找出用户所有所属角色
        //    var userroles = mingshiDb.UserRole.Where(u => u.UserId == UserId).Select(u => u.Role.RoleName).ToList();

        //    var roles = role.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    return (from s in roles from userrole in userroles where s.Equals(userrole) select s).Any();
        }

        //验证用户信息
        public bool IsInUser(string user)
        {
            //找出用户所有所属角色
            var users = user.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //return mingshiDb.User.Any(u => users.Contains(u.UserName));
            return true;
        }

        [ScriptIgnore]    //在序列化的时候忽略该属性
        public IIdentity Identity { get { throw new NotImplementedException(); } }
    }
}