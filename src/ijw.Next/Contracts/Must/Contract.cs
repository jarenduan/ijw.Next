using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 表示一个契约
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Contract<T> {
        /// <summary>
        /// 是否遵守
        /// </summary>
        public bool IsKept { get; set; }

        /// <summary>
        /// 破坏契约提示信息
        /// </summary>
        public string BrokenMessage { get; set; }

        /// <summary>
        /// 原始值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 破坏契约时抛出ContractBrokenException异常
        /// </summary>
        /// <returns>契约对象本身</returns>
        public T ThrowsWhenBroken() {
            if (!this.IsKept) {
                throw new ContractBrokenException(this.BrokenMessage);
            }
            return this.Value;
        }

        /// <summary>
        /// 破坏契约时抛出指定类型的异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        public Contract<T> ThrowsWhenBroken<TException>() where TException : Exception {
            if (!this.IsKept) {
                throw (TException)Activator.CreateInstance(typeof(TException), this.BrokenMessage);
            }
            return this;
        }

        /// <summary>
        /// 破坏契约时抛出异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="exception">默认是ContractBrokenException异常</param>
        /// <returns>契约对象本身</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public Contract<T> ThrowsWhenBroken<TException>(TException exception) where TException : Exception {
            if (!this.IsKept) {
                if (exception == null) {
                    throw new ContractBrokenException(this.BrokenMessage);
                } else {
                    throw exception;
                }
            }
            return this;
        }
    }
}