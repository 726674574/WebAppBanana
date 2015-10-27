using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banana.Wapsite
{
    public class OrderHelper
    {
        /// <summary>
        /// 订单时 生成订单编号
        /// </summary>
        /// <returns></returns>
        public static string GetProNo()
        {
            return "B" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + new Random().Next(1, 100);
        }
    }
}