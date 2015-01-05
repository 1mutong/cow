using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class MilkHall
    {
        //ID
        public int ID { get; set; }
        //奶厅名称
        public string Name { get; set; }
        //牧场ID
        public int PastureID { get; set; }
        //真空压力
        public float VacuumPressure { get; set; }
        //脉动次数
        public int Pulsation { get; set; }
        //设备清洗次数
        public int CleanupCount { get; set; }
        //修改时间
        public DateTime ModifyTime { get; set; }
    }
}
