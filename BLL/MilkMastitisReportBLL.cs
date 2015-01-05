using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.BLL
{
    public class MilkMastitisReportBLL
    {
        MilkMastitisReportDAL dalMilkMastitisReport = new MilkMastitisReportDAL();
        public List<MilkMastitisReport> GetMilkMastitisReportList()
        {
            List<MilkMastitisReport> lstMilkMastitisReport = new List<MilkMastitisReport>();
            DataTable datMilkMastitisReport = this.dalMilkMastitisReport.GetMilkMastitisReportList();
            foreach (DataRow drMilkMastitisReport in datMilkMastitisReport.Rows)
            {
                MilkMastitisReport milkMastitisReportItem = WrapMilkHallItem(drMilkMastitisReport);
                lstMilkMastitisReport.Add(milkMastitisReportItem);
            }
            return lstMilkMastitisReport;
        }
        public int InsertMilkMastitisReportInfo(MilkMastitisReport milkMastitisReport)
        {
            return dalMilkMastitisReport.InsertMilkMastitisReportInfo(milkMastitisReport);
        }

        private MilkMastitisReport WrapMilkHallItem(DataRow milkMastitisReportRow)
        {
            MilkMastitisReport milkMastitisReportItem = new MilkMastitisReport();
            if (milkMastitisReportRow != null)
            {
                milkMastitisReportItem.EarNum = Convert.ToInt32(milkMastitisReportRow["EarNum"]);
                milkMastitisReportItem.DetectionDate = Convert.ToDateTime(milkMastitisReportRow["DetectionDate"]);
                milkMastitisReportItem.Detector = milkMastitisReportRow["Detector"].ToString();
                if (milkMastitisReportRow["LeftFront"] != null && string.IsNullOrWhiteSpace(milkMastitisReportRow["LeftFront"].ToString()))
                {
                    milkMastitisReportItem.LeftFront = Convert.ToInt32(milkMastitisReportRow["LeftFront"]);
                }
                if (milkMastitisReportRow["RightFront"] != null && string.IsNullOrWhiteSpace(milkMastitisReportRow["RightFront"].ToString()))
                {
                    milkMastitisReportItem.RightFront = Convert.ToInt32(milkMastitisReportRow["RightFront"]);
                }
                if (milkMastitisReportRow["LeftBack"] != null && string.IsNullOrWhiteSpace(milkMastitisReportRow["LeftBack"].ToString()))
                {
                    milkMastitisReportItem.LeftBack = Convert.ToInt32(milkMastitisReportRow["LeftBack"]);
                }
                if (milkMastitisReportRow["RightBack"] != null && string.IsNullOrWhiteSpace(milkMastitisReportRow["RightBack"].ToString()))
                {
                    milkMastitisReportItem.RightBack = Convert.ToInt32(milkMastitisReportRow["RightBack"]);
                }
                milkMastitisReportItem.Description = milkMastitisReportRow["Description"].ToString();
            }
            return milkMastitisReportItem;
        }
    }
}
