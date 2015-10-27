using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// index_mine 的摘要说明
    /// </summary>
    public class index_mine : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string act = context.Request.QueryString["act"].Trim2();
            int id = context.Request.QueryString["id"].ToInt();
            string type = context.Request.QueryString["order"].Trim2();
            int page = context.Request.QueryString["page"].ToInt() + 1;
            if (act == "delete")
            {
              string result=  delete(id);
              context.Response.Write(result);
            }
            else
            {

                int pagesize = 4;
                int userid = 0;
                string result = "";
                //登录后的用户Id  没登录 返回末登录状态码
                if (Cookie.Get("AdminInfo") != null)
                {
                    HttpCookie cookie = Cookie.Get("AdminInfo");
                    userid = HttpUtility.UrlDecode(cookie.Values["AdminId"]).ToInt();
                    if (page > 0)
                    {
                        result = getprolist(page, pagesize, type, userid);
                    }
                    else
                    {
                        result = "-1";
                    }
                }
                else
                {
                    result = "未登陆";
                }


                context.Response.Write(result);
            }

        }

        public string getprolist(int page, int pagesize, string type,int userid)
        {
            OrderListBll orderlistbll = new OrderListBll();
            ProductBll probll = new ProductBll();
            string result = "";
            string temp = "";
            string sctemp = @" <li class=""clf"">
      <p class=""orderTitle clf""><span class=""fl"">订单号：<i class=""red"">{0}</i></span> </p>
        {2}
        <a class=""fl"">金额：<i class=""red"">￥{1}</i></a>
        <a href=""javascript:void(0);"" class=""fr""  onclick=""btdelete({3})"" >删除</a><a href=""http://m.kuaidi100.com/"" class=""fr""><span class=""red"">查看物流</span></a>
        
    </li> ";

            string sctemplist = @"<a href=""#"" class=""shop_list_detail"">
        <span class=""smallImgbox fl""><img src=""{0}""  alt=""{0}"" class=""lazy boxshowdow""></span>
        <span class=""Pdesc fl"">
           <b>{1}</b>
           <span class=""Titledesc""><i>单价：￥{2}</i> <i>数量：{3}</i>  </span>
        </span>
        </a>";
            if (type == "待付款")
            {
                type = "0";
            }
           
            else if (type == "待发货")
            {
                type = "1";
            }
            else if (type == "已完结")
            {
                type = "2";
            }
            else if (type == "")
            {
                type = "0";
            }

            var list = new OrderBll().GetAll("*", page, pagesize, " userid="+userid+" and Status="+type, "", "ordertime desc").Entity;
            if (list.Items.Count > 0)
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    var orderlist =
                        orderlistbll.GetAll("productid,count", " orderid=" + list.Items[i].Id, "", " id").Entity;
                    if (orderlist.Count > 0)
                    {
                        for (int j = 0; j < orderlist.Count; j++)
                        {
                           // var prolist = probll.GetAll("*", " id=" + orderlist[j].Productid, "", "").Entity;
                            var prolist = probll.GetByPrimaryKey(orderlist[j].Productid.ToString().ToInt()).Entity;
                            temp+= string.Format(sctemplist,
                                "/" + ApplicationSettings.Get("imgurl") + prolist.BigThumPic,
                                prolist.ProductName, prolist.OemPrice.ToString().Split('.')[0], orderlist[j].Count);

                        }

                    }
                    result += string.Format(sctemp, list.Items[i].OrderNo, list.Items[i].RealPrice, temp,  list.Items[i].Id);
                    temp = "";

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

        public string delete(int id)
        {
            string result = "";
            OrderBll order = new OrderBll();
            if (order.DeleteList("id=" + id).ResultStatus.Success)
            {
                result = "1";
            }
            else
            {
                result = "0";
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