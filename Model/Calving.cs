using System;

namespace DairyCow.Model
{
     /// <summary>
     /// 牛产犊记录
     /// </summary>
    public class Calving
    {
        public DateTime Birthday { get; set; }
        public int EarNum { get; set; }  //母牛耳号
        public string FatherSememNum { get; set; }
        public int OperatorID { get; set; }
        public string OperatorName { get; set; }
        public BirthType BirthType { get; set; }
        public string Difficulty { get; set; }
        public string PositionOfFetus { get; set; }
        public string Comment { get; set; }
        public bool InParityCount { get; set; }
        public int NumberOfMale { get; set; }
        public int NumberOfFemale { get; set; }
    }

    public enum BirthType
    {
        Normal = 0, //正常
        PrematureBirth = 1, //早产
        Miscarry = 2 //流产
    }
}
