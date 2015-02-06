using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.DAL
{
    /// <summary>
    /// 复检
    /// </summary>
    public class ReInspectionDAL : BaseDAL
    {
        //获得所有牛的复检信息
        public DataTable GetReInspectionList()
        {
            DataTable reInspectionList = null;
            string sql = string.Format(@"SELECT [InseminationID]
                                            ,[EarNum]
                                            ,[OperateDate]
                                            ,[ReInspectResult]
                                            ,[Operator]
                                            ,[HelpOperator]
                                            ,[AfterInsemDays]
                                            ,[AfterInitInspectDays]
                                            ,[Description] 
                                        FROM [Breed_ReInspection]");
            reInspectionList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return reInspectionList;
        }
        //获得一头牛的最新复检信息
        public DataTable GetReInspectionList(int earNum)
        {
            DataTable reInspectionList = null;
            string sql = string.Format(@"SELECT top(1) [InseminationID]
                                            ,[EarNum]
                                            ,[OperateDate]
                                            ,[ReInspectResult]
                                            ,[Operator]
                                            ,[HelpOperator]
                                            ,[AfterInsemDays]
                                            ,[AfterInitInspectDays]
                                            ,[Description] 
                                        FROM [Breed_ReInspection]
                                            where EarNum = '{0}' order by OperateDate", earNum);
            reInspectionList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return reInspectionList;
        }
        //根据牛耳号和配种信息ID获得一头牛某个配次的复检信息
        public DataTable GetReInspectionList(int earNum, int insemId)
        {
            DataTable reInspectionList = null;
            string sql = string.Format(@"SELECT [InseminationID]
                                            ,[EarNum]
                                            ,[OperateDate]
                                            ,[ReInspectResult]
                                            ,[Operator]
                                            ,[HelpOperator]
                                            ,[AfterInsemDays]
                                            ,[AfterInitInspectDays]
                                            ,[Description] 
                                        FROM [Breed_ReInspection]
                                            where InseminationID = '{0}' and EarNum = '{1}'", insemId, earNum);
            reInspectionList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return reInspectionList;
        }
        //根据耳号和配种信息ID判断是否存在复检信息
        public bool IsReInspectionExist(int earNum, int insemId)
        {
            string sql = string.Format(@"select * from [Breed_ReInspection] 
                                            where EarNum = '{0}' 
                                            and InseminationID = {1}", earNum, insemId);
            object reInspectInfo = dataProvider1mutong.ExecuteScalar(sql, CommandType.Text);
            if (reInspectInfo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //更新一头牛的复检信息
        public int UpdateReInspection(ReInspection reInspection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"UPDATE [Breed_ReInspection] set ");
            if (reInspection.InseminationID != null && reInspection.InseminationID != 0)
            {
                sql.Append("[InseminationID] = " + reInspection.InseminationID + ",");
            }
            if (reInspection.OperateDate != null && reInspection.OperateDate.ToString() != "")
            {
                sql.Append("[OperateDate] = '" + reInspection.OperateDate + "',");
            }
            if (reInspection.ReInspectResult != null)
            {
                sql.Append("[InspectResult] = " + reInspection.ReInspectResult + ",");
            }
            if (reInspection.Operator != 0)
            {
                sql.Append("[Operator] = " + reInspection.Operator + ",");
            }
            if (reInspection.HelpOperator != null && reInspection.HelpOperator != "")
            {
                sql.Append("[HelpOperator] = '" + reInspection.HelpOperator + "',");
            }
            if (reInspection.AfterInsemDays != null)
            {
                sql.Append("[AfterInsemDays] = " + reInspection.AfterInsemDays + ",");
            }
            if (reInspection.AfterInitInspectDays != null)
            {
                sql.Append("[AfterInitInspectDays] = " + reInspection.AfterInitInspectDays + ",");
            }
            if (reInspection.Description != null && reInspection.Description != "")
            {
                sql.Append("[Description] = '" + reInspection.Description + "',");
            }
            sql.Append(" EarNum = '" + reInspection.EarNum + "'" + " WHERE EarNum = '" + reInspection.EarNum
                + "' and InseminationID = " + reInspection.InseminationID);

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
        //插入一头牛的复检信息
        public int InsertReInspection(ReInspection reInspection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [Breed_ReInspection] values (
                                    " + reInspection.InseminationID + ","
                          + reInspection.EarNum + ",'"
                          + reInspection.OperateDate + "',"
                          + reInspection.ReInspectResult + ","
                          + reInspection.Operator + ",'"
                          + reInspection.HelpOperator + "',"
                          + reInspection.AfterInsemDays + ","
                          + reInspection.AfterInitInspectDays + ",'"
                          + reInspection.Description + "')");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
