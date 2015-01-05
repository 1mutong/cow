using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class UnForbidInsemination
    {
        public int ID { get; set; }
        public int EarNum { get; set; }
        public DateTime OperateDate { get; set; }
        public int Operator { get; set; }
        public string Description { get; set; }
    }
}
