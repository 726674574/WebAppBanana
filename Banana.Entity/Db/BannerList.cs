using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class BannerList
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 广告图片
        /// </summary>
        public String Bannerimg { get; set; }

        /// <summary>
        /// 广告链接地址
        /// </summary>
        public String Linkurl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Int32? Orderid { get; set; }

        /// <summary>
        /// 显示位置
        /// </summary>
        public Int32? Adid { get; set; }

        public string Recommendedinfo { get; set; }

    }
}

