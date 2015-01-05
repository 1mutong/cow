using System;

namespace DairyCow.Model
{
    public class MilkSale
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 牧场ID
        /// </summary>
        public int PastureID { get; set; }
        /// <summary>
        /// 挤奶售奶日期
        /// </summary>
        public DateTime MilkDate { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public float Amount { get; set; }
        /// <summary>
        /// 售奶公斤数
        /// </summary>
        public float MilkWeight { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 奶罐号
        /// </summary>
        public string TankerNum { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string TruckNum { get; set; }
        /// <summary>
        /// 奶车编号
        /// </summary>
        public string ShipCode { get; set; }
        /// <summary>
        /// 理化解码
        /// </summary>
        public string Decoding { get; set; }
        /// <summary>
        /// 乳脂率
        /// </summary>
        public float Fat { get; set; }
        /// <summary>
        /// 乳蛋白
        /// </summary>
        public float Protein { get; set; }
        /// <summary>
        /// 干物质
        /// </summary>
        public float DryMatter { get; set; }
        /// <summary>
        /// 非脂固体率
        /// </summary>
        public float NonFatSolid { get; set; }
        /// <summary>
        /// 乳糖率
        /// </summary>
        public float Lactose { get; set; }
        /// <summary>
        /// 微生物
        /// </summary>
        public float Microbe { get; set; }
        /// <summary>
        /// 冰点
        /// </summary>
        public float IcePoint { get; set; }
        /// <summary>
        /// 酸度
        /// </summary>
        public float Acidity { get; set; }
    }

    public class OtherMilk
    {
        public int ID { get; set; }
        public int PastureID { get; set; }
        public DateTime MilkDate { get; set; }
        public float MilkForCalf { get; set; }
        public float AbnormalSaleMilk { get; set; }
        public float BadMilk { get; set; }
        public float LeftMilk { get; set; }
    }
}
