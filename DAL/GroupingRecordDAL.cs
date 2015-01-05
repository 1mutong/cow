using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.DAL;
using DairyCow.Model;
using DairyCow.DAL.Base;

namespace DairyCow.DAL
{
    public class GroupingRecordDAL:BaseDAL
    {
        public DataTable GetGroupingRecordByTaskID(int taskID)
        {
            string sql = string.Format(@"select TaskID,
                                        EarNum,
                                        TargetGroupID,
                                        TargetHouseID,
                                        OriginalGroupID,
                                        OriginalHouseID
                                        from Feed_GroupingRecord
                                        where TaskID={0}", taskID);

            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }


        public int InsertGroupingRecord(int taskID,int earNum,int targetGroupID,int targetHouseID,int originalGroupID,int originalHouseID)
        {
            string sql = string.Format(@"INSERT Feed_GroupingRecord
                                        (TaskID,
                                        EarNum,
                                        TargetGroupID,
                                        TargetHouseID,
                                        OriginalGroupID,
                                        OriginalHouseID)
                                        values({0},{1},{2},{3},{4},{5})", taskID,earNum,targetGroupID,targetHouseID,originalGroupID,originalHouseID);

            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
    }
}
