using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// mine_sc_list 的摘要说明
    /// </summary>
    public class mine_sc_list : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            try
            {
                int page = context.Request.QueryString["page"].ToString().ToInt();
                int pagesize = context.Request.QueryString["pagesize"].ToString().ToInt();

                result = getUserCollection(page, pagesize);
            }
            catch
            {

                result = "-1";
            }


            context.Response.Write(result);
        }
        public string getUserCollection(int currentpage,int num)
        {
            string result = "";

            string sctemp = "<li class=\"clf\"><a href=\"shop_list_detail.aspx?pro_id={0}\"><span class=\"smallImgbox fl\"><img width=\"70px\" src=\"images/smallDefaultPic.png\" alt=\"{1}\" class=\"lazy boxshowdow\"></span><span class=\"Pdesc fl\"><b>{2}</b> <span class=\"Titledesc\">{3}</span> <span class=\"red\">￥{4}</span><span class=\"yj\">￥{5}</span> </span><span class=\"zk\" onclick=\"deleteIt(this,{6})\">取消</span> </a></li>";//0 产品链接地址 1产品图片 2产品名称 3产品描述 4商城价 5市场价 6删除收藏id
            UserCollectionBll ucbll = new UserCollectionBll();
            ProductBll pbll = new ProductBll();
            int userid = 1;
            var uclist = ucbll.GetAll("*", 1, num, " userid=" + userid, null, "id").Entity;
            if (uclist.Items.Count > 0)
            {
                for (int i = 0; i < uclist.Items.Count; i++)
                {
                    var p = pbll.GetByPrimaryKey(uclist.Items[i].Productid.ToString().ToInt()).Entity;
                    if (p != null)
                    {
                        result += string.Format(sctemp, uclist.Items[i].Productid, ApplicationSettings.Get("imgurl") + p.BigThumPic, p.ProductName, p.Keyword, ((decimal)p.OemPrice).ToString("f0"), ((decimal)p.MarketPrice).ToString("f0"), uclist.Items[i].Id);
                    }
                }
                result += "<span id=\"pager\" style=\"display:none\" pagesize=\"" + num + "\" pagecount=\"" + uclist.PageCount + "\" page=\""+currentpage+"\"></span>";
            }
            else
            {
                result += "<span id=\"pager\" style=\"display:none\" pagesize=\"" + num + "\" pagecount=\"1\" page=\"1\"></span>";
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