using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
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
        /// 每个元素都应该不等于0
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>每个元素都不等于0, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<int>> AndMustEachNotZero(this Contract<IEnumerable<int>> contract)
            => contract.Value.MustEachNotEquals(0);

        /// <summary>
        /// 每个元素都应该不小于0
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都不小于0, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<int>> MustEachNotLessThanZero(this IEnumerable<int> collection)
            => collection.MustEachNotLessThan(0);

        /// <summary>
        /// 每个元素都应该不小于0
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>每个元素都不小于0, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<int>> AndMustEachNotLessThanZero(this Contract<IEnumerable<int>> contract)
            => contract.Value.MustEachNotLessThan(0);

        /// <summary>
        /// 每个元素都应该是偶数
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都是偶数Contract的IsKept为true., 否则抛出ContractBrokenException异常</returns>

        public static Contract<IEnumerable<int>> MustEachBeEven(this IEnumerable<int> collection) 
            => collection.MustEachSatisfy((i) => i.IsEven());

        /// <summary>
        /// 每个元素都应该是偶数
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>每个元素都是偶数Contract的IsKept为true., 否则抛出ContractBrokenException异常</returns>

        public static Contract<IEnumerable<int>> AndMustEachBeEven(this Contract<IEnumerable<int>> contract)
            => contract.Value.MustEachBeEven();

        /// <summary>
        /// 每个元素都应该是奇数
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都是奇数Contract的IsKept为true., 否则抛出ContractBrokenException异常</returns>

        public static Contract<IEnumerable<int>> MustEachBeOdd(this IEnumerable<int> collection) 
            => collection.MustEachSatisfy((i) => i.IsOdd());

        /// <summary>
        /// 每个元素都应该是奇数
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都是奇数Contract的IsKept为true., 否则抛出ContractBrokenException异常</returns>

        public static Contract<IEnumerable<int>> AndMustEachBeOdd(this Contract<IEnumerable<int>> collection)
            => collection.Value.MustEachBeOdd();
    }
}
