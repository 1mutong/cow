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
    public class ForbidInseminationBLL
    {
        ForbidInseminationDAL dalForbidInsemination = new ForbidInseminationDAL();
        public List<ForbidInsemination> GetForbidInseminationList()
        {
            List<ForbidInsemination> lstForbidInsemination = new List<ForbidInsemination>();
            DataTable datForbidInsemination = this.dalForbidInsemination.GetForbidInseminationList();
            foreach (DataRow drForbidInsemination in datForbidInsemination.Rows)
            {
                ForbidInsemination forbidInseminationItem = WrapForbidInseminationItem(drForbidInsemination);
                lstForbidInsemination.Add(forbidInseminationItem);
            }
            return lstForbidInsemination;
        }

        private ForbidInsemination WrapForbidInseminationItem(DataRow forbidInseminationRow)
        {
            ForbidInsemination forbidInseminationItem = new ForbidInsemination();
            if (forbidInseminationRow != null)
            {
                if (Convert.ToInt32(forbidInseminationRow["ID"]) != 0)
                {
                    forbidInseminationItem.ID = Convert.ToInt32(forbidInseminationRow["ID"]);
                }
                forbidInseminationItem.EarNum = Convert.ToInt32(forbidInseminationRow["EarNum"]);
                forbidInseminationItem.OperateDate = Convert.ToDateTime(forbidInseminationRow["OperateDate"]);
                if (forbidInseminationRow["Operator"] != null && !string.IsNullOrWhiteSpace(forbidInseminationRow["Operator"].ToString()))
                {
                    forbidInseminationItem.Operator = Convert.ToInt32(forbidInseminationRow["Operator"]);
                }
                forbidInseminationItem.Description = forbidInseminationRow["Description"].ToString();
            }
            return forbidInseminationItem;
        }

        public int InsertForbidInseminationInfo(ForbidInsemination forBidInsemination)
        {
            return dalForbidInsemination.InsertForbidInseminationInfo(forBidInsemination);
        }
    }
}
