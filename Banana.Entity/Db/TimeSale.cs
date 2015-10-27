using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class TimeSale
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 对应的产品ID
        /// </summary>
        public Int32 ObjectId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 抢购价
        /// </summary>
        public Decimal SalePrice { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Int32 ClassId { get; set; }

        /// <summary>
        /// 排序ID
        /// </summary>
        public Int32 OrderId { get; set; }

        //产品参数

        /// <summary>
        /// 
        /// </summary>
        public Int32 ProId { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// 小封面图片
        /// </summary>
        public String SmallThumPic { get; set; }

        /// <summary>
        /// 产品大封面图片
        /// </summary>
        public String BigThumPic { get; set; }

        /// <summary>
        /// 市场价格
        /// </summary>
        public Decimal? MarketPrice { get; set; }

        /// <summary>
        /// 商城价格
        /// </summary>
        public Decimal? OemPrice { get; set; }

        /// <summary>
        /// 批发价 进货价
        /// </summary>
        public Decimal? Tradeprice { get; set; }

    }
}

