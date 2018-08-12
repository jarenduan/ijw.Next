using System.Collections;

namespace ijw.Next {
    /// <summary>
    /// 整形类与契约相关的扩展方法
    /// </summary>
    public static class IntContractMustExt {

        #region Must Not Zero

        /// <summary>
        /// 不必须等于0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>不等于0时, Contract的IsKept为true</returns>
        public static Contract<int> MustNotZero(this int obj)
            => new Contract<int>()
            {
                IsKept = obj.MustNotEquals(0).IsKept,
                BrokenMessage = $"{obj.ToString()} must not a zero",
                Value = obj
            };

        /// <summary>
        /// 不必须等于0
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>不等于0时, Contract的IsKept为true</returns>
        public static Contract<int> AndMustNotZero(this Contract<int> contract) 
            => contract.ThrowsWhenBroken().MustNotZero();

        #endregion

        #region Must >= 0

        /// <summary>
        /// 必须不小于0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>不小于0, Contract的IsKept为true</returns>
        public static Contract<int> MustNotLessThanZero(this int obj)
            => new Contract<int>() {
                IsKept = obj.ShouldNotLessThan(0),
                BrokenMessage = $"{obj.ToString()} must not less than zero",
                Value = obj
            };

        /// <summary>
        /// 必须不小于0
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>不小于0, Contract的IsKept为true</returns>
        public static Contract<int> AndMustNotLessThanZero(this Contract<int> contract) 
            => contract.ThrowsWhenBroken().MustNotLessThanZero();

        #endregion

        #region Must a Even Number

        /// <summary>
        /// 必须是偶数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>是偶数, Contract的IsKept为true</returns>
        public static Contract<int> MustEven(this int obj)
             => new Contract<int>() {
                 IsKept = obj % 2 == 0,
                 BrokenMessage = $"{obj.ToString()} must an even number",
                 Value = obj
             };

        /// <summary>
        /// 必须是偶数
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>是偶数, Contract的IsKept为true</returns>
        public static Contract<int> AndMustEven(this Contract<int> contract) 
            => contract.ThrowsWhenBroken().MustEven();

        #endregion

        #region Must a Odd Number

        /// <summary>
        /// 必须是奇数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>是奇数, Contract的IsKept为true</returns>
        public static Contract<int> MustOdd(this int obj) 
            => new Contract<int>() {
                 IsKept = obj % 2 != 0,
                 BrokenMessage = $"{obj.ToString()} must an odd number",
                 Value = obj
            };

        /// <summary>
        /// 必须是奇数
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>是奇数, Contract的IsKept为true</returns>
        public static Contract<int> AndMustOdd(this Contract<int> contract) 
            => contract.ThrowsWhenBroken().MustOdd();

        #endregion

        #region Must a Valid Index Number

        /// <summary>
        /// 必须是指定集合的有效索引值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="collection">指定的集合</param>
        /// <returns>有效索引，Contract的IsKept为true</returns>
        public static Contract<int> MustValidIndexFor(this int obj, ICollection collection)
            => new Contract<int>()
            {
                IsKept = obj >= 0 && obj < collection.Count,
                BrokenMessage = $"{obj.ToString()} must an odd number",
                Value = obj
            };

        /// <summary>
        /// 必须是指定集合的有效索引值
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="collection">指定的集合</param>
        /// <returns>有效索引，Contract的IsKept为true</returns>
        public static Contract<int> AndMustValidIndexFor(this Contract<int> contract, ICollection collection)
            => contract.ThrowsWhenBroken().MustOdd();

#endregion

    }
}