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
    public class RemoveHornBLL
    {
        RemoveHornDAL dalRemoveHorn = new RemoveHornDAL();
        public List<RemoveHorn> GetRemoveHornList()
        {
            List<RemoveHorn> lstRemoveHorn = new List<RemoveHorn>();
            DataTable datRemoveHorn = this.dalRemoveHorn.GetRemoveHornList();
            foreach (DataRow drRemoveHorn in datRemoveHorn.Rows)
            {
                RemoveHorn removeHornItem = WrapRemoveHornItem(drRemoveHorn);
                lstRemoveHorn.Add(removeHornItem);
            }
            return lstRemoveHorn;
        }
        public int InsertRemoveHornInfo(RemoveHorn removeHorn)
        {
            return dalRemoveHorn.InsertRemoveHornInfo(removeHorn);
        }
        private RemoveHorn WrapRemoveHornItem(DataRow removeHornRow)
        {
            RemoveHorn removeHornItem = new RemoveHorn();
            if (removeHornRow != null)
            {
                if (Convert.ToInt32(removeHornRow["ID"]) != 0)
                {
                    removeHornItem.ID = Convert.ToInt32(removeHornRow["ID"]);
                }
                if (Convert.ToInt32(removeHornRow["EarNum"]) != 0)
                {
                    removeHornItem.EarNum = Convert.ToInt32(removeHornRow["EarNum"]);
                }
                removeHornItem.Method = removeHornRow["Method"].ToString();
                removeHornItem.OprateDate = Convert.ToDateTime(removeHornRow["OprateDate"]);
            }
            return removeHornItem;
        }
    }
}
