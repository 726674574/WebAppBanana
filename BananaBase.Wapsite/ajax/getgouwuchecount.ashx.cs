using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// getgouwuchecount 的摘要说明
    /// </summary>
    public class getgouwuchecount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int num = 0;
            if (HttpContext.Current.Request.Cookies["shop"] != null)
            {
                
                HttpCookie cookies = Cookie.Get("shop");
                var productlist = cookies.Values["productId"].ToString();
                if (productlist.Trim2().Length > 1)
                {
                    productlist = productlist.Substring(1, productlist.Length - 2);
                    string[] ids = productlist.Split(',');
                    foreach (string idStr in ids)
                    {
                        num++;
                    }
                }

            }
            
            context.Response.Write(num);


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