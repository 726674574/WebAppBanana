
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity.Db;
using System.Data;
using Dapper;

namespace Banana.Dal.Db
{
    public class OrderDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 通过主键查询
        /// </summary>
        public Order GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [Order] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Order>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(Order entity)
        {
            string sql = @"insert into [Order]
                               ([orderNo], [userId], [reciverName], [revicerTel], [revicerAddress], [revicerPostCode], [remark], [count], [totalPrice], [realPrice], [status], [payMentTypeId], [deliverTypId], [deliveNo], [orderTime], [paymentTime], [deliverPrice],[Pno],[ExpressNo],[HigherOrderNo],[BananaCount])
                               values
                               (@orderNo, @userId, @reciverName, @revicerTel, @revicerAddress, @revicerPostCode, @remark, @count, @totalPrice, @realPrice, @status, @payMentTypeId, @deliverTypId, @deliveNo, @orderTime, @paymentTime, @deliverPrice,@pno,@expressno,@higherorderno,@BananaCount)";

            object param = new
            {
                orderNo = entity.OrderNo,
                userId = entity.UserId,
                reciverName = entity.ReciverName,
                revicerTel = entity.RevicerTel,
                revicerAddress = entity.RevicerAddress,
                revicerPostCode = entity.RevicerPostCode,
                remark = entity.Remark,
                count = entity.Count,
                totalPrice = entity.TotalPrice,
                realPrice = entity.RealPrice,
                status = entity.Status,
                payMentTypeId = entity.PayMentTypeId,
                deliverTypId = entity.DeliverTypId,
                deliveNo = entity.DeliveNo,
                orderTime = entity.OrderTime,
                paymentTime = entity.PaymentTime,
                deliverPrice = entity.DeliverPrice,
                pno = entity.Pno,
                expressno = entity.ExpressNo,
                higherorderno = entity.HigherOrderNo,
                BananaCount = entity.BananaCount,
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
        public int AddAndReturn(Order entity)
        {
            string sql = @"insert into [Order]
                               ([orderNo], [userId], [reciverName], [revicerTel], [revicerAddress], [revicerPostCode], [remark], [count], [totalPrice], [realPrice], [status], [payMentTypeId], [deliverTypId], [deliveNo], [orderTime], [paymentTime], [deliverPrice],[Pno],[ExpressNo],[HigherOrderNo],[BananaCount])
                               values
                               (@orderNo, @userId, @reciverName, @revicerTel, @revicerAddress, @revicerPostCode, @remark, @count, @totalPrice, @realPrice, @status, @payMentTypeId, @deliverTypId, @deliveNo, @orderTime, @paymentTime, @deliverPrice,@pno,@expressno,@higherorderno,@BananaCount)";

            object param = new
            {
                orderNo = entity.OrderNo,
                userId = entity.UserId,
                reciverName = entity.ReciverName,
                revicerTel = entity.RevicerTel,
                revicerAddress = entity.RevicerAddress,
                revicerPostCode = entity.RevicerPostCode,
                remark = entity.Remark,
                count = entity.Count,
                totalPrice = entity.TotalPrice,
                realPrice = entity.RealPrice,
                status = entity.Status,
                payMentTypeId = entity.PayMentTypeId,
                deliverTypId = entity.DeliverTypId,
                deliveNo = entity.DeliveNo,
                orderTime = entity.OrderTime,
                paymentTime = entity.PaymentTime,
                deliverPrice = entity.DeliverPrice,
                pno = entity.Pno,
                expressno = entity.ExpressNo,
                higherorderno = entity.HigherOrderNo,
                BananaCount = entity.BananaCount
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                 int row = conn.Execute(sql, entity);
                    if (row > 0)
                        return conn.Query<Order>("SELECT @@IDENTITY AS Id").Single<Order>().Id;
                    else
                        return -1;
            }
       }
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(Order entity, IDbTransaction tran)
        {
            string sql = @"insert into [Order]
                               ([orderNo], [userId], [reciverName], [revicerTel], [revicerAddress], [revicerPostCode], [remark], [count], [totalPrice], [realPrice], [status], [payMentTypeId], [deliverTypId], [deliveNo], [orderTime], [paymentTime], [deliverPrice],[ExpressNo],[HigherOrderNo],[BananaCount])
                               values
                               (@orderNo, @userId, @reciverName, @revicerTel, @revicerAddress, @revicerPostCode, @remark, @count, @totalPrice, @realPrice, @status, @payMentTypeId, @deliverTypId, @deliveNo, @orderTime, @paymentTime, @deliverPrice,@expressno,@higherorderno,@BananaCount)";
            
            object param = new
            {
                orderNo = entity.OrderNo, 
                userId = entity.UserId, 
                reciverName = entity.ReciverName, 
                revicerTel = entity.RevicerTel, 
                revicerAddress = entity.RevicerAddress, 
                revicerPostCode = entity.RevicerPostCode, 
                remark = entity.Remark, 
                count = entity.Count, 
                totalPrice = entity.TotalPrice, 
                realPrice = entity.RealPrice, 
                status = entity.Status, 
                payMentTypeId = entity.PayMentTypeId, 
                deliverTypId = entity.DeliverTypId, 
                deliveNo = entity.DeliveNo, 
                orderTime = entity.OrderTime, 
                paymentTime = entity.PaymentTime, 
                deliverPrice = entity.DeliverPrice,
                expressno = entity.ExpressNo,
                higherorderno = entity.HigherOrderNo,
                BananaCount = entity.BananaCount
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<Order> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [Order] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Order>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<Order> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [Order] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [Order] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<Order> list = r.Read<Order>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [Order] set {0} where {1}", fields, where);
            
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
            string sql = String.Format("update [Order] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
		public int Update(Order entity)
		{
			//GetUpdateSql2
            string sql = @"update [Order] set orderNo=@orderNo, userId=@userId, reciverName=@reciverName, revicerTel=@revicerTel, revicerAddress=@revicerAddress, revicerPostCode=@revicerPostCode, remark=@remark, count=@count, totalPrice=@totalPrice, realPrice=@realPrice, status=@status, payMentTypeId=@payMentTypeId, deliverTypId=@deliverTypId, deliveNo=@deliveNo, orderTime=@orderTime, paymentTime=@paymentTime, deliverPrice=@deliverPrice,expressno=@expressno,higherorderno=@higherorderno,BananaCount=@BananaCount  where id=@id ";
			   object param = new
            {
               id = entity.Id, 
               orderNo = entity.OrderNo, 
               userId = entity.UserId, 
               reciverName = entity.ReciverName, 
               revicerTel = entity.RevicerTel, 
               revicerAddress = entity.RevicerAddress, 
               revicerPostCode = entity.RevicerPostCode, 
               remark = entity.Remark, 
               count = entity.Count, 
               totalPrice = entity.TotalPrice, 
               realPrice = entity.RealPrice, 
               status = entity.Status, 
               payMentTypeId = entity.PayMentTypeId, 
               deliverTypId = entity.DeliverTypId, 
               deliveNo = entity.DeliveNo, 
               orderTime = entity.OrderTime, 
               paymentTime = entity.PaymentTime, 
               deliverPrice = entity.DeliverPrice,
               expressno = entity.ExpressNo,
               higherorderno = entity.HigherOrderNo,
               BananaCount = entity.BananaCount
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
            string sql=String.Format("delete from [Order] where {0}",where);
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
            string sql=String.Format("Select CAST(MAX({0}) AS float) from [Order] ",filed);
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

