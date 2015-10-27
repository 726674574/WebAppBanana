
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
    public class AdminResourceDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(AdminResource entity)
        {
            string sql = @"insert into [AdminResource]
                               ([id], [className], [parentId], [parentPath], [orderId], [linkURL], [resourceType], [resourceId], [isAdmin])
                               values
                               (@id, @className, @parentId, @parentPath, @orderId, @linkURL, @resourceType, @resourceId, @isAdmin)";
        
            object param = new
            {
                id = entity.Id, 
                className = entity.ClassName, 
                parentId = entity.ParentId, 
                parentPath = entity.ParentPath, 
                orderId = entity.OrderId, 
                linkURL = entity.LinkURL, 
                resourceType = entity.ResourceType, 
                resourceId = entity.ResourceId, 
                isAdmin = entity.IsAdmin
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
        public int Add(AdminResource entity, IDbTransaction tran)
        {
            string sql = @"insert into [AdminResource]
                               ([id], [className], [parentId], [parentPath], [orderId], [linkURL], [resourceType], [resourceId], [isAdmin])
                               values
                               (@id, @className, @parentId, @parentPath, @orderId, @linkURL, @resourceType, @resourceId, @isAdmin)";
            
            object param = new
            {
                id = entity.Id, 
                className = entity.ClassName, 
                parentId = entity.ParentId, 
                parentPath = entity.ParentPath, 
                orderId = entity.OrderId, 
                linkURL = entity.LinkURL, 
                resourceType = entity.ResourceType, 
                resourceId = entity.ResourceId, 
                isAdmin = entity.IsAdmin
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<AdminResource> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [AdminResource] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<AdminResource>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<AdminResource> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [AdminResource] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [AdminResource] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<AdminResource> list = r.Read<AdminResource>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [AdminResource] set {0} where {1}", fields, where);
            
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
            string sql = String.Format("update [AdminResource] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
        #endregion
        
        #region Extend
        #endregion
    }
}

