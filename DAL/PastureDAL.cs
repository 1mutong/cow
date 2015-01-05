using DairyCow.DAL.Base;
using System.Data;

namespace DairyCow.DAL
{
    public class PastureDAL:BaseDAL
    {
        public DataTable GetPastureTable()
        {
            string sql = string.Format(@"SELECT ID,Name,IsActive FROM [BASE_PASTURE]");
            return dataProvider1mutong.FillDataTable(sql, CommandType.Text);            
        }

        public int InsertPasture(string name)
        {
            string sql = string.Format(@"INSERT [BASE_PASTURE] ([Name],[IsActive]) values('{0}',1)", name);
            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }

        public int UpdatePastureActiveStatus(int id,bool isActive)
        {
            string sql;

            if (isActive)
            {
                sql = string.Format(@"update [BASE_PASTURE] SET IsActive=1 WHERE ID={0} ", id);
            }
            else
            {
                sql = string.Format(@"update [BASE_PASTURE] SET IsActive=0 WHERE ID={0} ", id);
            }

            return dataProvider1mutong.ExecuteNonQuery(sql, CommandType.Text);
        }
    }
}
