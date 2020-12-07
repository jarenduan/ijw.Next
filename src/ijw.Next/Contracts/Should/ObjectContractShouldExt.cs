using System;
using System.Diagnostics.CodeAnalysis;

namespace ijw.Next {
    /// <summary>
    /// 提供一系列Object的扩展方法
    /// </summary>
    public static class ObjectContractShouldExt {
        /// <summary>
        /// 不应该为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>不是Null,返回true. 反之抛出NullReferenceException异常</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool ShouldBeNotNull<T>([NotNullWhen(true)] this T obj) where T : class?
            => (obj is null) ? throw new NullReferenceException() : true;

        /// <summary>
        /// 不应该为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>不是Null,返回true. 反之抛出NullReferenceException异常</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool ShouldBeNotNull<T>([NotNullWhen(true)] this T? obj) where T : struct
            => obj is null ? throw new NullReferenceException() : true;

        /// <summary>
        /// 可空类型的实例不应该是Null参数, use ShouldBeNotNullArgument(this T? obj, string paramName) 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="paramName">参数的名字</param>
        /// <returns>不是Null,返回true. 反之抛出ArgumentNullException异常</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool ShouldBeNotNullArgument<T>(
#if NETSTANDARD2_1
            [NotNullWhen(true)]
#endif
        this T obj, string paramName)
            => (obj is null) ? throw new ArgumentNullException(paramName) : true;

        /// <summary>
        /// 可空类型的实例不应该是Null参数.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>不是Null,返回true. 反之抛出ArgumentNullException异常</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete]
        public static bool ShouldBeNotNullArgument<T>(this T? obj) where T : struct
            => (obj is null) ? throw new ArgumentNullException() : true;

        /// <summary>
        /// 可空类型的实例不应该是Null参数, use ShouldBeNotNull 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>不是Null,返回true. 反之抛出ArgumentNullException异常</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete]
        public static bool ShouldBeNotNullArgument<T>(this T obj)
            => (obj is null) ? throw new ArgumentNullException() : true;

        /// <summary>
        /// 不应该是空引用, use ShouldBeNotNull 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="message">错误信息</param>
        /// <returns>不是Null,返回true. 反之抛出NullReferenceException异常.</returns>
        /// <exception cref="NullReferenceException"></exception>
        [Obsolete]
        public static bool ShouldBeNotNullReference<T>(this T obj, string message = "")
            => (obj is null) ? throw new NullReferenceException(message) : true;

        /// <summary>
        /// 应该满足指定的条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="condition">应满足的条件</param>
        /// <returns>满足条件返回true. 反之抛出NotSatisfiedConditionException异常.</returns>
        /// <exception cref="NotSatisfiedConditionException{T,T}"></exception>
        public static bool ShouldSatisfy<T>(this T obj, Predicate<T> condition) 
            => condition(obj) ? true : throw new NotSatisfiedConditionException<T, T>(obj, condition);

        /// <summary>
        /// 应该满足指定的条件, 否则抛出指定的异常.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="obj"></param>
        /// <param name="condition">应满足的条件</param>
        /// <param name="excpetion">不满足时会抛出的异常</param>
        /// <returns>满足条件返回true, 否则抛出指定的异常.</returns>
        /// <exception cref="Exception"></exception>
        public static bool ShouldSatisfy<T, TException>(this T obj, Predicate<T> condition, TException excpetion) where TException : Exception 
            => condition(obj) ? true : throw excpetion;

#nullable disable
        /// <summary>
        /// 应该与指定的对象相等.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">用以比较的对象</param>
        /// <param name="objectName">对象名称, 用于错误提示</param>
        /// <returns>与指定的对象相等返回true, 否则抛出ContractBrokenException异常.</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEqual<T>(this T obj, T other, string objectName = "") 
            => obj.Equals(other) ? true : throw new ContractBrokenException($"{objectName} should equal {other}");

        /// <summary>
        /// 应该与指定的对象相等.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other">用以比较的对象</param>
        /// <param name="objectName">对象名称, 用于错误提示</param>
        /// <returns>与指定的对象相等返回true, 否则抛出ContractBrokenException异常.</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldNotEqual<T>(this T obj, T other, string objectName = "")
            => !obj.Equals(other) ? true : throw new ContractBrokenException($"{objectName} should not equal {other}");
#nullable enable
    }
}
