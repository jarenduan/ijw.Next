using System;
using System.Threading;
using System.Threading.Tasks;

namespace ijw.Next.Threading.Tasks {
    /// <summary>
    /// Task帮助类
    /// </summary>
    public static class TaskHelper {
        /// <summary>
        /// 用Task运行新的Action
        /// </summary>
        /// <param name="action">欲执行的Action</param>
        /// <returns>运行的Task</returns>
        public static async Task Run(Action action) {
#if NET35 || NET40
            await Task.Factory.StartNew(action);
#else
            await Task.Run(action);
#endif
        }

        /// <summary>
        /// 用Task运行新的Action
        /// </summary>
        /// <param name="action">欲执行的Action</param>
        /// <param name="cancellationToken">用以取消任务的token</param>
        /// <returns></returns>
        public static async Task Run(Action action, CancellationToken cancellationToken) {
#if NET35 || NET40
            await Task.Factory.StartNew(action, cancellationToken);
#else
            await Task.Run(action, cancellationToken);
#endif
        }

        /// <summary>
        /// 用Task运行新的Action
        /// </summary>
        /// <param name="func">欲执行的Func</param>
        /// <returns>运行的Task</returns>
        public static async Task<T> Run<T>(Func<T> func) {
#if NET35 || NET40
            return await Task.Factory.StartNew(func);
#else
            return await Task.Run(func);
#endif
        }

        /// <summary>
        /// 用Task运行新的Action
        /// </summary>
        /// <param name="func">欲执行的Action</param>
        /// <param name="cancellationToken">用以取消任务的token</param>
        /// <returns></returns>
        public static async Task<T> Run<T>(Func<T> func, CancellationToken cancellationToken) {
#if NET35 || NET40
            return await Task.Factory.StartNew(func, cancellationToken);
#else
            return await Task.Run(func, cancellationToken);
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Task CompletedTask =>
#if NET35 || NET40 || NET45
            Run(() => { });
#else
            Task.CompletedTask;
#endif
    }
}
