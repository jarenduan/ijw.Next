using System;
using System.Collections;
using System.Collections.Generic;

namespace ijw.Next.Collection {
    /// <summary>
    /// 表示多个集合的元素数量不匹配的异常
    /// </summary>
    public class CollectionCountNotMatchException<T1, T2> : Exception {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionCountNotMatchException(ICollection<T1> collection1, ICollection<T2> collection2) {
            _collection1 = collection1;
            _collection2 = collection2;
        }

        /// <summary>
        /// 第一个集合
        /// </summary>
        public ICollection<T1> Collection1 => _collection1;

        /// <summary>
        /// 第一个集合的元素数量
        /// </summary>
        public int CountOfCollection1 => _collection1.Count;

        /// <summary>
        /// 另一个集合
        /// </summary>
        public ICollection<T2> Collection2 => _collection2;

        /// <summary>
        /// 另一个集合的元素数量
        /// </summary>
        public int CountOfCollection2 => _collection2.Count;

        private readonly ICollection<T1> _collection1;
        private readonly ICollection<T2> _collection2;
    }

    /// <summary>
    /// 表示三个集合的元素数量不匹配的异常
    /// </summary>
    public class CollectionCountNotMatchException<T1, T2, T3> : Exception { 
        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionCountNotMatchException(ICollection<T1> collection1, ICollection<T2> collection2, ICollection<T3> collection3) {
            _collection1 = collection1;
            _collection2 = collection2;
            _collection3 = collection3;
        }

        /// <summary>
        /// 第一个集合
        /// </summary>
        public ICollection<T1> Collection1 => _collection1;

        /// <summary>
        /// 第一个集合的元素数量
        /// </summary>
        public int CountOfCollection1 => _collection1.Count;

        /// <summary>
        /// 另一个集合
        /// </summary>
        public ICollection<T2> Collection2 => _collection2;

        /// <summary>
        /// 另一个集合的元素数量
        /// </summary>
        public int CountOfCollection2 => _collection2.Count;

        /// <summary>
        /// 另一个集合
        /// </summary>
        public ICollection<T3> Collection3 => _collection3;

        /// <summary>
        /// 另一个集合的元素数量
        /// </summary>
        public int CountOfCollection3 => _collection3.Count;

        private readonly ICollection<T1> _collection1;
        private readonly ICollection<T2> _collection2;
        private readonly ICollection<T3> _collection3;
    }
}