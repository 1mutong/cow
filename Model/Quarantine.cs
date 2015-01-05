using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class Quarantine
    {

        public int ID { get; set; }
        public int PastureID { get; set; }
        public int DoctorID { get; set; }
        /// <summary>
        /// 检疫类型，只有，布鲁氏病、肺结核和口蹄疫三种。
        /// </summary>
        public string QuarantineType { get; set; }
        /// <summary>
        /// 检疫方式，“抽血”或“皮试”
        /// </summary>
        public string QuarantineMethod { get; set; }
        public int EarNum { get; set; }
        public DateTime QuarantineDate { get; set; }
        /// <summary>
        /// 检疫结果，0：阴性，1：阳性
        /// </summary>
        public int Result { get; set; } 
    }
}
