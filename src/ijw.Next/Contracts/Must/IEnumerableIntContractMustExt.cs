using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Contracts {
    /// <summary>
    /// 整数序列的契约扩展方法
    /// </summary>
    public static class IEnumerableIntContractMustExt {

        /// <summary>
        /// 每个元素都应该不等于0
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都不等于0, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<int>> MustEachNotZero(this IEnumerable<int> collection)
            => collection.MustEachNotEquals(0);

        /// <summary>
        /// 每个元素都应该不小于0
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都不小于0, Contract的IsKept为true.</returns>

        public static bool ShouldEachNotLessThanZero(this IEnumerable<int> collection) {
            return collection.ShouldEachSatisfy((i) => i.ShouldBeNotLessThanZero());
        }

        /// <summary>
        /// 每个元素都应该是偶数
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都是偶数Contract的IsKept为true., 否则抛出ContractBrokenException异常</returns>

        public static bool ShouldEachBeEven(this IEnumerable<int> collection) {
            return collection.ShouldEachSatisfy((i) => i.ShouldBeEven());
        }

        /// <summary>
        /// 每个元素都应该是奇数
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都是奇数Contract的IsKept为true., 否则抛出ContractBrokenException异常</returns>

        public static bool ShouldEachBeOdd(this IEnumerable<int> collection) {
            return collection.ShouldEachSatisfy((i) => i.ShouldBeOdd());
        }
    }
}
