using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class UserCollection
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Productid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 用户收藏产品表
        /// </summary>
        public Int32? UserId { get; set; }

    }
}

