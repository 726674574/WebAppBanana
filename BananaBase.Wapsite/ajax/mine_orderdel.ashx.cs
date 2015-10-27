using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Wapsite.Common;
using Banana.Bll.Db;

namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// mine_orderdel 的摘要说明
    /// </summary>
    public class mine_orderdel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            int id = context.Request.QueryString["id"].ToInt();
            var falg = 0;
            if (id > 0)
            {
                falg=  new OrderBll().DeleteList("id=" + id).ResultStatus.Code;
            }
            context.Response.Write(falg);

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