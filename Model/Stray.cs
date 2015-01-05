using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class Stray
    {
        public int EarNum { get; set; }
        public int PastureID { get; set; }
        /// <summary>
        /// 离群类型，0：淘汰，1:死亡
        /// </summary>
        public int StrayType { get; set; }
        /// <summary>
        /// 是否出售，0：否，1：是出售
        /// </summary>
        public int IsSold { get; set; }
        /// <summary>
        /// 出售价格，0：非出售
        /// </summary>
        public float Price { get; set; }
        public string Reason { get; set; }
        public DateTime StrayDate { get; set; }
    }
}
