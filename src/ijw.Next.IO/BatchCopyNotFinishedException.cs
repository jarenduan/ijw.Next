using System;

namespace ijw.Next.IO {
    /// <summary>
    /// 表示批量Copy没有顺利完成的异常.
    /// </summary>
    public class BatchCopyNotFinishedException : Exception {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="copied"></param>
        public BatchCopyNotFinishedException(string[] copied) {
            Copied = copied;
        }

        /// <summary>
        /// 已经拷贝的文件
        /// </summary>
        public string[] Copied { get; set; }
    }
}