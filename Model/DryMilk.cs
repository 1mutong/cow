using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 干奶记录
    /// </summary>
    public class DryMilk
    {
        public int ID { get; set; }
        public DateTime DryDate{get;set;}
        public int EarNum { get; set; }
        /// <summary>
        /// 干奶状况，0正常，1提前，2推后
        /// </summary>
        public int DrySituation { get; set; }
        public string DryReason { get; set; }
        public int OperatorID { get; set; }
    }
}
