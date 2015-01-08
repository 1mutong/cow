using DairyCow.DAL.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.DAL
{
    public class UserDAL : BaseDAL
    {
        /// <summary>
        /// 获得所有配种员
        /// </summary>
        /// <returns></returns>
        public DataTable GetInseminationOperatorList()
        {
            DataTable inseminationOperatorList = null;

            string sql = string.Format(@"select au.ID, au.Name, au.Account, au.Password, au.RoleID, au.PastureID
                                        from Auth_User au where au.RoleID = 3");

            inseminationOperatorList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return inseminationOperatorList;
        }

        public DataTable GetInseminationOperatorTable(int pastureID)
        {
            DataTable inseminationOperatorList = null;

            string sql = string.Format(@"select au.ID, au.Name, au.Account, au.Password, au.RoleID, au.PastureID
                                        from Auth_User au where au.RoleID = 1 and au.PastureID={0}", pastureID);

            inseminationOperatorList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return inseminationOperatorList;
        }


        /// <summary>
        /// 获取牧场饲养员table
        /// </summary>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public DataTable GetFeederTable(int pastureID)
        {
            DataTable feederList = null;

            string sql = string.Format(@"select au.ID, au.Name, au.Account, au.Password, au.RoleID, au.PastureID
                                        from Auth_User au  where au.RoleID = 3 and au.PastureID={0}", pastureID);

            feederList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return feederList;
        }

        /// <summary>
        /// 获取牧场兽医table
        /// </summary>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public DataTable GetDoctorTable(int pastureID)
        {
            DataTable doctorList = null;

            string sql = string.Format(@"select au.ID, au.Name, au.Account, au.Password, au.RoleID, au.PastureID
                                        from Auth_User au  where au.RoleID=5 and au.PastureID={0}", pastureID);

            doctorList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return doctorList;
        }

        public DataTable GetCurrentUser(string account, string password)
        {
            DataTable dt = null;

            string sql = string.Format(@"select *
                        from auth_user
                        where account='{0}' and password='{1}'", account, password);

            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return dt;
        }

        public DataTable GetCurrentUser(string account)
        {
            DataTable dt = null;

            string sql = string.Format(@"select *
                        from auth_user
                        where account='{0}'", account);

            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return dt;
        }

        public DataTable GetRoles()
        {
            DataTable dt = null;

            string sql = @"select *
                        from auth_role";

            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return dt;
        }

        public DataTable GetPartRoles()
        {
            DataTable dt = null;

            string sql = @"select *
                        from auth_role where Name not in ('场长','Admin')";

            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return dt;
        }

        public DataTable GetUsers(string pastureID)
        {
            DataTable dt = null;
            string sql;

            if (string.IsNullOrEmpty(pastureID))
            {
                sql = @"select * from auth_user";
            }
            else
            {
                sql = string.Format(@"select *
                            from auth_user
                            where PastureID='{0}'", pastureID);
            }


            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 取得一个牧场某一角色的用户
        /// </summary>
        /// <param name="pastureId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataTable GetUsers(int pastureId, int roleId)
        {
            DataTable dt = null;
            string sql = string.Format(@"select *
                            from auth_user
                            where PastureID={0} and RoleID={1}", pastureId, roleId);
            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return dt;
        }

        public void InsertUser(string name, string account, string password, string roleID, string pastureID)
        {
            string sql = string.Format(@"insert into auth_user(Name,Account,Password,RoleID,PastureID)
                                         values ('{0}','{1}','{2}','{3}','{4}')",
                                        name, account, password, roleID, pastureID);
            dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public void DeleteUser(int id)
        {
            string sql = string.Format(@"delete from auth_user where ID='{0}'", id);
            dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public void ChangePassword(int userID, string password)
        {
            string sql = string.Format(@"update auth_user set Password='{0}' where ID='{1}'", password, userID);
            dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        //获得一个默认的兽医
        public DataTable GetDefaultDoctor(int id)
        {
            DataTable userList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[Name]
                                            ,[Account]
                                            ,[Password]
                                            ,[RoleID]
                                            ,[PastureID]
                                        FROM [1mutong].[dbo].[Auth_User] where ID = {0}", id);
            userList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return userList;
        }
    }
}
