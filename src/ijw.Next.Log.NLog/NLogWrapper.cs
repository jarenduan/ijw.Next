using NLog;
using System;

namespace ijw.Next.Log.NLog {
    /// <summary>
    /// NLog wrapper
    /// </summary>
    public class NLogWrapper : ILogger {
        private readonly Logger _logger;

        /// <summary>
        /// Construct a NLogWrapper using a NLog logger object.
        /// </summary>
        /// <param name="logger"></param>
        public NLogWrapper(Logger logger) => _logger = logger;

        /// <summary>
        /// Use internal NLog to log information of DEBUG level.
        /// </summary>
        /// <param name="msg">logging information</param>
        public void WriteDebug(string msg) => _logger.Debug(msg);

        /// <summary>
        /// Use internal NLog to log exception of DEBUG level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void WriteDebug(Exception ex) => _logger.Debug(ex);

        /// <summary>
        /// Use internal NLog to log exception of DEBUG level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        /// <param name="msg">message</param>
        public void WriteDebug(Exception ex, string msg) => _logger.Debug(ex, msg);

        /// <summary>
        /// Use internal NLog to log information of ERROR level.
        /// </summary>
        /// <param name="msg">logging information</param>
        public void WriteError(string msg) => _logger.Error(msg);

        /// <summary>
        /// Use internal NLog to log exception of ERROR level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void WriteError(Exception ex) => _logger.Error(ex);

        /// <summary>
        /// Use internal NLog to log exception of ERROR level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        /// <param name="msg">message</param>
        public void WriteError(Exception ex, string msg) => _logger.Error(ex, msg);

        /// <summary>
        /// Use internal NLog to log information of INFO level.
        /// </summary>
        /// <param name="msg">logging information</param>
        public void WriteInfo(string msg) => _logger.Info(msg);

        /// <summary>
        /// Use internal NLog to log exception of INFO level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void WriteInfo(Exception ex) => _logger.Info(ex);

        /// <summary>
        /// Use internal NLog to log exception of INFO level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        /// <param name="msg">message</param>
        public void WriteInfo(Exception ex, string msg) => _logger.Info(ex, msg);

        /// <summary>
        /// Use internal NLog to log information of TRACE level.
        /// </summary>
        /// <param name="msg">logging information</param>
        public void WriteTrace(string msg) => _logger.Trace(msg);

        /// <summary>
        /// Use internal NLog to log exception of TRACE level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void WriteTrace(Exception ex) => _logger.Trace(ex);

        /// <summary>
        /// Use internal NLog to log exception of TRACE level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        /// <param name="msg">message</param>
        public void WriteTrace(Exception ex, string msg) => _logger.Trace(ex, msg);

        /// <summary>
        /// Use internal NLog to log information of WARN level.
        /// </summary>
        /// <param name="msg">logging information</param>
        public void WriteWarn(string msg) => _logger.Warn(msg);

        /// <summary>
        /// Use internal NLog to log exception of WARN level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void WriteWarn(Exception ex) => _logger.Warn(ex);

        /// <summary>
        /// Use internal NLog to log exception of WARN level.
        /// </summary>
        /// <param name="ex">exception to log</param>
        /// <param name="msg">message</param>
        public void WriteWarn(Exception ex, string msg) => _logger.Warn(ex, msg);
    }
}