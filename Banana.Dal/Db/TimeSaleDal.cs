
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity.Db;
using System.Data;
using Dapper;

namespace Banana.Dal.Db
{
    public class TimeSaleDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 通过主键查询
        /// </summary>
        public TimeSale GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [TimeSale] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<TimeSale>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(TimeSale entity)
        {
            string sql = @"insert into [TimeSale]
                               ([objectId], [addTime], [salePrice], [classId], [orderId])
                               values
                               (@objectId, @addTime, @salePrice, @classId, @orderId)";
        
            object param = new
            {
                objectId = entity.ObjectId, 
                addTime = entity.AddTime, 
                salePrice = entity.SalePrice, 
                classId = entity.ClassId, 
                orderId = entity.OrderId
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql, param);
                return count;
            }
       }
       
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(TimeSale entity, IDbTransaction tran)
        {
            string sql = @"insert into [TimeSale]
                               ([objectId], [addTime], [salePrice], [classId], [orderId])
                               values
                               (@objectId, @addTime, @salePrice, @classId, @orderId)";
            
            object param = new
            {
                objectId = entity.ObjectId, 
                addTime = entity.AddTime, 
                salePrice = entity.SalePrice, 
                classId = entity.ClassId, 
                orderId = entity.OrderId
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<TimeSale> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [TimeSale] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<TimeSale>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<TimeSale> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [TimeSale] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [TimeSale] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<TimeSale> list = r.Read<TimeSale>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [TimeSale] set {0} where {1}", fields, where);
            
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql, param);
                return count;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where, IDbTransaction tran)
        {
            string sql = String.Format("update [TimeSale] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
        #endregion
        
        #region Extend
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<TimeSale> GetAllandProduct(int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [TimeSale] A with(nolock) {0}", where);
            sql.AppendLine();

            sql.AppendFormat(@"select *
                                  from (select A.*,B.Id as proid,b.productname,b.SmallThumPic,b.BigThumPic,b.MarketPrice,b.OemPrice,b.Tradeprice, row_number() over(order by {0}) as rownum
                                          from [TimeSale] A with(nolock) left join Product B ON A.ObjectId =B.Id
                                          {1} ) as T
                                 where rownum between {2} and {3}",
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<TimeSale> list = r.Read<TimeSale>().ToList();
                return list;
            }
        }
        #endregion
    }
}

