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
    public class RemoveAddMilkBLL
    {
        RemoveAddMilkDAL dalRemoveAddMilk = new RemoveAddMilkDAL();
        public List<RemoveAddMilk> GetRemoveAddMilkList()
        {
            List<RemoveAddMilk> lstRemoveAddMilk = new List<RemoveAddMilk>();
            DataTable datRemoveAddMilk = this.dalRemoveAddMilk.GetRemoveAddMilkList();
            foreach (DataRow drRemoveAddMilk in datRemoveAddMilk.Rows)
            {
                RemoveAddMilk removeAddMilkItem = WrapRemoveAddMilkItem(drRemoveAddMilk);
                lstRemoveAddMilk.Add(removeAddMilkItem);
            }
            return lstRemoveAddMilk;
        }

        public int InsertRemoveAddMilkInfo(RemoveAddMilk removeAddMilk)
        {
            return dalRemoveAddMilk.InsertRemoveAddMilkInfo(removeAddMilk);
        }

        private RemoveAddMilk WrapRemoveAddMilkItem(DataRow removeAddMilkRow)
        {
            RemoveAddMilk removeAddMilkItem = new RemoveAddMilk();
            if (removeAddMilkRow != null)
            {
                if (Convert.ToInt32(removeAddMilkRow["ID"]) != 0)
                {
                    removeAddMilkItem.ID = Convert.ToInt32(removeAddMilkRow["ID"]);
                }
                if (Convert.ToInt32(removeAddMilkRow["EarNum"]) != 0)
                {
                    removeAddMilkItem.EarNum = Convert.ToInt32(removeAddMilkRow["EarNum"]);
                }
                removeAddMilkItem.Method = removeAddMilkRow["Method"].ToString();
                removeAddMilkItem.LeftFront = removeAddMilkRow["LeftFront"].ToString();
                removeAddMilkItem.RightFront = removeAddMilkRow["RightFront"].ToString();
                removeAddMilkItem.LeftBack = removeAddMilkRow["LeftBack"].ToString();
                removeAddMilkItem.RightBack = removeAddMilkRow["RightBack"].ToString();
                removeAddMilkItem.OprateDate = Convert.ToDateTime(removeAddMilkRow["OprateDate"]);
            }
            return removeAddMilkItem;
        }
    }
}
