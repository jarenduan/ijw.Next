using System;

namespace ijw.Next {
    /// <summary>
    /// string to Enum extension methods.
    /// </summary>
    public static class StringToOtherTypeExt {
        #region To Enum
        /// <summary>
        /// 将字符串转换为指定的枚举对象
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="enumType">欲转换的枚举类型</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>转换成功返回转换后的枚举对象, 转换失败时抛出异常</returns>
        public static object ToEnum(this string value, Type enumType, bool ignoreCase = false)
            => Enum.Parse(enumType, value, ignoreCase);

        /// <summary>
        /// 把字符串转换成指定的枚举, 如果转换失败返回指定的缺省值.
        /// </summary>
        /// <typeparam name="T">转换的枚举类型</typeparam>
        /// <param name="aString">此字符串</param>
        /// <param name="ignoreCase">转换时是否忽略大小写, 默认不忽略</param>
        /// <returns>转换后的枚举值</returns>
        public static T ToEnum<T>(this string aString, bool ignoreCase = false) where T : Enum
            => (T)aString.ToEnum(typeof(T), ignoreCase);

        /// <summary>
        /// 把字符串转换成指定的枚举, 如果转换失败返回指定的缺省值.
        /// </summary>
        /// <typeparam name="T">转换的枚举类型</typeparam>
        /// <param name="aString">此字符串</param>
        /// <param name="ignoreCase">转换时是否忽略大小写, 默认不忽略</param>
        /// <param name="defaultValue">可选参数, 表示转换失败的时候所取的缺省值, 默认是枚举的0值</param>
        /// <returns>转换后的枚举值</returns>
        public static T ToEnumAnyway<T>(this string aString, bool ignoreCase = false, T defaultValue = default) where T : Enum {
            T result = defaultValue;
            try {
                result = aString.ToEnum<T>(ignoreCase);
            }
            catch {
                result = defaultValue;
            }
            return result;
        } 
        #endregion

        #region To Numbers

        /// <summary>
        /// 尝试转换成int. 如果失败将返回defaultNumer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultNumer">转换失败时返回的值, 默认是0</param>
        /// <returns>转换后的int</returns>
        public static int ToIntAnyway(this string s, int defaultNumer = 0) {
            if (int.TryParse(s, out int i)) {
                return i;
            }
            else {
                return defaultNumer;
            }
        }

        /// <summary>
        /// 尝试转换成float. 如果NaN或转换失败将返回defaultNumer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultNumer">转换失败时返回的值, 默认是0</param>
        /// <returns>转换后的float</returns>
        public static float ToFloatAnyway(this string s, float defaultNumer = 0f) {
            var result = defaultNumer;
            if (float.TryParse(s, out float i)) {
                if (!float.IsNaN(i)) {
                    result = i;
                }
            }
            return result;
        }

        /// <summary>
        /// 尝试转换成double. 如果NaN或转换失败将返回defaultNumer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultNumer">转换失败时返回的值, 默认是0</param>
        /// <returns>转换后的float</returns>
        public static double ToDoubleAnyway(this string s, double defaultNumer = 0d) {
            var result = defaultNumer;
            if (double.TryParse(s, out double i)) {
                if (!double.IsNaN(i)) {
                    result = i;
                }
            }
            return result;
        }

        /// <summary>
        /// 尝试转换成Decimal. 如果NaN或转换失败将返回defaultNumer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultNumer">转换失败时返回的值, 默认是0</param>
        /// <returns>转换后的float</returns>
        public static decimal ToDecimalAnyway(this string s, decimal defaultNumer = 0m) {
            var result = defaultNumer;
            if (decimal.TryParse(s, out decimal i)) {
                result = i;
            }
            return result;
        }


        #endregion

        #region To ValueType
        /// <summary>
        /// 将字符串尝试转型成指定的值类型（用默认的FormatProvider）
        /// 支持的值类型目前包括:枚举、DBNull、Boolean/Char/(S)Byte/DateTime/(U)Int16/32/64/Float/Double/Decimal/Guid及相应可空类型
        /// </summary>
        /// <typeparam name="T">欲转换成的类型</typeparam>
        /// <param name="value">字符串</param>
        /// <param name="useDefaultValueWhenCastFail">不支持类型、转型失败或者值溢出的时候是否返回默认值而不抛出异常.默认是否</param>
        /// <returns>转型后的值</returns>
        /// <remarks>
        /// 性能提示: 此方法内部调用了String.To(typeof(T)), 因此对于值类型涉及装箱和拆箱.
        /// </remarks>
        public static T To<T>(this string value, bool useDefaultValueWhenCastFail = false) where T : struct {
            var r = value.ToType(typeof(T), useDefaultValueWhenCastFail);
            return (r is null) ? default : (T)r;
        }

        /// <summary>
        /// 将字符串尝试转型成指定的可空值类型（用默认的FormatProvider）
        /// 支持的值类型目前包括:枚举、DBNull、Boolean/Char/(S)Byte/DateTime/(U)Int16/32/64/Float/Double/Decimal/Guid及相应可空类型
        /// </summary>
        /// <typeparam name="T">欲转换成的类型</typeparam>
        /// <param name="value">字符串</param>
        /// <param name="useDefaultValueWhenCastFail">不支持类型、转型失败或者值溢出的时候是否返回默认值而不抛出异常.默认是否</param>
        /// <returns>转型后的值</returns>
        /// <remarks>
        /// 性能提示: 此方法内部调用了String.To(typeof(T)), 因此对于值类型涉及装箱和拆箱.
        /// </remarks>
        public static T? ToNullable<T>(this string value, bool useDefaultValueWhenCastFail = false) where T : struct
            => (T?)value.ToType(typeof(T?), useDefaultValueWhenCastFail);

        /*
            注意, 此处并没有实现针对引用类型的方法:
                T To<T>(this string value) where T: class { ... }
            这是因为将string转成string之外的复杂类型没有意义, 从字符串反序列化应该调用相应的反序列化方法.
            
            可以考虑内部增加xml/json的反序列化方法
        */
        #endregion

        #region To Any Type
        /// <summary>
        /// 将字符串尝试转型成指定的类型（用默认的FormatProvider）
        /// 支持的值类型目前包括:枚举、DBNull、Boolean/Char/(S)Byte/DateTime/(U)Int16/32/64/Float/Double/Decimal/Guid及相应可空类型
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="type">欲转换成的类型</param>
        /// <param name="useDefaultValueWhenCastFailed">不支持类型、转型失败或者值溢出的时候是否返回默认值而不抛出异常.默认是否</param>
        /// <returns>转型后的值</returns>
        /// <remarks>
        /// 性能提示: 此方法对于值类型涉及装箱和拆箱.
        /// </remarks>
        public static object? ToType(this string value, Type type, bool useDefaultValueWhenCastFailed = false) {
            string typeName = type.GetTypeName();
            try {
                switch (typeName) {
                    #region all kinds of type
                    case string t when (type.IsEnumType()):
                        return value.ToEnum(type, useDefaultValueWhenCastFailed);
                    case string t when (t.StartsWith ("System.Nullable`1") && value.Length == 0):
                        return null;
#if !NETSTANDARD1_4
                    case string t when (typeName == "System.DBNull" && value.Length == 0):
                        return DBNull.Value;
#endif
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
                        return Boolean.Parse(value);
                    case "System.Char":
                    case "System.Nullable`1[System.Char]":
                        return Char.Parse(value);
                    case "System.Byte":
                    case "System.Nullable`1[System.Byte]":
                        return Byte.Parse(value);
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
            catch when (useDefaultValueWhenCastFailed) {
                return type.GetDefaultValue();
            }
        } 
        #endregion
    }
}