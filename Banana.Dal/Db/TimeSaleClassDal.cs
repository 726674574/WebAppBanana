﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity.Db;
using System.Data;
using Dapper;

namespace Banana.Dal.Db
{
    public class TimeSaleClassDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 通过主键查询
        /// </summary>
        public TimeSaleClass GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [TimeSaleClass] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<TimeSaleClass>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(TimeSaleClass entity)
        {
            string sql = @"insert into [TimeSaleClass]
                               ([name], [startTime], [endTime])
                               values
                               (@name, @startTime, @endTime)";
        
            object param = new
            {
                name = entity.Name, 
                startTime = entity.StartTime, 
                endTime = entity.EndTime
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
        public int Add(TimeSaleClass entity, IDbTransaction tran)
        {
            string sql = @"insert into [TimeSaleClass]
                               ([name], [startTime], [endTime])
                               values
                               (@name, @startTime, @endTime)";
            
            object param = new
            {
                name = entity.Name, 
                startTime = entity.StartTime, 
                endTime = entity.EndTime
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<TimeSaleClass> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [TimeSaleClass] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<TimeSaleClass>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<TimeSaleClass> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [TimeSaleClass] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [TimeSaleClass] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<TimeSaleClass> list = r.Read<TimeSaleClass>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [TimeSaleClass] set {0} where {1}", fields, where);
            
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
            string sql = String.Format("update [TimeSaleClass] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
        #endregion
        
        #region Extend
        /// <summary>
        /// 获取首页限时抢购
        /// </summary>
        public IList<TimeSaleClass> GetTimeSaleandImg()
        {
            string sql = String.Format("select  A.*,B.ObjectId,B.SalePrice,C.SmallThumPic as imgurl,C.MarketPrice from TimeSaleClass A left join TimeSale B on A.Id =B.ClassId left join Product C ON B.ObjectId=C.Id  where GETDATE() between StartTime and EndTime order by B.OrderId desc");
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<TimeSaleClass>(sql, "");
                return r.ToList();
            }
        }
        #endregion
    }
}

