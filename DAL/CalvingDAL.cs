using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyCow.DAL.Base;
using System.Data;


namespace DairyCow.DAL
{
    /// <summary>
    /// 产犊DAL
    /// </summary>
    public partial class CalvingDAL:BaseDAL
    {

        public int InsertCalvingRecord(int earNum,DateTime birthday,int birthType,string difficulty,string positionOfFetus,string fatherSemenNum,int operatorID,string comment,int numberOfMale,int numberOfFemale,int inParityCount)
        {
            string sql = string.Format(@"Insert  Base_Calving 
                                       (EarNum,
                                       Birthday,
                                        BirthType,
                                        Difficulty,
                                        PositionOfFetus,
                                        FatherSemenNum,
                                        OperatorID,
                                        Comment,
                                        NumberOfMale,
                                        NumberOfFemale,
                                        InParityCount) Values({0},'{1}',{2},'{3}','{4}','{5}',{6},'{7}',{8},{9},{10})",earNum, birthday.Date.ToShortDateString(), birthType, difficulty, positionOfFetus, fatherSemenNum, operatorID, comment, numberOfMale, numberOfFemale, inParityCount);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }


        /// <summary>
        /// 获取产犊记录表,流产、早产算胎次的计入
        /// </summary>
        /// <param name="earNum">牛耳号（实际为系统编号）</param>
        /// <returns>产犊记录表</returns>
        public DataTable GetCowCalvingRecords(int earNum,bool InParityCountOnly)
        {
            
           
            
            DataTable calvingRecord = null;
            string sql;
            if (InParityCountOnly)
            {
                sql = string.Format(@"SELECT D.EarNum,
                                        D.Birthday,
                                        D.BirthType,
                                        D.Difficulty,
                                        D.PositionOfFetus,
                                        D.FatherSemenNum,
                                        D.OperatorID,
                                        D.Comment,
                                        D.NumberOfMale,
                                        D.NumberOfFemale,
                                        D.InParityCount,
                                        U.Name AS OperatorName
                                        FROM Base_Calving AS D Left Join [Auth_User] AS U ON D.OperatorID=U.ID
                                        WHERE D.InParityCount=1 AND D.EarNum={0} ORDER BY D.Birthday DESC ", earNum);
            }
            else
            {
                sql = string.Format(@"SELECT D.EarNum,
                                        D.Birthday,
                                        D.BirthType,
                                        D.Difficulty,
                                        D.PositionOfFetus,
                                        D.FatherSemenNum,
                                        D.OperatorID,
                                        D.Comment,
                                        D.NumberOfMale,
                                        D.NumberOfFemale,
                                        D.InParityCount,
                                        U.Name AS OperatorName
                                        FROM Base_Calving AS D Left Join [Auth_User] AS U ON D.OperatorID=U.ID
                                        WHERE D.EarNum={0}", earNum);
            }
            DateTime t = DateTime.Now;
            calvingRecord = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            double ss = DateTime.Now.Subtract(t).TotalSeconds;
            return calvingRecord;
        }

        /// <summary>
        /// 获取某日期牧场所产公犊数
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public int GetPastureMaleCalfNumber(int pastureID,DateTime date)
        {
            int sum=0;
            string sql = string.Format(@"SELECT SUM(NumberOfMale) as NumberOfMaleSum
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}
                                        and DAtepart(MM,Base_Cow.BirthDate)={2}
                                        and DAtepart(DD,dbo.Base_Cow.BirthDate)={3}", 
                                        pastureID,date.Year,date.Month,date.Day);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["NumberOfMaleSum"]!=DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["NumberOfMaleSum"]);
            }
            return sum;
        }


        /// <summary>
        /// 获取某日期牧场所产母犊数
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public int GetPastureFemaleCalfNumber(int pastureID, DateTime date)
        {
            int sum = 0;
            string sql = string.Format(@"SELECT SUM(NumberOfFemale) as NumberOfFemaleSum
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}
                                        and DAtepart(MM,Base_Cow.BirthDate)={2}
                                        and DAtepart(DD,dbo.Base_Cow.BirthDate)={3}",
                                        pastureID, date.Year, date.Month, date.Day);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["NumberOfFemaleSum"] != DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["NumberOfFemaleSum"]);
            }
            return sum;
        }

     
        /// <summary>
        /// 获取牧场某年某月母犊数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetPastureFemaleCalfNumber(int pastureID, int year,int month)
        {
            int sum = 0;
            string sql = string.Format(@"SELECT SUM(NumberOfFemale) as NumberOfFemaleSum
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}
                                        and DAtepart(MM,Base_Cow.BirthDate)={2}",
                                        pastureID, year, month);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["NumberOfFemaleSum"] != DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["NumberOfFemaleSum"]);
            }
            return sum;
        }

        /// <summary>
        /// 获取牧场某年母犊数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetPastureFemaleCalfNumber(int pastureID, int year)
        {
            int sum = 0;
            string sql = string.Format(@"SELECT SUM(NumberOfFemale) as NumberOfFemaleSum
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}",
                                        pastureID, year);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["NumberOfFemaleSum"] != DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["NumberOfFemaleSum"]);
            }
            return sum;
        }

        /// <summary>
        /// 获取牧场某年某月公犊数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetPastureMaleCalfNumber(int pastureID, int year, int month)
        {
            int sum = 0;
            string sql = string.Format(@"SELECT SUM(NumberOfMale) as NumberOfMaleSum
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}
                                        and DAtepart(MM,Base_Cow.BirthDate)={2}",
                                        pastureID, year, month);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["NumberOfMaleSum"] != DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["NumberOfMaleSum"]);
            }
            return sum;
        }

        /// <summary>
        /// 获取牧场某年公犊数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetPastureMaleCalfNumber(int pastureID, int year)
        {
            int sum = 0;
            string sql = string.Format(@"SELECT SUM(NumberOfMale) as NumberOfMaleSum
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}",
                                        pastureID, year);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["NumberOfMaleSum"] != DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["NumberOfMaleSum"]);
            }
            return sum;
        }


        /// <summary>
        /// 获取某日期牧场产犊次数
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public int GetPastureCalvingNumber(int pastureID, DateTime date)
        {
            int sum = 0;
            string sql = string.Format(@"SELECT count(Base_Calving.ID) as CalvingCount
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}
                                        and DAtepart(MM,Base_Cow.BirthDate)={2}
                                        and DAtepart(DD,dbo.Base_Cow.BirthDate)={3}",
                                        pastureID, date.Year, date.Month, date.Day);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["CalvingCount"] != DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["CalvingCount"]);
            }
            return sum;
        }


        /// <summary>
        /// 获取牧场某年某月产犊次数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetPastureCalvingNumber(int pastureID, int year, int month)
        {
            int sum = 0;
            string sql = string.Format(@"SELECT count(Base_Calving.ID) as CalvingCount
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}
                                        and DAtepart(MM,Base_Cow.BirthDate)={2}",
                                        pastureID, year, month);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["CalvingCount"] != DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["CalvingCount"]);
            }
            return sum;
        }

        /// <summary>
        /// 获取牧场某年产犊次数
        /// </summary>
        /// <param name="pastureID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetPastureCalvingNumber(int pastureID, int year)
        {
            int sum = 0;
            string sql = string.Format(@"SELECT count(Base_Calving.ID) as CalvingCount
                                        FROM Base_Calving inner join dbo.Base_Cow on
                                        dbo.Base_Calving.EarNum=dbo.Base_Cow.EarNum
                                        where Base_Cow.FarmID={0} 
                                        and Datepart(YEAR,Base_Cow.BirthDate)={1}",
                                        pastureID, year);
            DataTable table = dataProvider1mutong.FillDataTable(sql, CommandType.Text);
            if (table.Rows[0]["CalvingCount"] != DBNull.Value)
            {
                sum = Convert.ToInt32(table.Rows[0]["CalvingCount"]);
            }
            return sum;
        }

    }
}
