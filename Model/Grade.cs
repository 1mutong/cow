using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class Grade
    {
        //耳号
        public int EarNum { get; set; }
        public string DisplayEarNum { get; set; }
        //体高
        public int Height { get; set; }
        //体重
        public int Weight { get; set; }
        //胸围
        public int Chest { get; set; }
        //评分
        public int Score { get; set; } 
        //体况描述
        public string Description { get; set; }
        //记录时间
        public DateTime MeasureDate { get; set; }
        //记录人ID
        public int Measurer { get; set; }
    }
}
