using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// mine_order_getmore 的摘要说明
    /// </summary>
    public class mine_order_getmore : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            try
            {
                int pagesize = context.Request.QueryString["pagesize"] == null ? 1 : Convert.ToInt32(context.Request.QueryString["pagesize"].ToString());
                int page = context.Request.QueryString["page"] == null ? 1 : Convert.ToInt32(context.Request.QueryString["page"].ToString());
                string status = context.Request.QueryString["status"] == null ? "1" : context.Request.QueryString["status"]; ;//条件 time price sales
                string order = context.Request.QueryString["order"] == null ? "asc" : context.Request.QueryString["order"]; ;//升序倒序 asc desc
                result = LoadUserOrder(page, pagesize, GetQuery(status), getOrder(order));

            }
            catch
            {
                result = "-1";
            }

            context.Response.Write(result);

        }
        private string getOrder(string orderstr)
        {
            string sql = " {0} @ ";

            sql = string.Format(sql, "id");

            sql = sql.Replace("@", orderstr);
            return sql;
        }
        public string GetQuery(string status)
        {
            string sql = "";
            if (status == "1")
            {
                sql = " and [status]=1";//末完成 订单
            }
            else
            {
                sql = " and [status]=2";//已完成订单
            }
            return sql;
        }
        public string LoadUserOrder(int currentpage, int num, string where, string order)
        {
            string result = "";
            //if (Cookie.Get("AdminInfo") != null)
            //{
            //    string temp = " <li class=\"clf\"><a href=\"#\"><span class=\"smallImgbox fl\"><img src=\"images/smallDefaultPic.png\" alt=\"{0}\" class=\"lazy boxshowdow\"></span> <span class=\"Pdesc fl\"><b>{1}</b> <span class=\"Titledesc\">{2}</span></span><span class=\"zk\">￥{3}</span> </a></li>";//0产品图片 1产品名称 2产品描述 3订单价格
            //    OrderBll obll = new OrderBll();
            //    OrderListBll olistbll = new OrderListBll();
            //    ProductBll pbll = new ProductBll();
            //    int userid = Cookie.Get("AdminInfo").Values["AdminId"].ToInt();
            //    var blllist = obll.GetAll("*", currentpage, num, "userid=" + userid + " " + where + "", null, order).Entity;

            //    if (blllist.Items.Count > 0)
            //    {
            //        for (int i = 0; i < blllist.Items.Count; i++)
            //        {
            //            var olist = olistbll.GetAll("*", " orderid=" + blllist.Items[i].Id, null, "id desc").Entity;
            //            if (olist.Count > 0)
            //            {
            //                var p = pbll.GetByPrimaryKey(olist[0].Productid.ToString().ToInt()).Entity;
            //                if (p != null)
            //                {
            //                    result += string.Format(temp, ApplicationSettings.Get("imgurl") + p.BigThumPic, p.ProductName, p.Keyword, ((decimal)blllist.Items[i].TotalPrice).ToString("f1"));
            //                }
            //            }
            //        }
            //        result += "<span id=\"pager\" page=\"" + currentpage + "\" pagecount=\"" + blllist.PageCount + "\" style=\"display:none\">";
            //    }
            //    else
            //    {
            //        result += "<span id=\"pager\" page=\"1\" pagecount=\"1\" style=\"display:none\">";
            //    }
            //}
            //return result;
            if (Cookie.Get("AdminInfo") != null)
            {
                //string temp = " <li class=\"clf\"><a href=\"#\"><span class=\"smallImgbox fl\"><img src=\"images/smallDefaultPic.png\" alt=\"{0}\" class=\"lazy boxshowdow\"></span> <span class=\"Pdesc fl\"><b>{1}</b> <span class=\"Titledesc\">{2}</span></span><span class=\"zk\">￥{3}</span> </a></li>";//0产品图片 1产品名称 2产品描述 3订单价格
                string temp1 = "<li class=\"clf\"><p class=\"orderTitle\">订单号：<span class=\"red\">{0}</span>金额：<span class=\"red\">￥{1}</span></p>{2}</li>";
                string temp2 = "<a href=\"#\"><span class=\"smallImgbox fl\"><img src=\"images/smallDefaultPic.png\"  alt=\"{0}\" class=\"lazy boxshowdow\"></span><span class=\"Pdesc fl\"><b>{1}</b><span class=\"Titledesc\"><i>单价：￥{2}</i> <i>数量：{3}</i>  </span></span></a>";
                OrderBll obll = new OrderBll();
                OrderListBll olistbll = new OrderListBll();
                ProductBll pbll = new ProductBll();
                HttpCookie cookie = Cookie.Get("AdminInfo");
                string useridstr = HttpUtility.UrlDecode(cookie.Values["AdminId"]);
                int userid = useridstr.ToInt();
                var blllist = obll.GetAll("*", currentpage, num, "userid=" + userid + " " + where + "", null, order).Entity;

                if (blllist.Items.Count > 0)
                {
                    for (int i = 0; i < blllist.Items.Count; i++)
                    {
                        string orderlist = "";
                        var olist = olistbll.GetAll("*", " orderid=" + blllist.Items[i].Id, null, "id desc").Entity;
                        if (olist.Count > 0)
                        {
                            foreach (var o in olist)
                            {
                                var p = pbll.GetByPrimaryKey(o.Productid.ToString().ToInt()).Entity;
                                if (p != null)
                                {
                                    orderlist += string.Format(temp2, ApplicationSettings.Get("imgurl") + p.SmallThumPic, p.ProductName, ((decimal)p.OemPrice).ToString("f0"), o.Count);
                                }
                            }
                        }
                        result += string.Format(temp1, blllist.Items[i].OrderNo, ((decimal)blllist.Items[i].TotalPrice).ToString("f0"), orderlist);
                    }
                    result += "<span id=\"pager\" page=\"" + currentpage + "\"  pagesize=\"" + num + "\" pagecount=\"" + blllist.PageCount + "\" style=\"display:none\">";
                }
                else
                {
                    result += "<span id=\"pager\" page=\"" + currentpage + "\"  pagesize=\"" + num + "\"  pagecount=\"1\" style=\"display:none\">";
                }
            }
            return result;
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