using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyCow.DAL.Base;
using System.Data;

namespace DairyCow.DAL
{
    public class QuarantineDAL : BaseDAL
    {
        public int InsertQuarantineRecord(int pastureID, DateTime quarantineDate, string quarantineType, string quarantineMethod, int result, int earNum, int doctorID)
        {
            string sql = string.Format(@"Insert INTO Medical_Quarantine 
                                       (PastureID,
                                       QuarantineDate,
                                       QuarantineType,
                                       QuarantineMethod,
                                       result,
                                       EarNum,
                                       DoctorID) Values({0},'{1}','{2}','{3}',{4},{5},{6})",
                                        pastureID, quarantineDate.Date, quarantineType, quarantineMethod, result, earNum, doctorID);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public DataTable GetQurantineTable(int pastureID)
        {
            string sql = string.Format(@"Select PastureID,
                                       QuarantineDate,
                                        QuarantineType,
                                       QuaratineMethod,
                                       result,
                                       EarNum,
                                       DoctorID
                                        from Medical_Quarantine
                                        where PastureID={0}", pastureID);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);

        }

        public DataTable GetQurantineTable(int pastureID, int earNum)
        {
            string sql = string.Format(@"Select PastureID,
                                       QuarantineDate,
                                        QuarantineType,
                                       QuaratineMethod,
                                       result,
                                       EarNum,
                                       DoctorID
                                        from Medical_Quarantine
                                        where PastureID={0} and EarNum={1}", pastureID, earNum);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);

        }
    }
}
