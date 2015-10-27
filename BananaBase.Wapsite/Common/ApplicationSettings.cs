using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banana.Wapsite
{
    public class ApplicationSettings
    {
        /// <summary>
        /// 获取web.config的配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];		// for .net 2.0
        }
    }
}