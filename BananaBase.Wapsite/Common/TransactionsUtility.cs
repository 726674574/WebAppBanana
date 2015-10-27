using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

namespace Banana.Wapsite
{
    public static class TransactionsUtility
    {
        public static void Action(Action method)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                method();
                trans.Complete();
            }
        }
        public static void ActionRich(Action method)
        {
            TransactionOptions tOpt = new TransactionOptions();
            ////设置TransactionOptions模式
            //tOpt.IsolationLevel = IsolationLevel.ReadCommitted;
            // 设置超时间隔为2分钟，默认为60秒
            tOpt.Timeout = new TimeSpan(0, 2, 0);
            using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, tOpt))
            {
                method();
                trans.Complete();
            }
        }
    }
}