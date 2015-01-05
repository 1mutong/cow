using DairyCow.DAL;
using DairyCow.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DairyCow.BLL
{
    public class PastureBLL
    {
        private PastureDAL dalPasture = new PastureDAL();

        public List<Pasture> GetPastures()
        {
            List<Pasture> lstPasture = new List<Pasture>();

            DataTable datPasture = this.dalPasture.GetPastureTable();
            if (datPasture != null && datPasture.Rows.Count > 0)
            {
                foreach (DataRow drPasture in datPasture.Rows)
                {
                    lstPasture.Add(WrapPastureItem(drPasture));
                }
            }

            return lstPasture;
        }

        public Pasture WrapPastureItem(DataRow row)
        {
            Pasture p = new Pasture();
            p.ID = Convert.ToInt32(row["ID"].ToString());
            p.Name = row["Name"].ToString();
            int temp=Convert.ToInt32(row["IsActive"]);
            if (temp>0)
	        {
		        p.IsActive=true;
	        }
            else
            {
                p.IsActive=false;
            }
            return p;
        }

        public int AddPasture(string name)
        {
            return dalPasture.InsertPasture(name);
        }

        public int SetPastureActiveStatus(int id,bool isActive)
        {
            return dalPasture.UpdatePastureActiveStatus(id, isActive);
        }

        

        public Pasture GetPasture(string name)
        {
            List<Pasture> list = GetPastures().FindAll(p => p.Name.Equals(name));
            if (list.Count==1)
            {
                return list[0];
            }
            else
            {
                throw new Exception("叫这个名字的牧场有：" + list.Count.ToString() + "个。");
            }
        }
    }
}
