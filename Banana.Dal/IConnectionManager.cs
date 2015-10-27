using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Banana.Dal
{
    public interface IConnectionManager
    {
        void SetConnection();
        void SetConnection(string connStringName);

        IDbTransaction GetTransaction(string connectionStringName);
        IDbTransaction GetTransaction();
    }
}
