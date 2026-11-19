using System;
using System.ComponentModel;

namespace Common.Toolkit.Extention
{
    /// <summary>
    /// 枚举描述扩展方法
    /// </summary>
    public static class DescriptionEx
    {
        /// <summary>
        /// 获取枚举 DescriptionAttribute 的描述，未标记时返回枚举名称
        /// </summary>
        public static string GetDesc(this Enum value, bool nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(type.GetField(name), typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descriptionAttribute == null && nameInstead)
            {
                return name;
            }
            return descriptionAttribute?.Description;
        }
    }
}
