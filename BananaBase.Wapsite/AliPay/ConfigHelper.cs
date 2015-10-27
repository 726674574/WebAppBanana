using System;

namespace Banana.Wapsite.AliPay
{
    /// <summary>
    /// web.config操作类
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetConfigString(string key, string defaultValue = "")
        {
            var o = System.Configuration.ConfigurationManager.AppSettings[key] ?? defaultValue;
            return o;
        }
    }

}