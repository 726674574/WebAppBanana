
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity;
using Banana.Entity.Db;
using System.Data;
using Banana.Dal.Db;

namespace Banana.Bll.Db
{
    public class UserCollectionBll : BalBase
    {
        #region Auto
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public ResultSet Add(UserCollection entity)
        {
            Func<UserCollection, ResultStatus> validate = (_entity) =>
            {
                return new ResultStatus();
            };

            Func<UserCollection, ResultStatus> op = (_entity) =>
            {
                int ret = new UserCollectionDal().Add(entity);
                if (ret > 0)
                    return new ResultStatus();
                else
                    return new ResultStatus()
                    {
                        Success = false,
                        Code = StatusCollection.AddFailed.Code,
                        Description = StatusCollection.AddFailed.Description
                    };
            };

            return HandleBusiness(entity, op, validate);
        }
         /// <summary>
        /// 添加一条记录 返回自增ID
        /// </summary>
         public int AddAndReturn(UserCollection entity)
        {
          return  new UserCollectionDal().AddAndReturn(entity);
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        public ResultSet<IList<UserCollection>> GetAll(string fields, string where, object param, string orderBy)
        {
            Func<string, string, object, string, ResultStatus> validate = (_fields, _where, _param, _orderBy) =>
            {
                return new ResultStatus();
            };

            Func<string, string, object, string, IList<UserCollection>> op = (_fields, _where, _param, _orderBy) =>
            {
                return new UserCollectionDal().GetAll(_fields, _where, _param, _orderBy);
            };

            return HandleBusiness(fields, where, param, orderBy, op, validate);
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public ResultSet<Page<UserCollection>> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy)
        {
            Func<string, int, int, string, object, string, ResultStatus> validate = (_fields, _pageIndex, _pageSize, _where, _param, _orderBy) =>
            {
                if (_pageIndex <= 0)
                    return new ResultStatus()
                    {
                        Code = StatusCollection.ParameterError.Code,
                        Description = "参数 pageIndex 必须大于0",
                        Success = false
                    };

                if (_pageSize <= 0 || _pageSize > 100)
                    return new ResultStatus()
                    {
                        Code = StatusCollection.ParameterError.Code,
                        Description = "参数 pageSize 必须大于0，且小于等于100",
                        Success = false
                    };

                return new ResultStatus();
            };

            Func<string, int, int, string, object, string, Page<UserCollection>> op = (_fields, _pageIndex, _pageSize, _where, _param, _orderBy) =>
            {
                int recordCount = 0;

                IList<UserCollection> list = new UserCollectionDal().GetAll(_fields, _pageIndex, _pageSize, _where, _param, _orderBy, out recordCount);
                return new Page<UserCollection>(_pageIndex, _pageSize, recordCount, list);
            };

            return HandleBusiness(fields, pageIndex, pageSize, where, param, orderBy, op, validate);
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public ResultSet Update(string fields, object param, string where)
        {
			
            Func<string, object, string, ResultStatus> validate = (_fields, _param, _where) =>
            {
                if (String.IsNullOrEmpty(_fields))
                    return new ResultStatus()
                    {
                        Code = StatusCollection.ParameterError.Code,
                        Description = "参数 fields 不能为空",
                        Success = false
                    };

                if (_param == null)
                    return new ResultStatus()
                    {
                        Code = StatusCollection.ParameterError.Code,
                        Description = "参数 param 不能为空",
                        Success = false
                    };
            
                return new ResultStatus();
            };

            Func<string, object, string, ResultStatus> op = (_fields, _param, _where) =>
            {
                int ret = new UserCollectionDal().Update(_fields, _param, _where);
                if (ret > 0)
                    return new ResultStatus();
                else
                    return new ResultStatus()
                    {
                        Success = false,
                        Code = StatusCollection.UpdateFailed.Code,
                        Description = StatusCollection.UpdateFailed.Description
                    };
            };

            return HandleBusiness(fields, param, where, op, validate);
        }
        /// <summary>
        /// 更新
        /// </summary>
		 public ResultSet Update(UserCollection entity)
        {
            Func<UserCollection, ResultStatus> validate = (_entity) =>
            {
                return new ResultStatus();
            };

            Func<UserCollection, ResultStatus> op = (_entity) =>
            {
                int ret = new UserCollectionDal().Update(entity);
                if (ret > 0)
                    return new ResultStatus();
                else
                    return new ResultStatus()
                    {
                        Success = false,
                        Code = StatusCollection.AddFailed.Code,
                        Description = StatusCollection.AddFailed.Description
                    };
            };

            return HandleBusiness(entity, op, validate);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        public ResultSet DeleteList(string where)
        {
            Func<string, ResultStatus> validate = (_where) =>
            {
                if (String.IsNullOrEmpty(_where))
                    return new ResultStatus()
                    {
                        Code = StatusCollection.ParameterError.Code,
                        Description = "参数 where 不能为空",
                        Success = false
                    };
                 return new ResultStatus();
            };

            Func<string,ResultStatus> op = (_where) =>
            {
                int ret = new UserCollectionDal().DeleteList(_where);
                if (ret > 0)
                    return new ResultStatus();
                else
                    return new ResultStatus()
                    {
                        Success = false,
                        Code = StatusCollection.UpdateFailed.Code,
                        Description = StatusCollection.UpdateFailed.Description
                    };
            };

            return HandleBusiness(where, op, validate);
        }
        
        /// <summary>
        /// 获取 file max
        /// </summary>
         public double GetMaxField(string filed)
        {
            return new UserCollectionDal().GetMaxField(filed);
        }
        
        
       
        
        #endregion
        
        #region Extend
        #endregion
    }
}

