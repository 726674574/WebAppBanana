using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class OperateLog
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Module { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ObjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime { get; set; }

    }
}

