using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 饲料
    /// </summary>
    public class Fodder
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double RefPrice { get; set; }
        public double DM { get; set; }
        public double NND { get; set; }
        public double Ca { get; set; }
        public double P { get; set; }
        public double CP { get; set; }
        public double CF { get; set; }
        public double Fat { get; set; }
        public double NFE { get; set; }
        public double ASH { get; set; }
        public double NDF { get; set; }
        public double ADF { get; set; }
        public double NPP { get; set; }
        public double Arg { get; set; }
        public double His { get; set; }
        public double Ile { get; set; }
        public double Leu { get; set; }
        public double Lys { get; set; }
        public double Met { get; set; }
        public double Cys { get; set; }
        public double Phe { get; set; }
        public double Tyr { get; set; }
        public double Thr { get; set; }
        public double Trp { get; set; }
        public double Val { get; set; }
        public double Na { get; set; }
        public double Cl { get; set; }
        public double Mg { get; set; }
        public double K { get; set; }
        public double Fe { get; set; }
        public double Cu { get; set; }
        public double Mn { get; set; }
        public double Zn { get; set; }
        public double Se { get; set; }
       
    }
    public class PastureFodder
    {
        public int FodderID { get; set; }
        public int PastureID { get; set; }
        public int SysFodderID { get; set; }
        public string FodderName { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        /// <summary>
        /// 配方用量，来自配方中标准饲料量
        /// </summary>
        public double Usage { get; set; }
        public double Price { get; set; }
        public bool IsCurrent { get; set; }
        public string Status
        {
            get
            {
                return IsCurrent ? "在用" : "停用";
            }
        }
        public string SysFodderName { get; set; }

    }
    
}
