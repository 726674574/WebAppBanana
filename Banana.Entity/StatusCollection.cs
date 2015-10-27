using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banana.Entity
{
    public class StatusCollection
    {
        public static readonly Status Success = new Status
        {
            Code = 0,
            Description = "成功"
        };

        public static readonly Status ParameterError = new Status
        {
            Code = 1000,
            Description = "参数错误"
        };

        public static readonly Status DatabaseError = new Status
        {
            Code = 1001,
            Description = "数据库错误"
        };

        public static readonly Status NetworkError = new Status
        {
            Code = 1002,
            Description = "网络错误"
        };

        public static readonly Status BusinessError = new Status
        {
            Code = 1010,
            Description = "业务逻辑错误"
        };

        public static readonly Status AddFailed = new Status
        {
            Code = 1011,
            Description = "添加失败"
        };

        public static readonly Status DeleteFailed = new Status
        {
            Code = 1012,
            Description = "删除失败"
        };

        public static readonly Status GetFailed = new Status
        {
            Code = 1013,
            Description = "获取失败"
        };

        public static readonly Status UpdateFailed = new Status
        {
            Code = 1014,
            Description = "更新失败"
        };

        public static readonly Status NotFound = new Status
        {
            Code = 1015,
            Description = "未找到"
        };

        public static readonly Status ApplicationHasCut = new Status
        {
            Code = 1016,
            Description = "报名已截止"
        };

        public static readonly Status ActivityNotExist = new Status
        {
            Code = 1017,
            Description = "活动不存在"
        };

        public static readonly Status GetAccessTokenFaild = new Status
        {
            Code = 1018,
            Description = "获取accessToken 失败"
        };

        public static readonly Status GetUidFaild = new Status
        {
            Code = 1019,
            Description = "获取uid 失败"
        };

        public static readonly Status UserNotExist = new Status
        {
            Code = 1020,
            Description = "用户不存在"
        };
    }
}
