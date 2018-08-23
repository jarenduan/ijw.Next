using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 表示3个集合的元素数量不匹配的异常
    /// </summary>
    public class ThreeIEnumerableCountNotMatchException : Exception {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ThreeIEnumerableCountNotMatchException(IEnumerable collection1, IEnumerable collection2, IEnumerable collection3) {
            _collection1 = collection1;
            _collection2 = collection2;
            _collection3 = collection3;
        }

        /// <summary>
        /// 第一个集合
        /// </summary>
        public IEnumerable Collection1 => _collection1;

        /// <summary>
        /// 另一个集合
        /// </summary>
        public IEnumerable Collection2 => _collection2;

        /// <summary>
        /// 另一个集合
        /// </summary>
        public IEnumerable Collection3 => _collection3;

        private readonly IEnumerable _collection1;
        private readonly IEnumerable _collection2;
        private readonly IEnumerable _collection3;
    }
}
