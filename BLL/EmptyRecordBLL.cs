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
    public class EmptyRecordBLL
    {
        EmptyRecordDAL dalEmptyRecord = new EmptyRecordDAL();
        public List<EmptyRecord> GetEmptyRecordList()
        {
            List<EmptyRecord> lstEmptyRecord = new List<EmptyRecord>();
            DataTable datEmptyRecord = this.dalEmptyRecord.GetEmptyRecordList();
            foreach (DataRow drEmptyRecord in datEmptyRecord.Rows)
            {
                EmptyRecord emptyRecordItem = WrapEmptyRecordItem(drEmptyRecord);
                lstEmptyRecord.Add(emptyRecordItem);
            }
            return lstEmptyRecord;
        }

        private EmptyRecord WrapEmptyRecordItem(DataRow emptyRecordRow)
        {
            EmptyRecord emptyRecordItem = new EmptyRecord();
            if (emptyRecordRow != null)
            {
                emptyRecordItem.ID = Convert.ToInt32(emptyRecordRow["ID"]);
                if (Convert.ToInt32(emptyRecordRow["CowGroupID"]) != 0)
                {
                    emptyRecordItem.CowGroupID = Convert.ToInt32(emptyRecordRow["CowGroupID"]);
                }
                if (Convert.ToInt32(emptyRecordRow["FormulaID"]) != 0)
                {
                    emptyRecordItem.FormulaID = Convert.ToInt32(emptyRecordRow["FormulaID"]);
                }
                if (Convert.ToInt32(emptyRecordRow["RecordUserID"]) != 0)
                {
                    emptyRecordItem.RecordUserID = Convert.ToInt32(emptyRecordRow["RecordUserID"]);
                }
                emptyRecordItem.RecordTime = Convert.ToDateTime(emptyRecordRow["RecordTime"]);
                if (Convert.ToInt32(emptyRecordRow["EmptyHour"]) != 0)
                {
                    emptyRecordItem.EmptyHour = (float)(emptyRecordRow["EmptyHour"]);
                }
            }
            return emptyRecordItem;
        }

        public int InsertEmptyRecordInfo(EmptyRecord emptyRecord)
        {
            return dalEmptyRecord.InsertEmptyRecordInfo(emptyRecord);
        }

        public int GetCowGroupID(int id)
        {
            return dalEmptyRecord.GetCowGroupID(id);
        }
    }
}
