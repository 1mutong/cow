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
    public class RemainRecordDAL : BaseDAL
    {
        public DataTable GetRemainRecordList()
        {
            DataTable remainRecordList = null;
            string sql = string.Format(@"SELECT [ID]
                                            ,[CowGroupID]
                                            ,[FormulaID]
                                            ,[RecordUserID]
                                            ,[RecordTime]
                                            ,[RemainQuantity]
                                        FROM [1mutong].[dbo].[Feed_EmptyRecord]");
            remainRecordList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return remainRecordList;
        }
        public int InsertRemainRecordInfo(RemainRecord remainRecord)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [1mutong].[dbo].[Feed_RemainRecord] values (
                                    " + remainRecord.CowGroupID + ","
                                    + remainRecord.FormulaID + ","
                                    + remainRecord.RecordUserID + ",'"
                                    + remainRecord.RecordTime + "',"
                                    + remainRecord.RemainQuantity + ")");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }
    }
}
