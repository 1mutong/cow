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
    public class StrayBLL
    {
        private StrayDAL strayDAL = new StrayDAL();
        public List<Stray> GetStrayList(int pastureID)
        {
            DataTable table = strayDAL.GetStrayTable(pastureID);
            List<Stray> list = new List<Stray>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapStrayItem(item));
            }
            return list;
        }

        public Stray WrapStrayItem(DataRow row)
        {
            Stray stray = new Stray();
            stray.EarNum = Convert.ToInt32(row["EarNum"]);
            stray.PastureID = Convert.ToInt32(row["PastureID"]);
            stray.StrayType = Convert.ToInt32(row["StrayType"]);
            stray.IsSold = Convert.ToInt32(row["IsSold"]);
            stray.Price = Convert.ToSingle(row["Price"]);
            stray.Reason = row["Reason"].ToString();
            stray.StrayDate = Convert.ToDateTime(row["StrayDate"]);
            return stray;
        }

        private int InsertStray(Stray s)
        {
            return strayDAL.InsertStray(s.EarNum, s.PastureID, s.StrayType, s.IsSold, s.Price, s.Reason,s.StrayDate);
        }

        /// <summary>
        /// 插入离群记录，且标记牛离群
        /// </summary>
        /// <param name="s"></param>
        public void StrayCow(Stray s)
        {
            int i=InsertStray(s);
            if (i==0)
            {
                throw new Exception("无法插入离群记录。");
            }
            CowBLL cBLL = new CowBLL();
            cBLL.UpdateCowStrayStatus(s.EarNum, 1);
        }
        /// <summary>
        /// 某日所有离群牛数中，死亡1/淘汰0 数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="date"></param>
        /// <param name="strayType"></param>
        /// <returns></returns>
        public int GetStrayNumberByStrayType(int pastureID,DateTime date,int strayType)
        {
            List<Stray> list = GetStrayList(pastureID).FindAll(p => p.StrayDate.Date.Equals(date.Date)&& p.StrayType==strayType);
            return list.Count;
        }
        /// <summary>
        /// 出售牛头数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int GetSoldStrayNumber(int pastureID, DateTime date)
        {
            List<Stray> list = GetStrayList(pastureID).FindAll(p => p.StrayDate.Date.Equals(date.Date) && p.IsSold == 1);
            return list.Count;
        } 

    }
}
