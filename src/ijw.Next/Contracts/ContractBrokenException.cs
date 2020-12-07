using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next {
    /// <summary>
    /// 表示违反契约的异常
    /// </summary>
    public class ContractBrokenException : Exception {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">用以显示的信息</param>
        public ContractBrokenException(string message) : base(message) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">用以显示的信息</param>
        /// <param name="innerException">内部的异常</param>
        public ContractBrokenException(string message, Exception innerException) : base(message, innerException) {
        }

    }
}
