using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.DAL;
using DairyCow.Model;

namespace DairyCow.BLL
{
    public class HouseBLL
    {
        private HouseDAL houseDAL = new HouseDAL();
        private CowGroupBLL groupbll = new CowGroupBLL();

        public List<House> GetHouseList(int pastureID)
        {
            List<House> list = new List<House>();
            DataTable table = houseDAL.GetHouseTable(pastureID);
            var groups = groupbll.GetCowGroupList(UserBLL.Instance.CurrentUser.Pasture.ID);
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapHouseItem(item, groups));
            }
            return list;
        }
        /// <summary>
        /// 获取某牛群下所有牛舍
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<House> GetHouseListByGroup(int pastureID, int groupID)
        {
            return GetHouseList(pastureID).FindAll(p => p.GroupID == groupID);
        }
        /// <summary>
        /// 获取某牛群下所有牛舍
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<House> GetHouseListByGroup(CowGroup group)
        {
            return GetHouseList(group.PastureID).FindAll(p => p.GroupID == group.ID);
        }

        public List<House> GetUnusedHouseList(int pastureID)
        {
            return GetHouseList(pastureID).FindAll(p => p.GroupID == 0);
        }

        public House GetHouseByID(int pastureID,int houseID)
        {
            return GetHouseList(pastureID).Find(p => p.ID == houseID);
        }

        private House WrapHouseItem(DataRow row, List<CowGroup> groups)
        {
            House h = new House();
            h.ID = Convert.ToInt32(row["ID"]);
            h.PastureID = Convert.ToInt32(row["PastureID"]);
            h.Name = row["Name"].ToString();
            if (row["GroupID"] != DBNull.Value)
            {
                h.GroupID = Convert.ToInt32(row["GroupID"]);
            }
            else
            {
                h.GroupID = 0;
            }
            h.CowNumber = GetCowNumberInHouse(h);

            var group = groups.FirstOrDefault(g => g.ID == h.GroupID);
            if (group != null)
            {
                h.GroupName = group.Name;
                h.GroupType = group.GetCowGroupTypeStr(group.GroupType);
            }
            return h;
        }

        public int AddHouse(House house)
        {
            return houseDAL.InsertHouse(house.Name, house.GroupID, house.PastureID);
        }

        public int AddUnusedHouse(House unusedHouse)
        {
            return houseDAL.InsertHouse(unusedHouse.Name, unusedHouse.PastureID);
        }

        /// <summary>
        /// 更新牛舍的牛群，必须牛舍中无牛
        /// </summary>
        /// <param name="houseID"></param>
        /// <param name="groupID">新牛群号，0表示不分配</param>
        /// <returns></returns>
        public int UpdateHouseGroup(House house, int newGroupID)
        {
            int temp = 0;
            if (GetCowNumberInHouse(house) == 0)
            {
                temp = houseDAL.UpdateHouseGroup(house.ID, newGroupID);
            }
            return temp;

        }

        public int GetCowNumberInHouse(House house)
        {
            CowBLL cb = new CowBLL();
            return cb.GetCowNumberInHouse(house.PastureID, house.ID);
        }

        public int DeleteHouse(House house)
        {
            int temp;
            //如果没有牛，也没有分给牛群，就可以删
            int cowNumber = GetCowNumberInHouse(house);
            if (house.GroupID == 0 && cowNumber == 0)
            {
                temp = houseDAL.DeleteHouse(house.ID);
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
    }
}
