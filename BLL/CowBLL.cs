using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyCow.BLL
{
    public class CowBLL
    {
        CowDAL dalCow = new CowDAL();

        /// <summary>
        /// 获取牛舍中牛数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="houseID"></param>
        /// <returns></returns>
        public int GetCowNumberInHouse(int pastureID, int houseID)
        {
            return GetCowListInHouse(pastureID, houseID).Count;
        }

        /// <summary>
        /// 获取牛舍中牛list
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="houseID"></param>
        /// <returns></returns>
        public List<Cow> GetCowListInHouse(int pastureID, int houseID)
        {
            return GetCowList(pastureID).FindAll(p => p.HouseID == houseID);
        }

        /// <summary>
        /// 转换系统耳号到显示耳号,不存在返回String.Empty
        /// </summary>
        /// <param name="earNum"></param>
        /// <returns></returns>
        public static string ConvertEarNumToDisplayEarNum(int earNum)
        {
            if (earNum == -1)
            {
                return string.Empty;
            }
            CowDAL dal = new CowDAL();
            return dal.ConvertEarNumToDisplayEarNum(earNum);
        }

        /// <summary>
        /// 转换显示耳号到系统耳号,不存在返回-1
        /// </summary>
        /// <param name="displayEarNum"></param>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public static int ConvertDislayEarNumToEarNum(string displayEarNum, int pastureID)
        {
            if (string.IsNullOrEmpty(displayEarNum))
            {
                return -1;
            }
            CowDAL dal = new CowDAL();
            return dal.ConvertDislayEarNumToEarNum(displayEarNum, pastureID);
        }

        /// <summary>
        /// 获取当前用户的牧场Cowlist
        /// </summary>
        /// <returns></returns>
        public List<Cow> GetCowList()
        {
            return GetCowList(UserBLL.Instance.CurrentUser.Pasture.ID);
        }

        /// <summary>
        /// 获取牧场Cowlist
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <returns></returns>
        public List<Cow> GetCowList(int pastureID)
        {
            List<Cow> lstCow = new List<Cow>();

            DataTable datCow = this.dalCow.GetCowList(pastureID);
            foreach (DataRow drCow in datCow.Rows)
            {
                Cow cowItem = WrapCowItem(drCow);
                lstCow.Add(cowItem);
            }

            return lstCow;
        }

        public Cow GetCowInfo(int earNum)
        {
            Cow cow = new Cow();
            DataTable datCow = this.dalCow.GetCowInfo(earNum);
            if (datCow != null && datCow.Rows.Count == 1)
            {
                cow = WrapCowItem(datCow.Rows[0]);
            }
            return cow;
        }

        public CowLite GetCowLiteInfo(string displayEarNum)
        {
            int earNum = ConvertDislayEarNumToEarNum(displayEarNum, UserBLL.Instance.CurrentUser.Pasture.ID);
            DataTable dt = this.dalCow.GetCowLiteInfo(earNum);
            if(dt.Rows.Count ==0)
            {
                return null;
            }
            else
            {
                CowLite cow = new CowLite();
                cow.DisplayEarNum = displayEarNum;
                cow.HouseID = Convert.ToInt32(dt.Rows[0]["HouseID"]);
                cow.HouseName = dt.Rows[0]["HouseName"].ToString();
                cow.GroupID = Convert.ToInt32(dt.Rows[0]["GroupID"]);
                cow.GroupName = dt.Rows[0]["GroupName"].ToString();
                return cow;
            }           
        }
        
        public Cow GetCowInfo(int pastureID, string displayEarNum)
        {
            Cow cow = new Cow();
            DataTable datCow = this.dalCow.GetCowInfo(pastureID, displayEarNum);
            if (datCow != null && datCow.Rows.Count == 1)
            {
                cow = WrapCowItem(datCow.Rows[0]);
            }
            return cow;
        }

        private Cow WrapCowItem(DataRow cowRow)
        {
            Cow cowItem = new Cow();
            if (cowRow != null)
            {
                cowItem.EarNum = Convert.ToInt32(cowRow["EarNum"]);
                cowItem.DisplayEarNum = cowRow["DisplayEarNum"].ToString();
                cowItem.GroupID = Convert.ToInt32(cowRow["GroupID"]);
                cowItem.GroupName = cowRow["GroupName"].ToString();
                if (cowRow["HouseID"] != DBNull.Value)
                {
                    cowItem.HouseID = Convert.ToInt32(cowRow["HouseID"]);
                }
                else
                {
                    cowItem.HouseID = 0; //表示无牛舍？
                }

                cowItem.Gender = cowRow["Gender"].ToString();
                cowItem.FarmCode = Convert.ToInt32(cowRow["FarmID"]);
                cowItem.BirthDate = Convert.ToDateTime(cowRow["BirthDate"]);
                DateTime dtNow = DateTime.Now;
                //cowItem.AgeMonth = (dtNow.Year - cowItem.BirthDate.Year) * 12 + (dtNow.Month - cowItem.BirthDate.Month);

                if (cowRow["BirthWeight"] != null && !string.IsNullOrWhiteSpace(cowRow["BirthWeight"].ToString()))
                {
                    cowItem.BirthWeight = float.Parse(cowRow["BirthWeight"].ToString());
                }
                if (cowRow["Color"] != DBNull.Value)
                {
                    cowItem.Color = cowRow["Color"].ToString();
                }
                else
                {
                    cowItem.Color = String.Empty;
                }

                //经产牛和青年牛才有繁殖状态，从繁殖相关事件表可以得出，初始买的牛必须输入
                if (cowRow["Status"] != DBNull.Value)
                {
                    cowItem.Status = GetCowStatus(Convert.ToInt32(cowRow["Status"]));
                }
                else
                {
                    cowItem.Status = String.Empty;
                }
                int ill = Convert.ToInt32(cowRow["IsIll"]);
                if (ill == 0)
                {
                    cowItem.IsIll = false;
                }
                else
                {
                    cowItem.IsIll = true;
                }

                if (cowRow["FatherID"] != DBNull.Value)
                {
                    cowItem.FatherID = cowRow["FatherID"].ToString();
                }
                else
                {
                    cowItem.FatherID = String.Empty;
                }
                if (cowRow["MotherID"] != DBNull.Value)
                {
                    cowItem.MotherID = cowRow["MotherID"].ToString();
                }
                else
                {
                    cowItem.MotherID = String.Empty;
                }
                cowItem.IsStray = Convert.ToInt32(cowRow["IsStray"]) == 0 ? false : true;
            }
            return cowItem;
        }

        private string GetCowStatus(int i)
        {
            switch (i)
            {
                case 0:
                    return "未配";
                case 1:
                    return "已配未检";
                case 2:
                    return "初检-";
                case 3:
                    return "初检+";
                case 4:
                    return "复检-";
                case 5:
                    return "复检+";
                case 6:
                    return "禁配";
                default:
                    return string.Empty;
            }
        }

        private int GetCowStatusNum(string status)
        {
            switch (status)
            {
                case "未配":
                    return 0;
                case "已配未检":
                    return 1;
                case "初检-":
                    return 2;
                case "初检+":
                    return 3;
                case "复检-":
                    return 4;
                case "复检+":
                    return 5;
                case "禁配":
                    return 6;
                default:
                    return 0;
            }

        }

        /// <summary>
        /// 设牛繁殖状态
        /// </summary>
        /// <param name="earNum"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateCowBreedStatus(int earNum, string status)
        {
            return dalCow.UpdateCowBreedStatus(earNum, GetCowStatusNum(status));
        }


        public int UpdateCowGroupAndHouse(int earNum, int newGroupID, int newHouseID)
        {
            return dalCow.UpdateCow(earNum, newGroupID, newHouseID);
        }

        public int UpdateCowIllStatus(int earNum, bool isIll)
        {
            int temp;
            if (isIll)
            {
                temp = dalCow.UpdateCowIllStatus(earNum, 1);
            }
            else
            {
                temp = dalCow.UpdateCowIllStatus(earNum, 0);
            }
            return temp;
        }

        /// <summary>
        /// 获取牛群里所有牛
        /// </summary>
        /// <param name="farmID">牧场ID</param>
        /// <param name="groupID">牛群ID</param>
        /// <returns>牛列表</returns>
        public List<Cow> GetCowsByGroup(int groupID)
        {
            List<Cow> lstCow = new List<Cow>();

            DataTable datCow = this.dalCow.GetCowListByFarmGroup(UserBLL.Instance.CurrentUser.Pasture.ID, groupID);
            foreach (DataRow drCow in datCow.Rows)
            {
                Cow cowItem = WrapCowItem(drCow);
                lstCow.Add(cowItem);
            }

            return lstCow;
        }

        /// <summary>
        /// 获取某繁殖状态的牛
        /// </summary>
        /// <param name="farmID">牧场ID</param>
        /// <param name="status">状态号</param>
        /// <returns>牛列表</returns>
        public List<Cow> GetCowsByFarmStatus(int status)
        {
            List<Cow> lstCow = new List<Cow>();

            DataTable datCow = this.dalCow.GetCowListByFarmStatus(UserBLL.Instance.CurrentUser.Pasture.ID, status);
            foreach (DataRow drCow in datCow.Rows)
            {
                Cow cowItem = WrapCowItem(drCow);
                lstCow.Add(cowItem);
            }

            return lstCow;
        }

        public int UpdateCowStrayStatus(int earNum, int isStray)
        {
            return dalCow.UpdateCowStrayStatus(earNum, isStray);
        }

        /// <summary>
        /// 插入牛，确保各属性已赋值
        /// </summary>
        /// <param name="myCow"></param>
        /// <returns></returns>
        public int InsertCow(Cow myCow)
        {
            int temp = 0;
            int statusNum = GetCowStatusNum(myCow.Status);
            int isill = myCow.IsIll ? 1 : 0;
            temp = dalCow.InsertCow(myCow.DisplayEarNum, myCow.FarmCode, myCow.GroupID, myCow.HouseID, myCow.Gender, myCow.BirthDate, statusNum, isill, myCow.FatherID, myCow.MotherID, myCow.Color);
            return temp;
        }

        public bool CheckCowInFarm(string displayEarNum, int farmID)
        {
            return dalCow.CheckCowInFarm(displayEarNum, farmID);
        }
    }
}
