
using System.Collections.Generic;
namespace DairyCow.Model
{
    /// <summary>
    /// 配方
    /// </summary>
    public class Formula
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //public int FodderID { get; set; }
        //public string FodderName { get; set; }
        //public double FodderQuantity { get; set; }
        public List<Fodder> FodderList { get; set; }
    }
}
