﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next {
    /// <summary>
    /// IEnumerable泛型类与契约相关的扩展方法
    /// </summary>
    public static class IEnumerableContractExt {
        /// <summary>
        /// 不应该为空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns>集合有元素, 返回true. 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldNotBeEmpty<T>(this IEnumerable<T> collection) {
            collection.ShouldBeNotNull();
            return collection.Any();
        }

        /// <summary>
        /// 不应该是Null或者空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns>集合不是空且有元素, 返回true. 否则抛出ContractBrokenException异常</returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldNotBeNullOrEmpty<T>(this IEnumerable<T> collection) {
            collection.ShouldBeNotNull();
            return collection.ShouldNotBeEmpty();
        }

        /// <summary>
        /// 应该满足某条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="condition">应满足的条件</param>
        /// <returns>集合满足条件, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachSatisfy<T>(this IEnumerable<T> collection, Predicate<T> condition) {
            foreach (var item in collection) {
                item.ShouldSatisfy(condition);
            }
            return true;
        }

        /// <summary>
        /// 每个元素都应该等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="equalsTo">等于</param>
        /// <returns>每个元素都等于指定值, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachEquals(this IEnumerable<int> collection, int equalsTo) {
            return collection.ShouldEachSatisfy((i) => i.ShouldEquals(equalsTo));
        }

        /// <summary>
        /// 每个元素都应该不等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="notEqualsTo">不等于</param>
        /// <returns>每个元素都不等于指定值, 返回true. 否则抛出ContractBrokenException异常. </returns>
        /// <exception cref="ContractBrokenException"></exception>
        public static bool ShouldEachNotEquals(this IEnumerable<int> collection, int notEqualsTo) {
            return collection.ShouldEachSatisfy((i) => i.ShouldNotEquals(notEqualsTo));
        }
    }
}