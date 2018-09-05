using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 表示一个契约
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Contract<T> {
        /// <summary>
        /// 构造一个契约对象
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="brokenMessage">破坏契约的提示信息</param>
        /// <param name="isKept">是否遵守了契约</param>
        public Contract(T value, string brokenMessage = "", bool isKept = true) {
            Value = value;
            BrokenMessage = brokenMessage;
            IsKept = isKept;
        }

        /// <summary>
        /// 原始值
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// 是否遵守
        /// </summary>
        public bool IsKept { get; private set; }

        /// <summary>
        /// 破坏契约提示信息
        /// </summary>
        public string BrokenMessage { get; private set; }

        /// <summary>
        /// 破坏契约时抛出ContractBrokenException异常
        /// </summary>
        /// <returns>契约对象中的值</returns>
        public T ThrowsWhenBroken() 
            => IsKept ? Value : throw new ContractBrokenException(this.BrokenMessage);

        /// <summary>
        /// 破坏契约时抛出指定类型的异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        public Contract<T> ThrowsWhenBroken<TException>() where TException : Exception
            => IsKept ? this : throw (TException)Activator.CreateInstance(typeof(TException), this.BrokenMessage);

        /// <summary>
        /// 破坏契约时抛出异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="exception">默认是ContractBrokenException异常</param>
        /// <returns>契约对象本身</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public Contract<T> ThrowsWhenBroken<TException>(TException exception) where TException : Exception {
            if (!IsKept) {
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