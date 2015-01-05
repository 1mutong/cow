using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class House
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 牛群ID，为0表示未分配给牛群
        /// </summary>
        public int GroupID { get; set; }
        public int PastureID { get; set; }
        /// <summary>
        /// 牛舍中牛数
        /// </summary>
        public int CowNumber { get; set; }

        public string GroupName { get; set; }

        public string GroupType { get; set; }
    }
}
