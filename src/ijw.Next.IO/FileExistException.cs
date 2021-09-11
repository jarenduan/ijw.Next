using System;

namespace ijw.Next.IO {
    /// <summary>
    /// File already exist exception.
    /// </summary>
    public class FileExistException : Exception {
        /// <summary>
        /// 
        /// </summary>
        public FileExistException() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public FileExistException(string message) : base(message) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public FileExistException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}