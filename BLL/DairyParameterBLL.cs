using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DairyCow.DAL;
using DairyCow.Model;

namespace DairyCow.BLL
{
    public class DairyParameterBLL
    {
        /// <summary>
        /// 获取牧场参数list
        /// </summary>
        /// <param name="pastureID">牧场ID</param>
        /// <returns></returns>
        public static List<DairyParameter> GetDairyParameters(int pastureID)
        {
            List<DairyParameter> list = new List<DairyParameter>();
            DairyParameterDAL parameterDAL = new DairyParameterDAL();
            DataTable table = parameterDAL.GetPastureParameters(pastureID);
            foreach (DataRow item in table.Rows)
            {
                list.Add(WrapDairyParameterItem(item));
            }
            return list;
        }

        public static DairyParameter WrapDairyParameterItem(DataRow row)
        {
            DairyParameter p = new DairyParameter();
            p.PastureID = Convert.ToInt32(row["PastureID"]);
            p.ParameterName = row["ParameterName"].ToString();
            p.ParameterValue = Convert.ToSingle(row["ParameterValue"]);
            int temp = Convert.ToInt32(row["CanBeConfiguredByPasture"]);
            if (temp>0)
            {
                p.CanBeConfiguredByPasture = true;
            }
            else
            {
                p.CanBeConfiguredByPasture = false;
            }
            temp = Convert.ToInt32(row["CanBeConfiguredByAdmin"]);
            if (temp > 0)
            {
                p.CanBeConfiguredByAdmin = true;
            }
            else
            {
                p.CanBeConfiguredByAdmin = false;
            }
            p.Description = row["Description"].ToString();
            return p;
        }
        /// <summary>
        /// 获取牧场参数字典
        /// </summary>
        /// <param name="pastureID"></param>
        /// <returns></returns>
        public static Dictionary<string,float> GetCurrentParameterDictionary(int pastureID)
        {
            Dictionary<string, float> myDictionary = new Dictionary<string, float>();
            List<DairyParameter> list = GetDairyParameters(pastureID);
            foreach (DairyParameter item in list)
            {
                myDictionary.Add(item.ParameterName, item.ParameterValue);
            }
            return myDictionary;
        }

        /// <summary>
        /// 更新牧场参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static int UpdatePastureParameter(DairyParameter parameter)
        {
            int i=0;
            DairyParameterDAL dal=new DairyParameterDAL();
            if (parameter.CanBeConfiguredByPasture)
            {
                i = dal.UpdatePastureParameter(parameter.PastureID, parameter.ParameterName, parameter.ParameterValue);
            }
            return i;
        }

        public static void CopyPlatformParameters(int pastureID)
        {
            DairyParameterDAL dal = new DairyParameterDAL();
            dal.CopyParameters(pastureID);
        }
    }
}
