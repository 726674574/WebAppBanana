using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Bll
{
    public class Page<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public int PageCount { get; private set; }
        public IList<T> Items { get; set; }

        public Page(int pageIndex, int pageSize, int recordCount, IList<T> items)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.RecordCount = recordCount;
            this.PageCount = (recordCount % pageSize == 0) ? (recordCount / pageSize) : (recordCount / pageSize + 1);
            this.Items = items;
        }
    }
}
