using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// index_getHuwaiShenqi 的摘要说明
    /// </summary>
    public class index_getHuwaiShenqi : IHttpHandler
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
                result = getHuwaiShenqi(page, pagesize);
            }
            catch
            {

                result = "-1";
            }


            context.Response.Write(result);
        }
        /// <summary>
        /// 推荐 页面 的户外神器
        /// </summary>
        /// <param name="num">显示几个</param>
        /// <param name="adid">推荐页面 折扣区 推荐位id</param>
        /// <returns></returns>
        public string getHuwaiShenqi(int currentpage, int num, int adid = 5)
        {
            string result = "";
            string producttemp = "<li><div><a href=\"shop_list_detail.aspx?pro_id={0}\" class=\"BigA boxshowdow\"><img width=\"239\" height=\"239\" src=\"images/smallDefaultPic.png\" alt=\"{1}\"  class=\"lazy\"><em><span class=\"red\">￥{2}</span><span class=\"yj\">￥{3}</span></em><em class=\"pname01\"><span class=\"pname01\">{4}</span></em></a><a href=\"{5}\" class=\"toTAOBAO\">去淘宝买</a></div></li>";//0 产品详情链接 1产品图片地址 2商品商城价 3商品市场价 4产品名称 5产品淘宝链接
            var list = qgbll.GetAll("*", currentpage, num, " adid=" + adid, null, "orderid").Entity;
            if (list.Items.Count > 0)
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    var currenp = pbll.GetByPrimaryKey(list.Items[i].Objectid.ToString().ToInt()).Entity;
                    if (currenp != null)
                    {
                        result += string.Format(producttemp, currenp.Id, ApplicationSettings.Get("imgurl") + currenp.SmallThumPic, ((decimal)currenp.OemPrice).ToString("f2"), ((decimal)currenp.MarketPrice).ToString("f2"), currenp.ProductName, currenp.Taobaolink == null ? "javascript:void(0)" : currenp.Taobaolink);
                    }

                }
                result += "<span id=\"huwaisp\" style=\"display:none\" pagesize=\"" + num + "\" pagecount=\"" + list.PageCount + "\" page=\"" + currentpage + "\"></span>";
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