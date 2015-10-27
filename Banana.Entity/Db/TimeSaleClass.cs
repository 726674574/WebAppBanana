using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class TimeSaleClass
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        public string ImgUrl { get; set; }

        public int ObjectId { get; set; }

        public decimal SalePrice { get; set; }

        public decimal MarketPrice { get; set; }

    }
}

