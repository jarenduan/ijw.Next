using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next {
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExt {
        /// <summary>
        /// 查看Debug字符串, 如果没有, 则相当于ToString
        /// </summary>
        /// <returns>Debug字符串或者缺省的string</returns>
        public static string ToDebugString<T>(T obj) {
            if (obj is null) throw new ArgumentNullException(nameof(obj));
            return (obj is IDebugString debugString) ? debugString.ToDebugString() : obj.ToString();
        }

        /// <summary>
        /// 是否存在于指定集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="array">指定的集合</param>
        /// <returns></returns>
        public static bool In<T>(this T o, IEnumerable<T> array) => array.Contains(o);

        /// <summary>
        /// 是否存在于指定集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="array">指定的集合</param>
        /// <returns></returns>
        public static bool In<T>(this T o, params T[] array) => o.In(array as IEnumerable<T>);

        /// <summary>
        /// 是否不存在于指定集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="array">指定集合</param>
        /// <returns></returns>
        public static bool NotIn<T>(this T o, IEnumerable<T> array) => !o.In(array);

        /// <summary>
        /// 是否不存在于指定集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="array">指定集合</param>
        /// <returns></returns>
        public static bool NotIn<T>(this T o, params T[] array) => o.NotIn(array as IEnumerable<T>);

        /// <summary>
        /// 查询某对象是否拥有某属性.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static bool HasProperty(this object obj, string propertyName) 
            => obj.GetType().GetPropertyInfo(propertyName) is not null;
    }
}
