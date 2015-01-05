using DairyCow.BLL;
using DairyCow.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CowSite.Controllers.Breed
{
    public class IndexController : Controller
    {
        CowBLL bllCow = new CowBLL();

        public ActionResult Index()
        {
            return View("~/Views/Breed/Index.cshtml");
        }
        
        /// <summary>
        /// 繁殖指标
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBreedIndicant()
        {
            FarmInfo farm = new FarmInfo(UserBLL.Instance.CurrentUser.Pasture.ID);

            List<BreedIndicant> lstBreedIndicant = new List<BreedIndicant>();
            // 牧场繁殖指标
            BreedIndicant farmIndicant = new BreedIndicant();
            farmIndicant.IndicantScope = "牧场";
            farmIndicant.Indicant1 = Math.Round(farm.AverageAgeMonthOfFirstBirth, 2);
            farmIndicant.Indicant2 = Math.Round(farm.AverageAgeMonthOfFirstBirthOfParity1, 2);
            farmIndicant.Indicant3 = Math.Round(farm.AverageInseminationDaysAfterBirth, 2);
            farmIndicant.Indicant4 = Math.Round(farm.DaysOfUnpregnant, 2);
            farmIndicant.Indicant5 = Math.Round(farm.DaysFromBirthToSuccessfulInsemination, 2);
            farmIndicant.Indicant6 = Math.Round(farm.AverageParityInterval, 2);
            farmIndicant.Indicant7 = Math.Round(farm.MultiParityCowSuccessPercentageOfFirstInsemination, 2);
            farmIndicant.Indicant8 = Math.Round(farm.NullParityCowSuccessPercentageOfFirstInsemination, 2);
            farmIndicant.Indicant9 = Math.Round(farm.CowSuccessPercentageOfTwiceInsemination, 2);
           lstBreedIndicant.Add(farmIndicant);
            // 牧场繁殖国内参考水平
            BreedIndicant domesticIndicant = new BreedIndicant();
            domesticIndicant.IndicantScope = "国内参考水平";
            domesticIndicant.Indicant1 = FarmInfo.REF_AGEMONTH_FIRST_BIRTH_MAX;
            domesticIndicant.Indicant2 = FarmInfo.REF_AGEMONTH_FIRST_BIRTH_MAX;
            domesticIndicant.Indicant3 = FarmInfo.REF_DAYS_FROM_BIRTH_TO_FIRST_HEAT;
            domesticIndicant.Indicant4 = FarmInfo.REF_DAYS_OF_UNPREGNANT;
            domesticIndicant.Indicant5 = FarmInfo.REF_DAYS_FROM_BIRTH_TO_SUCCESSFUL_INSEMINATION_DEMESTIC;
            domesticIndicant.Indicant6 = FarmInfo.REF_AVERAGE_PARITY_INTERVAL_DEMESTIC;
            domesticIndicant.Indicant7 = FarmInfo.REF_MULTIPARITY_COW_SUCCESS_PERCENTAGE_OF_FIRST_INSEMINATION;
            domesticIndicant.Indicant8 = FarmInfo.REF_NULLIPARITY_COW_SUCCESS_PERCENTAGE_OF_FIRST_INSEMINATION;
            domesticIndicant.Indicant9 = FarmInfo.REF_ALL_COW_SUCCESS_PERCENTAGE_OF_TWICE_INSEMINATION;
            lstBreedIndicant.Add(domesticIndicant);
            // 牧场繁殖国际参考水平
            BreedIndicant abroadtIndicant = new BreedIndicant();
            abroadtIndicant.IndicantScope = "国际参考水平";
            abroadtIndicant.Indicant1 = 0;
            abroadtIndicant.Indicant2 = 0;
            abroadtIndicant.Indicant3 = 0;
            abroadtIndicant.Indicant4 = 0;
            abroadtIndicant.Indicant5 = FarmInfo.REF_DAYS_FROM_BIRTH_TO_SUCCESSFUL_INSEMINATION_ABROAD;
            abroadtIndicant.Indicant6 = FarmInfo.REF_AVERAGE_PARITY_INTERVAL_ABROAD;
            abroadtIndicant.Indicant7 = 0;
            abroadtIndicant.Indicant8 = 0;
            abroadtIndicant.Indicant9 = 0;
            lstBreedIndicant.Add(abroadtIndicant);
            var breedIndicant = new
            {
                Rows = lstBreedIndicant 
            };

            return Json(breedIndicant, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 繁殖饼图
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBreedChart()
        {
            FarmInfo farm = new FarmInfo(UserBLL.Instance.CurrentUser.Pasture.ID);

            // 经产牛繁殖状况
            ArrayList arrBreedSummary = new ArrayList();
            arrBreedSummary.Add(new { name = "未配牛", value = farm.CountOfUninseminatedMultiParity });
            arrBreedSummary.Add(new { name = "已配未检牛", value = farm.CountOfInseminatedMultiParity });
            arrBreedSummary.Add(new { name = "已孕头数", value = farm.CountOfPregnantMultiParity });
            // 青年牛繁殖状况
            ArrayList arrParitySummary = new ArrayList();
            arrParitySummary.Add(new { name = "未配牛", value = farm.CountOfUninseminatedNullParity });
            arrParitySummary.Add(new { name = "已配未检牛", value = farm.CountOfInseminatedNullParity });
            arrParitySummary.Add(new { name = "已孕头数", value = farm.CountOfPregnantNullParity });

            return Json(new
            {
                BreedSummary = arrBreedSummary,
                ParitySummary = arrParitySummary
            }, JsonRequestBehavior.AllowGet);
        }
    }

    public class BreedIndicant
    {
        public string IndicantScope { get; set; }
        public double Indicant1 { get; set; } //经产牛平均初产月龄
        public double Indicant2 { get; set; } //头胎牛平均初产月龄
        public double Indicant3 { get; set; } //经产牛产后首次发情平均天数(发情间隔)
        public double Indicant4 { get; set; } //经产牛未孕牛空怀天数
        public double Indicant5 { get; set; } //经产牛配准天数
        public double Indicant6 { get; set; } //经产牛胎间距
        public double Indicant7 { get; set; } //经产牛首次配种妊娠率
        public double Indicant8 { get; set; } //青年牛首次配种妊娠率
        public double Indicant9 { get; set; } //总配次<3的配种妊娠率
    }
}