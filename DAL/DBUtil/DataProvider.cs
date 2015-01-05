using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DairyCow.DAL.DBUtil
{
    public class DataProvider
    {
        #region ==== Field ====

        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        #endregion

        #region ==== Method ====

        #region 私有方法

        /// <summary>
        /// SqlCommand 对象执行SQL脚本前的准备工作
        /// </summary>
        /// <param name="cmd">SqlCommand 对象</param>
        /// <param name="conn">SqlConnection 对象</param>
        /// <param name="trans">SqlTransaction 对象</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdParms">SqlCommand 对象使用的 SqlParameter 参数集合</param>
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans,
            CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 600;
            cmd.CommandType = cmdType;

            if (trans != null)
                cmd.Transaction = trans;

            if (cmdParms != null)
            {
                foreach (SqlParameter param in cmdParms)
                    cmd.Parameters.Add(param);
            }
        }

        /// <summary>
        /// SqlDataAdapter 对象使用前的准备工作
        /// </summary>
        /// <param name="adapter">SqlDataAdapter 对象</param>
        /// <param name="conn">SqlConnection 对象</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdParms">SqlCommand 对象使用的 SqlParameter 参数集合</param>
        private void PrepareAdapter(SqlDataAdapter adapter, SqlConnection conn,
            CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter param in cmdParms)
                    cmd.Parameters.Add(param);
            }
            adapter.SelectCommand = cmd;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 对连接执行 SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string cmdText, CommandType cmdType)
        {
            return ExecuteNonQuery(cmdText, cmdType, null);
        }

        /// <summary>
        /// 对连接执行 SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="parameters">SqlParameter 参数集合</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                int val = cmd.ExecuteNonQuery();
                if (parameters != null)
                {
                    cmd.Parameters.Clear();
                }

                return val;
            }
        }

        /// <summary>
        /// 对连接执行多条 SQL 语句，并加入事务处理
        /// </summary>
        /// <param name="cmdTexts">SQL 语句数组</param>
        public bool ExecuteNonQueryWithTransaction(string[] cmdTexts)
        {
            bool execSucceed = false;
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    foreach (string sql in cmdTexts)
                    {
                        PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    execSucceed = true;
                }
                catch
                {
                    trans.Rollback();
                    conn.Close();
                }
            }
            return execSucceed;
        }

        /// <summary>
        /// 对连接执行多条 SQL 语句，并加入事务处理
        /// </summary>
        /// <param name="commands">SQL命令数组。
        /// Command 封装了 SqlCommand 对象需要的 CommandText、CommandType、SqlParameterCollection，以便分别执行每一组SQL脚本</param>
        public bool ExecuteNonQueryWithTransaction(Command[] commands)
        {
            bool execSucceed = false;
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    foreach (Command command in commands)
                    {
                        PrepareCommand(cmd, conn, trans, command.CommandType, command.CommandText, command.Parameters);
                        cmd.ExecuteNonQuery();
                        if (command.Parameters != null)
                        {
                            cmd.Parameters.Clear();
                        }
                    }
                    trans.Commit();
                    execSucceed = true;
                }
                catch
                {
                    trans.Rollback();
                    conn.Close();
                }
            }
            return execSucceed;
        }

        /// <summary>
        /// 执行SQL脚本，返回查询得到的 DataReader 结果集
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <returns>DataReader 结果集</returns>
        public SqlDataReader ExecuteReader(string cmdText, CommandType cmdType)
        {
            return ExecuteReader(cmdText, cmdType, null);
        }

        /// <summary>
        /// 执行SQL脚本进行查询，返回得到的 DataReader 结果集
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="parameters">SqlParameter 参数集合</param>
        /// <returns>DataReader 结果集</returns>
        public SqlDataReader ExecuteReader(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (parameters != null)
                {
                    cmd.Parameters.Clear();
                }

                return reader;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <returns>结果集中第一行的第一列</returns>
        public object ExecuteScalar(string cmdText, CommandType cmdType)
        {
            return ExecuteScalar(cmdText, cmdType, null);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="parameters">SqlParameter 参数集合</param>
        /// <returns>结果集中第一行的第一列</returns>
        public object ExecuteScalar(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                object val = cmd.ExecuteScalar();
                if (parameters != null)
                {
                    cmd.Parameters.Clear();
                }

                return val;
            }
        }

        /// <summary>
        /// 执行查询，将查询结果填充到 DataSet 并返回
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <returns>查询结果集</returns>
        public DataSet FillDataSet(string cmdText, CommandType cmdType)
        {
            return FillDataSet(cmdText, cmdType, null);
        }

        /// <summary>
        /// 执行查询，将查询结果填充到 DataSet 并返回
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="parameters">SqlParameter 参数集合</param>
        /// <returns>查询结果集</returns>
        public DataSet FillDataSet(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareAdapter(adapter, conn, cmdType, cmdText, parameters);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                return dataSet;
            }
        }

        /// <summary>
        /// 执行查询，将查询结果填充到 DataTable 并返回
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <returns>查询结果集</returns>
        public DataTable FillDataTable(string cmdText, CommandType cmdType)
        {
            return FillDataTable(cmdText, cmdType, null);
        }

        /// <summary>
        /// 执行查询，将查询结果填充到 DataTable 并返回
        /// </summary>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="parameters">SqlParameter 参数集合</param>
        /// <returns>查询结果集</returns>
        public DataTable FillDataTable(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareAdapter(adapter, conn, cmdType, cmdText, parameters);
                DataTable table = new DataTable();
                try
                {
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return table;
            }
        }

        /// <summary>
        /// 执行只返回一条记录的查询，把返回的记录反射成一个实体对象，实体对象类型由传入的类型决定。
        /// 传入的类型和返回类型要一致
        /// </summary>
        /// <typeparam name="T">要反射封装的对象实体类型。必须和返回类型一致</typeparam>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <returns>封装好的对象实体。必须和传入参数类型一致</returns>
        public T ReflectObject<T>(string cmdText, CommandType cmdType)
        {
            return ReflectObject<T>(cmdText, cmdType, null);
        }

        /// <summary>
        /// 执行只返回一条记录的查询，把返回的记录反射成一个实体对象，实体对象类型由传入的类型决定。
        /// 传入的类型和返回类型要一致
        /// </summary>
        /// <typeparam name="T">要反射封装的对象实体类型。必须和返回类型一致</typeparam>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="parameters">SqlParameter 参数集合</param>
        /// <returns>封装好的对象实体。必须和传入参数类型一致</returns>
        public T ReflectObject<T>(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            T obj = default(T);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        obj = (T)Activator.CreateInstance(typeof(T));
                        Type type = obj.GetType();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (!reader.IsDBNull(i))
                            {
                                try
                                {
                                    type.InvokeMember(reader.GetName(i), BindingFlags.Default | BindingFlags.SetProperty, null, obj, new object[] { reader.GetValue(i) });
                                }
                                catch (MissingMemberException exception)
                                {
                                    //Column/Property names don't match, thus throwing an exception. Ignored
                                    System.Diagnostics.Debug.WriteLine(exception.Message);
                                }
                            }
                        }

                        reader.Close();
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 执行查询，把返回的记录集反射成一个实体对象集合，实体对象类型由传入的类型决定。
        /// 传入的类型和返回类型要一致
        /// </summary>
        /// <typeparam name="T">要反射封装的对象实体类型。必须和返回类型一致</typeparam>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <returns>封装好的实体集合。必须和传入参数类型一致</returns>
        public List<T> ReflectCollection<T>(string cmdText, CommandType cmdType)
        {
            return ReflectCollection<T>(cmdText, cmdType, null);
        }

        /// <summary>
        /// 执行查询，把返回的记录集反射成一个实体对象集合，实体对象类型由传入的类型决定。
        /// 传入的类型和返回类型要一致
        /// </summary>
        /// <typeparam name="T">要反射封装的对象实体类型。必须和返回类型一致</typeparam>
        /// <param name="cmdText">SQL脚本</param>
        /// <param name="cmdType">命令类型：存储过程或普通SQL脚本</param>
        /// <param name="parameters">SqlParameter 参数集合</param>
        /// <returns>封装好的实体集合。必须和传入参数类型一致</returns>
        public List<T> ReflectCollection<T>(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    List<T> objList = new List<T>();

                    while (reader.Read())
                    {
                        T obj = (T)Activator.CreateInstance(typeof(T));
                        Type type = obj.GetType();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (!reader.IsDBNull(i))
                            {
                                try
                                {
                                    type.InvokeMember(reader.GetName(i), BindingFlags.Default | BindingFlags.SetProperty, null, obj, new object[] { reader.GetValue(i) });
                                }
                                catch (MissingMemberException exception)
                                {
                                    //Column/Property names don't match, thus throwing an exception. Ignored
                                    System.Diagnostics.Debug.WriteLine(exception.Message);
                                }
                            }
                        }
                        objList.Add(obj);
                    }

                    reader.Close();
                    return objList;
                }
            }
        }

        #endregion

        #endregion
    }
}
