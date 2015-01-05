using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 配种信息
    /// </summary>
    public class Insemination
    {
        //InseminationID
        public int ID { get; set; }
        //耳号
        public int EarNum { get; set; }
        //配次（改为冻精数）
        public int InseminationNum { get; set; }
        //冻精编号
        public string SemenNum { get; set; }
        //冻精类型
        public int SemenType { get; set; }
        //发情发现方式
        public int EstrusFindType { get; set; }
        //配种日期
        public DateTime OperateDate { get; set; }
        //配种员
        //public string Operator { get; set; }
        //配种员ID
        public int operatorID { get; set; }
        //描述
        public string Description { get; set; }
        //发情日期
        public DateTime EstrusDate { get; set; }
        //发情类型
        public int EstrusType { get; set; }
        //发情发现人
        public string EstrusFindPerson { get; set; }

    }
}
