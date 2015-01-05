using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 初检信息类
    /// </summary>
    public class InitialInspection
    {
        //耳号
        public int EarNum { get; set; }
        //配种信息ID
        public int InseminationID { get; set; }
        //初检日期
        public DateTime OperateDate { get; set; }
        //初检结果1:+,0:-
        public int InspectResult { get; set; }
        //初检人
        public int Operator { get; set; }
        //初检协助人
        public string HelpOperator { get; set; }
        //初检方式
        public int InspectWay { get; set; }
        //配后天数
        public int AfterInsemDays { get; set; }
        public string Description { get; set; }
    }
}
