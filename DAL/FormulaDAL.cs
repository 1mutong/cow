using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyCow.DAL
{
    public class FormulaDAL : BaseDAL
    {
        public DataTable GetFormulaFodder(int cowGroupID)
        {
            DataTable DT = null;
            string sql = string.Format(@"SELECT P.ID AS FormulaID, P.Name AS FormulaName, S.Name AS FodderName, X.Quantity
                                        FROM Base_CowGroup AS G LEFT JOIN Feed_Formula AS P ON G.FodderFormulaID = P.ID
                                        LEFT JOIN Feed_FodderFormula AS X ON P.ID = X.FormulaID
                                        LEFT JOIN Feed_Fodder AS S ON X.FodderID = S.ID
                                        WHERE G.ID = '{0}'", cowGroupID);
            DT = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return DT;
        }
        /// <summary>
        /// 获取某配方的饲料表
        /// </summary>
        /// <param name="formulaID"></param>
        /// <returns></returns>
        public DataTable GetStandardFodderTable(int formulaID)
        {
            string sql = String.Format(@"SELECT [FormulaID]
                                              ,[FodderID]
                                              ,[Quantity]
                                          FROM [Feed_FodderFormula] WHERE [FormulaID]={0}",formulaID);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }

        public DataTable GetPastureFodderTable(int standardFodderID)
        {
            string sql = String.Format(@"SELECT [PastureID]
                                          ,[FodderID]
                                          ,[FodderName]
                                          ,[SysFodderID]
                                          ,[Quantity]
                                          ,[IsCurrent]
                                          ,[Price]
                                      FROM [Feed_PastureFodder] WHERE [SysFodderID]={0} AND [IsCurrent]=1", standardFodderID);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 获取配方列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetFormulaList()
        {
            string sql = string.Format(@"SELECT [ID]
                                          ,[Name]
                                      FROM [Feed_Formula]");
            DataTable formulaList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return formulaList;
        }

        /// <summary>
        /// 获取配方详细列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetFormulaInfoByID(string formulaID)
        {
            string sql = string.Format(@"SELECT * FROM View_Recipe where FormulaID = {0}", formulaID);
            DataTable formulaList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return formulaList;
        }

        public DataTable GetFormulaOnlyByID(string formulaID)
        {
            string sql = string.Format(@"SELECT * FROM [Feed_Formula] where ID = {0}", formulaID);
            DataTable formulaList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return formulaList;
        }

        public int InsertFormula(string formulaName, List<Fodder> fodder)
        {
            int formulaID = 0;

            try
            {
                List<string> lstInsertSql = new List<string>();
                string insertFormulaSql = string.Format(@"INSERT INTO [dbo].[Feed_Formula]
                                                                ([Name])
                                                            VALUES
                                                                ('{0}')
                                                    select @@identity
                                                    ", formulaName);
                formulaID = Convert.ToInt32(dataProvider1mutong.ExecuteScalar(insertFormulaSql, CommandType.Text));

                foreach (Fodder item in fodder)
                {
                    lstInsertSql.Add(string.Format(@"INSERT INTO [dbo].[Feed_FodderFormula]
                                                       ([FormulaID]
                                                       ,[FodderID]
                                                       ,[Quantity])
                                                 VALUES
                                                       ({0}
                                                       ,{1}
                                                       ,{2})", formulaID, item.ID, item.Quantity));
                }
                dataProvider1mutong.ExecuteNonQueryWithTransaction(lstInsertSql.ToArray());
            }
            catch
            {
                formulaID = -1;
            }
            return formulaID;
        }

        public int UpdateFormula(string formulaID, string formulaName, List<Fodder> fodder)
        {
            int result = 0;

            try
            {
                List<string> lstSql = new List<string>();
                lstSql.Add(string.Format(@"UPDATE [dbo].[Feed_Formula]
                                           SET [Name] = '{0}'
                                           WHERE ID = {1}", formulaName, formulaID));
                lstSql.Add(string.Format(@"DELETE FROM [dbo].[Feed_FodderFormula]
                                           WHERE FormulaID = {0}", formulaID));

                foreach (Fodder item in fodder)
                {
                    lstSql.Add(string.Format(@"INSERT INTO [dbo].[Feed_FodderFormula]
                                                       ([FormulaID]
                                                       ,[FodderID]
                                                       ,[Quantity])
                                                 VALUES
                                                       ({0}
                                                       ,{1}
                                                       ,{2})", formulaID, item.ID, item.Quantity));
                }

                dataProvider1mutong.ExecuteNonQueryWithTransaction(lstSql.ToArray());
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public int UpdateFormulaOfCowGroup(string formulaID, string cowGroupID)
        {
            int result = 0;

            try
            {
                string sql = string.Format(@"UPDATE [dbo].[Base_CowGroup]
                                           SET [FormulaID] = '{0}'
                                           WHERE ID = {1}", formulaID, cowGroupID);
                dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public int AddFodder(string formulaID, string fodderID, string fodderQuantity)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[Feed_FodderFormula]
                                               ([FormulaID]
                                               ,[FodderID]
                                               ,[Quantity])
                                         VALUES
                                               ({0},{1},{2})", formulaID, fodderID, fodderQuantity);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public int DeleteFodder(string formulaID, string fodderID)
        {
            string sql = string.Format(@"DELETE FROM [dbo].[Feed_FodderFormula]
                                         WHERE [FormulaID]={0} AND [FodderID]={1}", formulaID, fodderID);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public int UpdateFodderQuantity(string formulaID, string fodderID, string fodderQuantity)
        {
            string sql = string.Format(@"UPDATE [dbo].[Feed_FodderFormula]
                        SET [Quantity]={2}
                        WHERE [FormulaID]={0} AND [FodderID]={1}", formulaID, fodderID, fodderQuantity);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public int AddFormula(string formulaName)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[Feed_Formula]([Name])
                                         VALUES ('{0}')
                                        select @@identity", formulaName);
            return Convert.ToInt32(dataProvider1mutong.ExecuteScalar(sql, CommandType.Text));
        }
    }
}
