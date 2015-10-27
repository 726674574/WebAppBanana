using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Entity.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// min_sc_delbyid 的摘要说明
    /// </summary>
    public class min_sc_delbyid : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "1";
            try
            {
                string type = context.Request.QueryString["type"].ToString();
                int id = context.Request.QueryString["id"].ToString().ToInt();

                UserCollectionBll ucbll = new UserCollectionBll();
                //用户末登录 返回 0 result=0;

                if (type == "add")
                {
                    //用户登录 id
                    if (Cookie.Get("AdminInfo") != null)
                    {
                        HttpCookie cookie = Cookie.Get("AdminInfo");
                        int userid = HttpUtility.UrlDecode(cookie.Values["AdminId"]).ToInt();
                        UserCollection model = new UserCollection();
                        model.UserId = userid;
                        model.AddTime = DateTime.Now;
                        model.Productid = id;
                        if (!ucbll.Add(model).ResultStatus.Success)
                            result = "-1";
                    }
                    else
                    {
                        //末登录
                        result = "-2";
                    }
                }
                else
                {
                    //用户登录 id
                    if (Cookie.Get("AdminInfo") != null)
                    {
                        HttpCookie cookie = Cookie.Get("AdminInfo");
                        int userid = HttpUtility.UrlDecode(cookie.Values["AdminId"]).ToInt();
                        if (!ucbll.DeleteList(" userid=" + userid + " and productid=" + id).ResultStatus.Success)
                            result = "-1";
                    }
                    else
                    {
                        //末登录
                        result = "-2";
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