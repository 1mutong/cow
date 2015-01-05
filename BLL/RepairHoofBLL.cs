using DairyCow.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.BLL
{
    public class RepairHoofBLL
    {
        RepairHoofDAL dalRepairHoof = new RepairHoofDAL();
        public List<RepairHoof> GetRepairHoofList()
        {
            List<RepairHoof> lstRepairHoof = new List<RepairHoof>();
            DataTable datRepairHoof = this.dalRepairHoof.GetRepairHoofList();
            foreach (DataRow drRepairHoof in datRepairHoof.Rows)
            {
                RepairHoof repairHoofItem = WrapRepairHoofItem(drRepairHoof);
                lstRepairHoof.Add(repairHoofItem);
            }
            return lstRepairHoof;
        }
        public int InsertRepairHoofInfo(RepairHoof repairHoof)
        {
            return dalRepairHoof.InsertRepairHoofInfo(repairHoof);
        }
        private RepairHoof WrapRepairHoofItem(DataRow repairHoofRow)
        {
            RepairHoof repairHoofItem = new RepairHoof();
            if (repairHoofRow != null)
            {
                if (Convert.ToInt32(repairHoofRow["ID"]) != 0)
                {
                    repairHoofItem.ID = Convert.ToInt32(repairHoofRow["ID"]);
                }
                if (Convert.ToInt32(repairHoofRow["EarNum"]) != 0)
                {
                    repairHoofItem.EarNum = Convert.ToInt32(repairHoofRow["EarNum"]);
                }
                repairHoofItem.Method = repairHoofRow["Method"].ToString();
                repairHoofItem.LeftFront = repairHoofRow["LeftFront"].ToString();
                repairHoofItem.RightFront = repairHoofRow["RightFront"].ToString();
                repairHoofItem.LeftBack = repairHoofRow["LeftBack"].ToString();
                repairHoofItem.RightBack = repairHoofRow["RightBack"].ToString();
                repairHoofItem.OprateDate = Convert.ToDateTime(repairHoofRow["OprateDate"]);
            }
            return repairHoofItem;
        }
    }
}
