using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 去附乳
    /// </summary>
    public class RemoveAddMilk
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
