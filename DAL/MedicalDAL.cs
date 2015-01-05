using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyCow.DAL.Base;
using System.Data;
using DairyCow.Model;


namespace DairyCow.DAL
{
    /// <summary>
    /// 免疫
    /// </summary>
    public class MedicalDAL : BaseDAL
    {
        /// <summary>
        /// 增加免疫记录
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="immuneDate"></param>
        /// <param name="vaccine"></param>
        /// <param name="earNum"></param>
        /// <param name="doctorID"></param>
        /// <returns></returns>
        public int InsertImmuneRecord(int pastureID, DateTime immuneDate, string vaccine, int earNum, int doctorID)
        {
            string sql = string.Format(@"Insert INTO Medical_Immune 
                                       (PastureID,
                                       ImmuneDate,
                                       Vaccine,
                                       EarNum,
                                       DoctorID) Values({0},'{1}','{2}',{3},{4})",
                                        pastureID, immuneDate.Date, vaccine, earNum, doctorID);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
        /// <summary>
        /// 完成免疫任务
        /// </summary>
        /// <returns></returns>
        public int CompleteImmune()
        {
            string sql = string.Format(@"Update Task set Status = 1 where TaskType = 8");
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
        public DataTable GetImmuneTable(int pastureID)
        {
            string sql = string.Format(@"Select PastureID,
                                       ImmuneDate,
                                       Vaccine,
                                       EarNum,
                                       DoctorID
                                        from Medical_Immune
                                        where PastureID={0}", pastureID);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);

        }

        public DataTable GetImmuneTable(int pastureID, int earNum)
        {
            string sql = string.Format(@"Select PastureID,
                                       ImmuneDate,
                                       Vaccine,
                                       EarNum,
                                       DoctorID
                                        from Medical_Immune
                                        where PastureID={0} and EarNum={1}", pastureID, earNum);
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);

        }

        public DataTable GetDiseaseTypes()
        {
            string sql = @"SELECT [DiseaseType_Id],[DiseaseType_Name],[DiseaseType_Code] FROM [Medical_DiseaseType]";
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }

        public DataTable GetDiseases()
        {
            string sql = @"SELECT Disease_Id,DiseaseType_Id,DiseaseName,Disease_Code  FROM Medical_DiseaseLibrary";
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);
        }

        public void InsertCare(Care care, bool isBeastOrFoot)
        {
            string sql;
            if (isBeastOrFoot)
            {
                sql = string.Format(@"insert into Medical_Care(EarNum,Disease_Id,Prescription,DoctorID,Date,LeftFront,RightFront,RightBack,LeftBack)
                                         values ({0},{1},'{2}',{3},'{4}',{5},{6},{7},{8})",
                                        care.EarNum, care.Disease_Id, care.Prescription, care.DoctorID, DateTime.Now.ToShortDateString(),
                                        care.LeftFront, care.RightFront, care.LeftBack, care.RightBack);
            }
            else
            {
                sql = string.Format(@"insert into Medical_Care(EarNum,Disease_Id,Prescription,DoctorID,Date)
                                         values ({0},{1},'{2}',{3},'{4}')",
                                        care.EarNum, care.Disease_Id, care.Prescription, care.DoctorID, DateTime.Now.ToShortDateString());
            }

            dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public int GetCareCowsCount(int diseaseId, DateTime date)
        {
            DataTable dt = null;

            string sql = string.Format(@"select count(*) from Medical_Care where Disease_Id = {0} and [Date]> '{1}'", diseaseId, date.Date.ToShortDateString());

            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return Convert.ToInt32(dt.Rows[0].ItemArray[0]);
        }
        /// <summary>
        /// 获取某期间某疾病头次
        /// </summary>
        /// <param name="diseaseId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int GetCareCowsCount(int diseaseId, DateTime startDate, DateTime endDate)
        {
            DataTable dt = null;

            string sql = string.Format(@"select count(*) from Medical_Care where Disease_Id = {0} and [Date]>='{1}' and [Date]<='{2}'", diseaseId, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString());

            dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return Convert.ToInt32(dt.Rows[0].ItemArray[0]);
        }

        public int GetDiseaseTypeID(int diseaseID)
        {
            string sql = string.Format(@"SELECT Disease_Id,DiseaseType_Id,DiseaseName,Disease_Code  FROM Medical_DiseaseLibrary
                                        where Disease_Id = {0} and [Date]>= '{1}'", diseaseID);

            DataTable dt = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            return Convert.ToInt32(dt.Rows[0].ItemArray[0]);
        }
    }
}
