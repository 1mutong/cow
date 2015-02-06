using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.DAL
{
    public class MilkHallDAL : BaseDAL
    {
        public DataTable GetMilkHallList()
        {
            DataTable milkHallList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[Name]
                                            ,[PastureID]
                                            ,[VacuumPressure]
                                            ,[Pulsation]
                                            ,[CleanupCount]
                                            ,[ModifyTime]
                                        FROM [Milk_Hall]");
            milkHallList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return milkHallList;
        }
        public DataTable GetMilkHallByID()
        {
            DataTable milkHallList = null;
            string sql = string.Format(@"SELECT top 1 [ID]
                                            ,[Name]
                                            ,[PastureID]
                                            ,[VacuumPressure]
                                            ,[Pulsation]
                                            ,[CleanupCount]
                                            ,[ModifyTime]
                                        FROM [Milk_Hall] order by ModifyTime DESC");
            milkHallList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return milkHallList;
        }
        public int UpdateMilkHallInfo(MilkHall milkHall)
        {
            //StringBuilder sql = new StringBuilder();
            //sql.Append(@"UPDATE [Milk_Hall] set ");
            //if (milkHall.VacuumPressure != null)
            //{
            //    sql.Append("[VacuumPressure] = " + milkHall.VacuumPressure + ",");
            //}
            //if (milkHall.Pulsation != null && milkHall.Pulsation != 0)
            //{
            //    sql.Append("[Pulsation] = " + milkHall.Pulsation + ",");
            //}
            //if (milkHall.CleanupCount != null && milkHall.CleanupCount != 0)
            //{
            //    sql.Append("[CleanupCount] = " + milkHall.CleanupCount + ",");
            //}
            //if (milkHall.ModifyTime != null && string.IsNullOrWhiteSpace(milkHall.ModifyTime.ToString()))
            //{
            //    sql.Append("[ModifyTime] = " + milkHall.ModifyTime + ",");
            //}
            //sql.Append("[Name]='" + milkHall.Name + "' where ID=" + id);
            string sql = String.Format(@"insert into Milk_Hall ([VacuumPressure]
                                                                      ,[Pulsation]
                                                                      ,[CleanupCount]
                                                                      ,[ModifyTime])
                                                                       values ({0},{1},{2},'{3}')",
                                              milkHall.VacuumPressure, milkHall.Pulsation, milkHall.CleanupCount, milkHall.ModifyTime);

            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }


    }
}
