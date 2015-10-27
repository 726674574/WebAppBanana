using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// yzm 的摘要说明
    /// </summary>
    public class yzm : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            try
            {
                string tel = context.Request.QueryString["tel"].ToString();
                string oid = context.Request.QueryString["oid"].ToString();
                //string yyid = context.Request.QueryString["yyid"].ToString();
                //string time = context.Request.QueryString["time"].ToString();
                //string imgyzm = context.Request.QueryString["imgyzm"].ToString();
                int userid = 0;
                string yyid = "27143";
                string time = "3";
                if (context.Request.Cookies["AdminInfo"] != null)
                {
                    HttpCookie cookie = Cookie.Get("AdminInfo");
                    //用户输入 了电话和地址  记录最新
                    userid = (cookie.Values["AdminId"]).ToInt();
                }
                else
                {
                    result = "-2";
                    return;
                }
                if (string.IsNullOrEmpty(oid))
                {
                    result = "-2";
                    return;
                }
                //判断产品是否符合要求
                var order = new Dal.Db.OrderDal().GetAll("*", "userid=" + userid + " and status=0 and id=" + oid, null, "");
                if (order == null || order.Count == 0)
                {
                    result = "-2";
                    return;
                }
                //判断图形验证码是否正确
                //if (context.Request.Cookies["banana_shop_imgyzm"] != null)
                //{
                //    HttpCookie cookie1 = Cookie.Get("banana_shop_imgyzm");

                //if (cookie1.Values["imgyzm"] == imgyzm)
                //{
                Random r = new Random();
                int y = r.Next(1000, 9999);
                if (context.Request.Cookies["banana_shop_yzm" + oid] == null)
                {
                    HttpCookie cookie = new HttpCookie("banana_shop_yzm" + oid);
                    cookie.Values.Add("yzm", y.ToString());

                    //设定此cookies值
                    //设定cookie的生命周期
                    cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(time));
                    //加入此cookie
                    context.Response.AppendCookie(cookie);
                    result = sendYzm(tel, yyid, new string[] { y.ToString(), time });
                }
                else
                {
                    result = "-2";
                }
                //}
                //else
                //{
                //    result = "-3";
                //}

                //}
                //else
                //{
                //    result = "-3";
                //}



            }
            catch
            {
                result = "-1";
            }
            context.Response.Write(result);
        }

        private string sendYzm(string phone, string yyid, string[] arr)
        {
            string result = "0";
            string ret = null;

            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            //ip格式如下，不带https://
            bool isInit = api.init("app.cloopen.com", "8883");
            api.setAccount("aaf98f894e8a784b014e8b9aacc30280", "3c12c5c2d099444cbf42bed59067989d");
            api.setAppId("8a48b5514e8a7522014e9098fef207d1");

            try
            {
                if (isInit)
                {
                    Dictionary<string, object> retData = api.SendTemplateSMS(phone, yyid, arr);
                    ret = getDictionaryData(retData);
                }
                else
                {
                    ret = "初始化失败";
                }
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            if (ret.IndexOf("000000") > -1)
                result = "1";
            return result;
        }

        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
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