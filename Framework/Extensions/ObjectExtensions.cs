using System;

namespace ConnectUO.Framework.Extensions
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this object obj)
        {
            if (obj != null && obj.GetType() == typeof(T))
            {
                return (T)obj;
            }

            if (obj != null && typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), obj.ToString());

            object convertTo;

            if (!Utility.TryConvert(obj.GetType(), obj, typeof(T), out convertTo))
            {
                return default(T);
            }

            return (T)convertTo;
        }

        public static bool TryConvertTo<T>(this object obj, out T outValue)
        {
            if (obj != null && obj.GetType() == typeof(T))
            {
                outValue = (T)obj;
                return true;
            }

            if (obj != null && typeof(T).IsEnum)
            {
                outValue = (T)Enum.Parse(typeof(T), obj.ToString());
                return true;
            }

            outValue = default(T);
            object convertTo;

            if (!Utility.TryConvert(obj.GetType(), obj, typeof(T), out convertTo))
            {
                return false;
            }

            outValue = (T)convertTo;
            return true;
        }

        public static object ConvertTo(this object obj, Type type)
        {
            if (obj != null && obj.GetType() == type)
                return obj;

            if (obj != null && type.IsEnum)
                return Enum.Parse(type, obj.ToString());

            object convertTo;

            if (!Utility.TryConvert(obj.GetType(), obj, type, out convertTo))
            {
                return null;
            }

            return convertTo;
        }

        public static bool TryConvertTo(this object obj, Type type, out object outValue)
        {
            if (obj != null && obj.GetType() == type)
            {
                outValue = obj;
                return true;
            }

            if (obj != null && type.IsEnum)
            {
                outValue = Enum.Parse(type, obj.ToString()); 
                return true;
            }

            outValue = null;
            object convertTo;

            if (!Utility.TryConvert(obj.GetType(), obj, type, out convertTo))
            {
                return false;
            }

            outValue = convertTo;
            return true;
        }
    }
}
