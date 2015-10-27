using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite
{
    public partial class OrderCheck1 : System.Web.UI.Page
    {
        protected int OrderId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["orderid"]);
                }
                catch
                {

                    return 0;
                }
            }

        }
        protected Entity.Db.Order orderEntity = new Entity.Db.Order();
        protected IList<Entity.Db.OrderList> orderListEntity = new List<Entity.Db.OrderList>();
        protected int time = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Cookie.Get("AdminInfo") != null || 1 == 1)
                {
                    LoadUserOrder(OrderId);
                }
                else
                {
                    Response.Write("参数错误,5秒后页面自动跳转到商城首页......");
                    Response.Write("<script language='javascript'>setTimeout(\"{location.href='shop.aspx'}\",5000);</script>");
                }
                if (orderEntity == null)
                {
                    Response.Write("参数错误,5秒后页面自动跳转到商城首页......");
                    Response.Write("<script language='javascript'>setTimeout(\"{location.href='index.aspx'}\",5000);</script>");
                    Response.End();
                }
                else
                {
                    if (orderListEntity.Count == 0)
                    {
                        Response.Write("参数错误,5秒后页面自动跳转到商城首页......");
                        Response.Write("<script language='javascript'>setTimeout(\"{location.href='index.aspx'}\",5000);</script>");
                        Response.End();
                    }
                }
            }
        }
        public void LoadUserOrder(int orderId)
        {
            OrderBll obll = new OrderBll();
            OrderListBll olistbll = new OrderListBll();
            ProductBll pbll = new ProductBll();
            if (HttpContext.Current.Request.Cookies["AdminInfo"] != null)
            {
                HttpCookie cookie = Cookie.Get("AdminInfo");
                string useridstr = HttpUtility.UrlDecode(cookie.Values["AdminId"]);
                int userid = useridstr.ToInt();
                orderEntity = obll.GetAll("*", "userid=" + userid + " and status=0 and id=" + orderId, null, "").Entity.SingleOrDefault();

                if (orderEntity != null)
                {
                    orderListEntity = olistbll.GetAll("*", " orderid=" + orderEntity.Id, null, "id desc").Entity;
                }
            }

        }

        public string GetProName(int proid)
        {

            var proEntity = new Bll.Db.ProductBll().GetByPrimaryKey(proid).Entity;
            if (proEntity != null)
                return proEntity.ProductName;
            else
                return "";
        }

        public string GetImgYzm()
        {
            string result = "";
            if (HttpContext.Current.Request.Cookies["banana_shop_imgyzm"] != null)
            {
                HttpCookie cookie = Cookie.Get("banana_shop_imgyzm");
                result = cookie.Values["imgyzm"].ToString();
            }
            return result;
        }
    }
}