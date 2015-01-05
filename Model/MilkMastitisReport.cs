using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class MilkMastitisReport
    {
        //耳号
        public int EarNum { get; set; }
        //检查时间
        public DateTime DetectionDate { get; set; }
        //检测人
        public string Detector { get; set; }
        public int LeftFront { get; set; }
        public int RightFront { get; set; }
        public int LeftBack { get; set; }
        public int RightBack { get; set; }
        public string Description { get; set; }
    }
}
