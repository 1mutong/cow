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
    public class EmptyRecordDAL : BaseDAL
    {


        //插入空槽记录信息
        public int InsertEmptyRecordInfo(EmptyRecord emptyRecord)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into [Feed_EmptyRecord] values (
                                    " + emptyRecord.CowGroupID + ","
                                    + emptyRecord.FormulaID + ","
                                    + emptyRecord.RecordUserID + ",'"
                                    + emptyRecord.RecordTime + "',"
                                    + emptyRecord.EmptyHour + ")");

            return dataProvider1mutong.ExecuteNonQuery(sql.ToString(), CommandType.Text);
        }

        public int GetCowGroupID(int id)
        {
            int cowGroupID = 0;
            string sql = string.Format(@"select top(1) CowGroupID from [Feed_EmptyRecord] where ID = {0}", id);
            cowGroupID = Convert.ToInt32(dataProvider1mutong.ExecuteScalar(sql, CommandType.Text));
            return cowGroupID;
        }

        //得到所有的空槽记录信息
        public DataTable GetEmptyRecordList()
        {
            DataTable emptyRecordList = null;
            string sql = string.Format(@"SELECT f.[ID]
             ,f.[CowGroupID]
             ,f.[FormulaID]
             ,f.[RecordUserID]
             ,f.[RecordTime]
             ,f.[EmptyHour]
             ,b.Name
         FROM [Feed_EmptyRecord] as f inner join Base_CowGroup as b on b.ID=f.CowGroupID");
            emptyRecordList = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return emptyRecordList;
        }
    }
}
