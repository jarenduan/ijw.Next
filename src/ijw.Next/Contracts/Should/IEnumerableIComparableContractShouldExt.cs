using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 
    /// </summary>
    public static
        class IEnumerableIComparableContractShouldExt {

        /// <summary>
        /// 每个元素都应该大于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="largerThan">大于</param>
        /// <returns>每个元素都大于指定值, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachLargerThan(this IEnumerable<int> collection, int largerThan) {
            return collection.ShouldEachSatisfy((i) => i.ShouldLargerThan(largerThan));
        }

        /// <summary>
        /// 每个元素都应该不大于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="notLargerThan">不大于</param>
        /// <returns>每个元素都不大于指定值, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachNotLargerThan(this IEnumerable<int> collection, int notLargerThan) {
            return collection.ShouldEachSatisfy((i) => i.ShouldNotLargerThan(notLargerThan));
        }

        /// <summary>
        /// 每个元素都应该小于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="lessThan">小于</param>
        /// <returns>每个元素都小于指定值, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachLessThan(this IEnumerable<int> collection, int lessThan) {
            return collection.ShouldEachSatisfy((i) => i.ShouldLessThan(lessThan));
        }

        /// <summary>
        /// 每个元素都应该不小于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="notLessThan">不小于</param>
        /// <returns>每个元素都不小于指定值, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachNotLessThan(this IEnumerable<int> collection, int notLessThan) {
            return collection.ShouldEachSatisfy((i) => i.ShouldNotLessThan(notLessThan));
        }

    }
}
