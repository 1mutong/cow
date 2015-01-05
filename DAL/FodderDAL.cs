using DairyCow.DAL.Base;
using System;
using System.Data;

namespace DairyCow.DAL
{
    public class FodderDAL : BaseDAL
    {
        /// <summary>
        /// 获取牧场饲料表
        /// </summary>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public DataTable GetPastureFodderTable(int pastureID)
        {
            DataTable fodderList = null;

            string sql = string.Format(@"SELECT P.[PastureID]
                                              ,P.[FodderID]
                                              ,P.[FodderName]
                                              ,P.[SysFodderID]
                                              ,P.[Quantity]
                                              ,P.[IsCurrent]
                                              ,P.[Price]
                                              ,S.[Name] AS SysFodderName
                                               ,S.[Description]
                                FROM [Feed_PastureFodder] AS P 
                                left join Feed_Fodder AS S on S.ID = P.SysFodderID
                                where PastureID = {0} and IsCurrent = 1", pastureID);

            fodderList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return fodderList;
        }
        /// <summary>
        /// 获取配方标准饲料
        /// </summary>
        /// <param name="formulaID"></param>
        /// <returns></returns>
        public DataTable GetStandardFodderTable(int formulaID)
        {
            DataTable fodderList = null;

            string sql = string.Format(@"SELECT [FormulaID]
                                              ,[FodderID]
	                                          ,Feed_Fodder.Name as FodderName
                                              ,[Quantity]
                                          FROM [Feed_FodderFormula]
                                          JOIN Feed_Fodder
                                          ON Feed_Fodder.ID = Feed_FodderFormula.FodderID
                                          WHERE Feed_FodderFormula.FormulaID = {0}", formulaID);

            fodderList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return fodderList;
        }
        /// <summary>
        /// 插入牧场饲料
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="fodderName"></param>
        /// <param name="sysFodderName"></param>
        /// <param name="quantity"></param>
        /// <param name="isCurrent"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public int InsertPastureFodder(int pastureID, string fodderName, int sysFodderName,double quantity,bool isCurrent,double price)
        {
            int current=isCurrent ?1:0;
            string sql = string.Format(@"Insert [Feed_PastureFodder] ( [PastureID]
                                                                      ,[FodderName]
                                                                      ,[SysFodderID]
                                                                      ,[Quantity]
                                                                      ,[IsCurrent]
                                                                      ,[Price]) values('{0}','{1}','{2}','{3}','{4}','{5}'
                                                    )", pastureID, fodderName, sysFodderName, quantity, current, price);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 删除牧场饲料
        /// </summary>
        /// <param name="fodderID"></param>
        /// <returns></returns>
        public int DeletePastureFodder(int fodderID)
        {
            string sql = string.Format(@"Delete from [Feed_PastureFodder] where [FodderID]={0}", fodderID);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }


        /// <summary>
        /// 获取所有标准饲料
        /// </summary>
        /// <returns></returns>
        public DataTable GetStandardFodderTable()
        {
            DataTable fodderList = null;

            string sql = string.Format(@"SELECT  [ID]
                                              ,[Name]
                                              ,[Description]
                                              ,[RefPrice]
                                              ,[DM]
                                              ,[NND]
                                              ,[Ca]
                                              ,[P]
                                              ,[CP]
                                              ,[Fat]
                                              ,[CF]
                                              ,[NFE]
                                              ,[ASH]
                                              ,[NDF]
                                              ,[ADF]
                                              ,[NPP]
                                              ,[Arg]
                                              ,[His]
                                              ,[Ile]
                                              ,[Leu]
                                              ,[Lys]
                                              ,[Met]
                                              ,[Cys]
                                              ,[Phe]
                                              ,[Tyr]
                                              ,[Thr]
                                              ,[Trp]
                                              ,[Val]
                                              ,[Na]
                                              ,[Cl]
                                              ,[Mg]
                                              ,[K]
                                              ,[Fe]
                                              ,[Cu]
                                              ,[Mn]
                                              ,[Zn]
                                              ,[Se]
                                          FROM [1mutong].[dbo].[Feed_Fodder]
                                          ");

            fodderList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return fodderList;
        }

        public int InsertStandardFodder(string name,string desc,double refPrice,double nuDM,
                                                                                            double nuNND,
                                                                                            double nuCa,
                                                                                            double nuP,
                                                                                            double nuCP,
                                                                                            double nuCF,
                                                                                            double nuFat,
                                                                                            double nuNFE,
                                                                                            double nuASH,
                                                                                            double nuNDF,
                                                                                            double nuADF,
                                                                                            double nuNPP,
                                                                                            double nuArg,
                                                                                            double nuHis,
                                                                                            double nuIle,
                                                                                            double nuLeu,
                                                                                            double nuLys,
                                                                                            double nuMet,
                                                                                            double nuCys,
                                                                                            double nuPhe,
                                                                                            double nuTyr,
                                                                                            double nuThr,
                                                                                            double nuTrp,
                                                                                            double nuVal,
                                                                                            double nuNa,
                                                                                            double nuCl,
                                                                                            double nuMg,
                                                                                            double nuK,
                                                                                            double nuFe,
                                                                                            double nuCu,
                                                                                            double nuMn,
                                                                                            double nuZn,
                                                                                            double nuSe
                                                                                            )
        {
            string str = "select * from [Feed_Fodder] where [Name] = '" + name + "'";
            DataTable dt = dataProvider1mutong.FillDataTable(str, CommandType.Text);
            if (dt.Rows.Count > 1)
            {
                return -10;
            }
            
            string sql = string.Format(@"Insert [Feed_Fodder] ([Name],
                                                                [Description],
                                                                [RefPrice],
                                                                [DM],
                                                                [NND],
                                                                [Ca],
                                                                [P],
                                                                [CP],
                                                                [CF],
                                                                [Fat],
                                                                [NFE],
                                                                [ASH],
                                                                [NDF],
                                                                [ADF],
                                                                [NPP],
                                                                [Arg],
                                                                [His],
                                                                [Ile],
                                                                [Leu],
                                                                [Lys],
                                                                [Met],
                                                                [Cys],
                                                                [Phe],
                                                                [Tyr],
                                                                [Thr],
                                                                [Trp],
                                                                [Val],
                                                                [Na],
                                                                [Cl],
                                                                [Mg],
                                                                [K],
                                                                [Fe],
                                                                [Cu],
                                                                [Mn],
                                                                [Zn],
                                                                [Se]
                                                                ) values ('{0}',
                                                                            '{1}',
                                                                            {2},
                                                                            {3},
                                                                            {4},
                                                                            {5},
                                                                            {6},
                                                                            {7},
                                                                            {8},
                                                                            {9},
                                                                            {10},
                                                                            {11},
                                                                            {12},
                                                                            {13},
                                                                            {14},
                                                                            {15},
                                                                            {16},
                                                                            {17},
                                                                            {18},
                                                                            {19},
                                                                            {20},
                                                                            {21},
                                                                            {22},
                                                                            {23},
                                                                            {24},
                                                                            {25},
                                                                            {26},
                                                                            {27},
                                                                            {28},
                                                                            {29},
                                                                            {30},
                                                                            {31},
                                                                            {32},
                                                                            {33},
                                                                            {34},
                                                                            {35}
                                                                            )",name,
                                                                            desc,
                                                                            refPrice,
                                                                             nuDM,
                                                                             nuNND,
                                                                             nuCa,
                                                                             nuP,
                                                                             nuCP,
                                                                             nuCF,
                                                                             nuFat,
                                                                             nuNFE,
                                                                             nuASH,
                                                                             nuNDF,
                                                                             nuADF,
                                                                             nuNPP,
                                                                             nuArg,
                                                                             nuHis,
                                                                             nuIle,
                                                                             nuLeu,
                                                                             nuLys,
                                                                             nuMet,
                                                                             nuCys,
                                                                             nuPhe,
                                                                             nuTyr,
                                                                             nuThr,
                                                                             nuTrp,
                                                                             nuVal,
                                                                             nuNa,
                                                                             nuCl,
                                                                             nuMg,
                                                                             nuK,
                                                                             nuFe,
                                                                             nuCu,
                                                                             nuMn,
                                                                             nuZn,
                                                                             nuSe
                                                                            );
            int i = dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
            if (i < 0) return i;
            str = "select [ID], [Name] from [Feed_Fodder] where [Name] = '" + name + "'";
            dt = dataProvider1mutong.FillDataTable(str, CommandType.Text);

            int id = Convert.ToInt32(dt.Rows[0]["ID"]);
            str = "select [ID] from [Feed_Formula]";
            dt = dataProvider1mutong.FillDataTable(str, CommandType.Text);
            foreach(DataRow row in dt.Rows)
            {
                sql = string.Format(@"Insert [Feed_FodderFormula] ([FormulaID],
                                                                [FodderID],
                                                                [Quantity]
                                                                ) values ('{0}',
                                                                            '{1}',
                                                                            {2})",Convert.ToInt32(row["ID"]),id,0);
                dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
            }
            return 1;
        }
    }



}
