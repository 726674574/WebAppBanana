using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Bll
{
    public class ResultSet
    {
        public ResultStatus ResultStatus { get; set; }

        public ResultSet()
        {
            this.ResultStatus = new ResultStatus();
        }

        public ResultSet(ResultStatus resultStatus)
        {
            this.ResultStatus = resultStatus;
        }

    }

    public class ResultSet<T> : ResultSet
    {
        public T Entity { get; set; }

        public ResultSet()
            : base()
        {

        }

        public ResultSet(ResultStatus resultStatus, T entity)
            : base(resultStatus)
        {
            this.Entity = entity;
        }
    }
}
