using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Data;

namespace DairyCow.DAL
{
    /// <summary>
    /// 奶牛基本信息数据库访问
    /// </summary>
    public class CowDAL : BaseDAL
    {
        /// <summary>
        /// 所有牛?
        /// </summary>
        /// <returns></returns>
        public DataTable GetCowList(int pastureID)
        {
            DataTable cowList = null;

            string sql = string.Format(@"SELECT D.EarNum, 
                                        D.DisplayEarNum,
                                        D.FarmID, 
                                        D.GroupID, 
                                        D.Gender, 
                                        D.BirthDate, 
                                        D.BirthWeight, 
                                        D.Color, 
                                        D.Status, 
                                        D.IsIll,
                                        D.HouseID,
                                        D.IsStray,
                                        D.FatherID,
                                        D.MotherID,
                                        G.Name AS GroupName
                                        FROM Base_Cow AS D
                                        LEFT JOIN [Base_CowGroup] AS G
                                        ON D.GroupID = G.ID
                                        WHERE D.FarmID={0} and IsStray=0", pastureID);

            cowList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return cowList;
        }

        public DataTable GetCowInfo(int earNum)
        {
            DataTable cowInfo = null;

            string sql = string.Format(@"SELECT D.EarNum,D.HouseID, D.DisplayEarNum, D.FarmID, D.GroupID, D.Gender, D.BirthDate,D.IsIll, D.BirthWeight, D.Color, D.Status,D.IsStray,D.FatherID,D.MotherID,D.IsStray, D.FatherID, D.MotherID,G.Name as GroupName 
                                        FROM Base_Cow AS D LEFT JOIN [Base_CowGroup] as G ON D.GroupID = G.ID 
                                        WHERE D.EarNum={0}", earNum);

            cowInfo = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return cowInfo;
        }

        public DataTable GetCowLiteInfo(int earNum)
        {
            DataTable cowInfo = null;

            string sql = string.Format(@"SELECT C.DisplayEarNum, H.ID AS HouseID, H.Name AS HouseName, G.ID AS GroupID, G.Name AS GroupName
                                FROM Base_Cow AS C
                                LEFT JOIN Base_CowHouse AS H ON C.HouseID = H.ID
                                LEFT JOIN Base_CowGroup AS G ON C.GroupID = G.ID
                                WHERE C.EarNum = '{0}'", earNum);

            cowInfo = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return cowInfo;
        }

        public int UpdateCowLiteInfo(CowLite cowLite)
        {
            string sql = string.Format(@"UPDATE Base_Cow
                                SET GroupID ={0},HouseID = {1}
                                WHERE EarNum = '{2}'", cowLite.GroupID,cowLite.HouseID,cowLite.EarNum);

            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public DataTable GetCowInfo(int pastureID, string dislayEarNum)
        {
            DataTable cowInfo = null;

            string sql = string.Format(@"SELECT D.EarNum,D.HouseID, D.DisplayEarNum, D.FarmID, D.GroupID, D.Gender, D.BirthDate,D.IsIll, D.BirthWeight, D.Color, D.Status,D.IsStray,D.FatherID,D.MotherID,D.IsStray, D.FatherID, D.MotherID,G.Name as GroupName 
                                        FROM Base_Cow AS D LEFT JOIN [Base_CowGroup] as G ON D.GroupID = G.ID 
                                        WHERE D.DisplayEarNum='{0}' and D.FarmID={1}", dislayEarNum, pastureID);

            cowInfo = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return cowInfo;
        }

        /// <summary>
        /// 获取牧场某群牛
        /// </summary>
        /// <param name="farmID">牧场ID</param>
        /// <param name="groupID">牛群ID</param>
        /// <returns></returns>
        public DataTable GetCowListByFarmGroup(int farmID, int groupID)
        {
            DataTable cowList = null;

            string sql = string.Format(@"SELECT D.EarNum, 
                                        D.FarmID, 
                                        D.GroupID, 
                                        D.Gender, 
                                        D.BirthDate, 
                                        D.BirthWeight, 
                                        D.Color, 
                                        D.Status,
                                        D.IsIll, 
                                        D.HouseID,
                                        D.IsStray,
                                        D.FatherID,
                                        D.MotherID,
                                        G.Name AS GroupName
                                        FROM Base_Cow AS D
                                        LEFT JOIN [Base_CowGroup] AS G
                                        ON D.GroupID = G.ID
                                        WHERE D.FarmID={0} and D.GroupID={1} and IsStray=0", farmID, groupID);

            cowList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return cowList;
        }

        /// <summary>
        /// 按繁殖状态获取牛
        /// </summary>
        /// <param name="farmID">牧场ID</param>
        /// <param name="status">繁殖状态，0-6</param>
        /// <returns>牛列表</returns>
        public DataTable GetCowListByFarmStatus(int farmID, int status)
        {
            DataTable cowList = null;

            string sql = string.Format(@"SELECT D.EarNum, 
                                        D.FarmID, 
                                        D.GroupID, 
                                        D.Gender, 
                                        D.BirthDate, 
                                        D.BirthWeight, 
                                        D.Color, 
                                        D.Status, 
                                        D.IsIll,
                                        D.HouseID,
                                        D.IsStray,
                                        D.FatherID,
                                        D.MotherID,
                                        G.Name AS GroupName
                                        FROM Base_Cow AS D
                                        LEFT JOIN [Base_CowGroup] AS G
                                        ON D.GroupID = G.ID
                                        WHERE D.FarmID={0} and D.Status={1} and IsStray=0", farmID, status);

            cowList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return cowList;
        }


        /// <summary>
        /// 更新牛牛群号，牛舍号
        /// </summary>
        /// <param name="earNum"></param>
        /// <param name="groupID"></param>
        /// <param name="houseID"></param>
        /// <returns></returns>
        public int UpdateCow(int earNum, int groupID, int houseID)
        {
            string sql = string.Format(@"UPDATE Base_Cow SET
                                        GroupID={1},
                                        HouseID={2}
                                        where EarNum={0} 
                                        ", earNum, groupID, houseID);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 设牛繁殖状态
        /// </summary>
        /// <param name="earNum"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateCowBreedStatus(int earNum, int status)
        {
            string sql = string.Format(@"UPDATE Base_Cow SET
                                        [status]='{1}'
                                        where EarNum={0} 
                                        ", earNum, status);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);

        }


        /// <summary>
        /// 设牛生病与否
        /// </summary>
        /// <param name="earNum"></param>
        /// <param name="isIll"></param>
        /// <returns></returns>
        public int UpdateCowIllStatus(int earNum, int isIll)
        {
            string sql = string.Format(@"UPDATE Base_Cow SET
                                        IsIll={1}
                                        where EarNum={0} 
                                        ", earNum, isIll);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
        /// <summary>
        /// 设置牛离群状态
        /// </summary>
        /// <param name="earNum"></param>
        /// <param name="isStray"></param>
        /// <returns></returns>
        public int UpdateCowStrayStatus(int earNum, int isStray)
        {
            string sql = string.Format(@"UPDATE Base_Cow SET
                                        IsStray={1}
                                        where EarNum={0} 
                                        ", earNum, isStray);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public int ConvertDislayEarNumToEarNum(string displayEarNum, int farm)
        {
            DataTable dt = null;

            string sql = string.Format(@"SELECT EarNum
                            FROM Base_Cow
                            WHERE DisplayEarNum = '{0}' AND FarmID = '{1}' and [IsStray]=0", displayEarNum, farm);

            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (dt.Rows.Count == 0)
                return -1;
            else
                return Convert.ToInt32(dt.Rows[0]["EarNum"]);
        }

        public string ConvertEarNumToDisplayEarNum(int earNum)
        {
            DataTable dt = null;

            string sql = string.Format(@"SELECT DisplayEarNum
                            FROM Base_Cow
                            WHERE EarNum = {0} and [IsStray]=0", earNum);

            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (dt.Rows.Count == 0)
                return string.Empty;
            else
                return dt.Rows[0]["DisplayEarNum"].ToString();
        }

        /// <summary>
        /// 出入一头牛
        /// </summary>
        /// <param name="displayEarNum"></param>
        /// <param name="farmID"></param>
        /// <param name="groupID"></param>
        /// <param name="houseID"></param>
        /// <param name="gender"></param>
        /// <param name="birthDate"></param>
        /// <param name="status"></param>
        /// <param name="isIll"></param>
        /// <param name="fatherID"></param>
        /// <param name="motherID"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public int InsertCow(string displayEarNum, int farmID, int groupID, int houseID, string gender, DateTime birthDate, int status, int isIll, string fatherID, string motherID, string color)
        {
            string sql = string.Format(@"INSERT Base_Cow ([DisplayEarNum]
                                                      ,[FarmID]
                                                      ,[GroupID]
                                                      ,[HouseID]
                                                      ,[Gender]
                                                      ,[BirthDate]
                                                      ,[Status]
                                                      ,[IsIll]
                                                    ,[IsStray]
                                                       ,[FatherID]
                                                    ,[MotherID]
                                                      ,[Color])
                                                        values('{0}',{1},{2},{3},'{4}','{5}',{6},{7},{8},'{9}','{10}','{11}')
                                    ", displayEarNum, farmID, groupID, houseID, gender, birthDate, status, isIll, 0, fatherID, motherID, color);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);

        }

        public bool CheckCowInFarm(string displayEarNum, int farmID)
        {
            bool result = false;

            string sql = string.Format(@"SELECT COUNT(0) 
                                    FROM [Base_Cow]
                                    WHERE FarmID = {0} AND DisplayEarNum = '{1}'", farmID, displayEarNum);

            int count = Convert.ToInt32(dataProvider1mutong.ExecuteScalar(sql, CommandType.Text));
            result = count > 0 ? true : false;
            return result;
        }
    }
}
