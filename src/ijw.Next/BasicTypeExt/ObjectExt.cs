using System;
using System.Reflection;

namespace ijw.Next {
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExt {
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
        public static void SetPropertyValue<T>(this T obj, string propertyName, object value) {
            PropertyInfo pi = typeof(T).GetPropertyInfo(propertyName);
            if (pi == null) {
                throw new ArgumentOutOfRangeException(propertyName);
            }
            pi.SetValue(obj, value, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object InvokeMethod<T>(this T obj, string methodName, params object[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName);
            return mi != null ? mi.Invoke(obj, paras) : throw new ArgumentOutOfRangeException(methodName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="result"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static bool TryInvokeMethod<T>(this T obj, string methodName, out object result, params object[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName);
            if (mi == null) {
                result = null;
                return false;
            }
            else {
                result = mi.Invoke(obj, paras);
                return true;
            }
        }
    }
}
