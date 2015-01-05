using Common;
using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Data;
using System.Text;

namespace DairyCow.DAL
{
    /// <summary>
    /// 配种数据库访问
    /// </summary>
    public class InseminationDAL : BaseDAL
    {
        public DataTable GetInseminationList()
        {
            DataTable inseminationList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[EarNum]
                                            ,[InseminationNum]
                                            ,[SemenNum]
                                            ,[SemenType]
                                            ,[EstrusFindType]
                                            ,[OperateDate]
                                            ,[Operator]
                                            ,[Description]
                                            ,[EstrusDate]
                                            ,[EstrusType]
                                            ,[EstrusFindPerson] 
                                        FROM [1mutong].[dbo].[Breed_Insemination]");
            inseminationList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return inseminationList;
        }

        //根据耳号得到一头牛最新的配种信息
        public DataTable GetInseminationList(int earNum)
        {
            DataTable inseminationList = null;
            string sql = string.Format(@"SELECT top(1) [ID] 
                                             ,[EarNum]
                                            ,[InseminationNum]
                                            ,[SemenNum]
                                            ,[SemenType]
                                            ,[EstrusFindType]
                                            ,[OperateDate]
                                            ,[Operator]
                                            ,[Description]
                                            ,[EstrusDate]
                                            ,[EstrusType]
                                            ,[EstrusFindPerson]
                                        FROM [1mutong].[dbo].[Breed_Insemination] 
                                        WHERE EarNum = {0} order by OperateDate DESC", earNum);
            inseminationList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return inseminationList;
        }

        public DataTable GetInseminationList(int earNum, int inseminationNum)
        {
            DataTable inseminationList = null;
            string sql = string.Format(@"select * from [Breed_Insemination] 
                                            where EarNum = {0} 
                                            and InseminationNum = {1}", earNum, inseminationNum);
            inseminationList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return inseminationList;
        }

        public DataTable GetInseminationTable(int earNum, DateTime lastCalvingDate)
        {
            DataTable inseminationList = null;
            string sql = string.Format(@"select * from [Breed_Insemination] 
                                            where EarNum ={0} 
                                            and OperateDate > '{1}'", earNum, lastCalvingDate.ToShortDateString());
            inseminationList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return inseminationList;
        }

        public bool IsInsemExist(int earNum, int inseminationNum)
        {
            string sql = string.Format(@"select * from [1mutong].[dbo].[Breed_Insemination] 
                                            where EarNum = {0}
                                            and InseminationNum = {1}", earNum, inseminationNum);
            object insemInfo = dataProvider1mutong.ExecuteScalar(sql, CommandType.Text);
            if (insemInfo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int InsertInseminationInfo(int earNum,int inseminationNum,string semenNum,int semenType,DateTime estrusDate,int estrusType,int estrusFindType,string estrusFindPerson,DateTime operateDate,string descrition,int operatorID)
        {
            string sql = String.Format(@"Insert [Breed_Insemination] ([EarNum]
                                                                      ,[InseminationNum]
                                                                      ,[SemenNum]
                                                                      ,[SemenType]
                                                                      ,[EstrusDate]
                                                                      ,[EstrusType]
                                                                      ,[EstrusFindType]
                                                                      ,[EstrusFindPerson]
                                                                      ,[OperateDate]
                                                                      ,[Description]
                                                                      ,[OperatorID])
                                                                       values ({0},{1},'{2}',{3},'{4}',{5},{6},'{7}','{8}','{9}',{10})",
                                     earNum, inseminationNum, semenNum, semenType, estrusDate, estrusType, estrusFindType, estrusFindPerson, operateDate, descrition, operatorID);
            //StringBuilder sql = new StringBuilder();
            ////sql.Append(@"insert into [1mutong].[dbo].[DairyCow_Insemination] values (" + insemination.EarNum + ")");
            //sql.Append(@"insert into [1mutong].[dbo].[Breed_Insemination] values ('" + insemination.EarNum + "',"
            //              + insemination.InseminationNum + ",'"
            //              + insemination.SemenNum + "',"
            //              + insemination.SemenType + ",'"
            //              + insemination.EstrusDate + "',"
            //              + insemination.EstrusType + ","
            //              + insemination.EstrusFindType + ",'"
            //              + insemination.EstrusFindPerson + "','"
            //              + insemination.OperateDate + "','"
            //              + insemination.Operator + "','"
            //              + insemination.Description + "')");

            ////UpdateCowStatus(insemination.EarNum, ECowStatus.YiPeiWeiJian);

            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
        /// <summary>
        /// 保存一头牛的配种信息
        /// </summary>
        /// <param name="insem"></param>
        //public int UpdateInseminationInfo(Insemination insem)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append(@"UPDATE [1mutong].[dbo].[Breed_Insemination] set ");
        //    if (insem.InseminationNum != null && insem.InseminationNum != 0)
        //    {
        //        sql.Append("[InseminationNum] = " + insem.InseminationNum + ",");
        //    }
        //    if (insem.SemenNum != null && insem.SemenNum != "")
        //    {
        //        sql.Append("[SemenNum] = '" + insem.SemenNum + "',");
        //    }
        //    if (insem.SemenType != null && insem.SemenType != 0)
        //    {
        //        sql.Append("[SemenType] = " + insem.SemenType + ",");
        //    }
        //    if (insem.EstrusFindType != null && insem.EstrusFindType != 0)
        //    {
        //        sql.Append("[EstrusFindType] = " + insem.EstrusFindType + ",");
        //    }
        //    if (insem.OperateDate != null && insem.OperateDate.ToString() != "")
        //    {
        //        sql.Append("[OperateDate] = '" + insem.OperateDate + "',");
        //    }
        //    if (insem.Operator != null && insem.Operator != "")
        //    {
        //        sql.Append("[Operator] = '" + insem.Operator + "',");
        //    }
        //    if (insem.Description != null && insem.Description != "")
        //    {
        //        sql.Append("[Description] = '" + insem.Description + "',");
        //    }
        //    if (insem.EstrusDate != null && insem.EstrusDate.ToString() != "")
        //    {
        //        sql.Append("[EstrusDate] = '" + insem.EstrusDate + "',");
        //    }
        //    sql.Append("[EstrusType] = " + insem.EstrusType + ",");
        //    if (insem.EstrusFindPerson != null && insem.EstrusFindPerson != "")
        //    {
        //        sql.Append("[EstrusFindPerson] = '" + insem.EstrusFindPerson + "',");
        //    }
        //    sql.Append(" EarNum = '" + insem.EarNum + "'" + " WHERE EarNum = '" + insem.EarNum + "'");

        //    UpdateCowStatus(insem.EarNum, ECowStatus.YiPeiWeiJian);

        //    return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        //}
        //禁配
        public int ForbidInsemination(int earNum)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"update [1mutong].[dbo].[Base_Cow] set [Status] = 6 where EarNum = " + earNum);
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
        //根据牛的耳号获得最近一次牛的配种信息ID
        public int GetLatestInseminationID(int earNum)
        {
            int inseminatinID = 0;
            string sql = string.Format(@"select top(1) ID from [1mutong].[dbo].[Breed_Insemination] where EarNum = '{0}' order by OperateDate DESC", earNum);
            inseminatinID = Convert.ToInt32(dataProvider1mutong.ExecuteScalar(sql, CommandType.Text));
            return inseminatinID;
        }
        //解禁
        public object UnDoForbidInsemination(int earNum)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"update [1mutong].[dbo].[Base_Cow] set [Status] = 0 where EarNum =" + earNum);
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }

        /// <summary>
        /// 获取牛最新配种记录
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <returns></returns>
        public DataRow GetLatestInsemination(int earNum)
        {
            DataTable inseminationList = null;
            string sql = string.Format(@"SELECT top(1) [ID] 
                                             ,[EarNum]
                                            ,[InseminationNum]
                                            ,[SemenNum]
                                            ,[SemenType]
                                            ,[EstrusFindType]
                                            ,[OperateDate]
                                            ,[Operator]
                                            ,[Description]
                                            ,[EstrusDate]
                                            ,[EstrusType]
                                            ,[EstrusFindPerson]
                                        FROM [1mutong].[dbo].[Breed_Insemination] 
                                        WHERE EarNum = '{0}' order by OperateDate DESC", earNum);
            inseminationList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (inseminationList.Rows.Count==1)
            {
                return inseminationList.Rows[0];
            }
            return null;
        }
    }
}
