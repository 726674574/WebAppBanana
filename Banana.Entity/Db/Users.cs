using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class Users
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Mobile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? UserType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserIP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RemoteHost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserAgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Imsi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Imei { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Pno { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Udid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String HeadUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String NickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ProvinceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? CityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Valid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? CollegeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// 香蕉数
        /// </summary>
        public Int32 BananaCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Address { get; set; }

    }
}

