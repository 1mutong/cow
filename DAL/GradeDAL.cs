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
    public class GradeDAL : BaseDAL
    {
        //得到所有的体况信息
        public DataTable GetGradeList()
        {
            DataTable gradeList = null;
            string sql = string.Format(@"SELECT [EarNum]
                                            ,[Height]
                                            ,[Weight]
                                            ,[Chest]
                                            ,[Score]
                                            ,[Description]
                                            ,[MeasureDate]
                                            ,[Measurer] 
                                        FROM [1mutong].[dbo].[Feed_Grade]");
            gradeList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return gradeList;
        }
        //得到最新的体况信息
        public DataTable GetGradeList(int earNum)
        {
            DataTable gradeList = null;
            string sql = string.Format(@"SELECT top(1) [EarNum]
                                            ,[Height]
                                            ,[Weight]
                                            ,[Chest]
                                            ,[Score]
                                            ,[Description]
                                            ,[MeasureDate]
                                            ,[Measurer]
                                        FROM [1mutong].[dbo].[Feed_Grade] 
                                        WHERE EarNum = '{0}' order by MeasureDate DESC", earNum);
            gradeList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return gradeList;
        }
        //插入牛的体况信息
        public int InsertGradeInfo(Grade grade)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [1mutong].[dbo].[Feed_Grade] values (
                                    '" + grade.EarNum + "',"
                                    + grade.Height + ","
                                    + grade.Weight + ","
                                    + grade.Chest + ","
                                    + grade.Score + ",'"
                                    + grade.Description + "','"
                                    + grade.MeasureDate + "',"
                                    + grade.Measurer + ")");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
