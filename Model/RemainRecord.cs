using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class RemainRecord
    {
        public int ID { get; set; }
        //牛群ID
        public int CowGroupID { get; set; }
        //配方ID
        public int FormulaID { get; set; }
        //记录人ID
        public int RecordUserID { get; set; }
        //剩料记录时间
        public DateTime RecordTime { get; set; }
        //剩料记录量
        public float RemainQuantity { get; set; }
    }
}
