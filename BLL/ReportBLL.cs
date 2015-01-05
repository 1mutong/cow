using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.Model;
using DairyCow.DAL;

namespace DairyCow.BLL
{
    
    public class MonthlyReportBLL
    {
        public int PastureID { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public MonthlyReportBLL(int pastureID,int year,int month)
        {
            this.PastureID = pastureID;
            this.Year = year;
            this.Month = month;
        }
        
        public MonthlyReport GetMonthlyReport(int pastureID,int year,int month)
        {
            MonthlyReport report = new MonthlyReport();
            DateTime reportDate = (new DateTime(year, month, 1)).AddMonths(1).AddDays(-1);//取本月最后一天
            DailyReportBLL drBLL = new DailyReportBLL(pastureID);
            //取该月日报
            List<DailyReport> dailyReports = drBLL.GetDailyReport(pastureID, new DateTime(year, month, 1), reportDate);
            var sorted=dailyReports.OrderByDescending(a => a.ReportDate);
            dailyReports=sorted.ToList();
            DailyReport latestDailyReport = dailyReports.First();

            //牛群结构按最后一天
            report.CalfNumber = latestDailyReport.CalfNumber;
            report.BredCattleNumber = latestDailyReport.BredCattleNumber;
            report.NullParityNumber = latestDailyReport.NullParityNumber;
            report.MultiParityNumber = latestDailyReport.MultiParityNumber;
            report.DryCowNumber = latestDailyReport.DryCowNumber;
            report.MilkCowNumber = latestDailyReport.MilkCowNumber;
            report.ReportMonth = month;
            report.ReportYear = year;

            int calvingNum = 0, maleCalf = 0, femaleCalf = 0;
            float saleMilk=0,milkForCalf=0,abnormal=0,badMilk=0,leftMilk=0,amount=0;
            int soldCow=0, deadCow=0, eliminatedCow=0;
            foreach (DailyReport item in dailyReports)
            {
                calvingNum = calvingNum + item.CalvingNumber;
                maleCalf = maleCalf + item.MaleCalfNumber;
                femaleCalf = femaleCalf + item.FemaleCalfNumber;

                saleMilk = saleMilk + item.SaleMilk;
                milkForCalf = milkForCalf + item.MilkForCalf;
                abnormal = abnormal + item.AbnormalSaleMilk;
                badMilk = badMilk + item.BadMilk;
                leftMilk = leftMilk + item.LeftMilk;
                amount = amount + item.Amount;

                soldCow = soldCow + item.SoldCowNum;
                deadCow = deadCow + item.DeadCowNum;
                eliminatedCow = eliminatedCow + item.ElimintedCowNum;
            }
            report.CalfNumber = calvingNum;
            report.MaleCalfNumber = maleCalf;
            report.FemaleCalfNumber = femaleCalf;

            report.SaleMilk = saleMilk;
            report.AbnormalSaleMilk = abnormal;
            report.MilkForCalf = milkForCalf;
            report.LeftMilk = leftMilk;
            report.Amount = amount;

            //离群
            report.SoldCowNum = soldCow;
            report.DeadCowNum = deadCow;
            report.ElimintedCowNum = eliminatedCow;

            return report;
        }

    }

    public class DailyReportBLL
    {
        FarmInfo myFarm;
        private int pastureID;
        public DailyReportBLL(int pastureID)
        {
            myFarm = new FarmInfo(pastureID);
            this.pastureID = pastureID;
        }
        /// <summary>
        /// 插入一条日报
        /// </summary>
        /// <returns></returns>
        public int InsertDailyReport()
        {
            int temp = 0;
            DailyReport myReport = new DailyReport();
            myReport.PastureID = this.pastureID;
            myReport.ReportDate = DateTime.Now.Date;
            //牛群结构
            myReport.CalfNumber = myFarm.CountOfCalf;
            myReport.BredCattleNumber = myFarm.CountOfBredCattle;
            myReport.MultiParityNumber = myFarm.CountOfMultiParity;
            myReport.NullParityNumber = myFarm.CountOfNullParity;
            myReport.MilkCowNumber = myFarm.CountOfMilkCows;
            myReport.DryCowNumber = myFarm.CountOfDryMilkCows;

            ReportDAL dal = new ReportDAL();
            temp = dal.InsertDailyReport(myReport.PastureID, myReport.ReportDate, myReport.MilkCowNumber, myReport.DryCowNumber, myReport.MultiParityNumber, myReport.NullParityNumber, myReport.CalfNumber, myReport.BredCattleNumber);

            
            ////产犊
            //myReport.CalvingNumber = FarmInfo.GetSumCalving(this.pastureID, myReport.ReportDate);
            //myReport.MaleCalfNumber = FarmInfo.GetSumMaleCalf(this.pastureID, myReport.ReportDate);
            //myReport.FemaleCalfNumber = FarmInfo.GetSumFemaleCalf(this.pastureID, myReport.ReportDate);

            ////产奶
            //MilkRecord milk=new MilkRecord(this.pastureID,myReport.ReportDate);
            //myReport.SaleMilk = milk.TotalMilkSale;
            //myReport.AbnormalSaleMilk = milk.OtherMilkRecord.AbnormalSaleMilk;
            //myReport.MilkForCalf = milk.OtherMilkRecord.MilkForCalf;
            //myReport.BadMilk = milk.OtherMilkRecord.BadMilk;
            //myReport.LeftMilk = milk.OtherMilkRecord.LeftMilk;

            return temp;

        }

        public DailyReport GetDailyReport(int pastureID,DateTime date)
        {
            DailyReport myReport ;
            ReportDAL dal = new ReportDAL();
            DataTable table = dal.GetDailyReportTable(pastureID, date.Date);
            if (table.Rows.Count==1)
            {
                myReport = WrapDailyReportItem(table.Rows[0]);
                myReport.PastureID = pastureID;

                //产犊
                myReport.CalvingNumber = FarmInfo.GetSumCalving(this.pastureID, myReport.ReportDate);
                myReport.MaleCalfNumber = FarmInfo.GetSumMaleCalf(this.pastureID, myReport.ReportDate);
                myReport.FemaleCalfNumber = FarmInfo.GetSumFemaleCalf(this.pastureID, myReport.ReportDate);

                //产奶
                MilkRecord milk = new MilkRecord(this.pastureID, myReport.ReportDate);
                myReport.SaleMilk = milk.TotalMilkSale;
                myReport.AbnormalSaleMilk = milk.OtherMilkRecord.AbnormalSaleMilk;
                myReport.MilkForCalf = milk.OtherMilkRecord.MilkForCalf;
                myReport.BadMilk = milk.OtherMilkRecord.BadMilk;
                myReport.LeftMilk = milk.OtherMilkRecord.LeftMilk;

                //离群
                StrayBLL sBLL = new StrayBLL();
                myReport.SoldCowNum = sBLL.GetSoldStrayNumber(pastureID, date.Date);
                myReport.DeadCowNum = sBLL.GetStrayNumberByStrayType(pastureID, date.Date, 1);
                myReport.ElimintedCowNum = sBLL.GetStrayNumberByStrayType(pastureID, date.Date, 0);

            }
            else
            {
                myReport = null;
            }
            return myReport;
        }

        public List<DailyReport> GetDailyReport(int pastureID, DateTime startDate,DateTime endDate)
        {
            List<DailyReport> list = new List<DailyReport>();
            ReportDAL dal = new ReportDAL();
            DataTable table = dal.GetDailyReportTable(pastureID, startDate.Date,endDate.Date);
            foreach (DataRow item in table.Rows)
            {
                DailyReport myReport = WrapDailyReportItem(item);
                myReport.PastureID = pastureID;

                //产犊
                myReport.CalvingNumber = FarmInfo.GetSumCalving(this.pastureID, myReport.ReportDate);
                myReport.MaleCalfNumber = FarmInfo.GetSumMaleCalf(this.pastureID, myReport.ReportDate);
                myReport.FemaleCalfNumber = FarmInfo.GetSumFemaleCalf(this.pastureID, myReport.ReportDate);

                //产奶
                MilkRecord milk = new MilkRecord(this.pastureID, myReport.ReportDate);
                myReport.SaleMilk = milk.TotalMilkSale;
                myReport.AbnormalSaleMilk = milk.OtherMilkRecord.AbnormalSaleMilk;
                myReport.MilkForCalf = milk.OtherMilkRecord.MilkForCalf;
                myReport.BadMilk = milk.OtherMilkRecord.BadMilk;
                myReport.LeftMilk = milk.OtherMilkRecord.LeftMilk;

                list.Add(myReport);
            }
            return list;
        }


        public DailyReport WrapDailyReportItem(DataRow row)
        {
            DailyReport report = new DailyReport();
            report.CalfNumber = Convert.ToInt32(row["CalfNumber"]);
            report.BredCattleNumber = Convert.ToInt32(row["BredCattleNumber"]);
            report.NullParityNumber = Convert.ToInt32(row["NullParityNumber"]);
            report.MultiParityNumber = Convert.ToInt32(row["MultiParityNumber"]);
            report.MilkCowNumber = Convert.ToInt32(row["MilkCowNumber"]);
            report.DryCowNumber = Convert.ToInt32(row["DryCowNumber"]);
            report.ReportDate = Convert.ToDateTime(row["ReportDate"]);
            return report;
        }


    
    }
}
