namespace ijw.Next {
    /// <summary>
    /// 整形类与契约相关的扩展方法
    /// </summary>
    public static class IntegerContractShouldExt {
        /// <summary>
        /// 应该不等于0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>不等于0返回true, 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldBeNotZero(this int obj) 
            => obj.ShouldNotEqual(0);

        /// <summary>
        /// 应该不小于0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>不小于0, 返回true, 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldBeNotLessThanZero(this int obj) {
            return obj.ShouldNotLessThan(0);
        }

        /// <summary>
        /// 应该是偶数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>是偶数返回true, 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldBeEven(this int obj) {
            if (obj % 2 != 0) {
                throw new ContractBrokenException("should be even");
            }
            return true;
        }

        /// <summary>
        /// 应该是奇数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>是奇数返回true, 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldBeOdd(this int obj) {
            if (obj % 2 == 0) {
                throw new  ContractBrokenException("should be odd");
            }
            return true;
        }
    }
}