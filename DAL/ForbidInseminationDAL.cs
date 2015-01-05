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
    public class ForbidInseminationDAL : BaseDAL
    {
        public DataTable GetForbidInseminationList()
        {
            DataTable forbidInseminationList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[EarNum]
                                            ,[OperateDate]
                                            ,[Operator]
                                            ,[Description]
                                        FROM [1mutong].[dbo].[Breed_ForbidInsemination]");
            forbidInseminationList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return forbidInseminationList;
        }

        public int InsertForbidInseminationInfo(ForbidInsemination forBidInsemination)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [1mutong].[dbo].[Breed_ForbidInsemination] values (
                                    " + forBidInsemination.EarNum + ",'"
                          + forBidInsemination.OperateDate + "',"
                          + forBidInsemination.Operator + ",'"
                          + forBidInsemination.Description + "')");
            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
