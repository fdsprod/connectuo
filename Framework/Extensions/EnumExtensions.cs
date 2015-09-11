using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace ConnectUO.Framework.Extensions
{
    public static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum en)
            where T : Attribute
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(T), false);

                foreach (object attr in attrs)
                {
                    if (attr is T)
                    {
                        return (T)attr;
                    }
                }

            }

            return default(T);
        }

        public static string GetDescription(this Enum en)
        {
            DescriptionAttribute descAttr = en.GetAttribute<DescriptionAttribute>();

            if (descAttr == null)
                return string.Empty;

            return descAttr.Description;
        }
    }
}
