using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class AdLocation
    {
        /// <summary>
        /// 自增列
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 广告位位置
        /// </summary>
        public String LocationName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Description { get; set; }

    }
}

