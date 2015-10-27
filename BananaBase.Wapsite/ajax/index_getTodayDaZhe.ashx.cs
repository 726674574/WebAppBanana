using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// index_getTodayDaZhe 的摘要说明
    /// </summary>
    public class index_getTodayDaZhe : IHttpHandler
    {
        protected QiangGouBll qgbll = new QiangGouBll();//推荐位 关联表
        protected ProductBll pbll = new ProductBll();//产品表
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            try
            {
                int page = context.Request.QueryString["page"].ToString().ToInt();
                int pagesize = context.Request.QueryString["pagesize"].ToString().ToInt();
                result = getTodayDaZhe(page, pagesize);
            }
            catch
            {

                result = "-1";
            }


            context.Response.Write(result);
        }
        /// <summary>
        /// 推荐 页面 的折扣区产品
        /// </summary>
        /// <param name="num">显示几个</param>
        /// <param name="adid">推荐页面 折扣区 推荐位id</param>
        /// <returns></returns>
        public string getTodayDaZhe(int currentpage, int num = 8, int adid = 3)
        {
            string result = "";
            string dazhetemp = "<li class=\"clf\"><a href=\"shop_list_detail.aspx?pro_id={0}\"><span class=\"smallImgbox fl\"><img width=\"70px\" height=\"70px\" src=\"images/smallDefaultPic.png\" alt=\"{1}\" class=\"lazy boxshowdow\"></span><span class=\"Pdesc fl\"><b>{2}</b> <span class=\"Titledesc\">{3}</span> <span class=\"red\">￥{4}</span> <span class=\"yj\">￥{5}</span> </span><span class=\"zk\">{6}折</span></a></li>";//0 今天打折产品链接地址 1产品图片 2产品名称 3产品小描述 4商城价 5市场价 6几折
            var list = qgbll.GetAll("*", currentpage, num, " adid=" + adid, num, "orderid").Entity;
            if (list.Items.Count > 0)
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    var p = pbll.GetByPrimaryKey(list.Items[i].Objectid.ToString().ToInt()).Entity;
                    if (p != null)
                    {
                        result += string.Format(dazhetemp, p.Id, "/" + ApplicationSettings.Get("imgurl") + p.SmallThumPic, p.ProductName, "", ((decimal)p.OemPrice).ToString("f0"), ((decimal)p.MarketPrice).ToString("f0"), ((decimal)p.OemPrice * 10 / (decimal)p.MarketPrice).ToString("f0"));
                    }

                }
                result += "<span id=\"zekousp\" style=\"display:none\" pagesize=\"" + num + "\" pagecount=\"" + list.PageCount + "\" page=\"" + currentpage + "\"></span>";
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