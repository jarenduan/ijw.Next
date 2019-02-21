using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ijw.Next.Collection.Indexable {
    /// <summary>
    /// 
    /// </summary>
    public static class ArrayExt {
        /// <summary>
        /// 将指定数组包装成为Indexable的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">欲包装的数组</param>
        /// <returns>包装了指定数组的Indexable实例</returns>
        public static Indexable<T> AsIndexable<T>(this T[] array) => new Indexable<T>(array);
    }
}
