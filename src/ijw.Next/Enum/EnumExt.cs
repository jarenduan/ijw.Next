#if !NETSTANDARD1_4
using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next {
    /// <summary>
    /// Enum Extensions
    /// </summary>
    public static class EnumExt {
        /// <summary>
        /// 获取指定枚举值的<see cref="EnumDescriptionAttribute"/>中的描述信息. 无此attribute则返回枚举值的Name. 
        /// 不是枚举类型将抛出<see cref="ArgumentOutOfRangeException"/>异常
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <returns>描述信息</returns>
        public static string GetEnumDescriptionOrName<T>(this T enumValue) where T : Enum 
            => GetEnumDescriptionOrName(typeof(T), enumValue);

        /// <summary>
        /// 获取指定枚举值的<see cref="EnumDescriptionAttribute"/>中的描述信息. 无此attribute则返回枚举值的Name
        /// 不是枚举类型将抛出<see cref="ArgumentOutOfRangeException"/>异常
        /// </summary>
        /// <param name="t"></param>
        /// <param name="enumValue"></param>
        /// <returns>描述信息</returns>
        public static string GetEnumDescriptionOrName<T>(this Type t, T enumValue) where T : Enum {
            var enumName = Enum.GetName(t, enumValue);

            var attrs = t.GetField(enumName).GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

            if (attrs?.FirstOrDefault() is EnumDescriptionAttribute descAttr) {
                return descAttr.Description;
            }
            else {
                return enumName;
            }
        }

        /// <summary>
        /// 获取指定枚举类型的所有枚举项.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns>所有枚举项组成的数组.</returns>
        public static EnumItem<T>[] GetItems<T>(this IEnumerable<T> enumerable) where T : Enum
            => enumerable.GetItems(v => v.GetEnumDescriptionOrName());

        /// <summary>
        /// 获取指定枚举类型的所有枚举项.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="getDescriptionFunc">一个函数, 根据枚举值来获取枚举项描述</param>
        /// <returns>所有枚举项组成的数组.</returns>
        public static EnumItem<T>[] GetItems<T>(this IEnumerable<T> enumerable, Func<T, string> getDescriptionFunc) where T: Enum
            => enumerable.GetItems(v => new(getDescriptionFunc(v), v));

        /// <summary>
        /// 获取指定枚举类型的所有枚举项.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="getEnumItemFunc">一个函数, 根据枚举名称和枚举值来获取枚举项</param>
        /// <returns>所有枚举项组成的数组.</returns>
        public static EnumItem<T>[] GetItems<T>(this IEnumerable<T> enumerable, Func<T, EnumItem<T>> getEnumItemFunc) where T : Enum
            => enumerable.Select(enumValue => {
                try {
                    return getEnumItemFunc(enumValue);
                }
                catch (NotAppliableException) {
                    return null;
                }
                catch (Exception e) {
                    throw e;
                }
            }).SkipNull().ToArray();
    }
}
#endif