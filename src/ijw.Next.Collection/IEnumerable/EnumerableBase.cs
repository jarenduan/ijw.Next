using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next.Collection {
    /// <summary>
    /// 可枚举对象基类, 不可以实例化该类.可以从此类继承, 从而方便地利用定长数组实现IEnumerable{T}.
    /// 通过在内部使用数组T[], 提供了一个最小的IEnumerable{T}实现.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EnumerableBase<T> : IEnumerable<T>, IEnumerable {
        /// <summary>
        /// 给定长度, 构造一个可枚举对象.
        /// </summary>
        protected EnumerableBase(int count) => _data = new T[count];

        /// <summary>
        /// 构造一个可枚举对象, 使用指定的数组初始化.
        /// </summary>
        /// <param name="data">用于初始化的数组.内部将直接引用这个数组.</param>
        protected EnumerableBase(T[] data) {
            data.ShouldBeNotNullArgument();
            _data = data;
        }

        /// <summary>
        /// 构造一个可枚举对象, 使用一个IEnumerable{T}初始化.
        /// </summary>
        /// <param name="data">用于初始化的集合. EnumerableBase将拷贝这个集合.</param>
        public EnumerableBase(IEnumerable<T> data) : this(data?.ToArray()) {
        }

        /// <summary>
        /// 内部数组
        /// </summary>
        protected T[] _data;

        /// <summary>
        /// 获取一个迭代器(由内部数组实现)
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => _data.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}