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
    /// <summary>
    /// 复检
    /// </summary>
    public class ReInspectionBLL
    {
        ReInspectionDAL dalReInspection = new ReInspectionDAL();
        public List<ReInspection> GetReInspectionList()
        {
            List<ReInspection> listReInspection = new List<ReInspection>();
            DataTable datReInspection = this.dalReInspection.GetReInspectionList();
            foreach (DataRow drReInspection in datReInspection.Rows)
            {
                ReInspection reInspectionItem = WrapReInspectionItem(drReInspection);
                listReInspection.Add(reInspectionItem);
            }
            return listReInspection;
        }
        public ReInspection GetReInspectionInfo(int earNum)
        {
            ReInspection reInspectionIfo = new ReInspection();
            DataTable datReInspection = this.dalReInspection.GetReInspectionList(earNum);
            if (datReInspection != null && datReInspection.Rows.Count == 1)
            {
                reInspectionIfo = WrapReInspectionItem(datReInspection.Rows[0]);
            }
            return reInspectionIfo;
        }
        public ReInspection GetReInspectionInfo(int earNum, int inseminationID)
        {
            ReInspection reInspectionIfo = new ReInspection();
            DataTable datReInspection = this.dalReInspection.GetReInspectionList(earNum, inseminationID);
            if (datReInspection != null && datReInspection.Rows.Count == 1)
            {
                reInspectionIfo = WrapReInspectionItem(datReInspection.Rows[0]);
            }
            return reInspectionIfo;
        }
        public bool IsReInspectionExist(int earNum, int insemId)
        {
            return dalReInspection.IsReInspectionExist(earNum, insemId);
        }
        private ReInspection WrapReInspectionItem(DataRow reInspectionRow)
        {
            ReInspection reInspectionItem = new ReInspection();
            if (reInspectionRow != null)
            {
                if (reInspectionRow["InseminationID"] != null && !string.IsNullOrWhiteSpace(reInspectionRow["InseminationID"].ToString()))
                {
                    reInspectionItem.InseminationID = Convert.ToInt32(reInspectionRow["InseminationID"]);
                }
                reInspectionItem.EarNum = Convert.ToInt32(reInspectionRow["EarNum"]);
                reInspectionItem.OperateDate = Convert.ToDateTime(reInspectionRow["OperateDate"]);
                if (reInspectionRow["ReInspectResult"] != null && !string.IsNullOrWhiteSpace(reInspectionRow["ReInspectResult"].ToString()))
                {
                    reInspectionItem.ReInspectResult = Convert.ToInt32(reInspectionRow["ReInspectResult"]);
                }
                reInspectionItem.Operator = Convert.ToInt32(reInspectionRow["Operator"]);
                reInspectionItem.HelpOperator = reInspectionRow["HelpOperator"].ToString();
                if (reInspectionRow["AfterInsemDays"] != null && !string.IsNullOrWhiteSpace(reInspectionRow["AfterInsemDays"].ToString()))
                {
                    reInspectionItem.AfterInsemDays = Convert.ToInt32(reInspectionRow["AfterInsemDays"]);
                }
                if (reInspectionRow["AfterInitInspectDays"] != null && !string.IsNullOrWhiteSpace(reInspectionRow["AfterInitInspectDays"].ToString()))
                {
                    reInspectionItem.AfterInitInspectDays = Convert.ToInt32(reInspectionRow["AfterInitInspectDays"]);
                }
                reInspectionItem.Description = reInspectionRow["Description"].ToString();
            }
            return reInspectionItem;
        }
        public int UpdateReInspection(ReInspection reInspection)
        {
            return dalReInspection.UpdateReInspection(reInspection);
        }
        public int InsertReInspection(ReInspection reInspection)
        {
            return dalReInspection.InsertReInspection(reInspection);
        }
    }
}
