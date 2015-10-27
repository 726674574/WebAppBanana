
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
    public class AdminsDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 通过主键查询
        /// </summary>
        public Admins GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [Admins] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Admins>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(Admins entity)
        {
            string sql = @"insert into [Admins]
                               ([logName], [logPwd], [userName], [role], [editRole], [checkRole])
                               values
                               (@logName, @logPwd, @userName, @role, @editRole, @checkRole)";
        
            object param = new
            {
                logName = entity.LogName, 
                logPwd = entity.LogPwd, 
                userName = entity.UserName, 
                role = entity.Role, 
                editRole = entity.EditRole, 
                checkRole = entity.CheckRole
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
        public int Add(Admins entity, IDbTransaction tran)
        {
            string sql = @"insert into [Admins]
                               ([logName], [logPwd], [userName], [role], [editRole], [checkRole])
                               values
                               (@logName, @logPwd, @userName, @role, @editRole, @checkRole)";
            
            object param = new
            {
                logName = entity.LogName, 
                logPwd = entity.LogPwd, 
                userName = entity.UserName, 
                role = entity.Role, 
                editRole = entity.EditRole, 
                checkRole = entity.CheckRole
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<Admins> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [Admins] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Admins>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<Admins> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [Admins] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [Admins] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<Admins> list = r.Read<Admins>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [Admins] set {0} where {1}", fields, where);
            
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
            string sql = String.Format("update [Admins] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
        #endregion
        
        #region Extend
        /// <summary>
        /// 通过登录名获取管理员信息
        /// </summary>
        public Admins GetAdminByLogName(string loginName)
        {
            string sql = "select * from [Admins] with(nolock) where [LogName] = @name";
            object param = new { name = loginName };

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Admins>(sql, param);
                return r.FirstOrDefault();
            }
        }
        #endregion
    }
}

