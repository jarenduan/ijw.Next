using System;

namespace ijw.Next {
    /// <summary>
    /// 表示达到最大尝试次数的异常
    /// </summary>
    public class ReachMaxRetryTimeException : Exception {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ReachMaxRetryTimeException() {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">用以显示的信息</param>
        public ReachMaxRetryTimeException(string message) : base(message) {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">用以显示的信息</param>
        /// <param name="innerException">内部包含的异常</param>
        public ReachMaxRetryTimeException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}