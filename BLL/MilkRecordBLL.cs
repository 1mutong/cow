using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyCow.DAL;
using DairyCow.Model;
using System.Data;

namespace DairyCow.BLL
{
    public class MilkRecordBLL
    {
        private MilkRecordDAL dalMilk = new MilkRecordDAL();

        public List<MilkRecord> GetMilkRecords(int pastureID, DateTime startDate, DateTime endDate)
        {
            List<MilkRecord> list = new List<MilkRecord>();
            for (DateTime date = startDate.Date; date.Subtract(endDate).TotalHours < 0.0; date = date.AddDays(1.0))
            {
                MilkRecord m = new MilkRecord(pastureID, date.Date);
                list.Add(m);
            }
            return list;
        }

        /// <summary>
        /// 插入售奶记录，未输入指标按0.0或者空字符串
        /// </summary>
        /// <param name="milkSale">售奶</param>
        /// <returns></returns>
        public int InsertMilkSale(MilkSale milkSale)
        {
            //validation
            if (milkSale.Company==null)
            {
                milkSale.Company = String.Empty;
            }
            if (milkSale.TruckNum == null)
            {
                milkSale.TruckNum = String.Empty;
            }
            if (milkSale.TankerNum == null)
            {
                milkSale.TankerNum = String.Empty;
            }
            if (milkSale.Decoding == null)
            {
                milkSale.Decoding = String.Empty;
            }
            if (milkSale.ShipCode == null)
            {
                milkSale.ShipCode = String.Empty;
            }

            if (milkSale.Fat == null)
            {
                milkSale.Fat = 0.0f;
            }
            if (milkSale.Protein == null)
            {
                milkSale.Protein = 0.0f;
            }
            if (milkSale.DryMatter == null)
            {
                milkSale.DryMatter = 0.0f;
            }
            if (milkSale.NonFatSolid == null)
            {
                milkSale.NonFatSolid = 0.0f;
            }
            if (milkSale.Microbe == null)
            {
                milkSale.Microbe = 0.0f;
            }
            if (milkSale.Lactose == null)
            {
                milkSale.Lactose = 0.0f;
            }
            if (milkSale.IcePoint == null)
            {
                milkSale.IcePoint = 0.0f;
            }
            if (milkSale.Acidity == null)
            {
                milkSale.Acidity = 0.0f;
            }


            return dalMilk.InsertMilkSale(milkSale.PastureID, milkSale.MilkDate.Date, milkSale.Amount, milkSale.MilkWeight, milkSale.Company, milkSale.ShipCode, milkSale.Decoding, milkSale.TankerNum, milkSale.TruckNum, milkSale.Fat, milkSale.Protein, milkSale.DryMatter, milkSale.NonFatSolid, milkSale.Microbe, milkSale.Lactose, milkSale.IcePoint, milkSale.Acidity);
        }

        /// <summary>
        /// 插入其他奶记录，有重复记录放弃插入记录。
        /// </summary>
        /// <param name="otherMilk"></param>
        /// <returns></returns>
        public int InsertOtherMilkRecord(OtherMilk otherMilk)
        {
            OtherMilk om = GetOtherMilk(otherMilk.PastureID,otherMilk.MilkDate);
            if (om==null)
            {
                return dalMilk.InsertOtherMilkRecord(otherMilk.PastureID, otherMilk.MilkDate.Date, otherMilk.MilkForCalf, otherMilk.AbnormalSaleMilk, otherMilk.BadMilk, otherMilk.LeftMilk);
            }
            else
            {
                //放弃插入，前台处理失败逻辑
                return 0;
            }
        }

        /// <summary>
        /// 获取牧场售奶记录
        /// </summary>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public List<MilkSale> GetMilkSaleList(int pastureID)
        {
            List<MilkSale> list=new List<MilkSale>();
            DataTable table=dalMilk.GetMilkSaleRecords(pastureID);
            foreach (DataRow item in table.Rows)
	        {
                list.Add(WrapMilkSaleItem(item));
	        }
            return list;
        }
        /// <summary>
        /// 获取牧场其他奶记录，如犊牛饲喂奶等。
        /// </summary>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public List<OtherMilk> GetOtherMilkList(int pastureID)
        {
            List<OtherMilk> list = new List<OtherMilk>();
            DataTable table = dalMilk.GetOtherMilkRecords(pastureID);
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapOtherMilkItem(item));
            }
            return list;
        }
        /// <summary>
        /// 获取牧场其他奶记录，如犊牛饲喂奶等。
        /// </summary>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public List<OtherMilk> GetOtherMilkList(int pastureID,DateTime date)
        {
            List<OtherMilk> list = new List<OtherMilk>();
            DataTable table = dalMilk.GetOtherMilkRecords(pastureID,date.Date);
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapOtherMilkItem(item));
            }
            return list;
        }
        /// <summary>
        /// 封装MilkSale
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private MilkSale WrapMilkSaleItem(DataRow row)
        {
            MilkSale sale = new MilkSale();
            //milkSale.PastureID, milkSale.MilkDate.Date, milkSale.Amount, milkSale.MilkWeight, 
            //不可空字段
            sale.ID = Convert.ToInt32(row["ID"]);
            sale.PastureID = Convert.ToInt32(row["PastureID"]);
            sale.MilkDate = Convert.ToDateTime(row["MilkDate"]);
            sale.Amount = Convert.ToSingle(row["Amount"]);
            sale.MilkWeight = Convert.ToSingle(row["MilkWeight"]);

            //可空字段
            
            if (row["Company"]!=DBNull.Value)
            {
                sale.Company = row["Company"].ToString();
            }
            if (row["ShipCode"] != DBNull.Value)
            {
                sale.ShipCode = row["ShipCode"].ToString();
            }
            if (row["Decoding"] != DBNull.Value)
            {
                sale.Decoding = row["Decoding"].ToString();
            }
            if (row["TankerNum"] != DBNull.Value)
            {
                sale.TankerNum = row["TankerNum"].ToString();
            }
            if (row["TruckNum"] != DBNull.Value)
            {
                sale.TruckNum = row["TruckNum"].ToString();
            }

            if (row["Fat"] != DBNull.Value)
            {
                sale.Fat = Convert.ToSingle(row["Fat"]);
            }
            if (row["Protein"] != DBNull.Value)
            {
                sale.Protein = Convert.ToSingle(row["Protein"]);
            }
            if (row["DryMatter"] != DBNull.Value)
            {
                sale.DryMatter = Convert.ToSingle(row["DryMatter"]);
            }
            if (row["NonFatSolid"] != DBNull.Value)
            {
                sale.NonFatSolid = Convert.ToSingle(row["NonFatSolid"]);
            }
            if (row["Microbe"] != DBNull.Value)
            {
                sale.Microbe = Convert.ToSingle(row["Microbe"]);
            }
            if (row["Lactose"] != DBNull.Value)
            {
                sale.Lactose = Convert.ToSingle(row["Lactose"]);
            }
            if (row["IcePoint"] != DBNull.Value)
            {
                sale.IcePoint = Convert.ToSingle(row["IcePoint"]);
            }
            if (row["Acidity"] != DBNull.Value)
            {
                sale.Acidity = Convert.ToSingle(row["Acidity"]);
            }
            return sale;        
           
        }

        /// <summary>
        /// 封装OtherMilk
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private OtherMilk WrapOtherMilkItem(DataRow row)
        {
            OtherMilk other = new OtherMilk();
            other.ID = Convert.ToInt32(row["ID"]);
            other.PastureID = Convert.ToInt32(row["PastureID"]);
            other.MilkDate = Convert.ToDateTime(row["MilkDate"]);
            other.MilkForCalf = Convert.ToSingle(row["MilkForCalf"]);
            other.AbnormalSaleMilk = Convert.ToSingle(row["AbnormalSaleMilk"]);
            other.BadMilk = Convert.ToSingle(row["BadMilk"]);
            other.LeftMilk = Convert.ToSingle(row["LeftMilk"]);
            return other;
        }

        /// <summary>
        /// 获取牧场某天售奶记录
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public List<MilkSale> GetMilkSaleList(int pastureID,DateTime date)
        {
            List<MilkSale> list = new List<MilkSale>();
            DataTable table = dalMilk.GetMilkSaleRecords(pastureID,date);
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapMilkSaleItem(item));
            }
            return list;
        }

        /// <summary>
        /// 获取牧场某天其他奶单个记录
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public OtherMilk GetOtherMilk(int pastureID,DateTime date)
        {
            OtherMilk other = null;
            List<OtherMilk> listOther = GetOtherMilkList(pastureID,date);
            if (listOther.Count == 1)
            {
                other= listOther[0];
            }
            return other;
        }
    }

    /// <summary>
    /// 牧场日奶量
    /// </summary>
    public class MilkRecord
    {
        private MilkRecordBLL milkBLL = new MilkRecordBLL();
        public MilkRecord(int pastureID, DateTime date)
        {
            this.PastureID = pastureID;
            this.MilkDate = date.Date;
            this.MilkSales = milkBLL.GetMilkSaleList(pastureID, this.MilkDate);
            this.OtherMilkRecord = milkBLL.GetOtherMilk(pastureID, this.MilkDate);
        }
        public string MilkDateStr
        {
            get
            {
                return MilkDate.ToShortDateString();
            }
        }
        public float MilkForCalf
        {
            get
            {
                if (OtherMilkRecord==null)
                {
                    return 0;
                }
                else
                {
                    return OtherMilkRecord.MilkForCalf;
                }
                
            }
        }
        public float AbnormalMilk
        {
            get
            {
                if (OtherMilkRecord == null)
                {
                    return 0;
                }
                else
                {
                    return OtherMilkRecord.AbnormalSaleMilk;
                }
                
            }
        }
        public float BadMilk
        {
            get
            {
                if (OtherMilkRecord == null)
                {
                    return 0;
                }
                else
                {
                    return OtherMilkRecord.BadMilk;
                }
                
            }
        }
        public float LeftMilk
        {
            get
            {
                if (OtherMilkRecord == null)
                {
                    return 0;
                }
                else
                {
                    return OtherMilkRecord.LeftMilk;
                }
                
            }
        }

        
        /// <summary>
        /// 牧场ID
        /// </summary>
        public int PastureID { get; private set; }
        /// <summary>
        /// 挤奶售奶日期
        /// </summary>
        public DateTime MilkDate { get; private set; }

        public List<MilkSale> MilkSales { get; private set; }

        public OtherMilk OtherMilkRecord { get; private set; }
        /// <summary>
        /// 总奶量
        /// </summary>
        public float TotalWeight
        {
            get
            {
                float weight = 0.0f;
                foreach (MilkSale item in MilkSales)
                {
                    weight = weight + item.MilkWeight;
                }
                weight = weight + AbnormalMilk + BadMilk + MilkForCalf + LeftMilk;
                return weight;
            }
        }

        /// <summary>
        /// 总销售金额
        /// </summary>
        public float TotalAmount
        {
            get
            {
                float t = 0.0f;
                foreach (MilkSale item in MilkSales)
                {
                    t = t + item.Amount;
                }
                return t;
            }
        }

        /// <summary>
        /// 总销售奶量
        /// </summary>
        public float TotalMilkSale
        {
            get
            {
                float t = 0.0f;
                foreach (MilkSale item in MilkSales)
                {
                    t = t + item.MilkWeight;
                }
                return t;
            }
        }

    }
}
