using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Banana.Wapsite.ajax
{
    public partial class topay : IHttpHandler
    {

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string data = context.Request.Form["id"].ToString();
            throw new NotImplementedException();
        }
    }
}