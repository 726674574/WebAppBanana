using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Entity.Db;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// social_tz_login 的摘要说明
    /// </summary>
    public class social_tz_login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "1";
            try
            {
               
                //发帖 登录
                if (Cookie.Get("AdminInfo") == null)
                {
                    //添加用户
                    string nickname = context.Request.Form["nickname"].ToString();
                    UsersBll ubll = new UsersBll();
                    Users u = new Users();
                    u.NickName = nickname;
                    u.UserName = nickname;
                    u.UserPwd = "123456";
                    u.UserType = 3;
                    int temp = 0;
                    Random rand = new Random();
                    temp = rand.Next(1, 61);
                    u.HeadUrl = "headfile/00" + temp+".jpg";
                    u.AddTime = DateTime.Now;
                    int userid = ubll.AddAndReturn(u);

                    //创建一个HttpCookie对象  对进行编码
                    //HttpUtility.UrlEncode
                    HttpCookie cookie = Cookie.Set("AdminInfo");
                    cookie.Values.Add("AdminId", HttpUtility.UrlEncode(userid.ToString()));
                    cookie.Values.Add("HeadUrl", HttpUtility.UrlEncode("images/head.png"));
                    cookie.Values.Add("NickName", HttpUtility.UrlEncode(nickname));
                    cookie.Values.Add("UserName", HttpUtility.UrlEncode(nickname));//进行编码     


                    //设定此cookies值
                    //设定cookie的生命周期
                    cookie.Expires = DateTime.Now.AddDays(7);
                    //加入此cookie
                    Cookie.Save(cookie);

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