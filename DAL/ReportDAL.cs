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
    public class ReportDAL:BaseDAL
    {
        public int InsertDailyReport(int pastureID,DateTime reportDate,int milkCowNumber,int dryCowNumber,int multiParityNumber,int nullParityNumber,int calfNumber,int bredCattleNumber)
        {
            string sql = string.Format(@"Insert  Report_DailyReport 
                                       (PastureId,
                                       ReportDate,
                                       MilkCowNumber,
                                       DryCowNumber,
                                       MultiParityNumber,
                                        NullParityNumber,
                                        BredCattleNumber,
                                       CalfNumber) Values({0},'{1}',[2}，{3},{4},{5},{6},{7},{8})",
                                         pastureID, reportDate.Date, milkCowNumber, dryCowNumber, multiParityNumber, nullParityNumber, bredCattleNumber, calfNumber);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }


        public DataTable GetDailyReportTable(int pastureID,DateTime date)
        {
            string sql = string.Format(@"Select  PastureId,
                                       ReportDate,
                                       MilkCowNumber,
                                       DryCowNumber,
                                       MultiParityNumber,
                                        NullParityNumber,
                                        BredCattleNumber,
                                       CalfNumber from Report_DailyReport
                                        where PastureId={0} and ReportDate='{1}'",
                                         pastureID, date.Date);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }

        public DataTable GetDailyReportTable(int pastureID)
        {
            string sql = string.Format(@"Select  PastureId,
                                       ReportDate,
                                       MilkCowNumber,
                                       DryCowNumber,
                                       MultiParityNumber,
                                        NullParityNumber,
                                        BredCattleNumber,
                                       CalfNumber from Report_DailyReport
                                        where PastureId={0} ",
                                         pastureID);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }

        public DataTable GetDailyReportTable(int pastureID,DateTime startDate,DateTime endDate)
        {
            string sql = string.Format(@"Select  PastureId,
                                       ReportDate,
                                       MilkCowNumber,
                                       DryCowNumber,
                                       MultiParityNumber,
                                        NullParityNumber,
                                        BredCattleNumber,
                                       CalfNumber from Report_DailyReport
                                        where PastureId={0} and ReportDate>'{1}' and  ReportDate<'{2}' ",
                                         pastureID,startDate,endDate);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }
    }
}
