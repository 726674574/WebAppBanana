using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 订单编码,商城唯一识别号 
        /// </summary>
        public String OrderNo { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Int32? UserId { get; set; }

        /// <summary>
        /// 订单收货人姓名
        /// </summary>
        public String ReciverName { get; set; }

        /// <summary>
        /// 订单 收货电话
        /// </summary>
        public String RevicerTel { get; set; }

        /// <summary>
        /// 订单收货地址 
        /// </summary>
        public String RevicerAddress { get; set; }

        /// <summary>
        /// 订单 收货 邮编
        /// </summary>
        public String RevicerPostCode { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public Int32? Count { get; set; }

        /// <summary>
        /// 应该支付多少钱,
        /// </summary>
        public Decimal? TotalPrice { get; set; }

        /// <summary>
        /// 实际支付多少钱
        /// </summary>
        public Decimal? RealPrice { get; set; }

        /// <summary>
        /// 订单状态 : 0 待付款, 1待发货,2 确认收货 交易成功
        /// </summary>
        public Byte? Status { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public Byte? PayMentTypeId { get; set; }

        /// <summary>
        /// 快递类别
        /// </summary>
        public Byte? DeliverTypId { get; set; }

        /// <summary>
        /// 快递 物流编号
        /// </summary>
        public String DeliveNo { get; set; }

        /// <summary>
        /// 订单下单时间
        /// </summary>
        public DateTime? OrderTime { get; set; }

        /// <summary>
        /// 订单 付款时间
        /// </summary>
        public DateTime? PaymentTime { get; set; }

        /// <summary>
        /// 快递价格
        /// </summary>
        public Decimal? DeliverPrice { get; set; }

        public string Pno { get; set; }

        public string ExpressNo { get; set; }
        public string HigherOrderNo { get; set; }
        public int BananaCount { get; set; }


    }
}

