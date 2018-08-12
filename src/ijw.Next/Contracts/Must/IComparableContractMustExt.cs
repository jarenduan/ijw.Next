using System;

namespace ijw.Next {
    /// <summary>
    /// IComparable泛型类的契约相关的扩展方法
    /// </summary>
    public static class IComparableContractMustExt {
        /// <summary>
        /// 应该大于指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">指定的对象</param>
        /// <returns>大于指定对象, Contract的IsKept为true.</returns>
        public static Contract<T> MustLargerThan<T>(this T obj, T other) where T : IComparable<T>
            => new Contract<T>() {
                IsKept = obj.CompareTo(other) > 0,
                BrokenMessage = $"{obj.ToString()} must larger than {other.ToString()}",
                Value = obj
            };

        /// <summary>
        /// 应该大于指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="other">指定的对象</param>
        /// <returns>大于指定对象, Contract的IsKept为true.</returns>
        public static Contract<T> AddMustLargerThan<T>(this Contract<T> contract, T other) where T : IComparable<T>
            => contract.ThrowsWhenBroken().MustLargerThan(other);

        /// <summary>
        /// 应该小于指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">指定的对象</param>
        /// <returns>小于指定对象, Contract的IsKept为true.</returns>
        public static Contract<T> MustLessThan<T>(this T obj, T other) where T : IComparable<T>
            => new Contract<T>() {
                IsKept = obj.CompareTo(other) < 0,
                BrokenMessage = $"{obj.ToString()} must less than {other.ToString()}",
                Value = obj
            };

        /// <summary>
        /// 应该小于指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="other">指定的对象</param>
        /// <returns>小于指定对象, Contract的IsKept为true.</returns>
        public static Contract<T> AndMustLessThan<T>(this Contract<T> contract, T other) where T : IComparable<T>
            => contract.ThrowsWhenBroken().MustLessThan(other);


        /// <summary>
        /// 应该不大于(小于等于)指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">指定的对象</param>
        /// <returns>不大于(小于等于)指定对象, Contract的IsKept为true.</returns>
        public static Contract<T> MustNotLargerThan<T>(this T obj, T other) where T : IComparable<T>
            => new Contract<T>() {
                IsKept = obj.CompareTo(other) <= 0,
                BrokenMessage = $"{obj.ToString()} must less than {other.ToString()}",
                Value = obj
            };

        /// <summary>
        /// 应该不大于(小于等于)指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="other">指定的对象</param>
        /// <returns>不大于(小于等于)指定对象, Contract的IsKept为true.</returns>
        public static Contract<T> AndMustNotLargerThan<T>(this Contract<T> contract, T other) where T : IComparable<T>
            => contract.ThrowsWhenBroken().MustNotLargerThan(other);


        /// <summary>
        /// 应该不小于(大于等于)定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">指定的对象</param>
        /// <returns>不小于(大于等于)指定对象, Contract的IsKept为true.</returns>
        public static Contract<T> MustNotLessThan<T>(this T obj, T other) where T : IComparable<T>
            => new Contract<T>() {
                IsKept = obj.CompareTo(other) >= 0,
                BrokenMessage = $"{obj.ToString()} must less than {other.ToString()}",
                Value = obj
            };
        /// <summary>
        /// 应该不小于(大于等于)定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="other">指定的对象</param>
        /// <returns>不小于(大于等于)指定对象, Contract的IsKept为true.</returns>
        public static Contract<T> AndMustNotLessThan<T>(this Contract<T> contract, T other) where T : IComparable<T>
            => contract.ThrowsWhenBroken().MustNotLessThan(other);

    }
}
