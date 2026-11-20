using System.ComponentModel;

namespace Common.Notify.WebAPI.Enums
{
    /// <summary>
    /// 返回值代码
    /// </summary>
    public enum ResponseEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 1,

        /// <summary>
        /// 未授权
        /// </summary>
        [Description("未授权")]
        Unauthorized = 401,

        /// <summary>
        /// 授权失效
        /// </summary>
        [Description("授权失效")]
        TimeExpired = 430,
    }
}
