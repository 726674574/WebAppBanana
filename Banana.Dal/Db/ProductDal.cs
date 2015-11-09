
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity.Db;
using System.Data;
using Dapper;

namespace Banana.Dal.Db
{
    public class ProductDal : DalBase
    {
        #region Auto

        /// <summary>+
        /// 通过主键查询
        /// </summary>
        public Product GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [Product] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Product>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(Product entity)
        {
            string sql = @"insert into [Product]
                               ([productName], [productNo], [marketPrice], [oemPrice], [tradeprice], [collects], [hits], [smallThumPic], [bigThumPic], [addTime], [status], [productTypeId], [creater], [keyword], [articleId], [taobaolink], [sale],[videourl],[Source],[Linepayment],[IsFree])
                               values
                               (@productName, @productNo, @marketPrice, @oemPrice, @tradeprice, @collects, @hits, @smallThumPic, @bigThumPic, @addTime, @status, @productTypeId, @creater, @keyword, @articleId, @taobaolink, @sale,@videourl,@Source,@Linepayment,@IsFree)";
        
            object param = new
            {
                productName = entity.ProductName, 
                productNo = entity.ProductNo, 
                marketPrice = entity.MarketPrice, 
                oemPrice = entity.OemPrice, 
                tradeprice = entity.Tradeprice, 
                collects = entity.Collects, 
                hits = entity.Hits, 
                smallThumPic = entity.SmallThumPic, 
                bigThumPic = entity.BigThumPic, 
                addTime = entity.AddTime, 
                status = entity.Status, 
                productTypeId = entity.ProductTypeId, 
                creater = entity.Creater, 
                keyword = entity.Keyword, 
                articleId = entity.ArticleId, 
                taobaolink = entity.Taobaolink, 
                sale = entity.Sale,
                videourl=entity.VideoUrl,
                Source = entity.Source,
                Linepayment = entity.Linepayment,
                IsFree = entity.IsFree
                
                
                
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
        public int AddAndReturn(Product entity)
        {
            string sql = @"insert into [Product]
                               ([productName], [productNo], [marketPrice], [oemPrice], [tradeprice], [collects], [hits], [smallThumPic], [bigThumPic], [addTime], [status], [productTypeId], [creater], [keyword], [articleId], [taobaolink], [sale],[videourl],[Source],[Linepayment],[IsFree])
                               values
                               (@productName, @productNo, @marketPrice, @oemPrice, @tradeprice, @collects, @hits, @smallThumPic, @bigThumPic, @addTime, @status, @productTypeId, @creater, @keyword, @articleId, @taobaolink, @sale,@videourl,@Source,@Linepayment,@IsFree)";
        
            object param = new
            {
                productName = entity.ProductName, 
                productNo = entity.ProductNo, 
                marketPrice = entity.MarketPrice, 
                oemPrice = entity.OemPrice, 
                tradeprice = entity.Tradeprice, 
                collects = entity.Collects, 
                hits = entity.Hits, 
                smallThumPic = entity.SmallThumPic, 
                bigThumPic = entity.BigThumPic, 
                addTime = entity.AddTime, 
                status = entity.Status, 
                productTypeId = entity.ProductTypeId, 
                creater = entity.Creater, 
                keyword = entity.Keyword, 
                articleId = entity.ArticleId, 
                taobaolink = entity.Taobaolink, 
                sale = entity.Sale,
                videourl=entity.VideoUrl,
                Source = entity.Source,
                Linepayment = entity.Linepayment,
                IsFree = entity.IsFree
                
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                 int row = conn.Execute(sql, entity);
                    if (row > 0)
                        return conn.Query<Product>("SELECT @@IDENTITY AS Id").Single<Product>().Id;
                    else
                        return -1;
            }
       }
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(Product entity, IDbTransaction tran)
        {
            string sql = @"insert into [Product]
                               ([productName], [productNo], [marketPrice], [oemPrice], [tradeprice], [collects], [hits], [smallThumPic], [bigThumPic], [addTime], [status], [productTypeId], [creater], [keyword], [articleId], [taobaolink], [sale],[videourl],[Source],[Linepayment],[IsFree])
                               values
                               (@productName, @productNo, @marketPrice, @oemPrice, @tradeprice, @collects, @hits, @smallThumPic, @bigThumPic, @addTime, @status, @productTypeId, @creater, @keyword, @articleId, @taobaolink, @sale,@videourl,@Source,@Linepayment,@IsFree)";
            
            object param = new
            {
                productName = entity.ProductName, 
                productNo = entity.ProductNo, 
                marketPrice = entity.MarketPrice, 
                oemPrice = entity.OemPrice, 
                tradeprice = entity.Tradeprice, 
                collects = entity.Collects, 
                hits = entity.Hits, 
                smallThumPic = entity.SmallThumPic, 
                bigThumPic = entity.BigThumPic, 
                addTime = entity.AddTime, 
                status = entity.Status, 
                productTypeId = entity.ProductTypeId, 
                creater = entity.Creater, 
                keyword = entity.Keyword, 
                articleId = entity.ArticleId, 
                taobaolink = entity.Taobaolink, 
                sale = entity.Sale,
                videourl=entity.VideoUrl,
                Source = entity.Source,
                Linepayment = entity.Linepayment,
                IsFree = entity.IsFree
               
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<Product> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [Product] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Product>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<Product> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [Product] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [Product] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<Product> list = r.Read<Product>().ToList();
                return list;
            }
        }

        /// <summary>
        /// 连表获取产品
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="param"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<Product> GetJoinAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();

            if (!String.IsNullOrEmpty(where))
                where = " where [QiangGou]." + where;

            sql.AppendFormat("select count(*) from [QiangGou] with(nolock) INNER JOIN [Product] on [QiangGou].objectid=[Product].Id {0}", where);
            sql.AppendLine();

            sql.AppendFormat(@"select *
                                  from (select [Product].{0}, row_number() over(order by [Product].{1}) as rownum
                                          from [QiangGou] with(nolock) INNER JOIN [Product] 
                                           on [QiangGou].objectid=[Product].Id
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<Product> list = r.Read<Product>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [Product] set {0} where {1}", fields, where);
            
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
            string sql = String.Format("update [Product] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
		public int Update(Product entity)
		{
			//GetUpdateSql2
            string sql = @"update [Product] set productName=@productName, productNo=@productNo, marketPrice=@marketPrice, oemPrice=@oemPrice, tradeprice=@tradeprice, collects=@collects, hits=@hits, smallThumPic=@smallThumPic, bigThumPic=@bigThumPic, addTime=@addTime, status=@status, productTypeId=@productTypeId, creater=@creater, keyword=@keyword, articleId=@articleId, taobaolink=@taobaolink, sale=@sale,videourl=@videourl,Source=@Source,Linepayment=@Linepayment,IsFree=@IsFree  where id=@id ";
			   object param = new
            {
               id = entity.Id, 
               productName = entity.ProductName, 
               productNo = entity.ProductNo, 
               marketPrice = entity.MarketPrice, 
               oemPrice = entity.OemPrice, 
               tradeprice = entity.Tradeprice, 
               collects = entity.Collects, 
               hits = entity.Hits, 
               smallThumPic = entity.SmallThumPic, 
               bigThumPic = entity.BigThumPic, 
               addTime = entity.AddTime, 
               status = entity.Status, 
               productTypeId = entity.ProductTypeId, 
               creater = entity.Creater, 
               keyword = entity.Keyword, 
               articleId = entity.ArticleId, 
               taobaolink = entity.Taobaolink, 
               sale = entity.Sale,
               videourl = entity.VideoUrl,
               Source = entity.Source,
               Linepayment = entity.Linepayment,
               IsFree = entity.IsFree
              
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
            string sql=String.Format("delete from [Product] where {0}",where);
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
            string sql=String.Format("Select CAST(MAX({0}) AS float) from [Product] ",filed);
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

