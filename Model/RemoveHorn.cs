using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 去角
    /// </summary>
    public class RemoveHorn
    {
        public int ID { get; set; }
        public int EarNum { get; set; }
        public string Method { get; set; }
        public DateTime OprateDate { get; set; }
    }
}
