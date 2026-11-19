using System;

namespace Common.Toolkit.Helper
{
    /// <summary>
    /// 异常校验帮助类
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// 为空时抛出异常
        /// </summary>
        public static void CheckNull(object obj, string msg)
        {
            if (obj == null)
            {
                throw new Exception(msg);
            }
        }

        /// <summary>
        /// 条件成立时抛出异常
        /// </summary>
        public static void CheckException(bool check, string errMsg)
        {
            if (check)
            {
                throw new Exception(errMsg);
            }
        }
    }
}
