using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyCow.BLL
{
    public class CowGroupBLL
    {
        CowGroupDAL dalCowGroup = new CowGroupDAL();

        public List<CowGroup> GetCowGroupList()
        {
            List<CowGroup> lstCowGroup = new List<CowGroup>();
            DataTable datCowGroup = this.dalCowGroup.GetCowGroupTable();
            foreach (DataRow drCowGroup in datCowGroup.Rows)
            {
                CowGroup cowGroupItem = WrapCowGroupItem(drCowGroup);
                lstCowGroup.Add(cowGroupItem);
            }
            return lstCowGroup;
        }

        public List<CowGroup> GetCowGroupList(int pastureID)
        {
            List<CowGroup> lstCowGroup = new List<CowGroup>();
            DataTable datCowGroup = this.dalCowGroup.GetCowGroupTable(pastureID);
            foreach (DataRow drCowGroup in datCowGroup.Rows)
            {
                CowGroup cowGroupItem = WrapCowGroupItem(drCowGroup);
                lstCowGroup.Add(cowGroupItem);
            }
            return lstCowGroup;
        }

        public int UpdateCowGroupInsemOperator(int cowGroupID, int insemOperatorID)
        {
            return dalCowGroup.UpdateCowGroupInsemOperator(cowGroupID, insemOperatorID);
        }

        public int UpdateCowGroupFeeder(int cowGroupID,int feederID)
        {
            return dalCowGroup.UpdateCowGroupFeeder(cowGroupID, feederID);
        }

        public int UpdateCowGroupDoctor(int cowGroupID,int doctorID)
        {
            return dalCowGroup.UpdateCowGroupDoctor(cowGroupID,doctorID);
        }

        public int UpdateCowGroupFormula(int cowGroupID, int formulaID)
        {
            return dalCowGroup.UpdateCowGroupFormula(cowGroupID, formulaID);
        }

        private CowGroup WrapCowGroupItem(DataRow cowGroupRow)
        {
            CowGroup cowGroupItem = new CowGroup();
            if (cowGroupRow != null)
            {
                UserBLL u = new UserBLL();
                User user;
               
                cowGroupItem.ID = Convert.ToInt32(cowGroupRow["ID"]);
                cowGroupItem.Name = cowGroupRow["GroupName"].ToString();
                cowGroupItem.TypeNum = Convert.ToInt32(cowGroupRow["Type"]);
                cowGroupItem.Description = cowGroupRow["Description"].ToString();

                cowGroupItem.PastureID = Convert.ToInt32(cowGroupRow["PastureID"]);
                if (cowGroupRow.Table.Columns.Contains("PastureName"))
                {
                    cowGroupItem.PastureName = cowGroupRow["PastureName"].ToString();
                }
                
                if (cowGroupRow["FormulaID"] != DBNull.Value)
                {
                    cowGroupItem.FormulaID = Convert.ToInt32(cowGroupRow["FormulaID"]);
                }
                if (cowGroupRow["InsemOperatorID"] != DBNull.Value)
                {
                    cowGroupItem.InsemOperatorID = Convert.ToInt32(cowGroupRow["InsemOperatorID"]);
                }
                else
                {
                    cowGroupItem.InsemOperatorID = 0;//0表示没有人
                }
                user = u.GetUsers().Find(p => p.ID == cowGroupItem.InsemOperatorID);

                cowGroupItem.InsemOperatorName = user == null ? null : user.Name;

                if (cowGroupRow["FeedOperatorID"] != DBNull.Value)
                {
                    cowGroupItem.FeederID = Convert.ToInt32(cowGroupRow["FeedOperatorID"]);
                }
                else
                {
                    cowGroupItem.FeederID = 0;//0表示没有人
                }
                
                user = u.GetUsers().Find(p => p.ID == cowGroupItem.FeederID);
                cowGroupItem.FeederName = user == null? null: user.Name;
                if (cowGroupRow["DoctorID"] != DBNull.Value)
                {
                    cowGroupItem.DoctorID = Convert.ToInt32(cowGroupRow["DoctorID"]);
                }
                else
                {
                    cowGroupItem.DoctorID = 0;//0表示没有人
                }
                user = u.GetUsers().Find(p => p.ID == cowGroupItem.DoctorID);
                cowGroupItem.DoctorName = user == null ? null : user.Name;

                cowGroupItem.CowNumber = GetCowCount(cowGroupItem.ID);
                FormulaBLL fbBLL = new FormulaBLL();
                cowGroupItem.FormulaName = fbBLL.GetFormulaByID(cowGroupItem.FormulaID).Name;
            }
            return cowGroupItem;
        }

        //public List<CowGroup> GetCowGroupInfo()
        //{
        //    List<CowGroup> lstCowGroup = new List<CowGroup>();

        //    DataTable datCowGroup = this.dalCowGroup.GetCowGroupTable();
        //    foreach (DataRow row in datCowGroup.Rows)
        //    {
        //        CowGroup info = WrapCowGroupItem(row);
        //        lstCowGroup.Add(info);
        //    }

        //    return lstCowGroup;
        //}

        public int GetFormulaIDByGroupID(int id)
        {
            return dalCowGroup.GetFormulaIDByGroupID(id);
        }

        public CowGroup GetCowGroupInfo(int id)
        {
            CowGroup cowGroupItem = new CowGroup();
            DataTable datCowGroup = this.dalCowGroup.GetCowGroupTableByID(id);
            if (datCowGroup != null && datCowGroup.Rows.Count == 1)
            {
                DataRow cowGroupRow = datCowGroup.Rows[0];
                cowGroupItem = WrapCowGroupItem(cowGroupRow);
            }
            return cowGroupItem;
        }

        public int UpdateCowGroupInfo(CowGroup group)
        {
            return this.dalCowGroup.UpdateCowGroupInfo(group);
        }

        /// <summary>
        /// 增加一个群，输入基本信息。（不包含配方，配种员，饲养员和兽医）
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public int AddCowGroupWithBasicInfo(CowGroup group)
        {
            if (String.IsNullOrEmpty(group.Description))
	        {
		        group.Description=String.Empty;
	        }
            return dalCowGroup.InsertCowGroup(group.Name, group.PastureID, group.TypeNum, group.Description);
        }
        ///<summary>
        /// 增加一个群，输入完全信息,包含配方，配种员，饲养员和兽医。
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public int AddCowGroupWithFullInfo(CowGroup group)
        {
            if (String.IsNullOrEmpty(group.Description))
            {
                group.Description = String.Empty;
            }
            return dalCowGroup.InsertCowGroup(group.Name, group.PastureID, group.TypeNum, group.Description,group.FormulaID,group.InsemOperatorID,group.FeederID,group.DoctorID);
        }
        /// <summary>
        /// 删除某个群，必须牛群中不含任何牛。
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>返回删除行数，未删除返回0.</returns>
        public int DeleteCowGroup(int groupID)
        {
            return dalCowGroup.DeleteCowGroupByID(groupID);
        }
        /// <summary>
        /// 获取牛群牛数
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public int GetCowCount(int groupID)
        {
            return dalCowGroup.GetCowCount(groupID);
        }
    }
}
