using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// index_prolist 的摘要说明
    /// </summary>
    public class index_prolist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int page = context.Request.QueryString["page"].ToInt() + 1;
            string order = context.Request.QueryString["order"].Trim2();
            string type = context.Request.QueryString["type"].Trim2();
            string type2= context.Request.QueryString["type2"].Trim2();
            int pagesize = 12;
            string result = "";
            if (page > 0)
            {
                result = getprolist(page, pagesize, order,type,type2);
            }
            else
            {
                result = "-1";
            }

            context.Response.Write(result);
        }

        public string getprolist(int page, int pagesize, string order,string type,string type2)
        {


            string result = "";
            string sctemp = @"<li class=""bdradius6""><a href=""shop_list_detail.aspx?pro_id={0}"">
          <span class=""smallImgbox""><img src=""/images/smallPic.png"" alt=""{1}"" class=""lazy"" ></span>
          <span class=""prodTitle"">{2}</span>
          <span class=""prodPrice clf""> {6}<i class=""fl"">已售 {3}</i></span>
          <span class=""prodPrice clf""><i class=""red fl"">￥{4}</i> <i class=""fl"">￥{5}</i></span>
          </a></li>";

            if (order == "推荐")
            {
                order = "orderid desc";
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
                order = "orderid desc";
            }


            if (type == "男性用品" || type == "男用玩具")
            {
                type = "1";
            }
            else if (type == "女性用品" || type == "女用玩具")
            {
                type = "4";
            }
            else if (type == "延时锻炼" || type == "锁住幸福")
            {
                type = "12";
            }
            else if (type == "情趣服饰" )
            {
                type = "41";
            }
            else if (type == "调情助兴")
            {
                type = "13";
            }
            else if (type == "套套")
            {
                type = "6";
            }
           

            //var list = new ProductBll().GetJoinAll("*", page, pagesize, " adid=3", "", " " + order + " desc").Entity;

            var list = new ProductBll().GetAll("*", page, pagesize, getQuery(type,type2), "",order).Entity;
            if (list.Items.Count > 0)
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    result += string.Format(sctemp, list.Items[i].Id,
                        "/" + ApplicationSettings.Get("imgurl") + list.Items[i].BigThumPic, list.Items[i].ProductName,
                        list.Items[i].Sale ?? new Random().Next(500), list.Items[i].OemPrice.ToString().Split('.')[0],
                        list.Items[i].MarketPrice.ToString().Split('.')[0], list.Items[i].IsFree == true ? "<i class=\"fl\">包邮</i>" : list.Items[i].OemPrice.ToString().Split('.')[0].ToInt() > 199 ? "<i class=\"fl\">包邮</i>" : "");

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

        private string getQuery(string type,string type2)
        {
            string sql = "";

            if (type.NoEmpty())
            {
                //一级类
                var childlist = new ProductTypeBll().GetAll("*", " [typeint]=" + type, null, "[order]").Entity;
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




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}