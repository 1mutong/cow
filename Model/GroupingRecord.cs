using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class GroupingRecord
    {
        public int TaskID { get; set; }
        public int EarNum { get; set; }
        public int TargetGroupID { get; set; }
        public int TargetHouseID { get; set; }
        public int OriginalGroupID { get; set; }
        public int OriginalHouseID { get; set; }
    }
}
