using System;

namespace ijw.Next {
    /// <summary>
    /// 提供一系列Object的扩展方法
    /// </summary>
    public static class ObjectContractMustExt {

        #region Must Not a Null Argument

        /// <summary>
        /// 应该不是Null参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="paramName">参数的名字</param>
        /// <param name="message"></param>
        /// <returns>不是Null,Contract的IsKept为true. 反之抛出ArgumentNullException异常</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Contract<T> MustNotNullArgument<T>(this T obj, string paramName, string message = "") where T : class
            => obj is null ? throw new ArgumentNullException(paramName, message)
                           : new Contract<T>(obj, $"Argument [{paramName}] must not be null ");

        /// <summary>
        /// 应该不是Null参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="paramName">参数的名字</param>
        /// <param name="message"></param>
        /// <returns>不是Null,Contract的IsKept为true. 反之抛出ArgumentNullException异常</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Contract<T> AndMustNotNullArgument<T>(this Contract<T> contract, string paramName, string message = "") where T : class
            => contract.Value.MustNotNullArgument(paramName, message);

        #endregion

        #region Must Not Null

        /// <summary>
        /// 应该不是Null引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="message">错误信息</param>
        /// <returns>不是Null,Contract的IsKept为true. 反之抛出NullReferenceException异常.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static Contract<T> MustNotNull<T>(this T obj, string message = "") where T : class {
            if (obj is null) {
                if (message == "") {
                    throw new NullReferenceException();
                }
                else {
                    throw new NullReferenceException(message);
                }
            }
            else {
                return new Contract<T>(obj, brokenMessage: message.IsNullOrEmpty() ? $" {obj} must not be null" : message);
            }
        }

        /// <summary>
        /// 应该不是Null引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="message">错误信息</param>
        /// <returns>不是Null,Contract的IsKept为true. 反之抛出NullReferenceException异常.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static Contract<T> AndMustNotNull<T>(this Contract<T> contract, string message = "") where T : class
            => contract.Value.MustNotNull(message);

        #endregion

        #region Must Satisfy Condition

        /// <summary>
        /// 应该满足指定的条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="condition">应满足的条件</param>
        /// <param name="conditionDescrption">条件的描述信息</param>
        /// <returns>满足条件Contract的IsKept为true.</returns>
        public static Contract<T> MustSatisfy<T>(this T obj, Predicate<T> condition, string conditionDescrption = "condition") {
            if (obj is null) throw new NullReferenceException();
            var isKept = condition(obj);
            var brokenMessage = $"{obj} must satisfy {conditionDescrption ?? condition.ToString()}";
            return isKept ? new Contract<T>(obj, brokenMessage) : throw new ContractBrokenException(brokenMessage);
        }

        /// <summary>
        /// 应该满足指定的条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="condition">应满足的条件</param>
        /// <param name="conditionDescrption">条件的描述信息</param>
        /// <returns>满足条件Contract的IsKept为true. </returns>
        public static Contract<T> AndMustSatisfy<T>(this Contract<T> contract, Predicate<T> condition, string conditionDescrption = "")
            => contract.Value.MustSatisfy(condition, conditionDescrption);

        #endregion

        #region Must Equal

        /// <summary>
        /// 应该与指定的对象相等.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">用以比较的对象</param>
        /// <returns>与指定的对象相等Contract的IsKept为true.</returns>
        public static Contract<T> MustEquals<T>(this T obj, T other) {
            if (obj is null) throw new NullReferenceException();
            var isKept = obj.Equals(other);
            var brokenMessage = $"{obj} must equal to {(other is null ? "[NULL]" : other.ToString())}";
            return isKept ? new Contract<T>(obj, brokenMessage) : throw new ContractBrokenException(brokenMessage);
        }

        /// <summary>
        /// 应该与指定的对象相等.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="other">用以比较的对象</param>
        /// <returns>与指定的对象相等Contract的IsKept为true.</returns>
        public static Contract<T> AndMustEquals<T>(this Contract<T> contract, T other)
            => contract.Value.MustEquals(other);

        #endregion

        #region Must Not Equal

        /// <summary>
        /// 应该与指定的对象不相等.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">用以比较的对象</param>
        /// <returns>与指定的对象相等Contract的IsKept为true.</returns>
        public static Contract<T> MustNotEquals<T>(this T obj, T other) {
            if (obj is null) throw new NullReferenceException();
            var isKept = !(obj.Equals(other));
            var brokenMessage = $"{obj} must not equal to {(other is null ? "[NULL]" : other.ToString())}";
            return isKept ? new Contract<T>(obj, brokenMessage) : throw new ContractBrokenException(brokenMessage);
        }

        /// <summary>
        /// 应该与指定的对象相等.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="other">用以比较的对象</param>
        /// <returns>与指定的对象相等Contract的IsKept为true.</returns>
        public static Contract<T> AndMustNotEquals<T>(this Contract<T> contract, T other)
            => contract.Value.MustNotEquals(other);
#endregion
    }
}
