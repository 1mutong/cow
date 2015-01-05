using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;

namespace DairyCow.BLL
{
    public class FarmInfo
    {
        //parameter names
        public const string PN_DAYSOFDRY = "DaysOfDry";
        public const string PN_DAYSOFINITIALINSPECTION = "DaysOfInitialInspection";
        public const string PN_DAYSOFREINSEPECTION = "DaysOfReInspection";
        public const string PN_MAXUNINSEMINATEDAGEMONTH = "MaxUninseminatedAgeMonth";
        public const string PN_MAXUNINSEMINATEDAYS = "MaxUninseminatedDays";
        public const string PN_MAXUNPREGNANTAGEMONTH = "MaxUnpregnantAgeMonth";
        public const string PN_MAXUNPREGNANTDAYS = "MaxUnpregnantDays";
        public const string PN_MINAGEDAY = "MinAgeDay";
        public const string PN_MINBADUDDER = "MinBadUdder";
        public const string PN_MINNORMALCALVINGDAYS = "MinNormalCalvingDays";
        public const string PN_MINNULLPARITYAGEMONTH = "MinNullParityAgeMonth";
        public const string PN_MINNULLPARITYWEIGTH = "MinNullParityWeight";
        public const string PN_NORMALCALVINGDAYS = "NormalCalvingDays";

        private CowBLL cowBll = new CowBLL();

        private List<Cow> cowList = new List<Cow>();

        private List<CowInfo> cowInfoList = new List<CowInfo>();

        //private static FarmInfo _instance;

        //public static FarmInfo Instance
        //{
        //    get
        //    {
        //        if (_instance==null)
        //        {
        //            _instance = new FarmInfo();
        //        }
        //        return _instance;
        //    }
        //}

        /// <summary>
        /// 全群牛
        /// </summary>
        public List<CowInfo> CowInfoList
        {
            get { return cowInfoList; }
            
        }
        
        /// <summary>
        /// 所有经产牛
        /// </summary>
        public List<CowInfo> MultiParityCows
        {
            get 
            {
                List<CowInfo> multiParityCows = new List<CowInfo>();
                foreach (CowInfo item in CowInfoList)
                {
                    if (item.CowType=="经产牛")
                    {
                        
                        multiParityCows.Add(item);
                    }
                }
                return multiParityCows; 
            }
            
        }
        /// <summary>
        /// 所有青年牛
        /// </summary>
        public List<CowInfo> NullParityCows
        {
            get 
            {
                List<CowInfo> nullParityCows = new List<CowInfo>();
                foreach (CowInfo item in CowInfoList)
                {
                    if (item.CowType == "青年牛")
                    {
                        nullParityCows.Add(item);
                    }
                }

                return nullParityCows; 
            }
           
        }
        /// <summary>
        /// 所有育成牛
        /// </summary>
        public List<CowInfo> BredCattleCows
        {
            get 
            {
                List<CowInfo> bredCattleCows = new List<CowInfo>();
                foreach (CowInfo item in CowInfoList)
                {
                    if (item.CowType == "育成牛")
                    {
                        bredCattleCows.Add(item);
                    }
                }
                return bredCattleCows; 
            }
            
        }
        /// <summary>
        /// 所有犊牛
        /// </summary>
        public List<CowInfo> Calfs
        {
            get 
            {
                List<CowInfo> calfs = new List<CowInfo>();
                foreach (CowInfo item in CowInfoList)
                {
                    if (item.CowType == "犊牛")
                    {
                        calfs.Add(item);
                    }
                }
                return calfs; 
            }
        }
        /// <summary>
        /// 所有公牛
        /// </summary>
        public List<CowInfo> Bulls
        {
            get 
            {
                List<CowInfo> bulls = new List<CowInfo>();
                foreach (CowInfo item in CowInfoList)
                {
                    if (item.CowType == "公牛")
                    {
                        bulls.Add(item);
                    }
                }
                return bulls; 
            }
        }
        

        
        public FarmInfo()
        {
            cowList = cowBll.GetCowList();
            foreach (Cow item in cowList)
            {
                CowInfo cowInfo = new CowInfo(item);
                cowInfoList.Add(cowInfo);
                switch (item.Status)
                {
                    case "未配":
                        countOfUnInseminated++;
                        break;
                    case "已配未检":
                        countOfInseminated++;
                        break;
                    case "初检-":
                        countOfIntialInspectNegative++;
                        break;
                    case "初检+":
                        countOfIntialInspectPositive++;
                        break;
                    case "复检-":
                        countOfReInspectNegative++;
                        break;
                    case "复检+":
                        countOfReInspectPositive++;
                        break;
                    case "禁配":
                        countOfForbidden++;
                        break;
                    default:
                        break;
                }
                //switch (cowInfo.CowType)
                //{
                //    case "公牛":
                //        bulls.Add(cowInfo);
                //        break;
                //    case "犊牛":
                //        countOfCows++;
                //        calfs.Add(cowInfo);
                //        break;
                //    case "育成牛":
                //        countOfCows++;
                //        bredCattleCows.Add(cowInfo);
                //        break;
                //    case "青年牛":
                //        nullParityCows.Add(cowInfo);
                //        countOfCows++;
                //        break;
                //    case "经产牛":
                //        multiParityCows.Add(cowInfo);
                //        countOfCows++;
                //        break;
                //    default:
                //        countOfOther++;
                //        countOfCows++;
                //        break;
                //}
            }            
        }

        public FarmInfo(int pastureID)
        {
            cowList = cowBll.GetCowList(pastureID);
            foreach (Cow item in cowList)
            {
                CowInfo cowInfo = new CowInfo(item);
                cowInfoList.Add(cowInfo);
                switch (item.Status)
                {
                    case "未配":
                        countOfUnInseminated++;
                        break;
                    case "已配未检":
                        countOfInseminated++;
                        break;
                    case "初检-":
                        countOfIntialInspectNegative++;
                        break;
                    case "初检+":
                        countOfIntialInspectPositive++;
                        break;
                    case "复检-":
                        countOfReInspectNegative++;
                        break;
                    case "复检+":
                        countOfReInspectPositive++;
                        break;
                    case "禁配":
                        countOfForbidden++;
                        break;
                    default:
                        break;
                }
                //switch (cowInfo.CowType)
                //{
                //    case "公牛":
                //        bulls.Add(cowInfo);
                //        break;
                //    case "犊牛":
                //        countOfCows++;
                //        calfs.Add(cowInfo);
                //        break;
                //    case "育成牛":
                //        countOfCows++;
                //        bredCattleCows.Add(cowInfo);
                //        break;
                //    case "青年牛":
                //        nullParityCows.Add(cowInfo);
                //        countOfCows++;
                //        break;
                //    case "经产牛":
                //        multiParityCows.Add(cowInfo);
                //        countOfCows++;
                //        break;
                //    default:
                //        countOfOther++;
                //        countOfCows++;
                //        break;
                //}
            }
        }

       /// <summary>
       /// 所有牛总数，包括公牛。
       /// </summary>
        public int CountOfAll
        {
            get
            {
                return cowInfoList.Count;
            }
        }      

        private int countOfCows=0;
        /// <summary>
        /// 母牛总数
        /// </summary>
        public int CountOfCows
        {
            get { return countOfCows; }
            //set { countOfCows = value; }
        }

        private int countOfUnInseminated=0;

        public int CountOfUnInseminated
        {
          get { return countOfUnInseminated; }
          //set { countOfUnInseminated = value; }
        }

        private int countOfInseminated = 0;

        public int CountOfInseminated
        {
            get { return countOfInseminated; }
            //set { countOfInseminated = value; }
        }

        private int countOfIntialInspectPositive = 0;

        public int CountOfIntialInspectPositive
        {
            get { return countOfIntialInspectPositive; }
            //set { countOfIntialInspectPositive = value; }
        }
        private int countOfIntialInspectNegative = 0;

        public int CountOfIntialInspectNegative
        {
            get { return countOfIntialInspectNegative; }
            //set { countOfIntialInspectNegative = value; }
        }
        private int countOfReInspectPositive = 0;

        public int CountOfReInspectPositive
        {
            get { return countOfReInspectPositive; }
            //set { countOfReInspectPositive = value; }
        }
        private int countOfReInspectNegative = 0;

        public int CountOfReInspectNegative
        {
            get { return countOfReInspectNegative; }
            //set { countOfReInspectNegative = value; }
        }
        private int countOfForbidden = 0;

        public int CountOfForbidden
        {
            get { return countOfForbidden; }
            //set { countOfForbidden = value; }
        }

        
        /// <summary>
        /// 公牛数
        /// </summary>
        public int CountOfBull
        {
            get { return Bulls.Count; }
            //set { countOfBull = value; }
        }

       
        /// <summary>
        /// 犊牛数
        /// </summary>
        public int CountOfCalf
        {
            get { return Calfs.Count; }
            //set { countOfCalf = value; }
        }

        

        /// <summary>
        /// 育成牛数
        /// </summary>
        public int CountOfBredCattle
        {
            get { return BredCattleCows.Count; }
            //set { countOfBredCattle = value; }
        }
        
   
        /// <summary>
        /// 青年牛数
        /// </summary>
        public int CountOfNullParity
        {
            get { return NullParityCows.Count; }
            //set { countOfYouth = value; }
        }
       
        /// <summary>
        /// 经产牛数
        /// </summary>
        public int CountOfMultiParity
        {
            get { return MultiParityCows.Count; }
            //set { countOfMultiparity = value; }
        }

        private int countOfOther = 0;

        /// <summary>
        /// 其他牛数
        /// </summary>
        public int CountOfOther
        {
            get { return countOfOther; }
            //set { countOfOther = value; }
        }

        /// <summary>
        /// 所有未配上的经产牛
        /// </summary>
        public List<CowInfo> UninseminatedMultiParity
        {
            get
            {
                return GetUninseminatedMultiParity();
            }
        }
        /// <summary>
        /// 所有未配上的经产牛数
        /// </summary>
        public int CountOfUninseminatedMultiParity
        {
            get
            {
                return UninseminatedMultiParity.Count;
            }
        }
        /// <summary>
        /// 获取所有未配上的经产牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetUninseminatedMultiParity()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.MultiParityCows)
            {
                if (item.Status=="未配"||item.Status=="初检-"||item.Status=="复检-")
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 已配未检的经产牛
        /// </summary>
        public List<CowInfo> InseminatedMultiParity
        {
            get
            {
                return GetInseminatedMultiParity();
            }
        }
        /// <summary>
        /// 已配未检的经产牛数
        /// </summary>
        public int CountOfInseminatedMultiParity
        {
            get
            {
                return InseminatedMultiParity.Count;
            }
        }

        /// <summary>
        /// 获取已配未检的经产牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetInseminatedMultiParity()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.MultiParityCows)
            {
                if (item.Status == "已配未检" )
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 已孕的经产牛
        /// </summary>
        public List<CowInfo> PregnantMultiParity
        {
            get
            {
                return GetPregnantMultiParity();
            }
        }
        /// <summary>
        /// 已孕的经产牛数
        /// </summary>
        public int CountOfPregnantMultiParity
        {
            get
            {
                return PregnantMultiParity.Count;
            }
        }
        /// <summary>
        /// 未孕的经产牛数(经产牛数-已孕牛数)
        /// </summary>
        public int CountOfUnPregnantMultiParity
        {
            get
            {
                return CountOfMultiParity - CountOfPregnantMultiParity;
            }
        }
        /// <summary>
        /// 国际参考未孕牛占全部成母牛（经产牛+青年牛）比例，单位%
        /// </summary>
        public const float REF_PERCENTAGE_OF_UNPREGNANT_COWS = 30.0f;

        /// <summary>
        /// 国际参考未孕经产牛牛占全部经产牛比例，单位%
        /// </summary>
        public const float REF_PERCENTAGE_OF_UNPREGNANT_MULTIPARITY_COWS = 40.0f;


        public float PercentageOfUnpregnantCows
        {
            get
            {
                return 100.0f * CountOfUnPregnantMultiParity / CountOfMultiParity;
            }
        }

        /// <summary>
        /// 获取已孕的经产牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetPregnantMultiParity()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.MultiParityCows)
            {
                if (item.Status == "初检+" || item.Status == "复检+")
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 所有未配上的青年牛
        /// </summary>
        public List<CowInfo> UninseminatedNullParity
        {
            get
            {
                return GetUninseminatedNullParity();
            }
        }
        /// <summary>
        /// 所有未配上的青年牛数
        /// </summary>
        public int CountOfUninseminatedNullParity
        {
            get
            {
                return UninseminatedNullParity.Count;
            }
        }

        /// <summary>
        /// 获取所有未配上的青年牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetUninseminatedNullParity()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.NullParityCows)
            {
                if (item.Status == "未配" || item.Status == "初检-" || item.Status == "复检-")
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 已配未检的青年牛
        /// </summary>
        public List<CowInfo> InseminatedNullParity
        {
            get
            {
                return GetInseminatedNullParity();
            }
        }
        /// <summary>
        /// 已配未检的青年牛数
        /// </summary>
        public int CountOfInseminatedNullParity
        {
            get
            {
                return InseminatedNullParity.Count;
            }
        }
        /// <summary>
        /// 获取已配未检的青年牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetInseminatedNullParity()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.NullParityCows)
            {
                if (item.Status == "已配未检")
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 已孕的青年牛
        /// </summary>
        public List<CowInfo> PregnantNullParity
        {
            get
            {
                return GetPregnantNullParity();
            }
        }
        /// <summary>
        /// 已孕的青年牛数
        /// </summary>
        public int CountOfPregnantNullParity
        {
            get
            {
                return PregnantNullParity.Count;
            }
        }

        /// <summary>
        /// 获取已孕的青年牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetPregnantNullParity()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.NullParityCows)
            {
                if (item.Status == "初检+" || item.Status == "复检+")
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 1胎牛
        /// </summary>
        public List<CowInfo> Parity1Cows
        {
            get
            {
                return GetParity1Cows();
            }
        }
        /// <summary>
        /// 1胎牛数
        /// </summary>
        public int CountOfParity1Cows
        {
            get
            {
                return Parity1Cows.Count;
            }
        }

        /// <summary>
        /// 1胎牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetParity1Cows()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.MultiParityCows)
            {
                if (item.Parity==1)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 2胎牛
        /// </summary>
        public List<CowInfo> Parity2Cows
        {
            get
            {
                return GetParity2Cows();
            }
        }
        /// <summary>
        /// 2胎牛数
        /// </summary>
        public int CountOfParity2Cows
        {
            get
            {
                return Parity2Cows.Count;
            }
        }

        /// <summary>
        /// 2胎牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetParity2Cows()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.MultiParityCows)
            {
                if (item.Parity == 2)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 3胎及以上牛
        /// </summary>
        public List<CowInfo> Parity3AndMoreCows
        {
            get
            {
                return GetParity3AndMoreCows();
            }
        }
        /// <summary>
        /// 3胎及以上牛数
        /// </summary>
        public int CountOfParity3AndMoreCows
        {
            get
            {
                return Parity3AndMoreCows.Count;
            }
        }

        /// <summary>
        /// 3胎及以上牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetParity3AndMoreCows()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.MultiParityCows)
            {
                if (item.Parity >= 3)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 泌乳牛
        /// </summary>
        public List<CowInfo> MilkCows
        {
            get
            {
                return GetMilkCows();
            }
        }
        /// <summary>
        /// 泌乳牛数
        /// </summary>
        public int CountOfMilkCows
        {
            get
            {
                return MilkCows.Count;
            }
        }

        /// <summary>
        /// 泌乳牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetMilkCows()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.cowInfoList)
            {
                if (item.MilkType == "泌乳牛")
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 干奶牛
        /// </summary>
        public List<CowInfo> DryMilkCows
        {
            get
            {
                return GetDryMilkCows();
            }
        }

        public int CountOfDryMilkCows
        {
            get
            {
                return DryMilkCows.Count;
            }
        }

        /// <summary>
        /// 获取所有干奶牛
        /// </summary>
        /// <returns></returns>
        private List<CowInfo> GetDryMilkCows()
        {
            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.cowInfoList)
            {
                if (item.MilkType == "干奶牛")
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 所有未孕青年牛和经产牛
        /// </summary>
        public int CountOfTotalUnpregnantCows
        {
            get
            {
                return CountOfUnPregnantMultiParity + (CountOfNullParity-CountOfPregnantNullParity);
            }
        }
        /// <summary>
        /// 所有未孕青年牛和经产牛比例，单位%
        /// </summary>
        public float PercentageOfTotalUnpregnantCows
        {
            get
            {
                return (CountOfUnPregnantMultiParity + (CountOfNullParity - CountOfPregnantNullParity))*100.0f/(CountOfNullParity+CountOfMultiParity);
            }
        }

        /// <summary>
        /// 国内参考水平,<170 天，
        /// </summary>
        public const int REF_AVERAGE_MILKDAYS_MAX = 170;

        /// <summary>
        /// 牛场泌乳牛平均泌乳天数,无泌乳牛返会0
        /// </summary>
        /// <returns></returns>
        public int GetAverageMilkDays()
        {
            int ave, total=0;
            List<CowInfo> list = GetMilkCows();
            if (list.Count==0)
            {
                return 0;
            }
            foreach (CowInfo item in list)
            {
                total = total + item.DaysInMilk;
            }
            if (list.Count==0)
            {
                ave = 0;
            }
            else
            {
                ave = total / list.Count;
            }
           
            return ave;
        }

        public const int REF_AVERAGE_DRY_DAYS_MIN=45;
        public const int REF_AVERAGE_DRY_DAYS_MAX = 60;

        /// <summary>
        /// 牛场干奶牛平均干奶天数,无干奶牛返会0
        /// </summary>
        /// <returns></returns>
        public int GetAverageDryDays()
        {
            int ave, total = 0;
            List<CowInfo> list = GetDryMilkCows();
            if (list.Count==0)
            {
                return 0;
            }
            foreach (CowInfo item in list)
            {
                total = total + item.DaysInDry;
            }
            if (list.Count==0)
            {
                ave = 0;
            }
            else
            {
                ave = total / list.Count;
            }
            
            return ave;
        }

        /// <summary>
        /// 参考初产平均月龄<24 月
        /// </summary>
        public const double REF_AGEMONTH_FIRST_BIRTH_MAX = 24;
        /// <summary>
        /// 获取平均初产月龄
        /// </summary>
        /// <returns></returns>
        public double GetAverageAgeMonthOfFirstBirth()
        {
            double ageM = 0.0;
            foreach (CowInfo item in MultiParityCows)
            {
                ageM = ageM + item.AgeMonthOfFirstBorn;
            }
            if (MultiParityCows.Count==0)
            {
                ageM = 0;
            }
            else
            {
                ageM = ageM / MultiParityCows.Count;
            }
            
            return ageM;
        }
        /// <summary>
        /// 获取平均初产月龄
        /// </summary>
        /// <returns></returns>
        public double GetAverageAgeMonthOfFirstBirth(List<CowInfo> list)
        {
            double ageM = 0.0;
            int count = list.Count;
            foreach (CowInfo item in list)
            {
                ageM = ageM + item.AgeMonthOfFirstBorn;
                //未产犊牛，不计入
                if (ageM==0)
                {
                    count--;
                }
            }
            if (count==0)
            {
                ageM = 0;
            }
            else
            {
                ageM = ageM / count;
            }
            return ageM;
        }
        /// <summary>
        /// 牧场经产牛平均初产月龄
        /// </summary>
        public double AverageAgeMonthOfFirstBirth
        {
            get
            {
                return GetAverageAgeMonthOfFirstBirth();
            }
        }
        /// <summary>
        /// 牧场1胎牛平均初产月龄
        /// </summary>
        public double AverageAgeMonthOfFirstBirthOfParity1
        { 
            get
            {
                return GetAverageAgeMonthOfFirstBirth(Parity1Cows);
            }
        }

        /// <summary>
        /// 国内参考胎间距
        /// </summary>
        public const double REF_AVERAGE_PARITY_INTERVAL_DEMESTIC = 500;
        /// <summary>
        /// 国外参考胎间距
        /// </summary>
        public const double REF_AVERAGE_PARITY_INTERVAL_ABROAD = 365;

        /// <summary>
        /// 1胎以上牛，平均胎间距,没有牛返回0
        /// </summary>
        public double AverageParityInterval
        {
            get
            {
                double interval = 0.0;
                foreach (CowInfo item in Parity2Cows)
                {
                    interval = interval + item.AverageParityInterval;
                }
                foreach (CowInfo item1 in Parity3AndMoreCows)
                {
                    interval = interval + item1.AverageParityInterval;
                }
                if ((Parity2Cows.Count + Parity3AndMoreCows.Count)==0)
                {
                    interval = 0.0;
                }
                else
                {
                    interval = interval / (Parity2Cows.Count + Parity3AndMoreCows.Count);
                }
                return interval;
            }
        }
        /// <summary>
        /// 国内参考，产后首次发情天数，小于40
        /// </summary>
        public const double REF_DAYS_FROM_BIRTH_TO_FIRST_HEAT = 40.0;
        /// <summary>
        /// 经产牛平均发情（第一次配种）天数
        /// </summary>
        public double AverageInseminationDaysAfterBirth
        {
            get
            {
                double days = 0.0;
                if (MultiParityCows.Count!=0)
                {
                    int count = MultiParityCows.Count;
                    foreach (CowInfo item in MultiParityCows)
                    {
                        Insemination insem= CowInfo.GetFirstInseminationOfCurrentBreedPeriod(item.EarNum);
                        if (insem==null)
                        {
                            count--;
                        }
                        else
                        {
                            days = days + item.GetDaysToFirstInsemination();
                        }
                        
                    }
                    if (count>0)
                    {
                        days = days / count;
                    }
                    
                }
                return days;
            }
        }
        /// <summary>
        /// 国内参考经产牛首配成功率50%-60%
        /// </summary>
        public const double REF_MULTIPARITY_COW_SUCCESS_PERCENTAGE_OF_FIRST_INSEMINATION = 50.0;
        /// <summary>
        /// 经产牛首配成功率
        /// </summary>
        public double MultiParityCowSuccessPercentageOfFirstInsemination
        {
            get
            {
                double d = 0.0;
                int total = MultiParityCows.Count, count = 0;
                foreach (CowInfo item in MultiParityCows)
                {
                    int num = item.TimesOfInsemination;
                    if (num > 0)
                    {
                        if (num==1 &(item.Status=="初检+"||item.Status=="复检+"))
                        {
                            count++;
                        }
                    }
                    else
                    {
                        //未配过的牛不计
                        total--;
                    }
                }
                if (total!=0)
                {
                    d = 100.0 * count / total;
                }
                return d;
            }
        }

        /// <summary>
        /// 国内参考经产牛首配成功率65%-70%
        /// </summary>
        public const double REF_NULLIPARITY_COW_SUCCESS_PERCENTAGE_OF_FIRST_INSEMINATION = 65.0;
        /// <summary>
        /// 青年牛首配成功率
        /// </summary>
        public double NullParityCowSuccessPercentageOfFirstInsemination
        {
            get
            {
                double d = 0.0;
                int total = NullParityCows.Count, count = 0;
                foreach (CowInfo item in NullParityCows)
                {
                    int num = item.TimesOfInsemination;
                    if (num > 0)
                    {
                        if (num == 1 & (item.Status == "初检+" || item.Status == "复检+"))
                        {
                            count++;
                        }
                    }
                    else
                    {
                        //未配过的牛不计
                        total--;
                    }
                }
                
                if (total != 0)
                {
                    d = 100.0 * count / total;
                }
                return d;
            }
        }

        /// <summary>
        /// 国内参考所有牛两配成功率90%
        /// </summary>
        public const double REF_ALL_COW_SUCCESS_PERCENTAGE_OF_TWICE_INSEMINATION = 90.0;
        /// <summary>
        /// 所有牛三配成功率
        /// </summary>
        public double CowSuccessPercentageOfTwiceInsemination
        {
            get
            {
                
                double d = 0.0;
                int total = NullParityCows.Count+MultiParityCows.Count, count = 0;
                foreach (CowInfo item in NullParityCows)
                {
                    int num = item.TimesOfInsemination;
                    if (num > 0)
                    {
                        if (num <3 & (item.Status == "初检+" || item.Status == "复检+"))
                        {
                            count++;
                        }
                    }
                    else
                    {
                        //未配过的牛不计
                        total--;
                    }
                }
                foreach (CowInfo item in MultiParityCows)
                {
                    int num = item.TimesOfInsemination;
                    if (num > 0)
                    {
                        if (num <3 & (item.Status == "初检+" || item.Status == "复检+"))
                        {
                            count++;
                        }
                    }
                    else
                    {
                        //未配过的牛不计
                        total--;
                    }
                }
                if (total != 0)
                {
                    d = 100.0 * count / total;
                }
                return d;
            }
        }

        /// <summary>
        /// 国内参考已孕牛配准天数，小于110天
        /// </summary>
        public const double REF_DAYS_FROM_BIRTH_TO_SUCCESSFUL_INSEMINATION_DEMESTIC = 110.0;
        /// <summary>
        /// 国外参考已孕牛配准天数，70-80天
        /// </summary>
        public const double REF_DAYS_FROM_BIRTH_TO_SUCCESSFUL_INSEMINATION_ABROAD = 110.0;
        /// <summary>
        /// 经产牛平均配准天数
        /// </summary>
        public double DaysFromBirthToSuccessfulInsemination
        {
            get
            {
                double days = 0.0;
                foreach (CowInfo item in PregnantMultiParity)
                {
                    DateTime t = DateTime.Now;
                    //数据正常，已孕牛一定有本轮配种记录
#if RELEASE
                    if (item.LatestInseminationOfCurrentBreedPeriod==null)
	                {
                        throw new Exception(String.Format(@"耳号为{0}的牛的繁殖数据异常，显示为已孕牛却没有本轮配种记录",item.EarNum));
	                }
                    else
                    {
                        t=item.LatestInseminationOfCurrentBreedPeriod.OperateDate; 
                    }
#elif DEBUG
                    if (item.LatestInseminationOfCurrentBreedPeriod!=null)
	                {
                        t = item.LatestInseminationOfCurrentBreedPeriod.OperateDate;   
	                }
                    else
                    {
                        t = DateTime.Now;//数据错误，仅供调试显示
                    }

#endif
                    days = days + t.Subtract(item.GetLatestCalvingDate()).TotalDays;

                }
                days = days / CountOfPregnantMultiParity;
                return days;
            }
        }

        /// <summary>
        /// 国内参考未孕牛空怀天数，80-110天
        /// </summary>
        public const double REF_DAYS_OF_UNPREGNANT = 110.0;
        /// <summary>
        /// 未孕牛空怀天数（未孕天数？）
        /// </summary>
        public double DaysOfUnpregnant
        {
            get
            {
                double days = 0.0;
                int total = CountOfUnPregnantMultiParity;
                foreach (CowInfo item in UninseminatedMultiParity)
                {
                    days = days + DateTime.Now.Subtract(item.GetLatestCalvingDate()).TotalDays;
                }
                foreach (CowInfo item in InseminatedMultiParity)
                {
                    days = days + DateTime.Now.Subtract(item.GetLatestCalvingDate()).TotalDays;
                }
                if (total>0)
                {
                    days = days / total;
                }
                return days;
                
            }
        }

        /// <summary>
        /// 16月龄以上未配青年牛
        /// </summary>
        public List<CowInfo> UninseminatedOldThan16MonthNullparityCows
        {
            get
            {
                List<CowInfo> list = new List<CowInfo>();
                foreach (CowInfo item in this.NullParityCows)
                {
                    if (item.Status =="未配"&& item.AgeMonth>16)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
        }
        /// <summary>
        /// 16月龄以上未配青年牛数
        /// </summary>
        public int CountOfUninseminatedOldThan16MonthNullparityCows
        {
            get
            {
                return UninseminatedOldThan16MonthNullparityCows.Count;
            }
        }

        /// <summary>
        /// 20月龄以上未孕青年牛
        /// </summary>
        public List<CowInfo> UnPregnantOldThan20MonthNullparityCows
        {
            get
            {
                List<CowInfo> list = new List<CowInfo>();
                foreach (CowInfo item in this.NullParityCows)
                {
                    if (item.Status != "复检+" && item.Status != "初检+" && item.AgeMonth > 20)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
        }
        /// <summary>
        /// 20月龄以上未孕青年牛数
        /// </summary>
        public int CountOfUnPregnantOldThan20MonthNullparityCows
        {
            get
            {
                return UnPregnantOldThan20MonthNullparityCows.Count;
            }
        }




        public static int GetSumMaleCalf(int pastureID,DateTime date)
        {
            CalvingDAL dal=new CalvingDAL();
            return dal.GetPastureMaleCalfNumber(pastureID, date);
        }

        public static int GetSumMaleCalf(int pastureID, int year,int month)
        {
            CalvingDAL dal = new CalvingDAL();
            return dal.GetPastureMaleCalfNumber(pastureID, year,month);


        }

        public static int GetSumMaleCalf(int pastureID,int year)
        {
            CalvingDAL dal = new CalvingDAL();
            return dal.GetPastureMaleCalfNumber(pastureID, year);
        }

        public static int GetSumFemaleCalf(int pastureID, DateTime date)
        {
            CalvingDAL dal = new CalvingDAL();
            return dal.GetPastureFemaleCalfNumber(pastureID, date);
        }

        public static int GetSumFemaleCalf(int pastureID, int year, int month)
        {
            CalvingDAL dal = new CalvingDAL();
            return dal.GetPastureFemaleCalfNumber(pastureID, year, month);


        }

        public static int GetSumFemaleCalf(int pastureID, int year)
        {
            CalvingDAL dal = new CalvingDAL();
            return dal.GetPastureFemaleCalfNumber(pastureID, year);
        }


        public static int GetSumCalving(int pastureID, DateTime date)
        {
            CalvingDAL dal = new CalvingDAL();
            return dal.GetPastureCalvingNumber(pastureID, date);
        }

        public static int GetSumCalving(int pastureID, int year, int month)
        {
            CalvingDAL dal = new CalvingDAL();
            return dal.GetPastureCalvingNumber(pastureID, year, month);

        }

        public static int GetSumCalving(int pastureID, int year)
        {
            CalvingDAL dal = new CalvingDAL();
            return dal.GetPastureCalvingNumber(pastureID, year);
        }

        /// <summary>
        /// 获取需要调群的牛
        /// </summary>
        /// <returns></returns>
        public List<CowInfo> GetNeedRegroupingCows(int pastrure)
        {
            CowGroupBLL cowGroupBll = new CowGroupBLL();
            List<CowGroup> groups = cowGroupBll.GetCowGroupList(pastrure);

            List<CowInfo> list = new List<CowInfo>();
            foreach (CowInfo item in this.CowInfoList)
            {
                item.CheckGrouping(groups);
                if (item.NeedGrouping)
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}
