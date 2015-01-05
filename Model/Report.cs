using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    public class DailyReport 
    {
        public int PastureID { get; set; }
        public DateTime ReportDate { get; set; }
        public int MilkCowNumber { get; set; }
        public int DryCowNumber { get; set; }
        public int MultiParityNumber { get; set; }
        public int NullParityNumber { get; set; }
        public int CalfNumber { get; set; }
        public int BredCattleNumber { get; set; }

        public int CalvingNumber { get; set; }
        public int MaleCalfNumber { get; set; }
        public int FemaleCalfNumber { get; set; }



        public float SaleMilk { get; set; }
        public float Amount { get; set; }
        public float MilkForCalf { get; set; }
        public float BadMilk { get; set; }
        public float LeftMilk { get; set; }
        public float AbnormalSaleMilk { get; set; }
        
        public float TotalMilk 
        {
            get
            {
                return SaleMilk + MilkForCalf + BadMilk + LeftMilk + AbnormalSaleMilk;
            }
        }
        public float AverageMilkByMilkCow
        {
            get
            {
                return TotalMilk / MilkCowNumber;
            }

        }

        public float AverageMilkByMultiparityCow
        {
            get
            {
                return TotalMilk / MultiParityNumber;
            }
        }

        /// <summary>
        /// 出售牛数
        /// </summary>
        public int SoldCowNum { get; set; }
        /// <summary>
        /// 死亡牛数
        /// </summary>
        public int DeadCowNum { get; set; }
        /// <summary>
        /// 淘汰牛数
        /// </summary>
        public int ElimintedCowNum { get; set; }
    }

    public class MonthlyReport
    {
        public int PastureID { get; set; }
        public int ReportYear { get; set; }
        public int ReportMonth { get; set; }
        public int MilkCowNumber { get; set; }
        public int DryCowNumber { get; set; }
        public int MultiParityNumber { get; set; }
        public int NullParityNumber { get; set; }
        public int CalfNumber { get; set; }
        public int BredCattleNumber { get; set; }

        public int CalvingNumber { get; set; }
        public int MaleCalfNumber { get; set; }
        public int FemaleCalfNumber { get; set; }



        public float SaleMilk { get; set; }
        public float Amount { get; set; }
        public float MilkForCalf { get; set; }
        public float BadMilk { get; set; }
        public float LeftMilk { get; set; }
        public float AbnormalSaleMilk { get; set; }

        public float TotalMilk
        {
            get
            {
                return SaleMilk + MilkForCalf + BadMilk + LeftMilk + AbnormalSaleMilk;
            }
        }
        public float AverageMilkByMilkCow
        {
            get
            {
                return TotalMilk / MilkCowNumber;
            }

        }

        public float AverageMilkByMultiparityCow
        {
            get
            {
                return TotalMilk / MultiParityNumber;
            }
        }
        /// <summary>
        /// 出售牛数
        /// </summary>
        public int SoldCowNum { get; set; }
        /// <summary>
        /// 死亡牛数
        /// </summary>
        public int DeadCowNum { get; set; }
        /// <summary>
        /// 淘汰牛数
        /// </summary>
        public int ElimintedCowNum { get; set; }
    }
}
