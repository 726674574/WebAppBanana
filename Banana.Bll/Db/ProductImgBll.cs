
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
    public class ProductImgBll : BalBase
    {
        #region Auto
        
        /// <summary>
        /// 通过主键查询
        /// </summary>
        public ResultSet<ProductImg> GetByPrimaryKey(Int32 primaryKey)
        {
            Func<Int32, ResultStatus> validate = (_primaryKey) =>
            {
                if (_primaryKey <= 0)
                    return new ResultStatus()
                    {
                        Success = false,
                        Code = StatusCollection.ParameterError.Code,
                        Description = "参数 primaryKey必须大于0"
                    };

                return new ResultStatus();
            };

            Func<Int32, ProductImg> op = (_primaryKey) =>
            {
                return new ProductImgDal().GetByPrimaryKey(_primaryKey);
            };
            return HandleBusiness<Int32, ProductImg>(primaryKey, op, validate);
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public ResultSet Add(ProductImg entity)
        {
            Func<ProductImg, ResultStatus> validate = (_entity) =>
            {
                return new ResultStatus();
            };

            Func<ProductImg, ResultStatus> op = (_entity) =>
            {
                int ret = new ProductImgDal().Add(entity);
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
        /// 获取所有
        /// </summary>
        public ResultSet<IList<ProductImg>> GetAll(string fields, string where, object param, string orderBy)
        {
            Func<string, string, object, string, ResultStatus> validate = (_fields, _where, _param, _orderBy) =>
            {
                return new ResultStatus();
            };

            Func<string, string, object, string, IList<ProductImg>> op = (_fields, _where, _param, _orderBy) =>
            {
                return new ProductImgDal().GetAll(_fields, _where, _param, _orderBy);
            };

            return HandleBusiness(fields, where, param, orderBy, op, validate);
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public ResultSet<Page<ProductImg>> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy)
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

            Func<string, int, int, string, object, string, Page<ProductImg>> op = (_fields, _pageIndex, _pageSize, _where, _param, _orderBy) =>
            {
                int recordCount = 0;

                IList<ProductImg> list = new ProductImgDal().GetAll(_fields, _pageIndex, _pageSize, _where, _param, _orderBy, out recordCount);
                return new Page<ProductImg>(_pageIndex, _pageSize, recordCount, list);
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
                int ret = new ProductImgDal().Update(_fields, _param, _where);
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
		 public ResultSet Update(ProductImg entity)
        {
            Func<ProductImg, ResultStatus> validate = (_entity) =>
            {
                return new ResultStatus();
            };

            Func<ProductImg, ResultStatus> op = (_entity) =>
            {
                int ret = new ProductImgDal().Update(entity);
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
                int ret = new ProductImgDal().DeleteList(_where);
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
            return new ProductImgDal().GetMaxField(filed);
        }
        
        
       
        
        #endregion
        
        #region Extend
        #endregion
    }
}

