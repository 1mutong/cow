using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.BLL
{

    public class RemainRecordBLL
    {
        RemainRecordDAL dalRemainRecord = new RemainRecordDAL();
        public List<RemainRecord> GetRemainRecordList()
        {
            List<RemainRecord> lstRemainRecord = new List<RemainRecord>();
            DataTable datRemainRecord = this.dalRemainRecord.GetRemainRecordList();
            foreach (DataRow drRemainRecord in datRemainRecord.Rows)
            {
                RemainRecord remainRecordItem = WrapRemainRecordItem(drRemainRecord);
                lstRemainRecord.Add(remainRecordItem);
            }
            return lstRemainRecord;
        }

        private RemainRecord WrapRemainRecordItem(DataRow remainRecordRow)
        {
            RemainRecord remainRecordItem = new RemainRecord();
            if (remainRecordRow != null)
            {
                remainRecordItem.ID = Convert.ToInt32(remainRecordRow["ID"]);
                if (Convert.ToInt32(remainRecordRow["CowGroupID"]) != 0)
                {
                    remainRecordItem.CowGroupID = Convert.ToInt32(remainRecordRow["CowGroupID"]);
                }
                if (Convert.ToInt32(remainRecordRow["FormulaID"]) != 0)
                {
                    remainRecordItem.FormulaID = Convert.ToInt32(remainRecordRow["FormulaID"]);
                }
                if (Convert.ToInt32(remainRecordRow["RecordUserID"]) != 0)
                {
                    remainRecordItem.RecordUserID = Convert.ToInt32(remainRecordRow["RecordUserID"]);
                }
                remainRecordItem.RecordTime = Convert.ToDateTime(remainRecordRow["RecordTime"]);
                if (Convert.ToInt32(remainRecordRow["RemainQuantity"]) != 0)
                {
                    remainRecordItem.RemainQuantity = (float)(remainRecordRow["RemainQuantity"]);
                }
            }
            return remainRecordItem;
        }
        public int InsertRemainRecordInfo(RemainRecord remainRecord)
        {
            return dalRemainRecord.InsertRemainRecordInfo(remainRecord);
        }
    }
}
