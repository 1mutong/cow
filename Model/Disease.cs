using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class Disease
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DiseaseTypeID { get; set; }
    }
}
