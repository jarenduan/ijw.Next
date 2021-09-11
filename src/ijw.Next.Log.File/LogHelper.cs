//实现采用ijw.dotnet.SimpleLog
//log4net的配置不能自动化, 比较麻烦, 不符合ijw基本原则.
using System;

namespace ijw.Next.Log.File {
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper : ILogHelper {
        private SimpleFileLog _logger = new SimpleFileLog();

        /// <summary>
        /// 日志文件路径, 默认文件当前目录的~.log.
        /// </summary>
        public string LogFilePath {
            get => _logger.LogFilePath;
            set => _logger.LogFilePath = value;
        }

        public void WriteDebug(string msg) {
            throw new NotImplementedException();
        }

        public void WriteDebug(Exception ex) {
            throw new NotImplementedException();
        }

        public void WriteDebug(Exception ex, string msg) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 输出错误日志到SimpleLog, 前面会标明Error字样
        /// </summary>
        /// <param name="ex"></param>
        public void WriteError(Exception ex) {
            if (ex is null) {
                return;
            }
            this._logger.Log("Error： " + ex.Message);
        }
        /// <summary>
        /// 输出错误日志到SimpleLog, 前面会标明Error字样
        /// </summary>
        /// <param name="msg"></param>
        public void WriteError(string msg) {
            if (msg is null) {
                return;
            }
            this._logger.Log("Error： " + msg);
        }

        public void WriteError(Exception ex, string msg) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 输出信息日志到SimpleLog, 前面会标明Info字样
        /// </summary>
        /// <param name="ex"></param>
        public void WriteInfo(Exception ex) {
            if (ex is null) {
                return;
            }
            this._logger.Log("Info: " + ex.Message);
        }
        /// <summary>
        /// 输出信息日志到SimpleLog, 前面会标明Info字样
        /// </summary>
        /// <param name="msg"></param>
        public void WriteInfo(string msg) {
            if (msg is null) {
                return;
            }
            this._logger.Log("Info: " + msg);
        }

        public void WriteInfo(Exception ex, string msg) {
            throw new NotImplementedException();
        }

        public void WriteTrace(string msg) {
            throw new NotImplementedException();
        }

        public void WriteTrace(Exception ex) {
            throw new NotImplementedException();
        }

        public void WriteTrace(Exception ex, string msg) {
            throw new NotImplementedException();
        }

        public void WriteWarn(string msg) {
            throw new NotImplementedException();
        }

        public void WriteWarn(Exception ex) {
            throw new NotImplementedException();
        }

        public void WriteWarn(Exception ex, string msg) {
            throw new NotImplementedException();
        }
    }
}
