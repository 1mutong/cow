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
    public class RemoveHornDAL : BaseDAL
    {
        public DataTable GetRemoveHornList()
        {
            DataTable removeHornList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[EarNum]
                                            ,[Method]
                                            ,[OprateDate]
                                        FROM [1mutong].[dbo].[Medical_RemoveHorn]");
            removeHornList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return removeHornList;
        }

        public int InsertRemoveHornInfo(RemoveHorn removeHorn)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [1mutong].[dbo].[Medical_RemoveHorn] values (
                                    " + removeHorn.EarNum + ",'"
                          + removeHorn.Method + "','"
                          + removeHorn.OprateDate + "')");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
