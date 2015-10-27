using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Banana.Wapsite
{
    public partial class temai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string GetTimeSaleEndTime()
        {
            var timeSaleClassEntity = new Dal.Db.TimeSaleClassDal().GetTimeSaleandImg();
            if (timeSaleClassEntity != null && timeSaleClassEntity.Count > 0)
                return timeSaleClassEntity[0].EndTime.ToString();
            return DateTime.Now.ToString();
        }
    }
}