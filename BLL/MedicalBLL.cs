using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyCow.BLL
{
    public class MedicalBLL
    {
        private MedicalDAL medicalDAL = new MedicalDAL();

        public int InsertQurantine(Immune immune)
        {
            return this.medicalDAL.InsertImmuneRecord(immune.PastureID, immune.ImmuneDate.Date, immune.Vaccine, immune.EarNum, immune.DoctorID);
        }

        public List<Immune> GetImmuneList(int pastureID)
        {
            DataTable table = this.medicalDAL.GetImmuneTable(pastureID);
            List<Immune> list = new List<Immune>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapImmuneItem(item));
            }
            return list;
        }

        public List<Immune> GetQuarantineList(int pastureID, int earNum)
        {
            DataTable table = this.medicalDAL.GetImmuneTable(pastureID, earNum);
            List<Immune> list = new List<Immune>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapImmuneItem(item));
            }
            return list;
        }

        public Immune WrapImmuneItem(DataRow row)
        {
            Immune immune = new Immune();
            immune.ID = Convert.ToInt32(row["ID"]);
            immune.PastureID = Convert.ToInt32(row["PastureID"]);
            immune.Vaccine = row["Vaccine"].ToString();
            immune.ImmuneDate = Convert.ToDateTime(row["ImmuneDate"]);
            immune.EarNum = Convert.ToInt32(row["EarNum"]);
            immune.DoctorID = Convert.ToInt32(row["DoctorID"]);
            return immune;
        }
        
        public List<DiseaseType> GetDiseasesType()
        {
            DataTable table = this.medicalDAL.GetDiseaseTypes();
            List<DiseaseType> list = new List<DiseaseType>();
            foreach (DataRow row in table.Rows)
            {
                DiseaseType d = new DiseaseType();
                d.ID = Convert.ToInt32(row["DiseaseType_Id"]);
                d.Name = row["DiseaseType_Name"].ToString();
                d.Code = row["DiseaseType_Code"].ToString();
                list.Add(d);
            }
            return list;
        }

        public List<Disease> GetDiseases()
        {
            DataTable table = this.medicalDAL.GetDiseases();
            List<Disease> list = new List<Disease>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(WrapDiseaseItem(row));
            }
            return list;
        }

        public Disease WrapDiseaseItem(DataRow row)
        {
            Disease d = new Disease();
            d.ID = Convert.ToInt32(row["Disease_Id"]);
            d.Name = row["DiseaseName"].ToString();
            d.Code = row["Disease_Code"].ToString();
            d.DiseaseTypeID = Convert.ToInt32(row["DiseaseType_Id"]);
            return d;
        }

        public void InsertCare(Care care)
        {
            Disease d = GetDiseases().Find(p => p.ID == care.Disease_Id);
            //乳房类：137，蹄类174
            if (d.DiseaseTypeID==137||d.DiseaseTypeID==174)
            {
                medicalDAL.InsertCare(care,true);
            }
            else
            {
                medicalDAL.InsertCare(care, false);
            }
            
        }


        public int GetCareCowsCount(int diseaseId, DateTime startDate,DateTime endDate)
        {
            return medicalDAL.GetCareCowsCount(diseaseId, startDate, endDate);
        }
    }
}
