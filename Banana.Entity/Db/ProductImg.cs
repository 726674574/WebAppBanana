using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class ProductImg
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 产品图片路径
        /// </summary>
        public String ImgUrl { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public Int32? ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? OrderId { get; set; }

    }
}

