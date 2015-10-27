using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// index_temai 的摘要说明
    /// </summary>
    public class index_temai : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int page = context.Request.QueryString["page"].ToInt() + 1;
            int pagesize = 12;
            string result = "";
            if (page > 0)
            {
                result = getprolist(page, pagesize);
            }
            else
            {
                result = "-1";
            }

            context.Response.Write(result);
        }

        public string getprolist(int page, int pagesize)
        {


            string result = "";
            string sctemp = @" <li>
        <a href=""shop_list_detail.aspx?pro_id={0}"" class=""BigA boxshowdow"">
        <EM class=""imgbox""><img src=""/images/smallPic.png""  alt=""{1}"" class=""lazy""></EM>
        <EM class=""pname"">{2}</EM>
        <EM class=""price"">
          <i class=""fl"">
          <b class=""zk01"">{3}折</b>
          <span class=""red"">￥{4}</span> 
          <span class=""yj"">￥{5}</span>
          </i>
          <b class=""xgbtn fr"">立即抢购</b></EM>
      </a>
    </li>";
            var list = new TimeSaleBll().GetAllandProduct(page, pagesize, " A.classid =(select Top 1 Id from TimeSaleClass where GETDATE() between StartTime and EndTime order by EndTime desc)", null, " A.orderid desc").Entity;

            if (list.Items.Count > 0)
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    result += string.Format(sctemp, list.Items[i].ProId,
                        "/" + ApplicationSettings.Get("imgurl") + list.Items[i].SmallThumPic, list.Items[i].ProductName,
                        ((decimal) list.Items[i].SalePrice/list.Items[i].MarketPrice.Value*10).ToString("f1"),
                        ((decimal) list.Items[i].SalePrice).ToString("f0"),
                        ((decimal) list.Items[i].MarketPrice).ToString("f0"));
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