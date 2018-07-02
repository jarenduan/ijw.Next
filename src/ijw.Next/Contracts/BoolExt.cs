using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 契约相关的bool的扩展类
    /// </summary>
    public static class BoolExt {
        /// <summary>
        /// 破坏契约时抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aBool"></param>
        public static void ThrowWhenBroke<T>(this bool aBool) where T : Exception, new() {
            if (!aBool) {
                var ex = new T();
                throw ex;
            }
        }

        /// <summary>
        /// 破坏契约时抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aBool"></param>
        /// <param name="exceptionMessage">异常消息字符串</param>
        public static void ThrowWhenBroke<T>(this bool aBool, string exceptionMessage) where T : Exception, new() {
            if (!aBool) {
                var ex = (T)Activator.CreateInstance(typeof(T), exceptionMessage);
                throw ex;
            }
        }
    }
}
