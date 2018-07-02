#if !NET35
using System.Collections.Generic;

namespace ijw.Next.Collection {
    /// <summary>
    /// 带有索引器的集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIndexable<out T> : IEnumerable<T> {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        T this[int index] { get; }

        /// <summary>
        /// 集合元素数量
        /// </summary>
        int Count { get; }
    }
}
#endif