using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class AdminResource
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ParentPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LinkURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ResourceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ResourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsAdmin { get; set; }

    }
}

