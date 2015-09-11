using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace ConnectUO.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T value)
        {
            Type type = value.GetType();
            MemberInfo[] memInfo = type.GetMember(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
