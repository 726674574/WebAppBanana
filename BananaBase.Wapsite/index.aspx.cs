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
    public partial class index : System.Web.UI.Page
    {
        protected BannerListBll bbll = new BannerListBll();
        protected QiangGouBll qgbll = new QiangGouBll();
        protected ProductBll pbll = new ProductBll();
        protected AdLocationBll adbll = new AdLocationBll();
        protected BannerList banner = new BannerList();
        protected string act = "";
        protected int userId = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            act = Request["act"].Trim2();
            userId = Request["userId"].ToInt();
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

            BindTodayDaZhe(3, 3);
                BindBannerList();
                GetBanner();
        }

        /// <summary>
        /// 推荐 页面 的折扣区产品
        /// </summary>
        /// <param name="num">显示几个</param>
        /// <param name="adid">推荐页面 折扣区 推荐位id</param>
        /// <returns></returns>
        public void BindTodayDaZhe(int num, int adid)
        {
            var list = qgbll.GetAll("*", 1, num, " adid=" + adid, null, "orderid").Entity;
            List<Product> pro = new List<Product>();
            for (int i = 0; i < list.Items.Count; i++)
            {
               var infolist= pbll.GetByPrimaryKey(list.Items[i].Objectid.ToString().ToInt()).Entity;
                if (infolist != null)
                {
                    pro.Add(infolist);
                }
            }
            TodayDaZhe.DataSource = pro;
            TodayDaZhe.DataBind();
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <param name="num">显示数量</param>
        /// <param name="adid">推荐位id</param>
        public void BindBannerList()
        {

            var adEntity = adbll.GetAll("*", " id in(32,33,34,35)", "", " orderid desc").Entity;
            BannerList.DataSource = adEntity;
            BannerList.DataBind();


        }

        /// <summary>
        /// 绑定banana图
        /// </summary>
        public void GetBanner()
        {
            var bannerlist = bbll.GetAll(" top 1 *", " adid=2", null, "orderid desc").Entity;
            if (bannerlist.Count > 0)
            {
                banner.Bannerimg = ApplicationSettings.Get("imgurl") + bannerlist[0].Bannerimg;
                banner.Linkurl = bannerlist[0].Linkurl ?? "";
            }
        }

        #region Repeater嵌套
        protected IList<BannerList> BananaList(string adid)
        {
            int top = 6;
            return bbll.GetAll("*", 1, top, " adid=" + adid, null, " orderid asc").Entity.Items;
        }

        protected IList<BannerList> BananaTop(string adid)
        {
            return bbll.GetAll("*", 1, 1, " adid=" + adid, null, " orderid desc").Entity.Items;
        }
        #endregion

        

    }
}