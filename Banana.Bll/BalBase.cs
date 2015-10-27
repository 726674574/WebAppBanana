using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

using Banana.Entity;
using Newtonsoft.Json;

namespace Banana.Bll
{
    public abstract class BalBase
    {
        protected Logger logger = LogManager.GetCurrentClassLogger();

        protected virtual void HandExceptionNoResult(ref ResultSet rs, Exception e, params object[] paras)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }

            StringBuilder exceptionDetail = new StringBuilder();
            exceptionDetail.Append(e.Message);
            if (paras != null && paras.Length > 0)
            {
                exceptionDetail.Append("\t异常方法的参数取值:");
                foreach (object o in paras)
                {
                    if (o == null)
                    {
                        exceptionDetail.Append("\tnull");
                    }
                    else
                    {
                        Type otype = o.GetType();
                        if (!(o is string) && otype.IsClass && otype.IsSerializable)
                        {
                            exceptionDetail.Append("\t" + JsonConvert.SerializeObject(o));
                        }
                        else
                        {
                            string empty = "";
                            string pv = o.ToString();
                            if (empty == pv)
                            {
                                exceptionDetail.Append("\tstring.Empty");
                            }
                            else
                            {
                                exceptionDetail.Append("\t" + pv);
                            }
                        }
                    }
                }
            }
            throw new Exception(exceptionDetail.ToString(), e);

        }
        protected virtual void HandException<T>(ref ResultSet<T> rs, Exception e, params object[] paras)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }

            StringBuilder exceptionDetail = new StringBuilder();
            exceptionDetail.Append(e.Message);
            if (paras != null && paras.Length > 0)
            {
                exceptionDetail.Append("\t异常方法的参数取值:");
                foreach (object o in paras)
                {
                    if (o == null)
                    {
                        exceptionDetail.Append("\tnull");
                    }
                    else
                    {
                        Type otype = o.GetType();
                        if (!(o is string) && otype.IsClass && otype.IsSerializable)
                        {
                            exceptionDetail.Append("\t" + JsonConvert.SerializeObject(o));
                        }
                        else
                        {
                            string empty = "";
                            string pv = o.ToString();
                            if (empty == pv)
                            {
                                exceptionDetail.Append("\tstring.Empty");
                            }
                            else
                            {
                                exceptionDetail.Append("\t" + pv);
                            }
                        }
                    }
                }
            }
            throw new Exception(exceptionDetail.ToString(), e);
        }

        protected ResultSet HandleBusiness(Func<ResultStatus> op, Func<ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate();
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op();
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e);
            }
            return resultSet;
        }
        protected ResultSet HandleBusiness<T>(T entity, Func<T, ResultStatus> op, Func<T, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(entity);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(entity);
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e, entity);
            }
            return resultSet;
        }
        protected ResultSet HandleBusiness<T1, T2>(T1 param1, T2 param2, Func<T1, T2, ResultStatus> op, Func<T1, T2, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2);
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e, param1, param2);
            }
            return resultSet;
        }
        protected ResultSet HandleBusiness<T1, T2, T3>(T1 param1, T2 param2, T3 param3, Func<T1, T2, T3, ResultStatus> op, Func<T1, T2, T3, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3);
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e, param1, param2, param3);
            }
            return resultSet;
        }
        protected ResultSet HandleBusiness<T1, T2, T3, T4>(T1 param1, T2 param2, T3 param3, T4 param4, Func<T1, T2, T3, T4, ResultStatus> op, Func<T1, T2, T3, T4, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4);
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e, param1, param2, param3, param4);
            }
            return resultSet;
        }
        protected ResultSet HandleBusiness<T1, T2, T3, T4, T5>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, Func<T1, T2, T3, T4, T5, ResultStatus> op, Func<T1, T2, T3, T4, T5, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4, param5);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4, param5);
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e, param1, param2, param3, param4, param5);
            }
            return resultSet;
        }
        protected ResultSet HandleBusiness<T1, T2, T3, T4, T5, T6>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, Func<T1, T2, T3, T4, T5, T6, ResultStatus> op, Func<T1, T2, T3, T4, T5, T6, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4, param5, param6);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4, param5, param6);
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e, param1, param2, param3, param4, param5, param6);
            }
            return resultSet;
        }
        protected ResultSet HandleBusiness<T1, T2, T3, T4, T5, T6, T7>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, Func<T1, T2, T3, T4, T5, T6, T7, ResultStatus> op, Func<T1, T2, T3, T4, T5, T6, T7, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4, param5, param6, param7);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4, param5, param6, param7);
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e, param1, param2, param3, param4, param5, param6, param7);
            }
            return resultSet;
        }
        protected ResultSet HandleBusiness<T1, T2, T3, T4, T5, T6, T7, T8>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, Func<T1, T2, T3, T4, T5, T6, T7, T8, ResultStatus> op, Func<T1, T2, T3, T4, T5, T6, T7, T8, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet resultSet = new ResultSet
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                }
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4, param5, param6, param7, param8);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4, param5, param6, param7, param8);
                resultSet.ResultStatus = opResult;
            }
            catch (Exception e)
            {
                HandExceptionNoResult(ref resultSet, e, param1, param2, param3, param4, param5, param6, param7, param8);
            }
            return resultSet;
        }

        protected ResultSet<Return> HandleBusiness<Return>(Func<Return> op, Func<ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate();
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op();
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e);
            }
            return resultSet;
        }
        protected ResultSet<Return> HandleBusiness<T, Return>(T entity, Func<T, Return> op, Func<T, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(entity);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(entity);
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e, entity);
            }
            return resultSet;
        }
        protected ResultSet<Return> HandleBusiness<T1, T2, Return>(T1 param1, T2 param2, Func<T1, T2, Return> op, Func<T1, T2, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2);
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e, param1, param2);
            }
            return resultSet;
        }
        protected ResultSet<Return> HandleBusiness<T1, T2, T3, Return>(T1 param1, T2 param2, T3 param3, Func<T1, T2, T3, Return> op, Func<T1, T2, T3, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3);
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e, param1, param2, param3);
            }
            return resultSet;
        }
        protected ResultSet<Return> HandleBusiness<T1, T2, T3, T4, Return>(T1 param1, T2 param2, T3 param3, T4 param4, Func<T1, T2, T3, T4, Return> op, Func<T1, T2, T3, T4, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4);
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e, param1, param2, param3, param4);
            }
            return resultSet;
        }
        protected ResultSet<Return> HandleBusiness<T1, T2, T3, T4, T5, Return>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, Func<T1, T2, T3, T4, T5, Return> op, Func<T1, T2, T3, T4, T5, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4, param5);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4, param5);
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e, param1, param2, param3, param4, param5);
            }
            return resultSet;
        }
        protected ResultSet<Return> HandleBusiness<T1, T2, T3, T4, T5, T6, Return>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, Func<T1, T2, T3, T4, T5, T6, Return> op, Func<T1, T2, T3, T4, T5, T6, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4, param5, param6);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4, param5, param6);
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e, param1, param2, param3, param4, param5, param6);
            }
            return resultSet;
        }
        protected ResultSet<Return> HandleBusiness<T1, T2, T3, T4, T5, T6, T7, Return>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, Func<T1, T2, T3, T4, T5, T6, T7, Return> op, Func<T1, T2, T3, T4, T5, T6, T7, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4, param5, param6, param7);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4, param5, param6, param7);
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e, param1, param2, param3, param4, param5, param6, param7);
            }
            return resultSet;
        }
        protected ResultSet<Return> HandleBusiness<T1, T2, T3, T4, T5, T6, T7, T8, Return>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, Func<T1, T2, T3, T4, T5, T6, T7, T8, Return> op, Func<T1, T2, T3, T4, T5, T6, T7, T8, ResultStatus> validate)
        {
            if (op == null)
                throw new ArgumentNullException("操作不能为空!");

            ResultSet<Return> resultSet = new ResultSet<Return>
            {
                ResultStatus = new ResultStatus
                {
                    Code = StatusCollection.BusinessError.Code,
                    Description = StatusCollection.BusinessError.Description,
                    Success = false
                },
                Entity = default(Return)
            };

            try
            {
                if (validate != null)
                {
                    ResultStatus rs = validate(param1, param2, param3, param4, param5, param6, param7, param8);
                    if (!rs.Success)
                    {
                        resultSet.ResultStatus = rs;
                        return resultSet;
                    }
                }

                var opResult = op(param1, param2, param3, param4, param5, param6, param7, param8);
                if (opResult is ResultStatus)
                {
                    resultSet.ResultStatus = opResult as ResultStatus;
                }
                else
                {
                    resultSet.ResultStatus = new ResultStatus();
                    resultSet.Entity = opResult;
                }
            }
            catch (Exception e)
            {
                HandException<Return>(ref resultSet, e, param1, param2, param3, param4, param5, param6, param7, param8);
            }
            return resultSet;
        }
    }
}
