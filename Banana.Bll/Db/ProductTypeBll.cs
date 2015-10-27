
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
    public class ProductTypeBll : BalBase
    {
        #region Auto
        
        /// <summary>
        /// 通过主键查询
        /// </summary>
        public ResultSet<ProductType> GetByPrimaryKey(Int32 primaryKey)
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

            Func<Int32, ProductType> op = (_primaryKey) =>
            {
                return new ProductTypeDal().GetByPrimaryKey(_primaryKey);
            };
            return HandleBusiness<Int32, ProductType>(primaryKey, op, validate);
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public ResultSet Add(ProductType entity)
        {
            Func<ProductType, ResultStatus> validate = (_entity) =>
            {
                return new ResultStatus();
            };

            Func<ProductType, ResultStatus> op = (_entity) =>
            {
                int ret = new ProductTypeDal().Add(entity);
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
        public ResultSet<IList<ProductType>> GetAll(string fields, string where, object param, string orderBy)
        {
            Func<string, string, object, string, ResultStatus> validate = (_fields, _where, _param, _orderBy) =>
            {
                return new ResultStatus();
            };

            Func<string, string, object, string, IList<ProductType>> op = (_fields, _where, _param, _orderBy) =>
            {
                return new ProductTypeDal().GetAll(_fields, _where, _param, _orderBy);
            };

            return HandleBusiness(fields, where, param, orderBy, op, validate);
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public ResultSet<Page<ProductType>> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy)
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

            Func<string, int, int, string, object, string, Page<ProductType>> op = (_fields, _pageIndex, _pageSize, _where, _param, _orderBy) =>
            {
                int recordCount = 0;

                IList<ProductType> list = new ProductTypeDal().GetAll(_fields, _pageIndex, _pageSize, _where, _param, _orderBy, out recordCount);
                return new Page<ProductType>(_pageIndex, _pageSize, recordCount, list);
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
                int ret = new ProductTypeDal().Update(_fields, _param, _where);
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
        
        #endregion
        
        #region Extend

       //  <summary>
       //  修改一条数据,参数为实体Entity, 需要先取得实体,更改实体属性后再操作
        // </summary>
        public bool Update2(ProductType entity)
        {
            return new ProductTypeDal().Update2(entity);
        }
        /// <summary>
        /// 删除多条数据，参数为删除条件语句，返回True or False
        /// </summary>
        public bool DeleteList(string where)
        {
            return new ProductTypeDal().DeleteList(where);
        }
        #endregion
    }
}

