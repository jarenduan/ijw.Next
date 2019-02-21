using System;
using System.Collections.Generic;

namespace ijw.Next.Collection.Indexable {
    /// <summary>
    /// 带索引的列表.内部用定长数组实现.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Indexable<T> : EnumerableBase<T>, IIndexable<T> {
        /// <summary>
        /// 给定长度进行初始化
        /// </summary>
        /// <param name="count"></param>
        public Indexable(int count) : base(count) { }

        /// <summary>
        /// 使用一个数组进行初始化
        /// </summary>
        /// <param name="data">进行初始化的一维数组</param>
        public Indexable(T[] data)
            : base(data) {
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public T this[int index] {
            get => _data[index];
            set => _data[index] = value;
        }


        /// <summary>
        /// 集合元素总数
        /// </summary>
        public int Count => this._data.Length;
    }
}
