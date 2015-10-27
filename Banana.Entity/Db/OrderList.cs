using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class OrderList
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public Int32? Orderid { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public Int32? Productid { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        public Int32? Count { get; set; }

        /// <summary>
        /// 产品属性
        /// </summary>
        public String ProductProterys { get; set; }

        /// <summary>
        /// 应当支付价格
        /// </summary>
        public Decimal? MarketPrice { get; set; }

        /// <summary>
        /// 实际 支付价格
        /// </summary>
        public Decimal? OemPrice { get; set; }

        /// <summary>
        /// 快递方式
        /// </summary>
        public Int32? DeliveryTypeid { get; set; }

    }
}

