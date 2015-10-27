using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Banana.Bll.Db;
using Banana.Entity.Db;
using System.Web.Services;
using Banana.Wapsite.Common;

namespace Banana.Wapsite
{
    public partial class shop_list_detail : System.Web.UI.Page
    {
        protected int pro_id = 0;
        protected string huodaofukuanlink = "/OrderInfo.aspx?pro_id=";
        protected string islike = "like";//登录用户是否 收藏，收藏 liked
        protected string title = "Banana情趣社区";
        protected bool Isnotvideo;
        protected string taobaolink = "javascript:void(0)";
        protected ProductBll pbll = new ProductBll();
        private StatisticsBll sbll = new StatisticsBll();
        protected ProductImgBll imgbll = new ProductImgBll();
        protected bool isLinepayment;
        protected string pno
        {
            get
            {
                if (Request.QueryString["pno"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["pno"] == null)
                    {
                        return "";
                    }
                    else
                    {
                        HttpCookie cookie = Cookie.Get("pno");
                        return cookie.Values["pnoid"].ToString();
                    }
                }
                else
                {
                    return Request.QueryString["pno"].ToString();
                }
            }

        }
        protected int userId;

        protected Product p = new Product();
        protected void Page_Load(object sender, EventArgs e)
        {
           pro_id=Request["pro_id"].ToInt();
           userId = Request["userId"].ToInt();
            if (pro_id > 0)
            {
                huodaofukuanlink = huodaofukuanlink + pro_id;
                if (pno.NoEmpty())
                {
                    huodaofukuanlink += "&pno=" + pno;
                }
                //检查是否登录 登录 检查是否收藏些产品
                int userid = 1;
                var col = new UserCollectionBll().GetAll("id", " userid=" + userid + " and productid=" + pro_id, null, "id").Entity;
                if (col.Count > 0)
                    islike = "liked";
                getProductDetail();
                getDetailPic();
                getProductDetailTuiJian();
                pro1();

            }

            if (HttpContext.Current.Request.Cookies["AdminInfo"] == null)
            {
                if (userId > 0)
                {
                    
                        HttpCookie cookie = new HttpCookie("AdminInfo");
                        cookie.Values.Add("AdminId", userId.ToString());
                        cookie.Expires = DateTime.Now.AddDays(7);
                        Response.AppendCookie(cookie);
                }
                else
                {
                    Response.Redirect("http://www.ibananas.cn/", false);
                }

            }
            else
            {
                if (userId != 0)
                {
                    HttpCookie cookie = Cookie.Get("AdminInfo");
                    //cookie不同 修改cookie
                    if (HttpUtility.UrlDecode(cookie.Values["AdminId"]).ToInt() != userId)
                    {
                        cookie.Values.Set("AdminId", userId.ToString());
                        Response.AppendCookie(cookie);
                    }
                }
            }

            var p = pbll.GetByPrimaryKey(pro_id).Entity;
            if (p != null)
                title = p.ProductName;

        }

        #region Band

        public void pro1()
        {
            var isball = sbll.GetAll(" *", " type=2 and objectid=" + pro_id, "", "").Entity;
            if (isball.Count > 0)
            {
                foreach (var a in isball)
                {

                    var json = JsonHelper.Deserialize<Productconfig>(a.Productconfig);


                    string where = " productconfig='{pro1:" + (json.pro1+1) + ",pro2:" + json.pro2  + ",pro3:" + json.pro3 + "}'";
                    sbll.Update(where, "", " type=2 and objectid=" + pro_id);



                }
            }
        }

        /// <summary>
        /// 绑定产品基本信息
        /// </summary>
        public void getProductDetail()
        {
            p = pbll.GetByPrimaryKey(pro_id).Entity;
            if (p.VideoUrl.Trim2() != "")
            {
                Isnotvideo = true;
            }
            //判断是否在限时抢购里
            var list = new Bll.Db.TimeSaleBll().GetAllandProduct(1, 1, " A.classid =(select Top 1 Id from TimeSaleClass where GETDATE() between StartTime and EndTime order by EndTime desc) and A.objectid=" + pro_id, null, " A.orderid desc").Entity;
            if (list.Items != null && list.Items.Count > 0)
                p.OemPrice = list.Items[0].SalePrice;
            if (p != null)
            {
                //淘宝链接 
                if (p.Taobaolink != null)
                    taobaolink = p.Taobaolink;
            }
            if (p.Linepayment)
            {
                isLinepayment = true;
            }
        }

        /// <summary>
        /// 绑定图文详情 
        /// </summary>
        public void getDetailPic()
        {
            var imglist = imgbll.GetAll("*", " productid=" + pro_id, null, "orderid").Entity;
            DetailPic.DataSource = imglist;
            DetailPic.DataBind();
        }

        /// <summary>
        /// 绑定相关推荐
        /// </summary>
        public void getProductDetailTuiJian()
        {
            string top = " top 8 * ";
            var p = pbll.GetByPrimaryKey(pro_id).Entity;
            if (p != null)
            {
                var ptuijian = pbll.GetAll(top, " id not in (" + p.Id + ") and producttypeid=" + p.ProductTypeId, null, " id desc").Entity;

                ProductDetailTuiJian.DataSource = ptuijian;
                ProductDetailTuiJian.DataBind();
            }

        }
        #endregion

        
    }
}