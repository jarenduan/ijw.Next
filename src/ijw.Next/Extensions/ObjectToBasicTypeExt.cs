#if !NETSTANDARD1_4
using System;

namespace ijw.Next {
    /// <summary>
    /// 转型扩展方法
    /// </summary>
    public static class ObjectToBasicTypeExt {
        #region to basic types
        /// <summary>
        /// 转换为Bool型,转型失败时将抛出异常.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool型</returns>
        public static bool ToBoolean(this object? obj) => Convert.ToBoolean(obj);

        /// <summary>
        /// 转换为16位整型,转型失败时将抛出异常.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>16位整型</returns>
        public static short ToInt16(this object? obj) => Convert.ToInt16(obj);

        /// <summary>
        /// 转换为32位整型,转型失败时将抛出异常.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>32位整型</returns>
        public static int ToInt32(this object? obj) => Convert.ToInt32(obj);

        /// <summary>
        /// 转换为64位整型,转型失败时将抛出异常.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>64位整型</returns>
        public static long ToInt64(this object? obj) => Convert.ToInt64(obj);

        /// <summary>
        /// 转换为单精度型,转型失败时将抛出异常.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>单精度型</returns>
        public static float ToFloat(this object? obj) => Convert.ToSingle(obj);

        /// <summary>
        /// 转换为双精度型,转型失败时将抛出异常.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>双精度型</returns>
        public static double ToDouble(this object? obj) => Convert.ToDouble(obj);

        /// <summary>
        /// 转换为Decimal型,转型失败时将抛出异常.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Decimal型</returns>
        public static decimal ToDecimal(this object? obj) => Convert.ToDecimal(obj);

        /// <summary>
        /// 转换为时间日期型,转型失败时将抛出异常.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>时间日期型</returns>
        public static DateTime ToDateTime(this object? obj) => Convert.ToDateTime(obj);

        #endregion

        #region To basic types, with default values
        /// <summary>
        /// 转换为Bool型,转型失败时将使用默认值.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转型失败时使用默认值</param>
        /// <returns>bool型</returns>
        public static bool ToBooleanAnyway(this object? obj, bool defaultValue = false) => obj.ToOtherTypeAnyway((o) => Convert.ToBoolean(obj), defaultValue);

        /// <summary>
        /// 转换为16位整型, 转型失败时将使用默认值.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转型失败时使用默认值</param>
        /// <returns>16位整型</returns>
        public static short ToInt16Anyway(this object? obj, short defaultValue = -1) => obj.ToOtherTypeAnyway((o) => Convert.ToInt16(o), defaultValue);

        /// <summary>
        /// 转换为32位整型, 转型失败时将使用默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转型失败时使用默认值</param>
        /// <returns>32位整型</returns>
        public static int ToInt32Anyway(this object? obj, int defaultValue = -1) => obj.ToOtherTypeAnyway((o) => Convert.ToInt32(o), defaultValue);

        /// <summary>
        /// 转换为64位整型, 转型失败时将使用默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转型失败时使用默认值</param>
        /// <returns>64位整型</returns>
        public static long ToInt64Anyway(this object? obj, long defaultValue = -1) => obj.ToOtherTypeAnyway((o) => Convert.ToInt64(o), defaultValue);

        /// <summary>
        /// 转换为单精度型, 转型失败时将使用默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转型失败时使用默认值</param>
        /// <returns>单精度型</returns>
        public static float ToFloatAnyway(this object? obj, float defaultValue = -9999f) => obj.ToOtherTypeAnyway((o) => Convert.ToSingle(o), defaultValue);

        /// <summary>
        /// 转换为双精度型, 转型失败时将使用默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转型失败时使用默认值</param>
        /// <returns>双精度型</returns>
        public static double ToDoubleAnyway(this object? obj, double defaultValue = -9999d) => obj.ToOtherTypeAnyway((o) => Convert.ToDouble(o), defaultValue);

        /// <summary>
        /// 转换为Decimal型, 转型失败时将使用默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转型失败时使用默认值</param>
        /// <returns>Decimal型</returns>
        public static decimal ToDecimalAnyway(this object? obj, decimal defaultValue = -9999m) => obj.ToOtherTypeAnyway((o) => Convert.ToDecimal(o), defaultValue);

        /// <summary>
        /// 转换为时间日期型, 转型失败时将使用默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转型失败时使用默认值</param>
        /// <returns>时间日期型</returns>
        public static DateTime ToDateTimeAnyway(this object? obj, DateTime defaultValue) => obj.ToOtherTypeAnyway((o) => Convert.ToDateTime(o), defaultValue);

        #endregion

        #region To nullbale basic types

        /// <summary>
        /// 转换为Bool型,转型失败时将使用null.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool型</returns>
        public static bool? ToBooleanNullable(this object? obj) => obj.ToOtherTypeNullable((o) => Convert.ToBoolean(o));


        /// <summary>
        /// 转换为16位可空整型, 无法转换时将转换成null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>16位可空整型</returns>
        public static short? ToInt16Nullable(this object? obj) => obj.ToOtherTypeNullable((o) => Convert.ToInt16(o));

        /// <summary>
        /// 转换为32位可空整型, 无法转换时将转换成null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>32位可空整型</returns>
        public static int? ToInt32Nullable(this object? obj) => obj.ToOtherTypeNullable((o) => Convert.ToInt32(o));

        /// <summary>
        /// 转换为64位可空整型, 无法转换时将转换成null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>64位可空整型</returns>
        public static long? ToInt64Nullable(this object? obj) => obj.ToOtherTypeNullable((o) => Convert.ToInt64(o));

        /// <summary>
        /// 转换为可空单精度型, 无法转换时将转换成null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>可空单精度型</returns>
        public static float? ToFloatNullable(this object? obj) => obj.ToOtherTypeNullable((o) => Convert.ToSingle(o));

        /// <summary>
        /// 转换为可空双精度型, 无法转换时将转换成null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>可空双精度型</returns>
        public static double? ToDoubleNullable(this object? obj) => obj.ToOtherTypeNullable((o) => Convert.ToDouble(o));

        /// <summary>
        /// 转换为可空Decimal型, 无法转换时将转换成null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>可空Decimal型</returns>
        public static decimal? ToDecimalNullable(this object? obj) => obj.ToOtherTypeNullable((o) => Convert.ToDecimal(o));

        /// <summary>
        /// 转换为可空时间日期型, 无法转换时将转换成null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>可空时间日期型</returns>
        public static DateTime? ToDateTimeNullable(this object? obj) => obj.ToOtherTypeNullable((o) => Convert.ToDateTime(o));

        #endregion

        #region To String
        /// <summary>
        /// 调用对象的ToString方法, 当对象为空失败时将返回默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值, 默认为空</param>
        /// <returns>字符串表达形式</returns>
        public static string ToStringAnyway(this object? obj, string defaultValue = "") 
            => (obj is null || obj is DBNull) ? defaultValue : obj.ToString();

        /// <summary>
        /// 转换为可空的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>可空的字符串</returns>
        public static string? ToStringNullable(this object? obj)
            => (obj is null || obj is DBNull) ? null : Convert.ToString(obj); 
        #endregion

        #region To other Types
        /// <summary>
        /// 使用指定的函数进行转型, 失败将返回null
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="V">转换后的类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="converFunc">转型func</param>
        /// <returns>转换后的对象</returns>
        public static V? ToOtherTypeNullable<T, V>(this T obj, Func<T, V> converFunc) where V : struct {
            if (obj is null || obj is DBNull) return null;
            try {
                return converFunc(obj);
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// 使用指定的函数进行转型, 失败将返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="obj"></param>
        /// <param name="func">转型函数</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的对象</returns>
        public static V ToOtherTypeAnyway<T, V>(this T obj, Func<T, V> func, V defaultValue = default) {
            try {
                return func(obj);
            }
            catch {
                return defaultValue;
            }
        }

        #endregion
    }
}
#endif