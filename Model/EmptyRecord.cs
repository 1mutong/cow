using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 空槽记录
    /// </summary>
    public class EmptyRecord
    {
        public int ID { get; set; }
        //牛群ID
        public int CowGroupID { get; set; }
        //配方ID
        public int FormulaID { get; set; }
        //记录人ID
        public int RecordUserID { get; set; }
        //空槽记录时间
        public DateTime RecordTime { get; set; }
        //空料时间（小时）
        public float EmptyHour { get; set; }
    }
}
