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
    public class MilkMastitisReportDAL : BaseDAL
    {
        public DataTable GetMilkMastitisReportList()
        {
            DataTable milkMastitisReportList = null;
            string sql = string.Format(@"SELECT [EarNum]
                                            ,[DetectionDate]
                                            ,[Detector]
                                            ,[LeftFront]
                                            ,[RightFront]
                                            ,[LeftBack]
                                            ,[RightBack]
                                            ,[Description]
                                        FROM [Milk_MastitisReport]");
            milkMastitisReportList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return milkMastitisReportList;
        }
        public int InsertMilkMastitisReportInfo(MilkMastitisReport milkMastitisReport)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [Milk_MastitisReport] values (
                                    '" + milkMastitisReport.EarNum + "','"
                          + milkMastitisReport.DetectionDate + "','"
                          + milkMastitisReport.Detector + "',"
                          + milkMastitisReport.LeftFront + ","
                          + milkMastitisReport.RightFront + ","
                          + milkMastitisReport.LeftBack + ","
                          + milkMastitisReport.RightBack + ",'"
                          + milkMastitisReport.Description + "')");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
