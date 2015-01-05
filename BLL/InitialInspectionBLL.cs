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
    /// 初检BLL
    /// </summary>
    public class InitialInspectionBLL
    {
        InitialInspectionDAL dalInitialInspection = new InitialInspectionDAL();
        //获得所有牛的初检信息
        public List<InitialInspection> GetInitialInspectionList()
        {
            List<InitialInspection> lstInitialInspection = new List<InitialInspection>();
            DataTable datInitialInspection = this.dalInitialInspection.GetInitialInspectionList();
            foreach (DataRow drInspection in datInitialInspection.Rows)
            {
                InitialInspection initialInspectionItem = WrapInitialInspectionItem(drInspection);
                lstInitialInspection.Add(initialInspectionItem);
            }
            return lstInitialInspection;
        }
        //根据牛的耳号获得一头牛的所有初检信息
        public InitialInspection GetInitialInspectionInfo(int earNum)
        {
            InitialInspection initialInspectionInfo = new InitialInspection();
            DataTable datInitialInspection = this.dalInitialInspection.GetInitialInspectionList(earNum);
            if (datInitialInspection != null && datInitialInspection.Rows.Count == 1)
            {
                initialInspectionInfo = WrapInitialInspectionItem(datInitialInspection.Rows[0]);
            }
            return initialInspectionInfo;
        }
        //根据牛耳号和配种信息ID获得一头牛的一条初检信息
        public InitialInspection GetInitialInspectionInfo(int earNum, int inseminationID)
        {
            InitialInspection initialInspectionInfo = new InitialInspection();
            DataTable datInitialInspection = this.dalInitialInspection.GetInitialInspectionList(earNum, inseminationID);
            if (datInitialInspection != null && datInitialInspection.Rows.Count == 1)
            {
                initialInspectionInfo = WrapInitialInspectionItem(datInitialInspection.Rows[0]);
            }
            return initialInspectionInfo;
        }
        public bool IsInitialInspectionExist(int earNum, int insemId)
        {
            return dalInitialInspection.IsInitialInspectionExist(earNum, insemId);
        }
        private InitialInspection WrapInitialInspectionItem(DataRow inspectionRow)
        {
            InitialInspection initialInspectionItem = new InitialInspection();
            if (inspectionRow != null)
            {
                if (inspectionRow["InseminationID"] != null && !string.IsNullOrWhiteSpace(inspectionRow["InseminationID"].ToString()))
                {
                    initialInspectionItem.InseminationID = Convert.ToInt32(inspectionRow["InseminationID"]);
                }
                initialInspectionItem.EarNum = Convert.ToInt32(inspectionRow["EarNum"]);
                initialInspectionItem.OperateDate = Convert.ToDateTime(inspectionRow["OperateDate"]);
                if (inspectionRow["InspectResult"] != null && !string.IsNullOrWhiteSpace(inspectionRow["InspectResult"].ToString()))
                {
                    initialInspectionItem.InspectResult = Convert.ToInt32(inspectionRow["InspectResult"]);
                }
                initialInspectionItem.Operator = Convert.ToInt32(inspectionRow["Operator"]);
                initialInspectionItem.HelpOperator = inspectionRow["HelpOperator"].ToString();
                if (inspectionRow["InspectWay"] != null && !string.IsNullOrWhiteSpace(inspectionRow["InspectWay"].ToString()))
                {
                    initialInspectionItem.InspectWay = Convert.ToInt32(inspectionRow["InspectWay"]);
                }
                if (inspectionRow["AfterInsemDays"] != null && !string.IsNullOrWhiteSpace(inspectionRow["AfterInsemDays"].ToString()))
                {
                    initialInspectionItem.AfterInsemDays = Convert.ToInt32(inspectionRow["AfterInsemDays"]);
                }
                initialInspectionItem.Description = inspectionRow["Description"].ToString();

            }
            return initialInspectionItem;
        }

        //更新牛的初检信息
        public int UpdateInitialInspection(InitialInspection initialInspection)
        {
            return dalInitialInspection.UpdateInitialInspection(initialInspection);
        }
        //插入牛的初检信息
        public int InsertInitialInspection(InitialInspection initialInspection)
        {
            return dalInitialInspection.InsertInitialInspection(initialInspection);
        }
    }
}
