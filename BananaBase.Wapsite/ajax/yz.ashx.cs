using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// yz 的摘要说明
    /// </summary>
    public class yz : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            try
            {
                string y = context.Request.QueryString["y"].ToString();
                int orderid = Convert.ToInt32(context.Request.QueryString["oid"].ToString());
                if (context.Request.Cookies["banana_shop_yzm"+orderid] == null)
                {
                    result = "-2";
                }
                else
                {
                    HttpCookie cookie = Cookie.Get("banana_shop_yzm" + orderid);
                    if (cookie.Values["yzm"].ToString() == y)
                    {
                        //验证成功，更改订单状态
                        if (orderid <= 0)
                            result = "0";
                        else
                        {
                            if (new Bll.Db.OrderBll().Update("status=1", "", " id=" + orderid).ResultStatus.Success)
                            {
                                result = "1";
                                if (context.Request.Cookies["AdminInfo"] != null)
                                {
                                    var BananaCount = new Bll.Db.OrderBll().GetByPrimaryKey(orderid).Entity.BananaCount;
                                    HttpCookie cookieadmin = Cookie.Get("AdminInfo");
                                    var userId = HttpUtility.UrlDecode(cookieadmin.Values["AdminId"]).ToInt();
                                   BananaCount= new Bll.Db.UsersBll().GetByPrimaryKey(userId).Entity.BananaCount - BananaCount;
                                   new Bll.Db.UsersBll().Update("BananaCount=" + BananaCount, "", "id=" + userId);

                                }

                            }

                            else
                            {
                                result = "0";
                            }
                            
                        }
                    }
                    else
                        result = "0";

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