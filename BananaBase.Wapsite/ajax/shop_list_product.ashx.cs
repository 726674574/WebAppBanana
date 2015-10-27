using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using System.Text;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// shop_list_product 的摘要说明
    /// </summary>
    public class shop_list_product : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            try
            {
                int pagesize = 8;
                int page = context.Request.QueryString["page"] == null ? 1 : Convert.ToInt32(context.Request.QueryString["page"].ToString());
                string pro_type1 = context.Request.QueryString["pro_type1"] == null ? "" : context.Request.QueryString["pro_type1"];//一级类别id
                string pro_type2 = context.Request.QueryString["pro_type2"] == null ? "" : context.Request.QueryString["pro_type2"]; ;//二级类别id
                string sort = context.Request.QueryString["sort"] == null ? "orderid" : context.Request.QueryString["sort"]; ;//条件 time price sales
                string order = context.Request.QueryString["order"] == null ? "desc" : context.Request.QueryString["order"]; ;//升序倒序 asc 默认 desc
                result = LoadProductData(page, pagesize, getQuery(pro_type1, pro_type2), getOrder(sort, order));

            }
            catch
            {
                result = "-1";
            }

            context.Response.Write(result);
        }
        /// <summary>
        /// 取分页数据
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public string LoadProductData(int currentpage, int pagesize, string where, string orderby)
        {
            int pageCount = 1;
            StringBuilder sb = new StringBuilder();
            var list = new ProductBll().GetAll("*", currentpage, pagesize, where, null, orderby).Entity;
            string producttemp = "<li><div><a href=\"shop_list_detail.aspx?pro_id={0}\" class=\"BigA boxshowdow\"><img  src=\"images/smallDefaultPic.png\" alt=\"{1}\"  class=\"lazy\"><em><span class=\"red\">￥{2}</span><span class=\"yj\">￥{3}</span></em><em class=\"pname01\"><span class=\"pname01\">{4}</span></em></a><a href=\"{5}\" class=\"toTAOBAO\">去淘宝买</a></div></li>";//0 产品详情链接 1产品图片地址 2商品商城价 3商品市场价 4产品名称 5产品淘宝链接
            string productstr = "";
            if (list.Items.Count > 0)
            {
                pageCount = list.PageCount;
                for (int l = 0; l < list.Items.Count; l++)
                {
                    productstr = "";
                    var currenp = list.Items[l];
                    productstr += string.Format(producttemp, currenp.Id, ApplicationSettings.Get("imgurl") + currenp.SmallThumPic, ((decimal)currenp.OemPrice).ToString("f2"), ((decimal)currenp.MarketPrice).ToString("f2"), currenp.ProductName, currenp.Taobaolink == null ? "javascript:void(0)" : currenp.Taobaolink);
                    sb.Append(productstr);
                }
            }
            //加载更多 判断条件
            sb.Append("<span id=\"pager\" page=\"" + currentpage + "\" pagecount=\"" + pageCount + "\" style=\"display:none\">");
            return sb.ToString();
        }
        /// <summary>
        /// 一级类和二级类的 查询条件
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        private string getQuery(string type1, string type2)
        {
            string sql = "";

            if (type1.NoEmpty())
            {
                //一级类
                var childlist = new ProductTypeBll().GetAll("*", " [typeint]=" + type1, null, "[order]").Entity;
                string type_ids = "";
                if (childlist.Count > 0)
                {
                    for (int i = 0; i < childlist.Count; i++)
                    {
                        type_ids += childlist[i].Id.ToString() + ",";
                    }
                    type_ids = type_ids.Trim(',');
                    sql = " ProductTypeId in (" + type_ids + ")";
                }
            }
            if (type2.NoEmpty())
            {
                //二级类
                sql = " ProductTypeId =" + type2;
            }
            return sql;
        }
        /// <summary>
        /// 排序 条件
        /// </summary>
        /// <param name="sortstr"></param>
        /// <param name="orderstr"></param>
        /// <returns></returns>
        private string getOrder(string sortstr, string orderstr)
        {
            string sql = " {0} @ ";
            switch (sortstr)
            {
                //默认时间排序
                case "orderid":
                    sql = string.Format(sql, "orderid");
                    break;
                case "sales":
                    sql = string.Format(sql, "sale");
                    break;
                case "price":
                    sql = string.Format(sql, "oemprice");
                    break;
            }
            sql = sql.Replace("@", orderstr);
            return sql;
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