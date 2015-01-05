using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.Model;
using DairyCow.DAL.Base;

namespace DairyCow.DAL
{
    public class StrayDAL:BaseDAL
    {
        public DataTable GetStrayTable(int pastureID)
        {
            string sql = string.Format(@"SELECT [EarNum]
                                      ,[PastureID]
                                      ,[StrayType]
                                      ,[IsSold]
                                      ,[Price]
                                      ,[Reason]
                                       ,[StrayDate]
                                  FROM [Base_Stray]
                                        where PastureID={0}", pastureID);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }

        public int InsertStray(int earNum,int pastureID,int strayType,int isSold,float price,string reason,DateTime date)
        {
            string sql = string.Format(@"Insert [Base_Stray] 
                                       ([EarNum]
                                      ,[PastureID]
                                      ,[StrayType]
                                      ,[IsSold]
                                      ,[Price]
                                      ,[Reason]
                                       ,[StrayDate]) Values({0},{1},{2},{3},{4},'{5}','{6}')",
                                        earNum, pastureID, strayType, isSold, price, reason,date.Date.ToShortDateString());
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
    }
}
