using DairyCow.DAL.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.DAL
{
    public class RepairHoofDAL : BaseDAL
    {
        public DataTable GetRepairHoofList()
        {
            DataTable repairHoofList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[EarNum]
                                            ,[Method]
                                            ,[LeftFront]
                                            ,[RightFront]
                                            ,[LeftBack]
                                            ,[RightBack]
                                            ,[OprateDate]
                                        FROM [1mutong].[dbo].[Medical_RepairHoof]");
            repairHoofList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return repairHoofList;
        }

        public int InsertRepairHoofInfo(RepairHoof repairHoof)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [1mutong].[dbo].[Medical_RepairHoof] values (
                                    " + repairHoof.EarNum + ",'"
                          + repairHoof.Method + "','"
                          + repairHoof.LeftFront + "','"
                          + repairHoof.RightFront + "','"
                          + repairHoof.LeftBack + "','"
                          + repairHoof.RightBack + "','"
                          + repairHoof.OprateDate + "')");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
