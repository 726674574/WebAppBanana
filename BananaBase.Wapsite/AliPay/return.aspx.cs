using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Banana.Bll.Db;
using Banana.Wapsite.Common;
using Banana.Bll;

namespace Banana.Wapsite.AliPay
{
    public partial class _return : System.Web.UI.Page
    {

        protected string BackUrl = "";
        protected string Number = "";
        protected int State = 0;
             
        protected void Page_Load(object sender, EventArgs e)
        {

            string number = Request.QueryString["number"].Trim2();
            string backUrl = Request.QueryString["backUrl"].Trim2();
            //todo 逻辑需要放到notify的请求中，此处只处理跳转
            SortedDictionary<string, string> sPara = GetRequestGet();
            if (sPara.Count > 0) //判断是否有带返回参数
            {
                var aliNotify = new Notify();
                var verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);
                if (verifyResult) //验证成功
                {
                    OrderBll oorderbll = new OrderBll();
                    string where = "orderno='" + number + "'";
                    oorderbll.Update("status=1", "",where );
                    if (Request.Cookies["AdminInfo"] != null)
                    {
                       var BananaCount=  new Bll.Db.OrderBll().GetAll("BananaCount", where, "", "id").Entity[0].BananaCount;
                        HttpCookie cookieadmin = Cookie.Get("AdminInfo");
                        var userId = HttpUtility.UrlDecode(cookieadmin.Values["AdminId"]).ToInt();
                        BananaCount = new Bll.Db.UsersBll().GetByPrimaryKey(userId).Entity.BananaCount - BananaCount;
                        new Bll.Db.UsersBll().Update("BananaCount=" + BananaCount, "", "id=" + userId);
                    }
                    State = 1;
                    Number = number;
                    BackUrl = backUrl;
                }


            }
            else
            {
                State = 0;
                Number = number;
                BackUrl = backUrl;

            }

        }

        #region 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sPara = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                if (requestItem[i].ToLower().Replace("?","") == "number" || requestItem[i].ToLower() == "backurl")
                {
                    continue;
                }

                sPara.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sPara;
        }
        #endregion
    }
}