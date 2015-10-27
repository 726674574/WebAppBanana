using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class Admins
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LogName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LogPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String EditRole { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CheckRole { get; set; }

    }
}

