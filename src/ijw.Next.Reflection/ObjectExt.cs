using System;
using System.Reflection;

namespace ijw.Next.Reflection {
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExt {
        /// <summary>
        /// 将字符串尝试转型成属性的类型（用默认的FormatProvider）, 并把成功转型后的值设置给指定的属性.多用于从文本文件中构建对象.
        /// 转型失败将抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性的名字, 必须存在</param>
        /// <param name="stringvalue">属性值</param>
        public static void SetPropertyValue<T>(this T obj, string propertyName, string stringvalue) {
            PropertyInfo pi = typeof(T).GetPropertyInfo(propertyName);
            if (pi == null) {
                throw new ArgumentOutOfRangeException(propertyName);
            }
            object typedValue = stringvalue.To(pi.PropertyType);
            pi.SetValue(obj, typedValue, null);
        }
    }
}
