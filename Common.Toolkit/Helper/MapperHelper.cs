using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Common.Toolkit.Helper
{
    /// <summary>
    /// 对象同名字段映射帮助类
    /// </summary>
    public static class MapperHelper
    {
        /// <summary>
        /// 将源对象同名字段赋值到已有的目标对象
        /// </summary>
        public static TOut AutoMap<TIn, TOut>(TIn obj, TOut outObj, bool ignorDesc = true) where TOut : new()
        {
            return AutoMap(obj, ignorDesc, outObj);
        }

        /// <summary>
        /// 将源对象同名字段赋值到新建的目标对象
        /// </summary>
        public static TOut AutoMap<TIn, TOut>(TIn obj, bool ignorDesc = true) where TOut : new()
        {
            TOut result = new TOut();
            return AutoMap(obj, ignorDesc, result);
        }

        private static TOut AutoMap<TIn, TOut>(TIn obj, bool ignorDesc, TOut result) where TOut : new()
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] array = properties;
            foreach (PropertyInfo item in array)
            {
                bool num = properties.Count((PropertyInfo f) => f.Name == item.Name) > 1;
                bool isHideBySig = item.GetGetMethod().IsHideBySig;
                if (!(num && isHideBySig) || !(item.DeclaringType != obj.GetType()))
                {
                    dictionary.Add(item.Name, item);
                }
            }
            array = result.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in array)
            {
                try
                {
                    DescriptionAttribute descriptionAttribute = (DescriptionAttribute)propertyInfo.GetCustomAttributes(inherit: false).FirstOrDefault((object f) => f.GetType() == typeof(DescriptionAttribute));
                    if (descriptionAttribute != null && !ignorDesc)
                    {
                        string description = descriptionAttribute.Description;
                        if (dictionary.ContainsKey(description))
                        {
                            propertyInfo.SetValue(result, dictionary[description].GetValue(obj));
                        }
                    }
                    else if (dictionary.ContainsKey(propertyInfo.Name))
                    {
                        propertyInfo.SetValue(result, dictionary[propertyInfo.Name].GetValue(obj));
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        propertyInfo.SetValue(result, Activator.CreateInstance(propertyInfo.PropertyType));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("转换前后类型不一致");
                    }
                }
            }
            return result;
        }
    }
}
