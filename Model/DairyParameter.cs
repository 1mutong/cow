using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class DairyParameter
    {
        public int PastureID { get; set; }
        public string ParameterName { get; set; }
        public float ParameterValue { get; set; }
        public bool CanBeConfiguredByPasture { get; set; }
        public bool CanBeConfiguredByAdmin { get; set; }
        public string Description { get; set; }
    }
}
