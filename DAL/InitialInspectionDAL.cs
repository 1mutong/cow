using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Data;
using System.Text;

namespace DairyCow.DAL
{
    /// <summary>
    /// 初检DAL
    /// </summary>
    public class InitialInspectionDAL : BaseDAL
    {
        //获得所有牛的初检信息
        public DataTable GetInitialInspectionList()
        {
            DataTable initialInspectionList = null;
            string sql = string.Format(@"SELECT [InseminationID]
                                            ,[EarNum]
                                            ,[OperateDate]
                                            ,[InspectResult]
                                            ,[Operator]
                                            ,[HelpOperator]
                                            ,[InspectWay]
                                            ,[AfterInsemDays]
                                            ,[Description]
                                        FROM [1mutong].[dbo].[Breed_InitialInspection]");
            initialInspectionList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return initialInspectionList;
        }

        //获得一头牛的最新的初检信息
        public DataTable GetInitialInspectionList(int earNum)
        {
            DataTable initialInspectionList = null;
            string sql = string.Format(@"SELECT top(1) [InseminationID]
                                            ,[EarNum]
                                            ,[OperateDate]
                                            ,[InspectResult]
                                            ,[Operator]
                                            ,[HelpOperator]
                                            ,[InspectWay]
                                            ,[AfterInsemDays]
                                            ,[Description]
                                        FROM [1mutong].[dbo].[Breed_InitialInspection]
                                            where EarNum = '{0}' order by OperateDate DESC", earNum);
            initialInspectionList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return initialInspectionList;
        }

        //根据牛耳号和配种信息ID获得一头牛某个配次的初检信息
        public DataTable GetInitialInspectionList(int earNum, int insemId)
        {
            DataTable initialInspectionList = null;
            string sql = string.Format(@"SELECT [InseminationID]
                                            ,[EarNum]
                                            ,[OperateDate]
                                            ,[InspectResult]
                                            ,[Operator]
                                            ,[HelpOperator]
                                            ,[InspectWay]
                                            ,[AfterInsemDays]
                                            ,[Description]
                                        FROM [1mutong].[dbo].[Breed_InitialInspection]
                                            where InseminationID = '{0}' and EarNum = '{1}'", insemId, earNum);
            initialInspectionList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return initialInspectionList;
        }

        //根据耳号和配种信息ID判断是否存在初检信息
        public bool IsInitialInspectionExist(int earNum, int insemId)
        {
            string sql = string.Format(@"select count(0) from [Breed_InitialInspection] 
                                         where EarNum = '{0}' 
                                            and InseminationID = {1}", earNum, insemId);
            int inspectInfo = Convert.ToInt32(dataProvider1mutong.ExecuteScalar(sql, CommandType.Text));
            if (inspectInfo > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //更新一头牛的初检信息
        public int UpdateInitialInspection(InitialInspection initialInspection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE [1mutong].[dbo].[Breed_InitialInspection] set ");
            if (initialInspection.InseminationID != null && initialInspection.InseminationID != 0)
            {
                sql.Append("[InseminationID] = " + initialInspection.InseminationID + ",");
            }
            if (initialInspection.OperateDate != null && initialInspection.OperateDate.ToString() != "")
            {
                sql.Append("[OperateDate] = '" + initialInspection.OperateDate + "',");
            }
            if (initialInspection.InspectResult != null)
            {
                sql.Append("[InspectResult] = " + initialInspection.InspectResult + ",");
            }
            if (initialInspection.Operator != 0)
            {
                sql.Append("[Operator] = " + initialInspection.Operator + ",");
            }
            if (initialInspection.HelpOperator != null && initialInspection.HelpOperator != "")
            {
                sql.Append("[HelpOperator] = '" + initialInspection.HelpOperator + "',");
            }
            if (initialInspection.InspectWay != null)
            {
                sql.Append("[InspectWay] = " + initialInspection.InspectWay + ",");
            }
            if (initialInspection.AfterInsemDays != null)
            {
                sql.Append("[AfterInsemDays] = " + initialInspection.AfterInsemDays + ",");
            }
            if (initialInspection.Description != null && initialInspection.Description != "")
            {
                sql.Append("[Description] = '" + initialInspection.Description + "',");
            }
            sql.Append(" EarNum = '" + initialInspection.EarNum + "'" + " WHERE EarNum = '" + initialInspection.EarNum
                + "' and InseminationID = " + initialInspection.InseminationID);

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }

        //插入一头牛的初检信息
        public int InsertInitialInspection(InitialInspection initialInspection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [Breed_InitialInspection] values (
                                    " + initialInspection.InseminationID + ","
                          + initialInspection.EarNum + ",'"
                          + initialInspection.OperateDate + "',"
                          + initialInspection.InspectResult + ","
                          + initialInspection.Operator + ",'"
                          + initialInspection.HelpOperator + "',"
                          + initialInspection.InspectWay + ","
                          + initialInspection.AfterInsemDays + ",'"
                          + initialInspection.Description + "')");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
