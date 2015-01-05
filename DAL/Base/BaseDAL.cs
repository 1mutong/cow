using Common;
using DairyCow.DAL.DBUtil;
using System.Data;

namespace DairyCow.DAL.Base
{
    public abstract class BaseDAL
    {
        protected DataProvider dataProvider1mutong = new DataProvider();

        public BaseDAL()
        {
            dataProvider1mutong.ConnectionString = ConfigHelper.DBConnStr1mutong;
        }

        public void UpdateCowStatus(int ear, ECowStatus status)
        {
            string sql = "UPDATE Base_Cow SET Status='{0}' WHERE EarNum={1}";
            dataProvider1mutong.ExecuteNonQuery(string.Format(sql, (int)status, ear), CommandType.Text);
        }
    }
}
