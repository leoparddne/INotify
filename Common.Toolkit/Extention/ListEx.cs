using System.Collections.Generic;
using System.Linq;

namespace Common.Toolkit.Extention
{
    /// <summary>
    /// 集合扩展方法
    /// </summary>
    public static class ListEx
    {
        /// <summary>
        /// 集合为 null 或不含任何元素时返回 true
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable != null)
            {
                return !enumerable.Any();
            }
            return true;
        }
    }
}
