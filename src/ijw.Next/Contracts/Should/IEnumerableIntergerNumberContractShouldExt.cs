using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableIntergerNumberContractShouldExt {
        /// <summary>
        /// 每个元素都应该不等于0
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都不等于0, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachNotBeZero(this IEnumerable<int> collection) {
            return collection.ShouldEachSatisfy((i) => i.ShouldNotEquals(0));
        }


        /// <summary>
        /// 每个元素都应该不小于0
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都不小于0, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachNotLessThanZero(this IEnumerable<int> collection) {
            return collection.ShouldEachSatisfy((i) => i.ShouldBeNotLessThanZero());
        }

        /// <summary>
        /// 每个元素都应该是偶数
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都是偶数返回true, 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachBeEven(this IEnumerable<int> collection) {
            return collection.ShouldEachSatisfy((i) => i.ShouldBeEven());
        }

        /// <summary>
        /// 每个元素都应该是奇数
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>每个元素都是奇数返回true, 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachBeOdd(this IEnumerable<int> collection) {
            return collection.ShouldEachSatisfy((i) => i.ShouldBeOdd());
        }
    }
}
