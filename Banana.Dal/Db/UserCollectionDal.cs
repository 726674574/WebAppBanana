
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity.Db;
using System.Data;
using Dapper;

namespace Banana.Dal.Db
{
    public class UserCollectionDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 通过主键查询
        /// </summary>
        public UserCollection GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [UserCollection] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<UserCollection>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(UserCollection entity)
        {
            string sql = @"insert into [UserCollection]
                               ([productid], [addTime], [userId])
                               values
                               (@productid, @addTime, @userId)";
        
            object param = new
            {
                productid = entity.Productid, 
                addTime = entity.AddTime, 
                userId = entity.UserId
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql, param);
                return count;
            }
       }
        /// <summary>
        /// 添加一条记录 返回自增ID
        /// </summary>
        public int AddAndReturn(UserCollection entity)
        {
            string sql = @"insert into [UserCollection]
                               ([productid], [addTime], [userId])
                               values
                               (@productid, @addTime, @userId)";
        
            object param = new
            {
                productid = entity.Productid, 
                addTime = entity.AddTime, 
                userId = entity.UserId
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                 int row = conn.Execute(sql, entity);
                    if (row > 0)
                        return conn.Query<UserCollection>("SELECT @@IDENTITY AS Id").Single<UserCollection>().Id;
                    else
                        return -1;
            }
       }
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(UserCollection entity, IDbTransaction tran)
        {
            string sql = @"insert into [UserCollection]
                               ([productid], [addTime], [userId])
                               values
                               (@productid, @addTime, @userId)";
            
            object param = new
            {
                productid = entity.Productid, 
                addTime = entity.AddTime, 
                userId = entity.UserId
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<UserCollection> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [UserCollection] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<UserCollection>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<UserCollection> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [UserCollection] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [UserCollection] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<UserCollection> list = r.Read<UserCollection>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [UserCollection] set {0} where {1}", fields, where);
            
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
            string sql = String.Format("update [UserCollection] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
		public int Update(UserCollection entity)
		{
			//GetUpdateSql2
			  string sql = @"update [UserCollection] set productid=@productid, addTime=@addTime, userId=@userId  where id=@id ";
			   object param = new
            {
               id = entity.Id, 
               productid = entity.Productid, 
               addTime = entity.AddTime, 
               userId = entity.UserId
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql, param);
                return count;
            }
		}
		/// <summary>
        /// 删除
        /// </summary>
        public int DeleteList(string where)
        {
            string sql=String.Format("delete from [UserCollection] where {0}",where);
             using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql);
                return count;
            }
           
        }
        
        /// <summary>
        /// 获取 file max
        /// </summary>
        public double GetMaxField(string filed)
        {
            string sql=String.Format("Select CAST(MAX({0}) AS float) from [UserCollection] ",filed);
              using (IDbConnection conn = OpenConnection())
            {
                try
                {
                  return conn.Query<double>(sql).Single();
                }catch{
                  return 0.0d;
                }
            }
        }
        
        
        #endregion
        
        #region Extend
        #endregion
    }
}

