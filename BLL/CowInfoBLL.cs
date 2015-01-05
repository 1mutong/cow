using DairyCow.DAL;
using DairyCow.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace DairyCow.BLL
{
    /// <summary>
    /// 取得牛的衍生信息，如泌乳天数，胎间距等
    /// </summary>
    public class CowInfo
    {
        private int earNum;
        private Cow myCow = new Cow();
        //private string cowType;
        private static CowDAL dalCow = new CowDAL();
        private static CalvingDAL calvingDAL = new CalvingDAL();
        private static InseminationDAL insemDAL = new InseminationDAL();
        private static DryMilkDAL dryMilkDAL = new DryMilkDAL();


        private List<Calving> calvingList = new List<Calving>();
        private List<DryMilk> dryMilkList = new List<DryMilk>();

        /// <summary>
        /// 最新产犊记录，未产犊的牛返回null
        /// </summary>
        public Calving LatestCalving
        {
            get
            {
                if (this.Parity > 0)
                {
                    return calvingList[0];
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 最新干奶记录，无返回null
        /// </summary>
        public DryMilk LatestDryMilk
        {
            get
            {
                if (dryMilkList.Count > 0)
                {
                    return dryMilkList[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public CowInfo(int earNum)
        {
            CowBLL cb = new CowBLL();
            this.earNum = earNum;
            myCow = cb.GetCowInfo(earNum);
            this.calvingList = CowInfo.GetCowCalvingRecords(earNum);
            this.dryMilkList = CowInfo.GetDryMilkRecords(earNum);
            this.parity = this.calvingList.Count;

        }


        public CowInfo(Cow cow)
        {
            myCow = cow;
            this.earNum = cow.EarNum;
            this.calvingList = CowInfo.GetCowCalvingRecords(this.earNum);
            this.dryMilkList = CowInfo.GetDryMilkRecords(this.earNum);
            this.parity = this.calvingList.Count;
        }

        public bool IsIll
        {
            get
            {
                return myCow.IsIll;
            }
        }


        public int EarNum
        {
            get
            {
                return myCow.EarNum;
            }
            //set;
        }
        public string DisplayEarNum
        {
            get
            {
                return myCow.DisplayEarNum;
            }
            //set; 
        }
        public int GroupID
        {
            get
            {
                return myCow.GroupID;
            }
            //set; 
        }

        public string GroupName
        {
            get
            {
                return myCow.GroupName;
            }
            //set; 
        }
        public string IllStatus
        {
            get
            {
                return myCow.IllStatus;
            }
        }

        public string GroupTypeStr
        {
            get
            {
                CowGroupBLL b = new CowGroupBLL();
                var g = b.GetCowGroupInfo(GroupID);
                return g.GetCowGroupTypeStr(g.GroupType);
            }
        }

        public string Status
        {
            get
            {
                return myCow.Status;
            }
            //set; 
        }
        public string Gender
        {
            get
            {
                return myCow.Gender;
            }
            //set;
        }
        public int FarmCode
        {
            get
            {
                return myCow.FarmCode;
            }
            //set;
        }
        public DateTime BirthDate
        {
            get
            {
                return myCow.BirthDate;
            }
            //set; 
        }
        public string BirthDateStr
        {
            get
            {
                return BirthDate.ToShortDateString();
            }
        }
        public double AgeMonth
        {
            get
            {
                return myCow.AgeMonth;
            }
        }
        public float BirthWeight
        {
            get
            {
                return myCow.BirthWeight;
            }
            //set; 
        }

        public string FatherNum
        {
            get
            {
                return myCow.FatherID;
            }
        }
        public string MotherNum
        {
            get
            {
                return myCow.MotherID;
            }
        }
        public string Color
        {
            get
            {
                return myCow.Color;
            }
            //set; 
        }

        /// <summary>
        /// 牛日龄
        /// </summary>
        public int AgeDay
        {
            get
            {
                return myCow.AgeDay;
            }
        }

        public bool ExceedMaxUninseminatedDays
        {
            get
            {
                Dictionary<string, float> myDictionary = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID);
                if (Parity > 0 || GetTimesOfInsemination(this.earNum) == 0 || (myDictionary.ContainsKey("MaxUninseminatedDays") && (DateTime.Now.Subtract(this.GetLatestCalvingDate()).TotalDays) > myDictionary["MaxUninseminatedDays"]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ExceedMaxUnpregnantDays
        {
            get
            {
                Dictionary<string, float> myDictionary = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID);
                if (Parity > 0 || (this.Status != "初检+" && this.Status != "复检+") || (myDictionary.ContainsKey("MaxUnpregnantDays") && (DateTime.Now.Subtract(this.GetLatestCalvingDate()).TotalDays) > myDictionary["MaxUnpregnantDays"]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ExceedMaxUninseminatedAgeMonth
        {
            get
            {
                Dictionary<string, float> myDictionary = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID);
                if (this.CowType == "青年牛" || GetTimesOfInsemination() == 0 ||( myDictionary.ContainsKey("MaxUninseminatedAgeMonth") && myCow.AgeMonth > myDictionary["MaxUninseminatedAgeMonth"]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ExceedMaxUnpregnantAgeMonth
        {
            get
            {
                Dictionary<string, float> myDictionary = DairyParameterBLL.GetCurrentParameterDictionary(UserBLL.Instance.CurrentUser.Pasture.ID);
                if (this.CowType == "青年牛" || (this.Status != "初检+" && this.Status != "复检+") || myDictionary.ContainsKey("MaxUnpregnantAgeMonth") && myCow.AgeMonth > myDictionary["MaxUnpregnantAgeMonth"])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string MilkType
        {
            get
            {
                return this.GetCowMilkType();
            }

        }

        /// <summary>
        /// 泌乳天数
        /// </summary>
        public int DaysInMilk
        {
            get
            {
                return GetCowDaysInMilk(this.earNum);
            }
        }
        /// <summary>
        /// 干奶天数
        /// </summary>
        public int DaysInDry
        {
            get
            {
                return GetCowDaysInDry(this.earNum);
            }
        }
        /// <summary>
        /// 最近产犊日期，非经产牛返回出生日期
        /// </summary>
        public DateTime GetLatestCalvingDate()
        {

            DateTime t = DateTime.MinValue;
            Calving cal = GetLatestCalving(this.earNum);
            if (cal != null)
            {
                t = cal.Birthday;
            }
            else
            {
                t = this.BirthDate;
            }
            return t;
        }

        public Insemination FirstInseminationOfCurrentBreedPeriod
        {
            get
            {
                return CowInfo.GetFirstInseminationOfCurrentBreedPeriod(this.EarNum);
            }
        }

        public Insemination LatestInsemination
        {
            get
            {
                return CowInfo.GetLatestInsemination(this.EarNum);
            }
        }

        public Insemination LatestInseminationOfCurrentBreedPeriod
        {
            get
            {
                return CowInfo.GetlatestInseminationOfCurrentBreedPeriod(this.EarNum);
            }
        }

        /// <summary>
        /// 产后第一次配种天数
        /// </summary>
        /// <returns></returns>
        public double GetDaysToFirstInsemination()
        {
            double d = 0.0;
            if (this.Parity > 0 && this.FirstInseminationOfCurrentBreedPeriod != null)
            {
                d = this.FirstInseminationOfCurrentBreedPeriod.OperateDate.Subtract(GetLatestCalvingDate()).TotalDays;
            }
            return d;
        }

        /// <summary>
        /// 胎间距（平均）,单位天,小于2胎的返回0。
        /// </summary>
        public double AverageParityInterval
        {
            get
            {
                return GetAverageParityInterval(this.earNum);
            }
        }
        /// <summary>
        /// 获取单牛平均胎间距,单位天
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <returns></returns>
        public static double GetAverageParityInterval(int earNum)
        {
            double interval = 0.0, total = 0.0;
            List<Calving> calvingList = GetCowCalvingRecords(earNum);
            //calvingList.Sort()
            int count = calvingList.Count;
            if (count > 1)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    total = total + Math.Abs(calvingList[i].Birthday.Subtract(calvingList[i + 1].Birthday).TotalDays);
                }
                interval = Math.Round(total / (count - 1),2);
            }
            return interval;
        }

        /// <summary>
        /// 怀孕天数
        /// </summary>
        public int DaysOfPregnant
        {
            get
            {
                //依赖于牛状态不错，否则还需要看产犊表
                if (myCow.Status == "初检+" | myCow.Status == "复检+")
                {
                    TimeSpan t = DateTime.Now.Subtract(GetLatestInsemination(this.earNum).OperateDate);
                    return Convert.ToInt32(t.TotalDays);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 取最后配种日期字符串
        /// </summary>
        public string LatestInseminationDateString
        {
            get
            {
                return GetLatestInsemination(this.earNum).OperateDate.Date.ToLongDateString();
            }
        }
        /// <summary>
        /// 配次
        /// </summary>
        public int TimesOfInsemination
        {
            get
            {
                //配次一定指本轮繁殖周期，即本次产犊后
                return GetTimesOfInsemination();
            }
        }
        /// <summary>
        /// 配次
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <returns></returns>
        public static int GetTimesOfInsemination(int earNum)
        {

            return GetInseminationListOfCurrentBreedPeriod(earNum).Count;

        }
        public int GetTimesOfInsemination()
        {

            return GetInseminationListOfCurrentBreedPeriod(this.earNum).Count;

        }

        public List<Insemination> GetInseminationListOfCurrentBreedPeriod()
        {
            InseminationBLL bllInsem = new InseminationBLL();
            DateTime latestCalTime = new DateTime(1900, 1, 1);
            Calving cal = GetLatestCalving(this.earNum);
            if (cal != null)
            {
                latestCalTime = cal.Birthday;
            }
            else
            {
                latestCalTime = this.BirthDate;
            }
            List<Insemination> list = bllInsem.GetInseminationList(this.earNum, latestCalTime);
            return list;
        }

        /// <summary>
        /// 取本轮配种记录列表
        /// </summary>
        /// <param name="earNum"></param>
        /// <returns></returns>
        public static List<Insemination> GetInseminationListOfCurrentBreedPeriod(int earNum)
        {
            InseminationBLL bllInsem = new InseminationBLL();
            DateTime latestCalTime = new DateTime(1900, 1, 1);
            Calving cal = GetLatestCalving(earNum);
            if (cal != null)
            {
                latestCalTime = cal.Birthday;
            }
            else
            {
                CowBLL cb = new CowBLL();
                Cow myCow = cb.GetCowInfo(earNum);
                latestCalTime = myCow.BirthDate;
            }
            List<Insemination> list = bllInsem.GetInseminationList(earNum, latestCalTime);
            return list;
        }

        public Insemination GetFirstInseminationOfCurrentBreedPeriod()
        {
            Insemination insem;
            // 取本轮配种记录列表
            List<Insemination> list = GetInseminationListOfCurrentBreedPeriod();
            if (list.Count > 0)
            {
                insem = list[0];
                for (int i = 1; i < list.Count; i++)
                {
                    if (insem.OperateDate.CompareTo(list[i].OperateDate) > 0)
                    {
                        insem = list[i];
                    }
                }
            }
            else
            {
                //本轮无配种记录
                insem = null;
            }
            return insem;

        }

        /// <summary>
        /// 获取本轮首次配种记录，未配返回null
        /// </summary>
        /// <param name="earNum"></param>
        /// <returns></returns>
        public static Insemination GetFirstInseminationOfCurrentBreedPeriod(int earNum)
        {
            Insemination insem;
            // 取本轮配种记录列表
            List<Insemination> list = GetInseminationListOfCurrentBreedPeriod(earNum);
            if (list.Count > 0)
            {
                insem = list[0];
                for (int i = 1; i < list.Count; i++)
                {
                    if (insem.OperateDate.CompareTo(list[i].OperateDate) > 0)
                    {
                        insem = list[i];
                    }
                }
            }
            else
            {
                //本轮无配种记录
                insem = null;
            }
            return insem;

        }

        public Insemination GetlatestInseminationOfCurrentBreedPeriod()
        {
            Insemination insem;
            // 取本轮配种记录列表
            List<Insemination> list = GetInseminationListOfCurrentBreedPeriod();
            if (list.Count > 0)
            {
                insem = list[0];
                for (int i = 1; i < list.Count; i++)
                {
                    if (insem.OperateDate.CompareTo(list[i].OperateDate) < 0)
                    {
                        insem = list[i];
                    }
                }
            }
            else
            {
                //本轮无配种记录
                insem = null;
            }
            return insem;

        }

        public static Insemination GetlatestInseminationOfCurrentBreedPeriod(int earNum)
        {
            Insemination insem;
            // 取本轮配种记录列表
            List<Insemination> list = GetInseminationListOfCurrentBreedPeriod(earNum);
            if (list.Count > 0)
            {
                insem = list[0];
                for (int i = 1; i < list.Count; i++)
                {
                    if (insem.OperateDate.CompareTo(list[i].OperateDate) < 0)
                    {
                        insem = list[i];
                    }
                }
            }
            else
            {
                //本轮无配种记录
                insem = null;
            }
            return insem;

        }
                
        /// <summary>
        /// 牛类型，公牛，犊牛，育成牛，青年牛，经产牛
        /// </summary>
        public string CowType
        {
            get
            {
                return GetCowType(this);
            }

        }

        /// <summary>
        /// 获取牛类型
        /// </summary>
        /// <param name="myCow"></param>
        /// <returns></returns>
        public static string GetCowType(CowInfo myCow)
        {
            string cowType;
            //set cow type            
            if (myCow.Gender == "公")
            {
                cowType = "公牛";
            }
            else
            {
                TimeSpan span = DateTime.Now.Subtract(myCow.BirthDate);
                //to-do：把天数换成参数配置中的数；
                if (span.TotalDays < Cow.DAYS_OF_CALF)
                {
                    cowType = "犊牛";
                }
                else
                {
                    if (span.TotalDays < (Cow.DAYS_OF_COW_MONTH * Cow.MONTHS_OF_NULLPARITY))
                    {
                        cowType = "育成牛";
                    }
                    else
                    {
                        if (myCow.Parity > 0)
                        {
                            cowType = "经产牛";
                        }
                        else
                        {
                            cowType = "青年牛";
                        }

                    }
                }
            }
            return cowType;
        }

        private int parity;
        /// <summary>
        /// 胎次
        /// </summary>
        public int Parity
        {
            get
            {
                return parity;
            }
        }

        /// <summary>
        /// 获取单个牛（经产牛）的泌乳天数
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns>泌乳天数，非泌乳牛，返回0；泌乳牛，产犊到当天；干奶牛，到停奶日。</returns>
        public static int GetCowDaysInMilk(int earNum)
        {
            int dayInMilk = 0;
            //非泌乳牛，返回0；
            //泌乳牛，产犊到当天；干奶牛，到停奶日；
            Calving cal = GetLatestCalving(earNum);
            DryMilk dry = GetLatestDryMilk(earNum);
            if (cal == null)
            {
                //未产过犊
                dayInMilk = 0;
            }
            else
            {
                TimeSpan span = new TimeSpan();
                if (dry == null)
                {
                    //没有干奶，从产犊到现在
                    span = DateTime.Now.Date.Subtract(cal.Birthday.Date);
                    dayInMilk = Convert.ToInt32(span.TotalDays);
                }
                else
                {
                    if (cal.Birthday.CompareTo(dry.DryDate) < 0)
                    {
                        //干奶在产犊后
                        span = dry.DryDate.Date.Subtract(cal.Birthday.Date);
                        dayInMilk = Convert.ToInt32(span.TotalDays);
                    }
                    else
                    {
                        //干奶在产犊前，是上一轮干奶
                        span = DateTime.Now.Date.Subtract(dry.DryDate.Date);
                        dayInMilk = Convert.ToInt32(span.TotalDays);
                    }
                }
            }
            return dayInMilk;
        }

        /// <summary>
        /// 获取单个牛（经产牛）的干奶天数
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns>泌乳天数，非经产牛，返回0；泌乳牛，0；干奶牛，干奶到当天。</returns>
        public static int GetCowDaysInDry(int earNum)
        {
            int dayInDry = 0;
            //非泌乳牛，返回0；
            //泌乳牛，产犊到当天；干奶牛，到停奶日；
            Calving cal = GetLatestCalving(earNum);
            DryMilk dry = GetLatestDryMilk(earNum);
            if (cal == null)
            {
                //未产过犊
                dayInDry = 0;
            }
            else
            {
                TimeSpan span = new TimeSpan();
                if (dry == null)
                {
                    //没有干奶，从产犊到现在
                    dayInDry = 0;
                }
                else
                {
                    if (cal.Birthday.CompareTo(dry.DryDate) < 0)
                    {
                        //干奶在产犊后
                        span = DateTime.Now.Subtract(dry.DryDate);
                        dayInDry = Convert.ToInt32(span.TotalDays);
                    }
                    else
                    {
                        //干奶在产犊前，是上一轮干奶
                        dayInDry = 0;
                    }
                }
            }
            return dayInDry;
        }
        public static string GetCowMilkType(int earNum)
        {
            string milkType;
            Calving cal = GetLatestCalving(earNum);
            DryMilk dry = GetLatestDryMilk(earNum);
            if (cal == null)
            {
                //未产过犊
                milkType = "非经产牛";
            }
            else
            {
                if (dry == null)
                {
                    milkType = "泌乳牛";
                }
                else
                {
                    if (cal.Birthday.CompareTo(dry.DryDate) < 0)
                    {
                        //干奶在产犊后
                        milkType = "干奶牛";
                    }
                    else
                    {
                        //干奶在产犊前，是上一轮干奶
                        milkType = "泌乳牛";
                    }
                }
            }
            return milkType;
        }

        public string GetCowMilkType()
        {
            string milkType;
            Calving cal;
            DryMilk dry;
            if (this.Parity == 0)
            {
                //未产过犊
                milkType = "非经产牛";
                //@2014.11.25,易牧通要求，青年牛，也干奶。
                if (this.dryMilkList.Count > 0)
                {
                    milkType = "干奶牛";
                }
            }
            else
            {
                cal = this.calvingList[0];
                if (this.dryMilkList.Count == 0)
                {
                    milkType = "泌乳牛";
                }
                else
                {
                    dry = this.dryMilkList[0];
                    if (cal.Birthday.CompareTo(dry.DryDate) < 0)
                    {
                        //干奶在产犊后
                        milkType = "干奶牛";
                    }
                    else
                    {
                        //干奶在产犊前，是上一轮干奶
                        milkType = "泌乳牛";
                    }
                }
            }
            return milkType;
        }

        /// <summary>
        /// 获取最新配种记录
        /// </summary>
        /// <param name="earNum"></param>
        /// <returns></returns>
        public static Insemination GetLatestInsemination(int earNum)
        {

            InseminationBLL insem1 = new InseminationBLL();
            return insem1.GetLatestInsemination(earNum);

        }

        /// <summary>
        /// 获取产犊记录表(包括计胎次的早产，流产)
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns></returns>
        public static List<Calving> GetCowCalvingRecords(int earNum)
        {
            List<Calving> calvingRecords = new List<DairyCow.Model.Calving>();
            DataTable calvingTable = calvingDAL.GetCowCalvingRecords(earNum, true);
            //按顺序插入
            int count = calvingTable.Rows.Count;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    calvingRecords.Add(WrapCalvingItem(calvingTable.Rows[i]));
                }
            }

            return calvingRecords;
        }
        /// <summary>
        /// 获取产犊记录表
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <param name="inParityCountOnly">只取算入胎次的记录，true=是，false=所有记录</param>
        /// <returns></returns>
        public static List<Calving> GetCowCalvingRecords(int earNum, bool inParityCountOnly)
        {
            List<Calving> calvingRecords = new List<DairyCow.Model.Calving>();
            DataTable calvingTable = calvingDAL.GetCowCalvingRecords(earNum, inParityCountOnly);
            foreach (DataRow item in calvingTable.Rows)
            {
                calvingRecords.Add(WrapCalvingItem(item));
            }
            return calvingRecords;
        }

        /// <summary>
        /// 插入产犊记录
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int InsertCalvingRecord(Calving c)
        {
            int birthType = 0, inParityCount = 0;
            switch (c.BirthType)
            {
                case BirthType.Normal:
                    birthType = 0;
                    break;
                case BirthType.Miscarry:
                    birthType = 2;
                    break;
                case BirthType.PrematureBirth:
                    birthType = 1;
                    break;
                default:
                    break;
            }
            if (c.InParityCount)
            {
                inParityCount = 1;
            }

            return calvingDAL.InsertCalvingRecord(c.EarNum, c.Birthday, birthType, c.Difficulty, c.PositionOfFetus, c.FatherSememNum, c.OperatorID, c.Comment, c.NumberOfMale, c.NumberOfFemale, inParityCount);
        }

        /// <summary>
        /// 处理产犊记录行
        /// </summary>
        /// <param name="calvingRow">数据行</param>
        /// <returns>产犊记录</returns>
        public static Calving WrapCalvingItem(DataRow calvingRow)
        {
            Calving myCalving = new Calving();
            if (calvingRow != null)
            {
                myCalving.EarNum = Convert.ToInt32(calvingRow["EarNum"]);
                //cal.ChildEarNum = Convert.ToInt32(calvingRow["ChildEarNum"]);
                myCalving.Birthday = Convert.ToDateTime(calvingRow["Birthday"]);
                myCalving.FatherSememNum = (calvingRow["FatherSemenNum"]).ToString();
                myCalving.OperatorID = Convert.ToInt32(calvingRow["OperatorID"]);
                myCalving.OperatorName = calvingRow["OperatorName"].ToString();
                int bType = Convert.ToInt32(calvingRow["BirthType"]);
                switch (bType)
                {
                    case 0:
                        myCalving.BirthType = BirthType.Normal;
                        break;
                    case 1:
                        myCalving.BirthType = BirthType.PrematureBirth;
                        break;
                    case 2:
                        myCalving.BirthType = BirthType.Miscarry;
                        break;
                    default:
                        break;
                }
                myCalving.NumberOfMale = calvingRow["NumberOfMale"] == DBNull.Value ? 0 : Convert.ToInt32(calvingRow["NumberOfMale"]);
                myCalving.NumberOfFemale = calvingRow["NumberOfFemale"] == DBNull.Value ? 0 : Convert.ToInt32(calvingRow["NumberOfFemale"]);
                if (calvingRow["Difficulty"] != DBNull.Value)
                {
                    myCalving.Difficulty = calvingRow["Difficulty"].ToString();
                }
                else
                {
                    myCalving.Difficulty = String.Empty;
                }
                if (calvingRow["PositionOfFetus"] != DBNull.Value)
                {
                    myCalving.PositionOfFetus = calvingRow["PositionOfFetus"].ToString();
                }
                else
                {
                    myCalving.PositionOfFetus = String.Empty;
                }

                int inCount = Convert.ToInt32(calvingRow["InParityCount"]);
                switch (inCount)
                {
                    case 0:
                        myCalving.InParityCount = false;
                        break;
                    case 1:
                        myCalving.InParityCount = true;
                        break;
                    default:
                        break;
                }
                if (calvingRow["Comment"] != DBNull.Value)
                {
                    myCalving.Comment = calvingRow["Comment"].ToString();
                }
                else
                {
                    myCalving.Comment = String.Empty;
                }
            }
            return myCalving;
        }

        /// <summary>
        /// 处理干奶记录行
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>干奶记录</returns>
        public static DryMilk WrapDryMilkItem(DataRow row)
        {
            DryMilk dry = new DryMilk();
            if (row != null)
            {
                dry.ID = Convert.ToInt32(row["ID"]);
                dry.EarNum = Convert.ToInt32(row["EarNum"]);
                dry.DryDate = Convert.ToDateTime(row["DryDate"]);
                dry.DrySituation = Convert.ToInt32(row["DrySituation"]);
                dry.DryReason = row["DryReason"].ToString();
                dry.OperatorID = Convert.ToInt32(row["OperatorID"]);
            }
            return dry;
        }

        /// <summary>
        /// 获取牛干奶记录
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns>干奶记录</returns>
        public static List<DryMilk> GetDryMilkRecords(int earNum)
        {
            List<DryMilk> list = new List<DryMilk>();
            DataTable table = dryMilkDAL.GetCowDryRecords(earNum);
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapDryMilkItem(item));
            }
            return list;
        }

        public static int InsertDryMilk(DryMilk dry)
        {
            return dryMilkDAL.InsertDryMilkRecord(dry.EarNum, dry.DryDate, dry.DrySituation, dry.DryReason, dry.OperatorID);
        }

        /// <summary>
        /// 获取最后干奶记录
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns>最后干奶记录，没有返回null</returns>
        public static DryMilk GetLatestDryMilk(int earNum)
        {
            DryMilk dry = new DryMilk();
            DataTable table = dryMilkDAL.GetLatestCowDryRecord(earNum);
            if (table.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                dry = WrapDryMilkItem(table.Rows[0]);
                return dry;
            }

        }

        /// <summary>
        /// 获取最新产犊记录,没有返回null
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns>产犊记录，未产过犊则返回null</returns>
        public static Calving GetLatestCalving(int earNum)
        {
            Calving cal = new Calving();
            List<Calving> calvingList = GetCowCalvingRecords(earNum);
            if (calvingList.Count == 0)
            {
                //未产过犊
                return null;
            }
            else
            {
                //该list已经是按产犊时间倒序。
                cal = calvingList[0];
                return cal;
            }

        }

        /// <summary>
        /// 获取产犊头胎记录
        /// </summary>
        /// <param name="earNum">牛耳号</param>
        /// <returns>产犊记录，未产过犊则返回null</returns>
        public static Calving GetFirstCalving(int earNum)
        {
            Calving cal = new Calving();
            List<Calving> calvingList = GetCowCalvingRecords(earNum);
            if (calvingList.Count == 0)
            {
                //未产过犊
                return null;
            }
            else
            {
                cal = calvingList[calvingList.Count - 1];
                return cal;
            }

        }
        /// <summary>
        /// 获取初产月龄,未产犊返回0.
        /// </summary>
        public double AgeMonthOfFirstBorn
        {
            get
            {
                double ageM = 0.0;
                Calving cal = GetFirstCalving(this.earNum);
                if (cal != null)
                {
                    //有产犊，
                    ageM = cal.Birthday.Subtract(this.BirthDate).TotalDays / 30.5;
                }
                return ageM;
            }
        }

        private bool needGrouping = false;
        /// <summary>
        /// 是否需要调群，（运行完ChecGrouping）
        /// </summary>
        public bool NeedGrouping
        {
            get { return needGrouping; }
        }

        private string regroupingReason;
        /// <summary>
        /// 调群原因，（运行完ChecGrouping）
        /// </summary>
        public string RegroupingReason
        {
            get { return regroupingReason; }
        }

        public void CheckGrouping(List<CowGroup> groups)
        {
            CowGroup g = groups.FirstOrDefault(p => p.ID == this.GroupID);
            if (g == null)
            {
                return;
            }
            switch (g.GroupType)
            {
                case CowGroupType.CalfCows:
                    if (!this.CowType.Equals("犊牛"))
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛不是犊牛，现在犊牛群。";
                    }
                    break;
                case CowGroupType.BredCattleCows:
                    if (!this.CowType.Equals("育成牛"))
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛不是育成牛，现在育成牛群。";
                    }
                    break;
                case CowGroupType.NullParityCows:
                    if (!this.CowType.Equals("青年牛"))
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛不是青年牛牛，现在青年牛牛群。";
                    }
                    break;
                case CowGroupType.JustBornCows:
                    if ((!this.CowType.Equals("经产牛")) || (!this.MilkType.Equals("泌乳牛")))
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛不是泌乳牛，现在初产牛群。";
                    }
                    break;
                case CowGroupType.LowMilkCows:
                    if ((!this.CowType.Equals("经产牛")) || (!this.MilkType.Equals("泌乳牛")))
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛不是泌乳牛，现在低产牛群。";
                    }
                    break;
                case CowGroupType.MediumMilkCows:
                    if ((!this.CowType.Equals("经产牛")) || (!this.MilkType.Equals("泌乳牛")))
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛不是泌乳牛，现在中产牛群。";
                    }
                    break;
                case CowGroupType.HighMilkCows:
                    if ((!this.CowType.Equals("经产牛")) || (!this.MilkType.Equals("泌乳牛")))
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛不是泌乳牛，现在高产牛群。";
                    }
                    break;
                case CowGroupType.DryMilkCows:
                    if ((!this.CowType.Equals("经产牛")) || (!this.MilkType.Equals("干奶牛")))
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛不是干奶牛，现在干奶牛群。";
                    }
                    break;
                case CowGroupType.DeliveryRoomCows:
                    //无检查
                    break;
                case CowGroupType.IsolatedCows:
                    //无检查
                    break;
                case CowGroupType.SickCows:
                    if (!this.IsIll)
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛为正常牛，现在病牛群。";
                    }
                    break;
                default:
                    if (this.IsIll && g.GroupType != CowGroupType.SickCows)
                    {
                        this.needGrouping = true;
                        this.regroupingReason = "此牛为病牛，现在正常牛群。";
                    }
                    break;
            }
        }
    }
}
