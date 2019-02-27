using System;

namespace ijw.Next {
    /// <summary>
    /// 表示不满足某条件的异常
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class NotSatisfiedConditionException<T, V> : ContractBrokenException {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="obj">不满足条件的对象</param>
        /// <param name="predicate">应该满足的条件</param>
        /// <param name="message">异常信息</param>
        public NotSatisfiedConditionException(T obj, Predicate<V> predicate, string message = "") : base(message) {
            this.Obj = obj;
            this.Predicate = predicate;
        }

        /// <summary>
        /// 
        /// </summary>
        public Predicate<V> Predicate { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public T Obj { get; private set; }
    }
}