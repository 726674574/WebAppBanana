using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Banana.Entity.Db;
using Banana.Bll.Db;
using Banana.Wapsite.AliPay;
using Banana.Wapsite.Common;

namespace Banana.Wapsite
{
    public partial class topay : System.Web.UI.Page
    {
        protected  OrderBll bll = new OrderBll();
        protected ProductBll pbll = new ProductBll();
        protected  decimal TtotalFee;
        protected object Model;
      

        protected void Page_Load(object sender, EventArgs e)
        { 

            int id = Request.QueryString["orderid"].ToInt();
            string name = Request.QueryString["name"].Trim2();
            string number = Request.QueryString["number"].Trim2();
            string backUrl = Request.QueryString["backUrl"].Trim2();//成功后跳转的 ss
          
             //是否重新支付
            if (name == "" && number != "")
            {
                string where =
                    " id=(select Productid from  OrderList where orderid=(select Id from [Order] where OrderNo='" +
                    number + "'))";
                var plist = pbll.GetAll("productname", where, "", "");
                string productName = plist.Entity[0].ProductName;
                var url = string.Format("number={1}&backUrl={0}", Server.UrlEncode(backUrl), number);
                var pay = new Alipay();
                Model = pay.GetBuildRequest(productName, productName, TtotalFee, url, number);
                //Model = pay.GetBuildRequest(productName, productName, (decimal)0.01, url, number);
            }
            else
            {
                var package = bll.GetByPrimaryKey(id);
                var url = string.Format("number={1}&backUrl={0}", Server.UrlEncode(backUrl), package.Entity.OrderNo);
                var pay = new Alipay();
                TtotalFee = (decimal)package.Entity.TotalPrice;
                Model = pay.GetBuildRequest(name, name, TtotalFee, url, package.Entity.OrderNo);
                //Model = pay.GetBuildRequest(name, name, (decimal)0.01, url, package.Entity.OrderNo);
            }
           
        }
    }
}