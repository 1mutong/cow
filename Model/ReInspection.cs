using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 复检信息类
    /// </summary>
    public class ReInspection
    {
        //耳号
        public int EarNum { get; set; }
        //配种信息ID
        public int InseminationID { get; set; }
        //复检日期
        public DateTime OperateDate { get; set; }
        //复检结果
        public int ReInspectResult { get; set; }
        //复检人
        public int Operator { get; set; }
        //复检协助人
        public string HelpOperator { get; set; }
        //配后天数
        public int AfterInsemDays { get; set; }
        //初检后天数
        public int AfterInitInspectDays { get; set; }
        //描述
        public string Description { get; set; }
    }
}
