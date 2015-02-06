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
    public class RemoveAddMilkDAL : BaseDAL
    {
        public DataTable GetRemoveAddMilkList()
        {
            DataTable removeAddMilkList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[EarNum]
                                            ,[Method]
                                            ,[LeftFront]
                                            ,[RightFront]
                                            ,[LeftBack]
                                            ,[RightBack]
                                            ,[OprateDate]
                                        FROM [Medical_RemoveAddMilk]");
            removeAddMilkList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return removeAddMilkList;
        }

        public int InsertRemoveAddMilkInfo(RemoveAddMilk removeAddMilk)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into Medical_RemoveAddMilk values (
                                    " + removeAddMilk.EarNum + ",'"
                          + removeAddMilk.Method + "','"
                          + removeAddMilk.LeftFront + "','"
                          + removeAddMilk.RightFront + "','"
                          + removeAddMilk.LeftBack + "','"
                          + removeAddMilk.RightBack + "','"
                          + removeAddMilk.OprateDate + "')");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
