using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.Model;
using DairyCow.DAL;

namespace DairyCow.BLL
{
    public class GroupingRecordBLL
    {
        private GroupingRecordDAL groupingDAL = new GroupingRecordDAL();

        public GroupingRecord GetGroupingRecordByTaskID(int taskID)
        {
            GroupingRecord r;
            DataTable tb = groupingDAL.GetGroupingRecordByTaskID(taskID);
            if (tb.Rows.Count==1)
            {
                r = WrapGroupingRecord(tb.Rows[0]);
            }
            else
            {
                throw new Exception("分群任务记录不唯一或遗漏。");
            }
            return r;
        }


        public GroupingRecord WrapGroupingRecord(DataRow row)
        {
            GroupingRecord r = new GroupingRecord();
            r.TaskID = Convert.ToInt32(row["TaskID"]);
            r.EarNum = Convert.ToInt32(row["EarNum"]);
            r.TargetGroupID = Convert.ToInt32(row["TargetGroupID"]);
            r.TargetHouseID = Convert.ToInt32(row["TargetHouseID"]);
            r.OriginalGroupID = Convert.ToInt32(row["OriginalGroupID"]);
            r.OriginalHouseID = Convert.ToInt32(row["OriginalHouseID"]);
            return r;
        }


        public int InsertGroupingRecord(GroupingRecord record)
        {
            return groupingDAL.InsertGroupingRecord(record.TaskID, record.EarNum, record.TargetGroupID, record.TargetHouseID, record.OriginalGroupID, record.OriginalHouseID);
        }
    }
}
