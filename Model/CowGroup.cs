using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.Model
{
    /// <summary>
    /// 牛群分配，将牛群分配给指定的配种员
    /// </summary>
    public class CowGroup
    {
        //牛群分配ID
        public int ID { get; set; }
        //牛群名称
        public string Name { get; set; }
        //牧场ID
        public int PastureID { get; set; }
        //牧场名称
        public string PastureName { get; set; }
        // 状态ID
        public int TypeNum { get; set; }
        //配方ID
        public int FormulaID { get; set; }
        //配方名称
        public string FormulaName { get; set; }
        //配种员ID
        public int InsemOperatorID { get; set; }
        //配种员姓名
        public string InsemOperatorName { get; set; }
        //牛群描述
        public string Description { get; set; }
        /// <summary>
        /// 兽医ID
        /// </summary>
        public int DoctorID { get; set; }
        /// <summary>
        /// 兽医姓名
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 饲养员ID
        /// </summary>
        public int FeederID { get; set; }
        /// <summary>
        /// 饲养员姓名
        /// </summary>
        public string FeederName { get; set; }
        /// <summary>
        /// 牛群类型
        /// </summary>
        public CowGroupType GroupType
        {
            get
            {
                return GetCowGroupType(this.TypeNum);
            }
        }

        public string CowGroupTypeString
        {
            get
            {
                return GetCowGroupTypeStr(this.GroupType);
            }
        }
        /// <summary>
        /// 牛群中牛数
        /// </summary>
        public int CowNumber { get; set; }
        public CowGroupType GetCowGroupType(int number)
        {
            CowGroupType t;
            switch (number)
            {
                case 0:
                    t = CowGroupType.CalfCows;
                    break;
                case 1:
                    t = CowGroupType.BredCattleCows;
                    break;
                case 2:
                    t = CowGroupType.NullParityCows;
                    break;
                case 3:
                    t = CowGroupType.JustBornCows;
                    break;
                case 4:
                    t = CowGroupType.LowMilkCows;
                    break;
                case 5:
                    t = CowGroupType.MediumMilkCows;
                    break;
                case 6:
                    t = CowGroupType.HighMilkCows;
                    break;
                case 7:
                    t = CowGroupType.DryMilkCows;
                    break;
                case 8:
                    t = CowGroupType.DeliveryRoomCows;
                    break;
                case 9:
                    t = CowGroupType.IsolatedCows;
                    break;
                case 10:
                    t = CowGroupType.SickCows;
                    break;
                case 11:
                    t = CowGroupType.Bulls;
                    break;
                default:
                    t = CowGroupType.IsolatedCows;
                    break;
                    
            }
            return t;
        }
        public int GetCowGroupTypeNum(CowGroupType t)
        {
            int temp;
            switch (t)
            {
                case CowGroupType.CalfCows:
                    temp = 0;
                    break;
                case CowGroupType.BredCattleCows:
                    temp = 1;
                    break;
                case CowGroupType.NullParityCows:
                    temp = 2;
                    break;
                case CowGroupType.JustBornCows:
                    temp = 3;
                    break;
                case CowGroupType.LowMilkCows:
                    temp = 4;
                    break;
                case CowGroupType.MediumMilkCows:
                    temp = 5;
                    break;
                case CowGroupType.HighMilkCows:
                    temp = 6;
                    break;
                case CowGroupType.DryMilkCows:
                    temp = 7;
                    break;
                case CowGroupType.DeliveryRoomCows:
                    temp = 8;
                    break;
                case CowGroupType.IsolatedCows:
                    temp = 9;
                    break;
                case CowGroupType.SickCows:
                    temp = 10;
                    break;
                case CowGroupType.Bulls:
                    temp = 11;
                    break;
                default:
                    temp = 9;
                    break;
            }
            return temp;
        }

         
    

    public string GetCowGroupTypeStr(CowGroupType t)
        {
            string temp;
            switch (t)
            {
                case CowGroupType.CalfCows:
                    temp = "犊牛群";
                    break;
                case CowGroupType.BredCattleCows:
                    temp = "育成牛群";
                    break;
                case CowGroupType.NullParityCows:
                    temp = "青年牛群";
                    break;
                case CowGroupType.JustBornCows:
                    temp = "初产牛群";
                    break;
                case CowGroupType.LowMilkCows:
                    temp = "低产牛群";
                    break;
                case CowGroupType.MediumMilkCows:
                    temp = "中产牛群";
                    break;
                case CowGroupType.HighMilkCows:
                    temp = "高产牛群";
                    break;
                case CowGroupType.DryMilkCows:
                    temp = "干奶牛群";
                    break;
                case CowGroupType.DeliveryRoomCows:
                    temp = "产房待产牛群";
                    break;
                case CowGroupType.IsolatedCows:
                    temp = "隔离群";
                    break;
                case CowGroupType.SickCows:
                    temp = "病牛群";
                    break;
                case CowGroupType.Bulls:
                    temp = "公牛群";
                    break;
                default:
                    temp = "隔离群";
                    break;
            }
            return temp;
        }

         
    }

    /// <summary>
    /// 牛群类型
    /// </summary>
    public enum CowGroupType
    {        
        /// <summary>
        /// 犊牛群
        /// </summary>
        CalfCows=0,
        /// <summary>
        /// 育成牛群
        /// </summary>
        BredCattleCows=1,
        /// <summary>
        /// 青年牛群
        /// </summary>
        NullParityCows=2,
        /// <summary>
        /// 初产牛群
        /// </summary>
        JustBornCows=3,
        /// <summary>
        /// 低产牛群
        /// </summary>
        LowMilkCows=4,
        /// <summary>
        /// 中产牛群
        /// </summary>
        MediumMilkCows=5,
        /// <summary>
        /// 高产牛群
        /// </summary>
        HighMilkCows=6,
        /// <summary>
        /// 干奶牛群
        /// </summary>
        DryMilkCows=7,
        /// <summary>
        /// 产房待产牛群
        /// </summary>
        DeliveryRoomCows=8,
        /// <summary>
        /// 隔离群
        /// </summary>
        IsolatedCows=9,
        /// <summary>
        /// 病牛群
        /// </summary>
        SickCows=10,
        /// <summary>
        /// 公牛群
        /// </summary>
        Bulls=11

    }
}
