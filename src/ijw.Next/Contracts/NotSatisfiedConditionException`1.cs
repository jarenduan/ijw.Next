using System;

namespace ijw.Next {
    /// <summary>
    /// 表示不满足某条件的异常
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotSatisfiedConditionException<T> : ContractBrokenException {
        private readonly T _obj;
        private readonly Predicate<T> _predicate;
        /// <summary>
        /// 构造函数
        /// </summary>
        public NotSatisfiedConditionException() {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public NotSatisfiedConditionException(string message) : base(message) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">内部异常</param>
        public NotSatisfiedConditionException(string message, Exception innerException) : base(message, innerException) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="obj">不满足条件的对象</param>
        /// <param name="predicate">应该满足的条件</param>
        public NotSatisfiedConditionException(T obj, Predicate<T> predicate) {
            this._obj = obj;
            this._predicate = predicate;
        }
    }
}