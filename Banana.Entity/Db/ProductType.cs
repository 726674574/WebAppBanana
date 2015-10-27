using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity.Db
{
    public class ProductType
    {
        /// <summary>
        /// 标识id自增加列
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 产品类别名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 排序id
        /// </summary>
        public Int32? Order { get; set; }

        /// <summary>
        /// 价格范围 1-200
        /// </summary>
        public String InPrice { get; set; }

        /// <summary>
        /// 关键词 搜索显示
        /// </summary>
        public String Keywords { get; set; }

        /// <summary>
        /// 类别描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 类别 级别  1:1级  2:2级  3:3级类别
        /// </summary>
        public Int32 Type { get; set; }

        /// <summary>
        /// 父级类别 id 当月type=1 为一级类别时 为0
        /// </summary>
        public Int32? TypeInt { get; set; }

    }
}

