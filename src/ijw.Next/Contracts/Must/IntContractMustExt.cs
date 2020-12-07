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
        public static Contract<int> MustNotZero(this int obj) {
            var isKept = obj != 0;
            var brokenMessage = $"{obj} must not a zero";
            return isKept ? new Contract<int>(obj, brokenMessage): throw new ContractBrokenException(brokenMessage);
        }
        /// <summary>
        /// 不必须等于0
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>不等于0时, Contract的IsKept为true</returns>
        public static Contract<int> AndMustNotZero(this Contract<int> contract) 
            => contract.Value.MustNotZero();

        #endregion

        #region Must >= 0

        /// <summary>
        /// 必须不小于0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>不小于0, Contract的IsKept为true</returns>
        public static Contract<int> MustNotLessThanZero(this int obj) {
            var isKept = obj >= 0;
            var brokenMessage = $"{obj} must Not Less Than Zero";
            return isKept ? new Contract<int>(obj, brokenMessage) : throw new ContractBrokenException(brokenMessage);
        }

        /// <summary>
        /// 必须不小于0
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>不小于0, Contract的IsKept为true</returns>
        public static Contract<int> AndMustNotLessThanZero(this Contract<int> contract) 
            => contract.Value.MustNotLessThanZero();

        #endregion

        #region Must an Even Number

        /// <summary>
        /// 必须是偶数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>是偶数, Contract的IsKept为true</returns>
        public static Contract<int> MustEven(this int obj) {
            var isKept = obj.IsEven();
            var brokenMessage = $"{obj} must an even number";
            return isKept ? new Contract<int>(obj, brokenMessage) : throw new ContractBrokenException(brokenMessage); 
        }

        /// <summary>
        /// 必须是偶数
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>是偶数, Contract的IsKept为true</returns>
        public static Contract<int> AndMustEven(this Contract<int> contract) 
            => contract.Value.MustEven();

        #endregion

        #region Must an Odd Number

        /// <summary>
        /// 必须是奇数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>是奇数, Contract的IsKept为true</returns>
        public static Contract<int> MustOdd(this int obj) {
            var isKept = obj.IsOdd();
            var brokenMessage = $"{obj} must an odd number";
            return isKept ? new Contract<int>(obj, brokenMessage) : throw new ContractBrokenException(brokenMessage); 
        }

        /// <summary>
        /// 必须是奇数
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>是奇数, Contract的IsKept为true</returns>
        public static Contract<int> AndMustOdd(this Contract<int> contract) 
            => contract.Value.MustOdd();

        #endregion

        #region Must a Valid Index Number

        /// <summary>
        /// 必须是指定集合的有效索引值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="collection">指定的集合</param>
        /// <returns>有效索引, Contract的IsKept为true</returns>
        public static Contract<int> MustValidIndexFor(this int obj, ICollection collection) {
            var isKept = obj >= 0 && obj < collection.Count;
            var brokenMessage = $"{obj} must an odd number";
            return isKept ? new Contract<int>(obj, brokenMessage) : throw new ContractBrokenException(brokenMessage);
        }

        /// <summary>
        /// 必须是指定集合的有效索引值
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="collection">指定的集合</param>
        /// <returns>有效索引, Contract的IsKept为true</returns>
        public static Contract<int> AndMustValidIndexFor(this Contract<int> contract, ICollection collection)
            => contract.Value.MustOdd();
#endregion
    }
}