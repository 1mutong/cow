using System;

namespace DairyCow.Model
{
    public class Care
    {
        public int EarNum { get; set; }
        public string DisplayEarNum { get; set; }
        public int Disease_Id { get; set; }
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
