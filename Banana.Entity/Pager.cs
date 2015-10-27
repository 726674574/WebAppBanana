using System;
using System.Collections.Generic;
using System.Text;

namespace Banana.Entity
{
    public class Pager
    {
        #region 字段
        private int _pageSize;
        private int _pageIndex;
        private bool _doCount;
        #endregion 字段结束

        #region 属性
        /// <summary>
        /// 页数大小
        /// </summary>
        public int PageSize
        {
            set { _pageSize = value; }
            get { return _pageSize; }
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            set { _pageIndex = value; }
            get { return _pageIndex; }
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public bool DoCount
        {
            set { _doCount = value; }
            get { return _doCount; }
        }
        #endregion
    }
}
