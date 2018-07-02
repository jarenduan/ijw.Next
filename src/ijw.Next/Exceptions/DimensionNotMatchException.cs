using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 表示集合元素维度不匹配的异常
    /// </summary>
    public class DimensionNotMatchException : Exception {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DimensionNotMatchException() {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">用以显示的信息</param>
        public DimensionNotMatchException(string message) : base(message) {
        }
    }
}
