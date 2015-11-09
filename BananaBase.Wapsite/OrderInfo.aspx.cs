using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Banana.Bll.Db;
using Banana.Entity.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite
{
    public partial class OrderInfo1 : System.Web.UI.Page
    {
        #region mstc
        protected StatisticsBll sbll = new StatisticsBll();
        protected string pro_id
        {
            get
            {
                try
                {
                    return Request.QueryString["pro_id"].ToString();
                }
                catch
                {

                    return "";
                }
            }

        }
        protected string pno
        {
            get
            {
                try
                {
                    return Request.QueryString["pno"].ToString();
                }
                catch
                {

                    return "";
                }
            }

        }
        protected string proname = "", proimg = "", proprice = "", prono = "", userp = "", userc = "", ads = "";
        protected Users current = new Users();
        protected IList<Product> productList = new List<Product>();
        protected bool isLinepayment;
        protected bool isFree;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取购物车
            string productId = "";
            if (HttpContext.Current.Request.Cookies["shop"] != null)
            {
                //HttpCookie cookie = Cookie.Get("shop");

                //HttpCookie cookie = new HttpCookie("shop");
                //cookie.Expires = DateTime.Now.AddMonths(-10);
                //Response.Cookies.Add(cookie);
                //Request.Cookies.Remove("shop");

                HttpCookie cookies = Cookie.Get("shop");
                productId = cookies.Values["productId"].ToString();
                if (pro_id.NoEmpty() && productId.IndexOf("," + pro_id + ",") == -1)
                    productId += pro_id + ",";
            }
            else
            {
                if (pro_id.NoEmpty())
                    productId = "," + pro_id + ",";
                pro2();
            }


            if (productId.Trim2().Length > 1)
            {
                productId = productId.Substring(1, productId.Length - 2);
                ProductBll pbll = new ProductBll();
                productList = pbll.GetAll("", "id in (" + productId + ")", "", "").Entity;
                foreach (var p in productList)
                {
                    //判断是否在限时抢购里
                    var list = new Bll.Db.TimeSaleBll().GetAllandProduct(1, 1, " A.classid =(select Top 1 Id from TimeSaleClass where GETDATE() between StartTime and EndTime order by EndTime desc) and A.objectid=" + p.Id, null, " A.orderid desc").Entity;
                    if (list.Items != null && list.Items.Count > 0)
                        p.OemPrice = list.Items[0].SalePrice;
                    isLinepayment = true;
                    if (!p.Linepayment)
                    {
                        isLinepayment = false;
                    }
                    isFree = p.Linepayment;
                    
                }

                var model = pbll.GetByPrimaryKey(pro_id.ToInt()).Entity;
                if (model != null)
                {
                    proname = model.ProductName;
                    proimg = ApplicationSettings.Get("imgurl") + model.BigThumPic;
                    proprice = ((decimal)model.OemPrice).ToString("f0");
                    prono = model.ProductNo;
                }
            }

            if (Cookie.Get("AdminInfo") != null)
            {
                HttpCookie cookie = Cookie.Get("AdminInfo");
                string useridstr = HttpUtility.UrlDecode(cookie.Values["AdminId"]);
                int userid = useridstr.ToInt();

                current = new UsersBll().GetByPrimaryKey(userid).Entity;

                var lastorder = new OrderBll().GetAll("*", " userid=" + userid, null, "id desc").Entity;
                if (lastorder != null)
                {
                    if (lastorder.Count > 0)
                    {
                        string address = lastorder[0].RevicerAddress;

                        string[] arr = address.Split(' ');
                        userp = arr[0].Replace("省", "");
                        userc = arr[1].Replace("市", "");
                        if (address.IndexOf("市") > -1)
                        {
                            ads = address.Split('市')[1].Replace(" ", "");
                        }
                    }
                }

            }
        }
        public void pro2()
        {
            var isball = sbll.GetAll(" *", " type=2 and objectid=" + pro_id.ToInt(), "", "").Entity;
            if (isball.Count > 0)
            {
                foreach (var a in isball)
                {

                    var json = JsonHelper.Deserialize<Productconfig>(a.Productconfig);


                    string where = " productconfig='{pro1:" + json.pro1 + ",pro2:" + (json.pro2 + 1) + ",pro3:" + json.pro3 + "}'";
                    sbll.Update(where, "", " type=2 and objectid=" + pro_id.ToInt());



                }
            }



        }
    }
}