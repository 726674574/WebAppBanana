using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity;

namespace Banana.Bll
{
    public class ResultStatus
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public bool Success { get; set; }

        public ResultStatus()
        {
            this.Code = StatusCollection.Success.Code;
            this.Description = StatusCollection.Success.Description;
            this.Success = true;
        }
    }
}
