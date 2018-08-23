using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 表示多个集合的元素数量不匹配的异常
    /// </summary>
    public class TwoIEnumerableCountNotMatchException : Exception {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TwoIEnumerableCountNotMatchException(IEnumerable collection1, IEnumerable collection2) {
            _collection1 = collection1;
            _collection2 = collection2;
        }

        /// <summary>
        /// 第一个集合
        /// </summary>
        public IEnumerable Collection1 => _collection1;

        /// <summary>
        /// 另一个集合
        /// </summary>
        public IEnumerable Collection2 => _collection2;

        private readonly IEnumerable _collection1;
        private readonly IEnumerable _collection2;
    }
}
