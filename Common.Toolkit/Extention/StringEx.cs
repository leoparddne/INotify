namespace Common.Toolkit.Extention
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringEx
    {
        /// <summary>
        /// 字符串转 int，转换失败时返回默认值
        /// </summary>
        public static int ToInt(this string str, int defaultInt = -1)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultInt;
            }
            if (int.TryParse(str, out var result))
            {
                return result;
            }
            return defaultInt;
        }
    }
}
