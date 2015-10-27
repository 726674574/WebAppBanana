﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity.Db;
using System.Data;
using Dapper;
using Banana.Dal;

namespace Banana.Dal.Db
{
    public class OperateLogDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 通过主键查询
        /// </summary>
        public OperateLog GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [OperateLog] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<OperateLog>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(OperateLog entity)
        {
            string sql = @"insert into [OperateLog]
                               ([module], [objectId], [userId], [userName], [content], [addTime])
                               values
                               (@module, @objectId, @userId, @userName, @content, @addTime)";
        
            object param = new
            {
                module = entity.Module, 
                objectId = entity.ObjectId, 
                userId = entity.UserId, 
                userName = entity.UserName, 
                content = entity.Content, 
                addTime = entity.AddTime
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
        public int Add(OperateLog entity, IDbTransaction tran)
        {
            string sql = @"insert into [OperateLog]
                               ([module], [objectId], [userId], [userName], [content], [addTime])
                               values
                               (@module, @objectId, @userId, @userName, @content, @addTime)";
            
            object param = new
            {
                module = entity.Module, 
                objectId = entity.ObjectId, 
                userId = entity.UserId, 
                userName = entity.UserName, 
                content = entity.Content, 
                addTime = entity.AddTime
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<OperateLog> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [OperateLog] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<OperateLog>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<OperateLog> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [OperateLog] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [OperateLog] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<OperateLog> list = r.Read<OperateLog>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [OperateLog] set {0} where {1}", fields, where);
            
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
            string sql = String.Format("update [OperateLog] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
        #endregion
        
        #region Extend
        #endregion
    }
}

