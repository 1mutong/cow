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
    public class UnForbidInseminationDAL : BaseDAL
    {
        public DataTable GetUnForbidInseminationList()
        {
            DataTable unForbidInseminationList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[EarNum]
                                            ,[OperateDate]
                                            ,[Operator]
                                            ,[Description]
                                        FROM [1mutong].[dbo].[Breed_UnForbidInsemination]");
            unForbidInseminationList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return unForbidInseminationList;
        }

        public int InsertUnForbidInseminationInfo(UnForbidInsemination unForBidInsemination)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [1mutong].[dbo].[Breed_UnForbidInsemination] values (
                                    " + unForBidInsemination.EarNum + ",'"
                          + unForBidInsemination.OperateDate + "',"
                          + unForBidInsemination.Operator + ",'"
                          + unForBidInsemination.Description + "')");
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
