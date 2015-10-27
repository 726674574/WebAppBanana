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
    public partial class search : System.Web.UI.Page
    {
        protected string seach = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            seach = Request["seach"].Trim2();
            
        }
        
    }
}