using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.DAL
{
    /// <summary>
    /// 修脚
    /// </summary>
    public class RepairHoof
    {
        public int ID { get; set; }
        public int EarNum { get; set; }
        public string Method { get; set; }
        public string LeftFront { get; set; }
        public string RightFront { get; set; }
        public string LeftBack { get; set; }
        public string RightBack { get; set; }
        public DateTime OprateDate { get; set; }
    }
}
