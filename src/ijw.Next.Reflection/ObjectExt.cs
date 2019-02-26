using System;
using System.Reflection;

namespace ijw.Next.Reflection {
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExt {
        /// <summary>
        /// 将字符串尝试转型成属性的类型（用默认的FormatProvider）, 并把成功转型后的值设置给指定的属性. 多用于从文本文件中构建对象.
        /// 转型失败将抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性的名字, 必须存在</param>
        /// <param name="stringvalue">属性值</param>
        public static void SetPropertyValue<T>(this T obj, string propertyName, string stringvalue) {
            PropertyInfo pi = typeof(T).GetPropertyInfo(propertyName);
            if (pi is null) throw new ArgumentOutOfRangeException(propertyName);
            var typedValue = stringvalue.ToType(pi.PropertyType);
            pi.SetValue(obj, typedValue, null);
        }

        /// <summary>
        /// 设置指定的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性的名字</param>
        /// <param name="value">属性值</param>
        /// <remarks>
        /// 属性值运行时类型如果不符合, 将会抛出异常
        /// </remarks>
        public static void SetPropertyValue<T>(this T obj, string propertyName, object? value) {
            PropertyInfo pi = typeof(T).GetPropertyInfo(propertyName);
            if (pi is null) throw new ArgumentOutOfRangeException(propertyName);
            pi.SetValue(obj, value, null);
        }

        /// <summary>
        /// 反射调用某个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static object? InvokeMethod<T>(this T obj, string methodName, params object?[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName);
            return mi != null ? mi.Invoke(obj, paras) : throw new ArgumentOutOfRangeException(methodName);
        }

        /// <summary>
        /// 尝试反射调用某个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static bool TryInvokeMethod<T>(this T obj, string methodName, params object?[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName);
            mi?.Invoke(obj, paras);
            return !(mi is null);
        }

        /// <summary>
        /// 尝试反射调用某个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="result">返回结果</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static bool TryInvokeMethod<T>(this T obj, string methodName, out object? result, params object?[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName);
            result = mi?.Invoke(obj, paras);
            return !(mi is null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool EqualsNullable<T>(this T obj, T other) =>
            (obj is null && other is null) || (!(obj is null) && obj.Equals(other));
    }
}