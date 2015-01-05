using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.BLL
{
    public class GradeBLL
    {
        GradeDAL dalGrade = new GradeDAL();
        public List<Grade> GetGradeList()
        {
            List<Grade> lstGrade = new List<Grade>();
            DataTable datGrade = this.dalGrade.GetGradeList();
            foreach (DataRow drGrade in datGrade.Rows)
            {
                Grade gradeItem = WrapGradeItem(drGrade);
                lstGrade.Add(gradeItem);
            }
            return lstGrade;
        }
        public Grade GetGradeList(int earNum)
        {
            Grade gradeInfo = new Grade();
            DataTable datGrade = this.dalGrade.GetGradeList(earNum);
            if (datGrade != null && datGrade.Rows.Count == 1)
            {
                gradeInfo = WrapGradeItem(datGrade.Rows[0]);
            }
            return gradeInfo;
        }
        private Grade WrapGradeItem(DataRow gradeRow)
        {
            Grade gradeItem = new Grade();
            if (gradeRow != null)
            {
                gradeItem.EarNum = Convert.ToInt32(gradeRow["EarNum"]);
                gradeItem.DisplayEarNum = CowBLL.ConvertEarNumToDisplayEarNum(gradeItem.EarNum);
                if (Convert.ToInt32(gradeRow["Height"]) != 0)
                {
                    gradeItem.Height = Convert.ToInt32(gradeRow["Height"]);
                }
                if (Convert.ToInt32(gradeRow["Weight"]) != 0)
                {
                    gradeItem.Weight = Convert.ToInt32(gradeRow["Weight"]);
                }
                if (Convert.ToInt32(gradeRow["Chest"]) != 0)
                {
                    gradeItem.Chest = Convert.ToInt32(gradeRow["Chest"]);
                }
                if (Convert.ToInt32(gradeRow["Score"]) != 0)
                {
                    gradeItem.Score = Convert.ToInt32(gradeRow["Score"]);
                }
                gradeItem.Description = gradeRow["Description"].ToString();
                gradeItem.MeasureDate = Convert.ToDateTime(gradeRow["MeasureDate"]);
                if (Convert.ToInt32(gradeRow["Measurer"]) != 0)
                {
                    gradeItem.Measurer = Convert.ToInt32(gradeRow["Measurer"]);
                }
            }
            return gradeItem;
        }
        public int InsertGradeInfo(Grade grade)
        {
            return dalGrade.InsertGradeInfo(grade);
        }
    }
}
