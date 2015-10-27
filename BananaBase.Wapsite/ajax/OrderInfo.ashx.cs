using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Entity.Db;
using Banana.Bll.Db;
using System.Data;
using Banana.Dal.Db;
using System.Web.SessionState;
using Banana.Entity;
using Banana.Wapsite.Common;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// OrderInfo 的摘要说明
    /// </summary>
    public class OrderInfo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {  
            context.Response.ContentType = "text/plain";
           string act=  context.Request["act"].Trim2();
            if (act == "getBananaCount")
            {
                int count = getBananaCount(context);
                context.Response.Write(count);

            }
            else
            {


                HttpCookie cookie1 = new HttpCookie("shop");
                cookie1.Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies.Add(cookie1);
                context.Request.Cookies.Remove("shop");
                string result = "";
                try
                {
                    string name = context.Request.Form["name"].ToString();
                    string phone = context.Request.Form["phone"].ToString();
                    string provice = context.Request.Form["provice"].ToString();
                    string city = context.Request.Form["city"].ToString();
                    string address = context.Request.Form["address"].ToString();
                    string pno = context.Request.Form["pno"].ToString();
                    //int pro_id = context.Request.Form["pro_id"].ToString().ToInt();
                    //int count = context.Request.Form["count"].ToString().ToInt();
                    string data = context.Request.Form["data"].ToString();
                    string payMentType = context.Request.Form["payMentType"].ToString();
                    int price = context.Request.Form["price"].ToString().ToInt();
                    int bananaCount = context.Request.Form["bananaCount"].ToInt()*10;

                    if (string.IsNullOrEmpty(data) || data.IndexOf("|") == -1)
                    {
                        result = "-1";
                    }
                    else
                    {
                        int userid = 0;
                        UsersBll ubll = new UsersBll();
                        //Cookie.Get("AdminInfo") == null
                        if (context.Request.Cookies["AdminInfo"] == null)
                        {

                            context.Response.Redirect("http://www.ibananas.cn/", false); 
                            //如果没登录  直接  注册会员 

                            //Users model = new Users();
                            //model.HeadUrl = "images/head.png"; //默认会员头像
                            //model.Address = provice + "省 " + city + "市 " + address;
                            //model.AddTime = DateTime.Now;
                            //model.UserName = name; //13732243651
                            //model.UserPwd = phone.Substring(7, 4); //电话后4位
                            //model.ProvinceId = provice.ToInt();
                            //model.CityId = provice.ToInt();
                            //model.Mobile = phone;
                            //model.RealName = name;
                            //model.NickName = name;
                            //model.UserType = 3;
                            //int temp = 0;
                            //Random rand = new Random();
                            //temp = rand.Next(1, 61);
                            //model.HeadUrl = "headfile/00" + temp + ".jpg";
                            //userid = ubll.AddAndReturn(model);

                            ////登录后 创建一个HttpCookie对象
                            ////AdminInfo admin = new AdminInfo();
                            ////admin.AdminId = userid;
                            ////admin.HeadUrl = model.HeadUrl;
                            ////admin.NickName = model.UserName;
                            ////admin.UserName = model.UserName;
                            ////HttpContext.Current.Session["Admininfo"] = admin;

                            ////创建一个HttpCookie对象  对进行编码
                            ////HttpUtility.UrlEncode
                            //HttpCookie cookie = new HttpCookie("AdminInfo");
                            //cookie.Values.Add("AdminId", HttpUtility.UrlEncode(userid.ToString()));
                            //cookie.Values.Add("HeadUrl", HttpUtility.UrlEncode(model.HeadUrl));
                            //cookie.Values.Add("NickName", HttpUtility.UrlEncode(model.UserName));
                            //cookie.Values.Add("UserName", HttpUtility.UrlEncode(model.UserName)); //进行编码     


                            ////设定此cookies值
                            ////设定cookie的生命周期
                            //cookie.Expires = DateTime.Now.AddDays(7);
                            ////加入此cookie
                            //context.Response.AppendCookie(cookie);
                        }
                        else
                        {


                            //AdminInfo admin = (AdminInfo)HttpContext.Current.Session["AdminInfo"];
                            HttpCookie cookie = Cookie.Get("AdminInfo");
                            //用户输入 了电话和地址  记录最新
                            var currentuser =
                                ubll.GetByPrimaryKey(HttpUtility.UrlDecode(cookie.Values["AdminId"]).ToInt()).Entity;
                            currentuser.UserName = name;
                            currentuser.RealName = name;

                            currentuser.Mobile = phone;
                            currentuser.Address = provice + "省 " + city + "市 " + address;
                            ubll.Update(currentuser);

                            userid = currentuser.Id;
                        }



                        //添加 订单 添加订单详情表
                        result = AddOrder(userid, data, price, pno, payMentType, bananaCount);
                        //如果是登录 会员
                    }

                }
                catch
                {

                    result = "-1";
                }


                context.Response.Write(result);
            }
        }

        public string AddOrder(int userid, string data, int price, string pno, string payMentType, int bananaCount)
        {
            string result = "0";
            Order order = new Order();
            OrderBll orderbll = new OrderBll();
   
            UsersBll ubll = new UsersBll();
            var u = ubll.GetByPrimaryKey(userid).Entity;
            try
            {
                order.UserId = userid;
                order.ReciverName = u.UserName;
                order.PayMentTypeId = (byte?) (payMentType == "货到付款" ? 1 : 2);//货到付款
                order.RevicerAddress = u.Address;
                order.RevicerTel = u.Mobile;
                order.Status = 0;//等待确认
                order.RealPrice = price;
                order.TotalPrice = price;
                order.Count = 1;
                order.OrderTime = DateTime.Now;
                order.OrderNo = OrderHelper.GetProNo();
                order.Pno = pno == "" ? null : pno;
                order.BananaCount = bananaCount;
                //添加订单
                int orderaddid = orderbll.AddAndReturn(order);
                sendYzm("18758177964", "26871", new string[] { u.UserName + u.Mobile, DateTime.Now.ToString() });
                string[] dataArr = data.Split(',');
                if (dataArr.Length > 0)
                {
                    for (int d = 0; d < dataArr.Length; d++)
                    {
                        if (!string.IsNullOrEmpty(dataArr[d]))
                        {
                            if (dataArr[d].IndexOf("|") > -1)
                            {
                                string[] arr = dataArr[d].Split('|');
                                AddOrderList(u, Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]), orderaddid);
                                result = orderaddid.ToString();
                            }
                        }
                    }
                }
                

            }
            catch
            {

                result = "-1";
            }



            return result;

        }

        public void AddOrderList(Users u, int productid, int num, int orderid)
        {
            OrderList orderlist = new OrderList();
            OrderListBll orderlistbll = new OrderListBll();


            ProductBll pbll = new ProductBll();
            var p = pbll.GetByPrimaryKey(productid).Entity;


            //添加订单详情
            orderlist.Count = num;
            orderlist.Productid = productid;
            orderlist.Orderid = orderid;
            orderlist.OemPrice = p.OemPrice;
            orderlist.MarketPrice = p.MarketPrice;
            orderlistbll.Add(orderlist);
        }

        public string AddOrder(int userid, int productid, int pronum)
        {
            string result = "1";
            Order order = new Order();
            OrderBll orderbll = new OrderBll();
            OrderList orderlist = new OrderList();
            OrderListBll orderlistbll = new OrderListBll();


            ProductBll pbll = new ProductBll();
            UsersBll ubll = new UsersBll();
            try
            {
                //检查 当前用户是否有末完成的订单  有不能
                //if (orderbll.GetAll("*", " [status]=1 and userid=" + userid, null, "id").Entity.Count <= 0)
                //{


                var p = pbll.GetByPrimaryKey(productid).Entity;
                var u = ubll.GetByPrimaryKey(userid).Entity;
                order.UserId = userid;
                order.ReciverName = u.UserName;
                order.PayMentTypeId = 1;//货到付款
                order.RevicerAddress = u.Address;
                order.RevicerTel = u.Mobile;
                order.Status = 1;//
                order.RealPrice = pronum * p.OemPrice;
                order.TotalPrice = pronum * p.OemPrice;
                order.Count = 1;
                order.OrderTime = DateTime.Now;
                order.OrderNo = OrderHelper.GetProNo();
                //添加订单
                int orderaddid = orderbll.AddAndReturn(order);

                //添加订单详情
                orderlist.Count = order.Count;
                orderlist.Productid = productid;
                orderlist.Orderid = orderaddid;
                orderlist.OemPrice = p.OemPrice;
                orderlist.MarketPrice = p.MarketPrice;
                orderlistbll.Add(orderlist);
                //}
                //else
                //{
                //    result = "-2";
                //}

            }
            catch
            {

                result = "-1";
            }



            return result;

        }

        private string sendYzm(string phone, string yyid, string[] arr)
        {
            string result = "0";
            string ret = null;

            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            //ip格式如下，不带https://
            bool isInit = api.init("app.cloopen.com", "8883");
            api.setAccount("aaf98f894e8a784b014e8b9aacc30280", "3c12c5c2d099444cbf42bed59067989d");
            api.setAppId("8a48b5514e8a7522014e9098fef207d1");

            try
            {
                if (isInit)
                {
                    Dictionary<string, object> retData = api.SendTemplateSMS(phone, yyid, arr);
                    ret = getDictionaryData(retData);
                }
                else
                {
                    ret = "初始化失败";
                }
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            if (ret.IndexOf("000000") > -1)
                result = "1";
            return result;
        }

        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }

        public int getBananaCount(HttpContext context)
        {
            int count = 0;
            UsersBll ubll = new UsersBll();
            HttpCookie cookie = Cookie.Get("AdminInfo");
            if (context.Request.Cookies["AdminInfo"] != null)
            {
                var  user = ubll.GetByPrimaryKey(HttpUtility.UrlDecode(cookie.Values["AdminId"]).ToInt()).Entity;
                if (user != null)
                {
                    count = user.BananaCount;
                }
                
            }
            
            return count;
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