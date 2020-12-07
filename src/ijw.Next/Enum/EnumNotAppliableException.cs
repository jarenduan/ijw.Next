using System;

namespace ijw.Next {
    /// <summary>
    /// 不适用时抛出此异常
    /// </summary>
    public class NotAppliableException : Exception {
        /// <summary>
        /// 构造一个不适用异常
        /// </summary>
        public NotAppliableException() {
        }

        /// <summary>
        /// 构造不适用异常
        /// </summary>
        /// <param name="message"></param>
        public NotAppliableException(string message) : base(message) {
        }

        /// <summary>
        /// 构造不适用异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NotAppliableException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}