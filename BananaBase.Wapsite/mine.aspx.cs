using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Banana.Wapsite.Common;
using Banana.Bll.Db;
using Banana.Entity.Db;

namespace Banana.Wapsite
{
    public partial class mine : System.Web.UI.Page
    {
        
        private OrderBll orderbll = new OrderBll();
        private OrderListBll orderlistbll = new OrderListBll();
        protected void Page_Load(object sender, EventArgs e)
        {

           
        }

        #region

        public void orderBind()
        {
           //orderbll.GetAll
        }

        #endregion
    }
}