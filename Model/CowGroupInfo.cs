using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class CowGroupInfo
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool HasFodderFormula { get; set; }

        public int CowCount { get; set; }
    }
}
