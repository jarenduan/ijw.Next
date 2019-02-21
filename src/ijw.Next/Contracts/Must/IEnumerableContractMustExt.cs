using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next {
    /// <summary>
    /// IEnumerable泛型类与契约相关的扩展方法
    /// </summary>
    public static class IEnumerableContractMustExt {
        /// <summary>
        /// 不应该是Null或者空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns>集合不是空且有元素, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> MustNotNullOrEmpty<T>(this IEnumerable<T> collection) {
            collection.MustNotNull();
            var isKept = collection.Any();
            var brokenMessage = $"Collection {collection.ToString()} is empty";
            if (isKept) {
                throw new ContractBrokenException(brokenMessage);
            }
            return new Contract<IEnumerable<T>>(collection, brokenMessage);
        }

        /// <summary>
        /// 不应该是Null或者空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <returns>集合不是空且有元素, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> AndMustNotNullOrEmpty<T>(this Contract<IEnumerable<T>> contract)
            => contract.Value.MustNotNullOrEmpty();

        /// <summary>
        /// 每一项都必须满足某条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="pred">应满足的条件</param>
        /// <param name="conditionDescrption">条件描述</param>
        /// <returns>集合满足条件, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> MustEachSatisfy<T>(this IEnumerable<T> collection, Predicate<T> pred, string conditionDescrption = "condition") {
            int i = 0;
            bool broke = false;
            foreach (var item in collection) {
                broke = pred(item);
                if (!broke) {
                    break;
                }
                i++;
            }
            var brokenMessage = $"The {i.ToOrdinalString()} item in collection doesn't satisfy {conditionDescrption ?? conditionDescrption.ToString()}";
            if (broke) {
                throw new NotSatisfiedConditionException<IEnumerable<T>, T>(collection, pred, brokenMessage);
            }
            else {
                return new Contract<IEnumerable<T>>(collection, brokenMessage);
            }
        }

        /// <summary>
        /// 每一项都必须满足某条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="condition">应满足的条件</param>
        /// <param name="conditionDescrption">条件描述</param>
        /// <returns>集合满足条件, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> AndMustEachSatisfy<T>(this Contract<IEnumerable<T>> contract, Predicate<T> condition, string conditionDescrption = "condition")
            => contract.Value.MustEachSatisfy(condition, conditionDescrption);

        /// <summary>
        /// 每个元素都应该等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">等于</param>
        /// <returns>每个元素都等于指定值, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> MustEachEquals<T>(this IEnumerable<T> collection, T other)
            => collection.MustEachSatisfy((i) => i?.Equals(other) ?? false, $"must for each item equals to {(other is null? "[NULL]" : other.ToString())}");

        /// <summary>
        /// 每个元素都应该等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">等于</param>
        /// <returns>每个元素都等于指定值, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> AndMustEachEquals<T>(this Contract<IEnumerable<T>> collection, T other)
            => collection.Value.MustEachEquals(other);

        /// <summary>
        /// 每个元素都应该不等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">等于</param>
        /// <returns>每个元素都不等于指定值, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> MustEachNotEquals<T>(this IEnumerable<T> collection, T other)
            => collection.MustEachSatisfy((i) => !i?.Equals(other) ?? false, $"must for each item equals to {(other is null ? "[NULL]" : other.ToString())}");

        /// <summary>
        /// 每个元素都应该不等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">等于</param>
        /// <returns>每个元素都不等于指定值, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> AndMustEachNotEquals<T>(this Contract<IEnumerable<T>> collection, T other)
            => collection.Value.MustEachNotEquals(other);
    }
}