using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class Immune
    {
        public int ID { get; set; }
        public int PastureID { get; set;}
        public int DoctorID { get;set; }
        /// <summary>
        /// 免疫疫苗，只有布鲁氏病疫苗、肺结核疫苗、口蹄疫疫苗和驱虫药四种。
        /// </summary>
        public string Vaccine { get; set; }
        public int EarNum { get; set; }
        public DateTime ImmuneDate { get; set; }

    }

    
}
