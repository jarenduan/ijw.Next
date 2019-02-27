using ijw.Next.Collection;
using ijw.Next.Collection.Indexable;

namespace ijw.Next.Grid {
    /// <summary>
    /// 表头
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Header<T> : Indexable<T> {
        /// <summary>
        /// 构造一个表头对象
        /// </summary>
        /// <param name="data"></param>
        public Header(T[] data) : base(data) {
        }
    }
}