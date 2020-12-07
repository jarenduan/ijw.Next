using System;

namespace ijw.Next {
    /// <summary>
    /// IComparable泛型类的契约相关的扩展方法
    /// </summary>
    public static class IComparableContractShouldExt {
        /// <summary>
        /// 应该大于指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">指定的对象</param>
        /// <param name="objectName">对象名称, 用于错误提示</param>
        /// <returns>大于指定对象, 返回true. 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldLargerThan<T>(this T obj, T other, string objectName = "") where T : IComparable<T>
            => obj.CompareTo(other) > 0 ? true : throw new ContractBrokenException($"{objectName} should be larger than {other}");

        /// <summary>
        /// 应该小于指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">指定的对象</param>
        /// <param name="objectName">对象名称, 用于错误提示</param>
        /// <returns>小于指定对象, 返回true. 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldLessThan<T>(this T obj, T other, string objectName = "") where T : IComparable<T>
            => obj.CompareTo(other) < 0 ? true : throw new ContractBrokenException($"{objectName} should be less than {other}");

        /// <summary>
        /// 应该不大于(小于等于)指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">指定的对象</param>
        /// <param name="objectName">对象名称, 用于错误提示</param>
        /// <returns>不大于(小于等于)指定对象, 返回true. 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldNotLargerThan<T>(this T obj, T other, string objectName = "") where T : IComparable<T>
           => obj.CompareTo(other) <= 0 ? true : throw new ContractBrokenException($"{objectName} should be not larger than {other}");

        /// <summary>
        /// 应该不小于(大于等于)定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">指定的对象</param>
        /// <param name="objectName">对象名称, 用于错误提示</param>
        /// <returns>不小于(大于等于)指定对象, 返回true. 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldNotLessThan<T>(this T obj, T other, string objectName = "") where T : IComparable<T>
            => obj.CompareTo(other) >= 0 ? true : throw new ContractBrokenException($"{objectName} should be not less than {other}");
    }
}
