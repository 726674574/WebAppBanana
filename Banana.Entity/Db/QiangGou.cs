using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class QiangGou
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public Int32? Objectid { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public String LastModifyer { get; set; }

        /// <summary>
        /// 抢购价格
        /// </summary>
        public Decimal? QgPrice { get; set; }

        /// <summary>
        /// 位置类型id
        /// </summary>
        public Int32? AdId { get; set; }

        /// <summary>
        /// 排序id
        /// </summary>
        public Int32? OrderId { get; set; }

        /// <summary>
        /// 如果是商品 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

    }
}

