using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.DAL;
using DairyCow.Model;
using Common;

namespace DairyCow.BLL
{
    public class IndividualProdcutionBLL
    {
        private IndividualProdcutionDAL individualProdcutionDAL = new IndividualProdcutionDAL();
        public int InsertIndividualProdcution(IndividualProdcution individualProdcution)
        {
            return this.individualProdcutionDAL.InsertIndividualProdcution(individualProdcution.EarNum, individualProdcution.MilkDate, individualProdcution.MilkWeight, individualProdcution.Round);
        }

        public List<IndividualProdcutionTotal> GetIndividualDayProdcutionList(int earNum, DateTime startDate, DateTime endDate)
        {
            List<IndividualProdcutionTotal> list = new List<IndividualProdcutionTotal>();
            DataTable table = this.individualProdcutionDAL.GetIndividualProductionTotalTable(earNum, startDate.Date, endDate.Date);
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapIndividualProdcutionItem(item));
            }
            return list;
        }

        public IndividualProdcutionTotal WrapIndividualProdcutionItem(DataRow row)
        {
            IndividualProdcutionTotal individualProdcutionTotal = new IndividualProdcutionTotal();
            individualProdcutionTotal.EarNum = Convert.ToInt32(row["EarNum"]);
            individualProdcutionTotal.MilkDate = Convert.ToDateTime(row["MilkDate"]);
            individualProdcutionTotal.TotalMilk = Convert.ToSingle(row["MilkDate"]);
            return individualProdcutionTotal;
        }

        /// <summary>
        /// 获取第parityNum胎次的日产奶list
        /// </summary>
        /// <param name="earNum">耳号</param>
        /// <param name="parityNum">第胎次，从1开始计算</param>
        /// <returns></returns>
        public List<IndividualProdcutionTotal> GetIndividualDayProdcutionList(int earNum, int parityNum)
        {
            DateTime startDate, endDate;

            CowInfo cowInfo = new CowInfo(earNum);
            int parity = cowInfo.Parity;
            //ORDER BY D.Birthday DESC 降序的
            List<Calving> calvingList = CowInfo.GetCowCalvingRecords(earNum);
            if (parityNum > parity | parityNum < 1)
            {
                throw new ArgumentException("此牛仅有" + parity.ToString() + "个胎次，不能有此记录");
            }
            else
            {
                startDate = calvingList[parity - parityNum].Birthday.Date;
                if (parityNum < parity)
                {
                    endDate = calvingList[parity - parityNum + 1].Birthday.Date;
                }
                else
                {
                    //最后一胎
                    endDate = DateTime.Now.Date;
                }
            }
            return GetIndividualDayProdcutionList(earNum, startDate, endDate);
        }


        public List<MilkCheckModel> GetMilkCheckList(int farmID)
        {

            DataTable dt = this.individualProdcutionDAL.GetMilkCheckList(farmID);
            List<MilkCheckModel> ListMilkCheck = ConvertHelper<MilkCheckModel>.ConvertToList(dt);
            return ListMilkCheck;
        }
    }
}
