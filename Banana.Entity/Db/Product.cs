using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class Product
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public String ProductName { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public String ProductNo { get; set; }

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

        /// <summary>
        /// 库存
        /// </summary>
        public Int32? Collects { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public Int32? Hits { get; set; }

        /// <summary>
        /// 小封面图片
        /// </summary>
        public String SmallThumPic { get; set; }

        /// <summary>
        /// 产品大封面图片
        /// </summary>
        public String BigThumPic { get; set; }

        /// <summary>
        /// 产品添加时间
        /// </summary>
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 商品状态 1:商用 0:下架 
        /// </summary>
        public Int32? Status { get; set; }

        /// <summary>
        /// 产品所属类别id
        /// </summary>
        public Int32? ProductTypeId { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public String Creater { get; set; }

        /// <summary>
        /// 搜索时 关键词
        /// </summary>
        public String Keyword { get; set; }

        /// <summary>
        /// 关联 文章 
        /// </summary>
        public String ArticleId { get; set; }

        /// <summary>
        /// 淘宝链接
        /// </summary>
        public String Taobaolink { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public Int32? Sale { get; set; }
        public Int32? OrderId { get; set; }
        public String VideoUrl { get; set; }
        public string Source { get; set; }
        public bool Linepayment { get; set; }
       

    }

}

