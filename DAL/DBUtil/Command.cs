using System.Data;
using System.Data.SqlClient;

namespace DairyCow.DAL.DBUtil
{
    public class Command
    {
        #region Field

        private string _commandText;
        private CommandType _commandType;
        private SqlParameter[] _parameters;

        #endregion Field

        #region Property

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string CommandText
        {
            get { return _commandText; }
            set { _commandText = value; }
        }

        /// <summary>
        /// SQL命令类型
        /// </summary>
        public CommandType CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        }

        /// <summary>
        /// 参数集合
        /// </summary>
        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        #endregion Property
    }
}
