using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class IndividualProdcution
    {
        public int ID { get; set; }
        public int EarNum { get; set; }
        public DateTime MilkDate { get; set; }
        public float MilkWeight { get; set; }
        /// <summary>
        /// 班次
        /// </summary>
        public string Round { get; set; }
    }

    public class IndividualProdcutionTotal
    {

        public int EarNum { get; set; }
        public DateTime MilkDate { get; set; }
        public float TotalMilk { get; set; }

    }


    public class MilkCheckModel
    {
        public int ID { get; set; }
        public int EarNum { get; set; }
        public DateTime MilkDate { get; set; }
        public double Weight { get; set; }
        /// <summary>
        /// 班次
        /// </summary>
        public string Round { get; set; }
    }
}
