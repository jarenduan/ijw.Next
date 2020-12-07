using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next {
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper {
#if !NET35 && !NETSTANDARD1_4

        /// <summary>
        /// 获取所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllValue<T>() where T : Enum 
            => typeof(T).GetEnumValues().OfType<T>();
#endif

    }
}
