
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
    public partial class ProductTypeDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 通过主键查询
        /// </summary>
        public ProductType GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [ProductType] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<ProductType>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(ProductType entity)
        {
            string sql = @"insert into [ProductType]
                               ([name], [order], [inPrice], [keywords], [description], [type], [typeInt])
                               values
                               (@name, @order, @inPrice, @keywords, @description, @type, @typeInt)";
        
            object param = new
            {
                name = entity.Name, 
                order = entity.Order, 
                inPrice = entity.InPrice, 
                keywords = entity.Keywords, 
                description = entity.Description, 
                type = entity.Type, 
                typeInt = entity.TypeInt
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
        public int Add(ProductType entity, IDbTransaction tran)
        {
            string sql = @"insert into [ProductType]
                               ([name], [order], [inPrice], [keywords], [description], [type], [typeInt])
                               values
                               (@name, @order, @inPrice, @keywords, @description, @type, @typeInt)";
            
            object param = new
            {
                name = entity.Name, 
                order = entity.Order, 
                inPrice = entity.InPrice, 
                keywords = entity.Keywords, 
                description = entity.Description, 
                type = entity.Type, 
                typeInt = entity.TypeInt
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<ProductType> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [ProductType] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<ProductType>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<ProductType> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [ProductType] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [ProductType] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<ProductType> list = r.Read<ProductType>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [ProductType] set {0} where {1}", fields, where);
            
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
            string sql = String.Format("update [ProductType] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
        #endregion
        
        #region Extend
        /// <summary>
        /// 修改一条数据,参数为实体Entity, 需要先取得实体,更改实体属性后再操作.
        /// </summary>
        public bool Update2(ProductType entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update [ProductType] set ");
            sb.Append("[Name] = @Name, ");
            sb.Append("[Order] = @Order, ");
            sb.Append("[InPrice] = @InPrice, ");
            sb.Append("[Keywords] = @Keywords, ");
            sb.Append("[Description] = @Description, ");
            sb.Append("[Type] = @Type, ");
            sb.Append("[TypeInt] = @TypeInt ");
            sb.Append(" where   [Id]= @Id ");

            int rows = 0;
            using (IDbConnection conn = OpenConnection())
            {
                string sql = sb.ToString();
                rows = conn.Execute(sql, entity);
            }
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除多条数据，参数为删除条件语句，返回True or False
        /// </summary>
        public bool DeleteList(string where)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete From [ProductType] ");
            sb.Append("  where {0} ");

            try
            {
                using (IDbConnection conn =OpenConnection())
                {
                    string sql = string.Format(sb.ToString(), where.Trim());
                    return conn.Execute(sql) > 0 ? true : false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

