using System;
using ijw.Next.IO;

namespace ijw.Next.Log.File {
    /// <summary>
    /// 最简单的文件日志器, 所有方法线程安全.
    /// </summary>
    public class SimpleFileLog : ILogHelper {
        private readonly object _syncRoot = new object();
        private string _logfilePath = "~.log";

        /// <summary>
        /// 日志文件路径, 默认是当前工作目录的 ~.log 文件.
        /// </summary>
        public string LogFilePath {
            get {
                return this._logfilePath;
            }
            set {
                lock (this._syncRoot) {
                    this._logfilePath = value;
                }
            }
        }

        /// <summary>
        /// 向日志中追加写入字符串并换行
        /// </summary>
        /// <param name="content"></param>
        public void WriteLine(string content) {
            lock (this._syncRoot) {
                this.Write($"{content}{System.Environment.NewLine}");
            }
        }

        /// <summary>
        /// 向日志中追加写入字符串
        /// </summary>
        /// <param name="content"></param>
        public void Write(string content) {
            lock (this._syncRoot) {
                FileHelper.WriteStringToFile(this.LogFilePath, content, true);
            }
        }

        /// <summary>
        /// 向日志文件中写入一条日志（自动添加时间）
        /// </summary>
        /// <param name="log"></param>
        public void Log(string log) {
            lock (this._syncRoot) {
                this.WriteLine(log.PrefixWithNowShortTimeLabel());
            }
        }

        /// <summary>
        /// 写入一条错误日志（自动添加时间）
        /// </summary>
        /// <param name="msg">写入的内容</param>
        public void WriteError(string msg) => Log(msg ?? "Exception is Null");

        /// <summary>
        /// 写入一条错误日志（自动添加时间）
        /// </summary>
        /// <param name="ex">写入的内容</param>
        public void WriteError(Exception ex) => Log(ex is null ? "Exception is Null" : ex.Message);

        /// <summary>
        /// 写入一条日志（自动添加时间）
        /// </summary>
        /// <param name="msg">写入的内容</param>
        public void WriteInfo(string msg) => Log(msg ?? "Exception is Null");

        /// <summary>
        /// 写入一条日志（自动添加时间）
        /// </summary>
        /// <param name="ex">写入的内容</param>
        public void WriteInfo(Exception ex) => Log(ex is null ? "Exception is Null" : ex.Message);
    }
}