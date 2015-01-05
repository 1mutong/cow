using DairyCow.DAL.Base;
using DairyCow.Model;
using System;
using System.Data;

namespace DairyCow.DAL
{
    public class MilkRecordDAL:BaseDAL
    {

        public DataTable GetMilkSaleRecords(int pastureID)
        {
            DataTable sales = null;

            string sql = string.Format(@"SELECT D.Id, 
                                                    D.PastureId,
                                                    D.MilkDate,
                                                    D.Amount,
                                                    D.MilkWeight,
                                                    D.Company,
                                                    D.ShipCode,
                                                    D.Decoding,
                                                    D.TankerNum,
                                                    D.TruckNum,
                                                    D.Fat,
                                                    D.Protein,
                                                    D.DryMatter,
                                                    D.NonFatSolid,
                                                    D.Microbe,
                                                    D.Lactose,
                                                    D.IcePoint, 
                                                    D.Acidity
                                                    FROM Milk_Shipping AS D
                                                    WHERE D.PastureId={0}", pastureID);



            sales = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return sales;
        }

        public DataTable GetMilkSaleRecords(int pastureID,DateTime date)
        {
            DataTable sales = null;

            string sql = string.Format(@"SELECT D.Id, 
                                                    D.PastureId,
                                                    D.MilkDate,
                                                    D.Amount,
                                                    D.MilkWeight,
                                                    D.Company,
                                                    D.ShipCode,
                                                    D.Decoding,
                                                    D.TankerNum,
                                                    D.TruckNum,
                                                    D.Fat,
                                                    D.Protein,
                                                    D.DryMatter,
                                                    D.NonFatSolid,
                                                    D.Microbe,
                                                    D.Lactose,
                                                    D.IcePoint, 
                                                    D.Acidity
                                                    FROM Milk_Shipping AS D
                                                    WHERE D.PastureId={0} AND D.MilkDate='{1}'", pastureID,date.Date.ToShortDateString());



            sales = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return sales;
        }

        public DataTable GetOtherMilkRecords(int pastureID)
        {
            DataTable omTable = null;

            string sql = string.Format(@"SELECT D.Id, 
                                                    D.PastureId,
                                                    D.MilkDate,
                                                    D.MilkForCalf,
                                                    D.AbnormalSaleMilk,
                                                    D.BadMilk,
                                                    D.LeftMilk
                                                    FROM Milk_OtherMilk AS D
                                                    WHERE D.PastureId={0}", pastureID);



            omTable = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return omTable;
        }

        public DataTable GetOtherMilkRecords(int pastureID,DateTime date)
        {
            DataTable omTable = null;

            string sql = string.Format(@"SELECT D.Id, 
                                                    D.PastureId,
                                                    D.MilkDate,
                                                    D.MilkForCalf,
                                                    D.AbnormalSaleMilk,
                                                    D.BadMilk,
                                                    D.LeftMilk
                                                    FROM Milk_OtherMilk AS D
                                                    WHERE D.PastureId={0} AND D.MilkDate='{1}'", pastureID,date.Date.ToShortDateString());



            omTable = dataProvider1mutong.FillDataTable(sql, CommandType.Text);

            return omTable;
        }

        /// <summary>
        /// 插入售奶记录
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="milkDate"></param>
        /// <param name="amount"></param>
        /// <param name="milkWeight"></param>
        /// <param name="company"></param>
        /// <param name="shipCode"></param>
        /// <param name="decoding"></param>
        /// <param name="tankerNum"></param>
        /// <param name="truckNum"></param>
        /// <param name="fat"></param>
        /// <param name="protein"></param>
        /// <param name="dryMatter"></param>
        /// <param name="nonFatSolid"></param>
        /// <param name="microbe"></param>
        /// <param name="lactose"></param>
        /// <param name="icePoint"></param>
        /// <param name="acidity"></param>
        /// <returns></returns>
        public int InsertMilkSale(int pastureID, DateTime milkDate,float amount,float milkWeight,string company,string shipCode,string decoding,string tankerNum,string truckNum,float fat,float protein,float dryMatter,float nonFatSolid,float microbe,float lactose,float icePoint,float acidity)
        {
            string sql = string.Format(@"Insert INTO Milk_Shipping (PastureId,
                                                    MilkDate,
                                                    Amount,
                                                    MilkWeight,
                                                    Company,
                                                    ShipCode,
                                                    Decoding,
                                                    TankerNum,
                                                    TruckNum,
                                                    Fat,
                                                    Protein,
                                                    DryMatter,
                                                    NonFatSolid,
                                                    Microbe,
                                                    Lactose,
                                                    IcePoint, 
                                                    Acidity) Values({0},'{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},{12},{13},{14},{15},{16})", 
                                                    pastureID,  milkDate.Date.ToShortDateString(), amount, milkWeight, company, shipCode, decoding, tankerNum, truckNum, fat, protein, dryMatter, nonFatSolid, microbe, lactose, icePoint, acidity);
           return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);


        }

        /// <summary>
        /// 插入其它奶售奶记录
        /// </summary>
        /// <param name="pastureId"></param>
        /// <param name="milkDate"></param>
        /// <param name="milkForCalf"></param>
        /// <param name="abnormalSaleMilk"></param>
        /// <param name="badMilk"></param>
        /// <param name="leftMilk"></param>
        /// <returns></returns>
        public int InsertOtherMilkRecord(int pastureId,DateTime milkDate,float milkForCalf,float abnormalSaleMilk,float badMilk,float leftMilk)
        {
            string sql = string.Format(@"Insert INTO Milk_OtherMilk 
                                       (PastureId,
                                       MilkDate,
                                       MilkForCalf,
                                       AbnormalSaleMilk,
                                       BadMilk,
                                       LeftMilk) Values({0},'{1}',{2},{3},{4},{5})",
                                        pastureId,milkDate.Date.ToShortDateString(), milkForCalf, abnormalSaleMilk, badMilk, leftMilk);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
    }
}
