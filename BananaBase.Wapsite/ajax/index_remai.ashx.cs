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
    /// index_remai 的摘要说明
    /// </summary>
    public class index_remai : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int page = context.Request.QueryString["page"].ToInt() + 1;
            string order = context.Request.QueryString["order"].Trim2();
            int pagesize = 12;
            string result = "";
            if (page > 0)
            {
                result = getnew(page, pagesize,order);
            }
            else
            {
                result = "-1";
            }

            context.Response.Write(result);
        }
        public string getnew(int page, int pagesize,string order)
        {


            string result = "";
            string sctemp = @"<li class=""bdradius6""><a href=""shop_list_detail.aspx?pro_id={0}"">
          <span class=""smallImgbox""><img src=""/images/smallPic.png"" alt=""{1}"" class=""lazy"" ></span>
          <span class=""prodTitle"">{2}</span>
          <span class=""prodPrice clf""> <i class=""fl"">{6}</i><i class=""fl"">已售 {3}</i></span>
          <span class=""prodPrice clf""><i class=""red fl"">￥{4}</i> <i class=""fl"">￥{5}</i></span>
          </a></li>";

            if (order == "推荐")
            {
                order = "id asc";
            }
            else if (order == "价格↑")
            {
                order = "OemPrice asc";
            }
            else if (order == "价格↓")
            {
                order = "OemPrice desc";
            }
            else if (order == "销量↓")
            {
                order = "sale desc";
            }
            else if (order == "销量↑")
            {
                order = "sale asc";
            }
            else
            {
                order = "id asc";
            }

            var list = new ProductBll().GetJoinAll("*", page, pagesize, " adid=3", "",  order ).Entity;

            //var list = new ProductBll().GetAll("*", page, pagesize, "", "", " AddTime desc").Entity;
            if (list.Items.Count > 0)
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    result += string.Format(sctemp, list.Items[i].Id,
                        "/" + ApplicationSettings.Get("imgurl") + list.Items[i].BigThumPic, list.Items[i].ProductName,
                        list.Items[i].Sale ?? new Random().Next(500), list.Items[i].OemPrice.ToString().Split('.')[0],
                        list.Items[i].MarketPrice.ToString().Split('.')[0], list.Items[i].OemPrice.ToString().Split('.')[0].ToInt() > 199 ? "包邮" : "");

                }
                result += "<span id=\"pager\" style=\"display:none\" pagesize=\"" + pagesize + "\" pagecount=\"" +
                              list.PageCount + "\" page=\"" + page + "\"></span>";
            }
            else
            {
                result += "<span id=\"pager\" style=\"display:none\" pagesize=\"" + pagesize + "\" pagecount=\"1\" page=\"1\"></span>";
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