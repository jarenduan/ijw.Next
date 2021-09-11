using System;
using System.Linq;
using System.Reflection;

namespace ijw.Next.Reflection {
    /// <summary>
    /// 
    /// </summary>
    public static class PropertyInfoExt {
        /// <summary>
        /// 获取属性上指定类型的Attribute的第一个实例. 找不到将抛出异常.
        /// </summary>
        /// <typeparam name="T">Attribute类型</typeparam>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static T FindAttribute<T>(this PropertyInfo pi) where T : Attribute 
            => pi.FirstAttributeOrDefault<T>() ?? throw new ArgumentOutOfRangeException(typeof(T).Name);

        /// <summary>
        /// 获取属性上指定类型的Attribute的第一个实例. 找不到将返回空.
        /// </summary>
        /// <typeparam name="T">Attribute类型</typeparam>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static T? FirstAttributeOrDefault<T>(this PropertyInfo pi) where T : Attribute 
            => pi.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;

#if !NET35 && !NET40

        /// <summary>
        /// 检查是否是Nullable或者C# 8.0的可空类型
        /// </summary>
        /// <param name="p"></param>
        /// <returns>如果可空, 返回true, 反之返回false</returns>
        public static bool IsNullable(this PropertyInfo p) {
            //bool sub = p.PropertyType.;
            var na = p.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "NullableAttribute");
            if (na is null) return false;
            var args = (byte)na.ConstructorArguments[0].Value;
            return (args == 2);
        }

        /// <summary>
        /// 是否存在指定类型的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns>存在返回true, 不存在返回false</returns>
        public static bool HasAttribute<T>(this PropertyInfo p) =>
            p.CustomAttributes.Any(a => a.AttributeType == typeof(T));
#endif
    }
}