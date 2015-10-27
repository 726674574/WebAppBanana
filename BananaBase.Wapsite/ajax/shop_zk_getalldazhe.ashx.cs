using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// shop_zk_getalldazhe 的摘要说明
    /// </summary>
    public class shop_zk_getalldazhe : IHttpHandler
    {
        protected QiangGouBll qgbll = new QiangGouBll();
        protected ProductBll pbll = new ProductBll();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            try
            {
                int page = context.Request.QueryString["page"].ToString().ToInt();
                int pagesize = context.Request.QueryString["pagesize"].ToString().ToInt();
                int adid = context.Request.QueryString["adid"].ToString().ToInt();
                string sort = context.Request.QueryString["sort"] == null ? "id" : context.Request.QueryString["sort"].ToString();
                string order = context.Request.QueryString["order"] == null ? "asc" : context.Request.QueryString["order"].ToString();
                result = getallDaZhe(page, pagesize, getOrder(sort, order),adid);
            }
            catch
            {

                result = "-1";
            }


            context.Response.Write(result);
        }
        public string getallDaZhe(int currentpage, int num, string orderby, int adid = 10)
        {
            string result = "";
            //string dazhetemp1 = "<li class=\"boxshowdow\"><a href=\"shop_list_detail.aspx?pro_id={0}\"><span class=\"zkImgbox\"><img src=\"images/bannerDefaultPic.png\" width=\"450px\" height=\"230px\" alt=\"{1}\" class=\"lazy\"></span><p> {2}</p><p><span class=\"red\">￥{3}</span> <span class=\"yj\">￥{4}</span> <span class=\"fr ts\">{5}</span></p></a></li>";//0 产品链接 1产品图片 2产品名称 3商城价 4市场价 5时间差
            string dazhetemp1 = "<li><div><a href=\"shop_list_detail.aspx?pro_id={0}\" class=\"BigA boxshowdow\"><img width=\"219px\" src=\"{1}\"  alt=\"{1}\" class=\"lazy\"><EM><span class=\"red\">￥{3}</span> <span class=\"yj\">￥{4}</span></EM><EM class=\"pname01\"><span class=\"toblock pname01\">{2}</span></EM></a><a href=\"{5}\" class=\"toTAOBAO\">去淘宝买</a></div></li>";//0 时间差剩余:2天 1商品链接地址 2折扣产品图片 3商城价 4市场价 5产品名称 6淘宝链接
            var list = qgbll.GetAll("*", currentpage, num, " adid=" + adid, null, orderby).Entity;
            if (list.Items.Count > 0)
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    var p = pbll.GetByPrimaryKey(list.Items[i].Objectid.ToString().ToInt()).Entity;
                    if (p != null)
                    {
                        result += string.Format(dazhetemp1, p.Id, ApplicationSettings.Get("imgurl") + p.SmallThumPic, p.ProductName, ((decimal)p.OemPrice).ToString("f0"), ((decimal)p.MarketPrice).ToString("f0"), p.Taobaolink == null ? "javascript:void(0)" : p.Taobaolink);
                    }

                }
                result += "<span id=\"pager\" style=\"display:none\" pagesize=\"" + num + "\" pagecount=\"" + list.PageCount + "\" page=\"" + currentpage + "\"></span>";
            }
            return result;
        }
        private string getOrder(string sortstr, string orderstr)
        {
            string sql = " {0} @ ";
            switch (sortstr)
            {
                //默认时间排序
                case "id":
                    sql = string.Format(sql, "id");
                    break;
                case "price":
                    sql = string.Format(sql, "qgprice");
                    break;
                case "sales":
                    sql = string.Format(sql, "orderid");
                    break;
            }
            sql = sql.Replace("@", orderstr);
            return sql;
        }
        public string getTimeSpanStr(object time)
        {
            DateTime currenttime = new DateTime();
            if (time == null)
                currenttime = DateTime.Now;
            else
                currenttime = (DateTime)time;
            string result = "末知";

            DateTime t1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            TimeSpan t = t1 - currenttime;
            int num = (int)Math.Ceiling(t.TotalDays);
            if (num == 0)
                num = 1;
            result = "剩余" + num + "天";

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