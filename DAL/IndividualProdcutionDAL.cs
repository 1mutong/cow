using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.Model;
using DairyCow.DAL.Base;

namespace DairyCow.DAL
{
    public class IndividualProdcutionDAL : BaseDAL
    {
        /// <summary>
        /// 插入单牛单班次产奶记录
        /// </summary>
        /// <param name="earNum"></param>
        /// <param name="milkDate"></param>
        /// <param name="milkWeight"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public int InsertIndividualProdcution(int earNum, DateTime milkDate, float milkWeight, string round)
        {
            string sql = String.Format(@"Insert INTO Milk_IndividualProduction 
                                       (EarNum,MilkDate,Weight,Round) Values({0},'{1}',{2},'{3}')",
                                        earNum, milkDate.Date, milkWeight, round);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
        /// <summary>
        /// 获取某段时间单牛日产奶记录
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public DataTable GetIndividualProductionTotalTable(int earNum, DateTime startDate, DateTime endDate)
        {
            string sql = string.Format(@"SELECT     EarNum, MilkDate, SUM(Weight) AS TotalMilk
                                        FROM         dbo.Milk_IndividualProduction
                                        GROUP BY MilkDate, EarNum 
                                        where EarNum={0} and MilkDate>={1} AND MilkDate<={2} ", earNum, startDate, endDate);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }


        public DataTable GetMilkCheckList(int farmID)
        {
            string sql = string.Format("select m.ID,m.EarNum, m.MilkDate,m.[Weight] as MilkWeight,m.[Round] from Milk_IndividualProduction as m inner join Base_Cow as b on m.EarNum=b.EarNum where b.FarmID={0} ", farmID);

            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }

    }
}
