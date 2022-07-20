using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools
{
    public static class Ex
    {

        public static string GetDesc(this Enum value)
        {
            Type type = value.GetType();
            List<string> list = Enum.GetNames(type).ToList();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo fieldInfo in fields)
            {
                if (list.Contains(fieldInfo.Name) && !(value.ToString() != fieldInfo.Name))
                {
                    DescriptionAttribute[] array = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
                    if (array.Length != 0)
                    {
                        return array[0].Description;
                    }

                    return "";
                }
            }

            return "";
        }
    }
}
