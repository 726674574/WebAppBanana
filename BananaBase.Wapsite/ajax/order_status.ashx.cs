using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// order_status 的摘要说明
    /// </summary>
    public class order_status : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            try
            {
                int orderid = Convert.ToInt32(context.Request.QueryString["oid"].ToString());
                result = UpdateOrderStatus(orderid);
            }
            catch
            {
                result = "-1";
            }
            context.Response.Write(result);
        }

        private string UpdateOrderStatus(int orderid) {
            if (orderid <= 0)
                return "0";
            else
            {
                if (new Bll.Db.OrderBll().Update("status=1", "", " id=" + orderid).ResultStatus.Success)
                    return "1";
                else
                    return "0";
            }
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