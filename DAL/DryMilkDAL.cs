using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyCow.DAL.Base;
using System.Data;

namespace DairyCow.DAL
{
    public class DryMilkDAL:BaseDAL
    {
        /// <summary>
        /// 获取干奶记录,按日期倒序。
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns>干奶记录</returns>
        public DataTable GetCowDryRecords(int earNum)
        {
            DataTable dryRecords = null;
            string sql = string.Format(@"SELECT D.ID,D.EarNum,D.DryDate,D.DrySituation,D.DryReason,D.OperatorID,U.Name AS OperatorName
                                        FROM Milk_DryEvent AS D Left Join [Auth_User] AS U ON D.OperatorID=U.ID
                                        WHERE D.EarNum={0} ORDER BY D.DryDate DESC", earNum);
            dryRecords = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return dryRecords;
        }
        /// <summary>
        /// 获取最新干奶记录
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns>干奶记录</returns>
        public DataTable GetLatestCowDryRecord(int earNum)
        {
            DataTable dryRecords = null;
            string sql = string.Format(@"SELECT TOP 1 D.ID,D.EarNum,D.DryDate,D.DrySituation,D.DryReason,D.OperatorID,U.Name AS OperatorName
                                        FROM Milk_DryEvent AS D Left Join [Auth_User] AS U ON D.OperatorID=U.ID
                                        WHERE D.EarNum={0} ORDER BY D.DryDate DESC", earNum);
            dryRecords = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return dryRecords;
        }

        public int InsertDryMilkRecord(int earNum,DateTime dryDate,int drySitudation,string dryReason,int operatorID)
        {
                  //      [DryDate]
                  //,[DrySituation]
                  //,[DryReason]
                  //,[EarNum]
                  //,[OperatorID]
            string sql = string.Format(@"Insert [Milk_DryEvent] ([DryDate]
                                                          ,[DrySituation]
                                                          ,[DryReason]
                                                          ,[EarNum]
                                                          ,[OperatorID]) VALUES('{0}',{1},'{2}',{3},{4})", dryDate, drySitudation, dryReason, earNum, operatorID);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
    }
}
