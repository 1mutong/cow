using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class Pasture
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 牧场是否有服务，0：用户不能登录
        /// </summary>
        public bool IsActive { get; set; }

        public string Status
        {
            get
            {
                return IsActive ? "正常运营" : "暂停运营";
            }
        }
    }
}
