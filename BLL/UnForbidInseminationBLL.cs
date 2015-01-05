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
    public class UnForbidInseminationBLL
    {
        UnForbidInseminationDAL dalUnForbidInsemination = new UnForbidInseminationDAL();
        public List<UnForbidInsemination> GetUnForbidInseminationList()
        {
            List<UnForbidInsemination> lstUnForbidInsemination = new List<UnForbidInsemination>();
            DataTable datUnForbidInsemination = this.dalUnForbidInsemination.GetUnForbidInseminationList();
            foreach (DataRow drUnForbidInsemination in datUnForbidInsemination.Rows)
            {
                UnForbidInsemination unForbidInseminationItem = WrapUnForbidInseminationItem(drUnForbidInsemination);
                lstUnForbidInsemination.Add(unForbidInseminationItem);
            }
            return lstUnForbidInsemination;
        }

        private UnForbidInsemination WrapUnForbidInseminationItem(DataRow unForbidInseminationRow)
        {
            UnForbidInsemination unForbidInseminationItem = new UnForbidInsemination();
            if (unForbidInseminationRow != null)
            {
                if (Convert.ToInt32(unForbidInseminationRow["ID"]) != 0)
                {
                    unForbidInseminationItem.ID = Convert.ToInt32(unForbidInseminationRow["ID"]);
                }
                unForbidInseminationItem.EarNum = Convert.ToInt32(unForbidInseminationRow["EarNum"]);
                unForbidInseminationItem.OperateDate = Convert.ToDateTime(unForbidInseminationRow["OperateDate"]);
                if (unForbidInseminationRow["Operator"] != null && !string.IsNullOrWhiteSpace(unForbidInseminationRow["Operator"].ToString()))
                {
                    unForbidInseminationItem.Operator = Convert.ToInt32(unForbidInseminationRow["Operator"]);
                }
                unForbidInseminationItem.Description = unForbidInseminationRow["Description"].ToString();
            }
            return unForbidInseminationItem;
        }

        public int InsertUnForbidInseminationInfo(UnForbidInsemination unForBidInsemination)
        {
            return dalUnForbidInsemination.InsertUnForbidInseminationInfo(unForBidInsemination);
        }
    }
}
