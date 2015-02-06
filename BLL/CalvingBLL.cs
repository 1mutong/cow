using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.Model;
using DairyCow.DAL;

namespace DairyCow.BLL
{
    public partial class CalvingBLL
    {
        CalvingDAL calvingDAL = new CalvingDAL();
        /// <summary>
        /// 出入产犊记录，包含全部信息。
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int InsertCalving(Calving c)
        {
            return calvingDAL.InsertCalvingRecord(c.EarNum, c.Birthday, Convert.ToInt32(c.BirthType), c.Difficulty, c.PositionOfFetus, c.FatherSememNum, c.OperatorID, c.Comment, c.NumberOfMale, c.NumberOfFemale, (c.InParityCount?1:0));
        }

        public int InsertFakeCalvings(int earNum, DateTime lastDate,int count)
        {
            int temp = 0;
            for (int i = 0; i < count; i++)
            {
                temp = temp + calvingDAL.InsertCalvingRecord(earNum, lastDate, 0, "NA", "NA", "NA", 0, "", 1, 0, 1);
                lastDate = lastDate.AddMonths(-12);
            }
            return temp;
        }
    }
}
