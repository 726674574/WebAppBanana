using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// order_add_shop 的摘要说明
    /// </summary>
    public class order_add_shop : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string result = "0";
            try
            {
                int productId = context.Request.QueryString["productId"].ToString().ToInt();
                context.Response.ContentType = "text/plain";

                if (context.Request.Cookies["shop"] == null)
                {
                    HttpCookie cookie = new HttpCookie("shop");
                    cookie.Values.Add("productId", "," + HttpUtility.UrlEncode(productId.ToString())+",");

                    //设定此cookies值
                    //设定cookie的生命周期
                    cookie.Expires = DateTime.Now.AddDays(7);
                    //加入此cookie
                    context.Response.AppendCookie(cookie);
                    result = "1";
                }
                else
                {
                    HttpCookie cookie = Cookie.Get("shop");
                    if (cookie.Values["productId"].ToString().IndexOf("," + productId.ToString() + ",")==-1)
                    {
                        cookie.Values["productId"] = cookie.Values["productId"].ToString() + HttpUtility.UrlEncode(productId.ToString()) + ",";
                        cookie.Expires = DateTime.Now.AddDays(7);
                        context.Response.AppendCookie(cookie);
                        result = "1";
                    }
                    
                }
            }
            catch
            {

                result = "-1";
            }
            context.Response.Write(result);
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