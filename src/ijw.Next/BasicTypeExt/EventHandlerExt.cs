using System;
using System.Reflection;
using System.Diagnostics;


namespace ijw.Next {
    /// <summary>
    /// EventHandler的扩展方法
    /// </summary>
    public static class EventHandlerExt {
        /// <summary>
        /// 如果有挂接的事件处理器, 就激活事件, 处理完毕后返回一个值.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">事件参数</param>
        /// <returns>激活了事件, 返回真; 反之返回假</returns>
        public static bool InvokeIfNotNull(this EventHandler handler, object sender, EventArgs eventArgs) {
            if (handler != null) {
                debugWriteInvokingInfo(handler);
                handler.Invoke(sender, eventArgs);
                return true;
            }
            else {
                DebugHelper.WriteLine("Null handler, none invoked.");
                return false;
            }
        }

        [Conditional("DEBUG")]
        private static void debugWriteInvokingInfo(EventHandler handler) {
#if !NETSTANDARD1_4
            DebugHelper.WriteLine("Try to invoke event handler: " + handler.Method.Name + ".");
#else
            DebugHelper.WriteLine("Try to invoke event handler: " + handler.GetMethodInfo().Name + ".");
#endif
        }

        /// <summary>
        /// 如果有挂接的事件处理器, 就激活事件.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="sender">sender</param>
        /// <param name="e">事件参数</param>
        /// <returns>激活了事件, 返回真; 反之返回假</returns>
        public static bool InvokeIfNotNull<T>(this EventHandler<T> handler, object sender, T e)
#if NET35 || NET40
            where T : EventArgs
#endif
        {
            if (handler != null) {
                debugWriteInvokingInfo(handler);
                handler.Invoke(sender, e);
                return true;
            }
            else {
                DebugHelper.WriteLine("Null handler, none invoked.");
                return false;
            }
        }

        [Conditional("DEBUG")]
        //only the last added method's name is written.
        private static void debugWriteInvokingInfo<T>(EventHandler<T> handler)
#if NET35 || NET40
            where T : EventArgs
#endif
        {
#if !NETSTANDARD1_4
            DebugHelper.WriteLine("Try to invoke event handler: " + handler.Method.Name + ".");
#else
            DebugHelper.WriteLine("Try to invoke event handler: " + handler.GetMethodInfo().Name + ".");
#endif
        }
    }
}
