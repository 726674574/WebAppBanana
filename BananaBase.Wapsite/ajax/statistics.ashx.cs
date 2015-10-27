using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Banana.Wapsite.Common;
using Banana.Bll.Db;
using NPOI.SS.Formula.Functions;
using Banana.Entity.Db;


namespace Banana.Wapsite.ajax
{
    /// <summary>
    /// statistics 的摘要说明
    /// </summary>
    public class statistics : IHttpHandler
    {
        protected string act;
        protected int objId;
        protected StatisticsBll bll = new StatisticsBll();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            act = context.Request["act"].Trim2();
            objId = context.Request["objId"].ToInt();
            switch (act)
            {
                case "appcount":
                    appcount();
                    break;
                case "pro3" :
                    pro3();
                    break;


            }
        }

        #region 

        public void appcount()
        {
            string file = " appcount=(select appcount from [Statistics] where type=1)+1 ";
            bll.Update(file, "", " type=1");
        }

        public void pro3()
        {
            var isball = bll.GetAll(" *", " type=2 and objectid=" + objId, "", "").Entity;
            if (isball.Count > 0)
            {
                foreach (var a  in isball)
                {
                    var json = JsonHelper.Deserialize<Productconfig>(a.Productconfig);
                    string where = " productconfig='{pro1:" + json.pro1 + ",pro2:" + json.pro2 + ",pro3:" +
                                   (json.pro3 + 1) + "}'";
                    bll.Update(where, "", " type=2 and objectid=" + objId);


                }
            }

        }

     

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

    }
   
}