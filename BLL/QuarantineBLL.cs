using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyCow.Model;
using DairyCow.DAL;
using System.Data;

namespace DairyCow.BLL
{
    public class QuarantineBLL
    {
        private QuarantineDAL quarantineDAL = new QuarantineDAL();

        public int InsertQurantine(Quarantine quarantine)
        {
            return this.quarantineDAL.InsertQuarantineRecord(quarantine.PastureID, quarantine.QuarantineDate.Date, quarantine.QuarantineType, quarantine.QuarantineMethod, quarantine.Result, quarantine.EarNum, quarantine.DoctorID);
        }

        public List<Quarantine> GetQuarantineList(int pastureID)
        {
            DataTable table = this.quarantineDAL.GetQurantineTable(pastureID);
            List<Quarantine> list = new List<Quarantine>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapQuarantineItem(item));
            }
            return list;
        }
        public List<Quarantine> GetQuarantineList(int pastureID,int earNum)
        {
            DataTable table = this.quarantineDAL.GetQurantineTable(pastureID,earNum);
            List<Quarantine> list = new List<Quarantine>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapQuarantineItem(item));
            }
            return list;
        }

        public Quarantine WrapQuarantineItem(DataRow row)
        {
            Quarantine q = new Quarantine();
            q.ID = Convert.ToInt32(row["ID"]);
            q.PastureID = Convert.ToInt32(row["PastureID"]);
            q.Result = Convert.ToInt32(row["Result"]);
            q.QuarantineDate = Convert.ToDateTime(row["QuarantineDate"]);
            q.QuarantineType = row["QuarantineType"].ToString();
            q.QuarantineMethod = row["QuarantineMethod"].ToString();
            q.EarNum = Convert.ToInt32(row["EarNum"]);
            q.DoctorID = Convert.ToInt32(row["DoctorID"]);
            return q;
        }
    }
}
