using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigHelper
    {
        public static string DBConnStr1mutong
        {
            get
            {
                return GetConnectionString("DBConnStr1mutong");
            }
        }

        #region Common

        /// <summary>
        /// 获取配置文件中的数据库连接字符串
        /// </summary>
        /// <param name="connStrName">连接字符串名称</param>
        /// <returns>连接字符串</returns>
        private static string GetConnectionString(string connStrName)
        {
            string connStr = null;

            if (ConfigurationManager.ConnectionStrings[connStrName] != null)
            {
                connStr = ConfigurationManager.ConnectionStrings[connStrName].ToString();
            }

            return connStr;
        }

        /// <summary>
        /// 获取配置文件中的应用程序设置
        /// </summary>
        /// <param name="appSettingName">应用程序设置名称</param>
        /// <returns>应用程序设置内容</returns>
        private static string GetAppSettings(string appSettingName)
        {
            string appSetting = null;

            if (ConfigurationManager.AppSettings[appSettingName] != null)
            {
                appSetting = ConfigurationManager.AppSettings[appSettingName].ToString();
            }

            return appSetting;
        }

        #endregion Common
    }
}
