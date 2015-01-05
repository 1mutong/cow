using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class ForbidInsemination
    {
        public int ID { get; set; }
        public int EarNum { get; set; }
        //禁配时间
        public DateTime OperateDate { get; set; }
        //禁配人ID
        public int Operator { get; set; }
        //禁配原因
        public string Description { get; set; }
    }
}
