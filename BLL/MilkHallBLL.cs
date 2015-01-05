using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyCow.BLL
{
    public class MilkHallBLL
    {
        MilkHallDAL dalMilkHall = new MilkHallDAL();

        public List<MilkHall> GetMilkHallList()
        {
            List<MilkHall> milkHallList = new List<MilkHall>();
            DataTable milkHallData = dalMilkHall.GetMilkHallList();
            foreach (DataRow drCow in milkHallData.Rows)
            {
                MilkHall cowItem = WrapMilkHallItem(drCow);
                milkHallList.Add(cowItem);
            }
            return milkHallList;
        }

        public MilkHall GetMilkHallByID()
        {
            MilkHall milkHallInfo = new MilkHall();
            DataTable milkHallData = dalMilkHall.GetMilkHallByID();
            if (milkHallData != null && milkHallData.Rows.Count == 1)
            {
                milkHallInfo = WrapMilkHallItem(milkHallData.Rows[0]);
            }
            return milkHallInfo;
        }

        public int UpdateMilkHallInfo(MilkHall milkHall)
        {
            return dalMilkHall.UpdateMilkHallInfo(milkHall);
        }

        private MilkHall WrapMilkHallItem(DataRow milkHallRow)
        {
            MilkHall milkHallItem = new MilkHall();
            if (milkHallRow != null)
            {
                milkHallItem.ID = Convert.ToInt32(milkHallRow["ID"]);
                //milkHallItem.Name = milkHallRow["Name"].ToString();
                //milkHallItem.PastureID = Convert.ToInt32(milkHallRow["PastureID"]);
                if (milkHallRow["VacuumPressure"] != null
                    && !string.IsNullOrWhiteSpace(milkHallRow["VacuumPressure"].ToString()))
                {
                    milkHallItem.VacuumPressure = float.Parse(milkHallRow["VacuumPressure"].ToString());
                }
                if (milkHallRow["Pulsation"] != null && !string.IsNullOrWhiteSpace(milkHallRow["Pulsation"].ToString()))
                {
                    milkHallItem.Pulsation = Convert.ToInt32(milkHallRow["Pulsation"]);
                }
                if (milkHallRow["CleanupCount"] != null)
                {
                    milkHallItem.CleanupCount = Convert.ToInt32(milkHallRow["CleanupCount"]);
                }
            }
            return milkHallItem;
        }


    }
}
