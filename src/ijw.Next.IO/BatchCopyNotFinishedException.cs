using System;
using System.IO;

namespace ijw.Next.IO {
    /// <summary>
    /// 批量复制没有完成的异常
    /// </summary>

    public class BatchCopyNotFinishedException : IOException { //TODO: adapt net35, and change into AggregateException.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="copied"></param>
        /// <param name="message"></param>
        public BatchCopyNotFinishedException(string[] copied, string message = "") : base(message) {
            Copied = copied;
        }

        public BatchCopyNotFinishedException(string[] copied, Exception innerException, string message = "") : base(message, innerException) {
            Copied = copied;
        }

        /// <summary>
        /// 已经拷贝的文件
        /// </summary>
        public string[] Copied { get; set; }
    }
}