using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DairyCow.Model
{
    public class MedicalViewModel
    {
        public string EarNum { get; set; }
        public string DisplayEarNum { get; set; }
        public string DiseaseName { get; set; }
        public string DiseaseTypeName { get; set; }
        public int DiseaseTypeID { get; set; }
        public string Prescription { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; }
        public int LeftFront { get; set; }
        public int RightFront { get; set; }
        public int RightBack { get; set; }
        public int LeftBack { get; set; }
    }
}