using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class Statistics
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Objectid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Appcount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Productconfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Createtime { get; set; }

    }

    public class Productconfig
    {
        public int pro1 { get; set; }
        public int pro2 { get; set; }
        public int pro3 { get; set; }
    }

}

