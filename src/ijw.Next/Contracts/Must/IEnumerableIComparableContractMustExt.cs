using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// IComparable序列的契约扩展方法
    /// </summary>
    public static class IEnumerableIComparableContractMustExt {
        /// <summary>
        /// 每个元素都应该大于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">大于</param>
        /// <returns>每个元素都大于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> MustEachLargerThan<T>(this IEnumerable<T> collection, T other) where T: IComparable<T>
            => collection.MustEachSatisfy((i) => i.CompareTo(other) > 0);

        /// <summary>
        /// 每个元素都应该大于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">大于</param>
        /// <returns>每个元素都大于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> AndMustEachLargerThan<T>(this Contract<IEnumerable<T>> collection, T other) where T : IComparable<T>
            => collection.Value.MustEachLargerThan(other);

        /// <summary>
        /// 每个元素都应该不大于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">不大于</param>
        /// <returns>每个元素都不大于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> MustEachNotLargerThan<T>(this IEnumerable<T> collection, T other) where T : IComparable<T>
            => collection.MustEachSatisfy((i) => i.CompareTo(other) <= 0);

        /// <summary>
        /// 每个元素都应该不大于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">不大于</param>
        /// <returns>每个元素都不大于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> AndMustEachNotLargerThan<T>(this Contract<IEnumerable<T>> collection, T other) where T : IComparable<T>
            => collection.Value.MustEachNotLargerThan(other);


        /// <summary>
        /// 每个元素都应该小于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">小于</param>
        /// <returns>每个元素都小于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> MustEachLessThan<T>(this IEnumerable<T> collection, T other) where T : IComparable<T>
            => collection.MustEachSatisfy((i) => i.CompareTo(other) < 0);

        /// <summary>
        /// 每个元素都应该小于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">小于</param>
        /// <returns>每个元素都小于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> AndMustEachLessThan<T>(this Contract<IEnumerable<T>> collection, T other) where T : IComparable<T>
             => collection.Value.MustEachLessThan(other);

        /// <summary>
        /// 每个元素都应该不小于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">不小于</param>
        /// <returns>每个元素都不小于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> MustEachNotLessThan<T>(this IEnumerable<T> collection, T other) where T : IComparable<T>
            => collection.MustEachSatisfy((i) => i.CompareTo(other) >= 0);

        /// <summary>
        /// 每个元素都应该不小于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">不小于</param>
        /// <returns>每个元素都不小于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> AndMustEachNotLessThan<T>(this Contract<IEnumerable<T>> collection, T other) where T : IComparable<T>
            => collection.Value.MustEachNotLessThan(other);
    }
}
