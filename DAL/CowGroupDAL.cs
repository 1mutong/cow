using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Data;

namespace DairyCow.DAL
{
    /// <summary>
    /// 牛群非配DAL
    /// </summary>
    public class CowGroupDAL : BaseDAL
    {
        /// <summary>
        /// 获得所有的牛群
        /// </summary>
        /// <returns></returns>
        public DataTable GetCowGroupTable()
        {
            DataTable cowGroupTable = null;
            string sql = string.Format(@"SELECT G.[ID]
                                            ,G.[Name] AS GroupName
                                            ,P.[Name] AS PastureName
                                            ,G.[PastureID]
                                            ,G.[Type]
                                            ,G.[FormulaID]
                                            ,G.[InsemOperatorID] 
                                            ,G.[Description] 
											,A.Name AS InsemOperatorName
                                            ,FeedOperatorID
                                            ,DoctorID
                                        FROM [Base_CowGroup] G 
										LEFT JOIN [Auth_User] A ON A.ID = G.InsemOperatorID
                                        JOIN [Base_Pasture] P ON G.PastureID = P.ID");
            cowGroupTable = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return cowGroupTable;
        }
        /// <summary>
        /// 获得牧场所有的牛群
        /// </summary>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public DataTable GetCowGroupTable(int pastureID)
        {
            DataTable cowGroupTable = null;
            string sql = string.Format(@"SELECT G.[ID]
                                            ,G.[Name] AS GroupName
                                            ,P.[Name] AS PastureName
                                            ,G.[PastureID]
                                            ,G.[Type]
                                            ,G.[FormulaID]
                                            ,G.[InsemOperatorID] 
                                            ,G.[Description] 
											,A.Name AS InsemOperatorName
                                            ,FeedOperatorID
                                            ,DoctorID
                                        FROM [Base_CowGroup] G 
										LEFT JOIN [Auth_User] A ON A.ID = G.InsemOperatorID
                                        JOIN [Base_Pasture] P ON G.PastureID = P.ID
                                        WHERE G.[PastureID]={0} ", pastureID);
            cowGroupTable = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return cowGroupTable;
        }


        public int InsertCowGroup(string name, int pastureID, int groupType, string description, int formulaID, int insemOperatorID, int feedOperatorID, int doctorID)
        {
            int temp;
            string sql = string.Format(@"INSERT [Base_CowGroup]
                                          ([Name]
                                          ,[PastureID]
                                          ,[Type]
                                          ,[Description]
                                          ,[FormulaID]
                                          ,[InsemOperatorID]
                                          ,[FeedOperatorID]
                                          ,[DoctorID]) values('{0}',{1},{2},'{3}',{4},{5},{6},{7}) "
                                            , name, pastureID, groupType, description, formulaID, insemOperatorID, feedOperatorID, doctorID);
            temp = dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
            return temp;
        }

        public int InsertCowGroup(string name, int pastureID, int groupType, string description)
        {
            int temp;
            string sql = string.Format(@"INSERT [Base_CowGroup]
                                          ([Name]
                                          ,[PastureID]
                                          ,[Type]
                                          ,[Description]
                                          ) values('{0}',{1},{2},'{3}') "
                                            , name, pastureID, groupType, description);
            temp = dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
            return temp;
        }
        /// <summary>
        /// 删除某群，如该群仍有牛，不可删。
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>删除的行数</returns>
        public int DeleteCowGroupByID(int groupID)
        {
            int cowNum = GetCowCount(groupID);
            if (cowNum == 0)
            {
                string sql = string.Format(@"delete from  [Base_CowGroup]  where ID ={0}", groupID);
                return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新牛群配种员分配信息
        /// </summary>
        /// <param name="cowGroupID">牛群ID</param>
        /// <param name="insemOperator">配种员ID</param>
        /// <returns></returns>
        public int UpdateCowGroupInsemOperator(int cowGroupID, int insemOperatorID)
        {
            string sql = string.Format(@"update [Base_CowGroup] set InsemOperatorID ={1} where ID ={0}", cowGroupID, insemOperatorID);
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }


        /// <summary>
        /// 更新牛群兽医分配信息
        /// </summary>
        /// <param name="cowGroupID">牛群ID</param>
        /// <param name="doctorID">兽医ID</param>
        /// <returns></returns>
        public int UpdateCowGroupDoctor(int cowGroupID, int doctorID)
        {
            string sql = string.Format(@"update [Base_CowGroup] set DoctorID ={1} where ID ={0}", cowGroupID, doctorID);
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }

        public int UpdateCowGroupFeeder(int cowGroupID, int feederID)
        {
            string sql = string.Format(@"update [Base_CowGroup] set FeedOperatorID ={1} where ID ={0}", cowGroupID, feederID);
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }


        public int UpdateCowGroupFormula(int cowGroupID, int formulaID)
        {
            string sql = string.Format(@"update [Base_CowGroup] set FormulaID ={1} where ID ={0}", cowGroupID, formulaID);
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }

        /// <summary>
        /// 更新牛群信息
        /// </summary>
        /// <returns></returns>
        public int UpdateCowGroupInfo(CowGroup group)
        {
            string sql = string.Format(@"UPDATE [Base_CowGroup]
                                           SET [Name] = '{0}'
                                              ,[PastureID] = {1}
                                              ,[FormulaID] = {2}
                                              ,[Type] = {3}
                                              ,[Description] = '{4}'
                                        WHERE ID = {5}", group.Name, group.PastureID, group.FormulaID, group.TypeNum, group.Description, group.ID);
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }

        /// <summary>
        /// 更新牛群信息
        /// </summary>
        /// <returns></returns>
        public int UpdateCowGroupInfo(int groupID, string groupName, int pastureID, int formulaID, int groupType, string description, int feederID, int insemOperatorID, int doctorID)
        {
            string sql = string.Format(@"UPDATE [Base_CowGroup]
                                           SET [Name] = '{0}'
                                              ,[PastureID] = {1}
                                              ,[FormulaID] = {2}
                                              ,[Type] = {3}
                                              ,[Description] = '{4}'
                                              ,[FeedOperatorID]={6}
                                              ,[InsemOperatorID]={7}
                                              ,[DoctorID]={8}
                                        WHERE ID = {5}", groupName, pastureID, formulaID, groupType, description, groupID, feederID, insemOperatorID, doctorID);
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }



        //应该统一用一个
        //        public DataTable GetCowGroupInfo()
        //        {
        //            DataTable cowGroup = null;

        //            string sql = string.Format(@"SELECT G.[ID],G.[NAME],G.[FORMULAID],G.PastureID,G.TYPE,P.NAME AS PastureName,G.FORMULAID,G.Description,F.Name AS FormulaName
        //                                        FROM [DBO].[BASE_COWGROUP] AS G
        //										LEFT JOIN [DBO].[BASE_PASTURE] AS P
        //										ON P.ID = G.PASTUREID
        //										LEFT JOIN [DBO].[FEED_FORMULA] F
        //										ON F.ID = G.FORMULAID");

        //            cowGroup = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

        //            return cowGroup;
        //        }

        /// <summary>
        /// 根据牛群ID获得配方ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetFormulaIDByGroupID(int id)
        {
            int formulaID = 0;
            string sql = string.Format(@"select top(1) FormulaID from [1mutong].[dbo].[Base_CowGroup] where ID = {0}", id);
            formulaID = Convert.ToInt32(dataProvider1mutong.ExecuteScalar(sql, CommandType.Text));
            return formulaID;
        }
        /// <summary>
        /// 获取某个牛群
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetCowGroupTableByID(int id)
        {
            DataTable cowGroup = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[Name] AS GroupName
                                            ,[PastureID]
                                            ,[FormulaID]
                                            ,[Description] 
                                            ,[Type]
                                            ,[InsemOperatorID]
                                            ,[FeedOperatorID]
                                            ,[DoctorID]
                                            ,[Description]
                                        FROM [Base_CowGroup] where ID = {0}", id);
            cowGroup = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return cowGroup;
        }
        /// <summary>
        /// 获取牛群牛数
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public int GetCowCount(int groupID)
        {
            DataTable DT = null;
            //            string sql = string.Format(@"SELECT COUNT(*)
            //                                        FROM Base_CowGroup AS G LEFT JOIN Base_Cow AS C ON G.ID = C.GroupID
            //                                        WHERE G.ID = '{0}'", groupID);
            string sql = string.Format(@"SELECT COUNT(*)
                                        FROM Base_Cow WHERE GroupID = {0}", groupID);
            DT = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return Convert.ToInt32(DT.Rows[0].ItemArray[0]);
        }

        /// <summary>
        /// 获取牛舍牛数
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public DataTable GetCowCountInHouse(int groupID)
        {
            DataTable DT = null;
            string sql = string.Format(@"SELECT H.Name, count(*) AS CowCount
                                      FROM Base_Cow AS C left join Base_CowHouse as H on C.HouseID = H.ID
                                      WHERE C.GROUPID = '{0}'
                                      GROUP BY H.Name", groupID);
            DT = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return DT;
        }
    }
}
