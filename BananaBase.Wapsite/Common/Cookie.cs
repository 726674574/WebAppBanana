using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banana.Wapsite
{
    public class Cookie
    {
        #region 获取Cookie值 public static HttpCookie Get(string name)
        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }
        #endregion

        #region 设置Cookie值 	public static HttpCookie Set(string name)
        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpCookie Set(string name)
        {
            return new HttpCookie(name);
        }
        #endregion

        #region 保存Cookie值 public static void Save(HttpCookie cookie)
        /// <summary>
        ///  保存Cookie值
        /// </summary>
        /// <param name="cookie"></param>
        public static void Save(HttpCookie cookie)
        {
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion

        #region 移除Cookie值 public static void Remove(HttpCookie cookie)
        /// <summary>
        ///移除Cookie值
        /// </summary>
        /// <param name="cookie"></param>
        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = new System.DateTime(1983, 5, 21);
                Save(cookie);
            }
        }
        #endregion

        #region 移除Cookie值 public static void Remove(string name)
        /// <summary>
        ///移除Cookie值
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            Remove(Get(name));
        }
        #endregion
    }
}