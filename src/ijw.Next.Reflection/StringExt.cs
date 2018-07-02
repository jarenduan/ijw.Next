using System;
using System.Collections.Generic;

namespace ijw.Next.Reflection {
    /// <summary>
    /// 
    /// </summary>
    public static class StringExt {
        /// <summary>
        /// 将字符串转换为指定的枚举对象
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="enumType">欲转换的枚举类型</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <param name="defaultValue">转换失败时返回一个默认值</param>
        /// <returns>转换成功返回转换后的枚举对象, 转换失败返回指定的默认值, 默认值为null时抛出异常</returns>
        public static object ToEnum(this string value, Type enumType, bool ignoreCase = false, object defaultValue = null) {
            Type t = enumType;
            if (!t.IsEnumType()) {
                throw new ArgumentException($"{t.Name} is not a enumeration type.");
            }
            try {
                return Enum.Parse(enumType, value);
            }
            catch when (defaultValue != null) {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将字符串尝试转型成指定类型（用默认的FormatProvider）
        /// 支持属性类型目前包括string、Boolean/Char/(S)Byte/DateTime/(U)Int16/32/64/Float/Double/Decimal及相应可空类型
        /// </summary>
        /// <typeparam name="T">欲转换成的类型</typeparam>
        /// <param name="value">字符串</param>
        /// <param name="ifUseDefaultValueWhenFailed">不支持类型、转型失败或者值溢出的时候是否返回默认值而不抛出异常.默认是否.</param>
        /// <returns>成功转型后的值</returns>
        /// <remarks>
        /// 性能提示: 此方法内部调用了String.To(typeof(T)), 因此对于值类型涉及装箱和拆箱.
        /// </remarks>
        public static T To<T>(this string value, bool ifUseDefaultValueWhenFailed = false) {
            var result = value.To(typeof(T), ifUseDefaultValueWhenFailed);
            return (T)result;
        }

        /// <summary>
        /// 将字符串尝试转型成指定类型（用默认的FormatProvider）
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="type">欲转换成的类型</param>
        /// <param name="ifUseDefaultValueWhenFailed">不支持类型、转型失败或者值溢出的时候是否返回默认值而不抛出异常.默认是否</param>
        /// <returns></returns>
        public static object To(this string value, Type type, bool ifUseDefaultValueWhenFailed = false) {
            string typeName = type.GetTypeName();
            if (typeName.StartsWith("System.Nullable`1") && value.Length == 0) {
                return null;
            }
            if (type.IsEnumType()) {
                return value.ToEnum(type, ifUseDefaultValueWhenFailed);
            }
            else if (typeName == "System.DBNull") {
                if (value.Length == 0) {
                    return DBNull.Value;
                }
            }
            try {
                switch (typeName) {
                    #region all kinds of type
#if !NET35
                    case "System.Guid":
                    case "System.Nullable`1[System.Guid]":
                        return Guid.Parse(value);
#endif
                   
                    case "System.SByte":
                    case "System.Nullable`1[System.SByte]":
                        return SByte.Parse(value);
                    case "System.DateTime":
                    case "System.Nullable`1[System.DateTime]":
                        return DateTime.Parse(value);
                    case "System.String":
                        return value;
                    case "System.Boolean":
                    case "System.Nullable`1[System.Boolean]":
                        return bool.Parse(value);
                    case "System.Char":
                    case "System.Nullable`1[System.Char]":
                        return char.Parse(value);
                    case "System.Byte":
                    case "System.Nullable`1[System.Byte]":
                        return byte.Parse(value);
                    case "System.Single":
                    case "System.Nullable`1[System.Single]":
                        return Single.Parse(value);
                    case "System.Double":
                    case "System.Nullable`1[System.Double]":
                        return Double.Parse(value);
                    case "System.Int16":
                    case "System.Nullable`1[System.Int16]":
                        return Int16.Parse(value);
                    case "System.Int32":
                    case "System.Nullable`1[System.Int32]":
                        return Int32.Parse(value);
                    case "System.Int64":
                    case "System.Nullable`1[System.Int64]":
                        return Int64.Parse(value);
                    case "System.Decimal":
                    case "System.Nullable`1[System.Decimal]":
                        return Decimal.Parse(value);
                    case "System.UInt16":
                    case "System.Nullable`1[System.UInt16]":
                        return UInt16.Parse(value);
                    case "System.UInt32":
                    case "System.Nullable`1[System.UInt32]":
                        return UInt32.Parse(value);
                    case "System.UInt64":
                    case "System.Nullable`1[System.UInt64]":
                        return UInt64.Parse(value);
#endregion
                    default:
                        throw new InvalidCastException($"{typeName} is not supported currently.");
                }
            }
            catch when (ifUseDefaultValueWhenFailed) {
                return type.GetDefaultValue();
            }
        }
    }
}
