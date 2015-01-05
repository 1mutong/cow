using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyCow.BLL
{
    public class FormulaBLL
    {
        private FormulaDAL dalFormula = new FormulaDAL();
        CowGroupDAL groupDAL = new CowGroupDAL();
        private FodderBLL fBLL=new FodderBLL();       

        //public Dictionary<string, double> ExecFodder(int groupID, int machineNumber)
        //{
        //    Dictionary<string, double> ret = new Dictionary<string, double>();
        //    int cowCount = group.GetCowCount(groupID);
        //    DataTable dt = dalFormula.GetFormulaFodder(groupID);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ret.Add(dr["FodderName"].ToString(), Convert.ToDouble(dr["Quantity"]) * cowCount / machineNumber);
        //    }
        //    return ret;
        //}

        //返回值说明： 牛舍为key，value 的 list中按顺序为早中晚的喂的量。
        //public Dictionary<string, List<double>> ExecFeed(int groupID, int feedTimes, bool isSummer)
        //{
        //    DataTable dtCowCountInHouse = group.GetCowCountInHouse(groupID);
        //    DataTable dtFormularFodder = dalFormula.GetFormulaFodder(groupID);
            
        //    double quantity = 0;
        //    foreach(DataRow row in dtFormularFodder.Rows)
        //    {
        //        quantity += Convert.ToDouble(row["Quantity"]);
        //    }

        //    Dictionary<string, List<double>> houseFeed = new Dictionary<string, List<double>>();
        //    foreach(DataRow row in dtCowCountInHouse.Rows)
        //    {
        //        string houseName = row["Name"].ToString();
        //        int count = Convert.ToInt32(row["CowCount"]);
        //        List<double> dailyFeed = new List<double>();
        //        if(isSummer)
        //        {
        //            dailyFeed.Add(quantity * count * 0.35);
        //            dailyFeed.Add(quantity * count * 0.3);
        //            dailyFeed.Add(quantity * count * 0.35);
        //        }
        //        else
        //        {
        //            double oneTime = quantity * count / feedTimes;
        //            for (int i = 0; i < feedTimes; i++)
        //            {
        //                dailyFeed.Add(oneTime);
        //            }
        //        }
        //        if(!houseFeed.ContainsKey(houseName))
        //        {
        //            houseFeed.Add(houseName, dailyFeed);
        //        }                
        //    }

        //    return houseFeed;
        //}

        public List<Formula> GetFormulaList()
        {
            DataTable datFormula = this.dalFormula.GetFormulaList();
            List<Formula> lstFormula = new List<Formula>();

            foreach (DataRow formula in datFormula.Rows)
            {
                Formula formulaItem = WrapFormula(formula);
                lstFormula.Add(formulaItem);
            }

            return lstFormula;
        }

        public Formula GetFormulaByIDWithFodders(string formulaID)
        {
            Formula formula = new Formula();

            DataTable datFormula = this.dalFormula.GetFormulaInfoByID(formulaID);
            if (datFormula != null && datFormula.Rows.Count > 0)
            {
                List<Fodder> lstFormula = new List<Fodder>();
                foreach (DataRow darFormula in datFormula.Rows)
                {
                    lstFormula.Add(WrapFodderWithQuantityAndNutritionFact(darFormula));
                }

                formula.ID = Convert.ToInt32(datFormula.Rows[0]["FormulaID"]);
                formula.Name = datFormula.Rows[0]["FormulaName"].ToString();
                formula.FodderList = lstFormula;
            }

            return formula;
        }

        public Formula GetFormulaByID(int formulaID)
        {
            Formula formula = new Formula();

            DataTable datFormula = this.dalFormula.GetFormulaOnlyByID(formulaID.ToString());
            if (datFormula != null && datFormula.Rows.Count > 0)
            {
                formula.ID = Convert.ToInt32(datFormula.Rows[0]["ID"]);
                formula.Name = datFormula.Rows[0]["Name"].ToString();
            }

            return formula;
        }

        private Fodder WrapFodderWithQuantityAndNutritionFact(DataRow row)
        {
            //字段均不可空
            Fodder formulaItem = new Fodder()
                    {
                        ID = Convert.ToInt32(row["FodderID"]),
                        Name = row["FodderName"].ToString(),
                        Quantity = Convert.ToDouble(row["Quantity"]),
                        DM = Convert.ToDouble(row["DM"]),
                        NND = Convert.ToDouble(row["NND"]),
                        Ca = Convert.ToDouble(row["Ca"]),
                        P = Convert.ToDouble(row["P"]),
                        CP = Convert.ToDouble(row["CP"]),
                        Fat = Convert.ToDouble(row["Fat"]),
                        RefPrice = Convert.ToDouble(row["RefPrice"]),
                        CF = Convert.ToDouble(row["CF"]),
                        NFE = Convert.ToDouble(row["NFE"]),
                        ASH = Convert.ToDouble(row["ASH"]),
                        NDF = Convert.ToDouble(row["NDF"]),
                        ADF = Convert.ToDouble(row["ADF"]),
                        Arg = Convert.ToDouble(row["Arg"]),
                        His = Convert.ToDouble(row["His"]),
                        Ile = Convert.ToDouble(row["Ile"]),
                        Leu = Convert.ToDouble(row["Leu"]),
                        Lys = Convert.ToDouble(row["Lys"]),
                        Met = Convert.ToDouble(row["Met"]),
                        Cys = Convert.ToDouble(row["Cys"]),
                        Phe = Convert.ToDouble(row["Phe"]),
                        Tyr = Convert.ToDouble(row["Tyr"]),
                        Thr = Convert.ToDouble(row["Thr"]),
                        Trp = Convert.ToDouble(row["Trp"]),
                        Val = Convert.ToDouble(row["Val"]),
                        Na = Convert.ToDouble(row["Na"]),
                        Cl = Convert.ToDouble(row["Cl"]),
                        Mg = Convert.ToDouble(row["Mg"]),
                        K = Convert.ToDouble(row["K"]),
                        Fe = Convert.ToDouble(row["Fe"]),
                        Cu = Convert.ToDouble(row["Cu"]),
                        Mn = Convert.ToDouble(row["Mn"]),
                        Zn = Convert.ToDouble(row["Zn"]),
                        Se = Convert.ToDouble(row["Se"]),
                       
                    };
            return formulaItem;
        }

        public Formula WrapFormula(DataRow formulaRow)
        {
            Formula formulaItem = new Formula();
            if (formulaRow != null)
            {
                formulaItem.ID = Convert.ToInt32(formulaRow["ID"]);
                formulaItem.Name = formulaRow["Name"].ToString();
            }
            return formulaItem;
        }

        public int SaveFormula(string formulaID, string formulaName, List<Fodder> fodder)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(formulaID))
            {
                result = this.dalFormula.UpdateFormula(formulaID, formulaName, fodder);
            }
            else
            {
                result = this.dalFormula.InsertFormula(formulaName, fodder);
            }
            return result;
        }

        public void UpdateFormulaOfCowGroup(string formulaID, string cowGroupID)
        {
            this.dalFormula.UpdateFormulaOfCowGroup(formulaID, cowGroupID);
        }

        public int AddFodder(string formulaID, string fodderID, string fodderQuantity)
        {
            return this.dalFormula.AddFodder(formulaID, fodderID, fodderQuantity);
        }

        public int DeleteFodder(string formulaID, string fodderID)
        {
            return this.dalFormula.DeleteFodder(formulaID, fodderID);
        }

        public int UpdateFodderQuantity(string formulaID, string fodderID, string fodderQuantity)
        {
            return this.dalFormula.UpdateFodderQuantity(formulaID, fodderID, fodderQuantity);
        }

        public int AddFormula(string formulaName)
        {
            return this.dalFormula.AddFormula(formulaName);
        }
    }
}
