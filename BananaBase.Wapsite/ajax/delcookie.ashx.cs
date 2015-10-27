using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// delcookie 的摘要说明
    /// </summary>
    public class delcookie : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int proId = context.Request["id"].ToInt();
            if (HttpContext.Current.Request.Cookies["shop"] != null)
            {
                HttpCookie cookies = Cookie.Get("shop");
                var productlist = cookies.Values["productId"].ToString();
                productlist = productlist.Replace(proId+",", "");
                cookies.Values["productId"] = productlist;
                cookies.Expires = DateTime.Now.AddDays(7);
                context.Response.AppendCookie(cookies);

            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}