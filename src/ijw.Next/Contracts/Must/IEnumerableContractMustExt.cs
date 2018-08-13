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
            return new Contract<IEnumerable<T>>() {
                IsKept = collection.Any(),
                BrokenMessage = $"Collection {collection.ToString()} is empty",
                Value = collection
            };
        }

        /// <summary>
        /// 不应该是Null或者空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <returns>集合不是空且有元素, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> MustNotNullOrEmpty<T>(this Contract<IEnumerable<T>> contract)
            => contract.ThrowsWhenBroken().MustNotNullOrEmpty();

        /// <summary>
        /// 每一项都必须满足某条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="condition">应满足的条件</param>
        /// <param name="conditionDescrption">条件描述</param>
        /// <returns>集合满足条件, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> MustEachSatisfy<T>(this IEnumerable<T> collection, Predicate<T> condition, string conditionDescrption = null) {
            int i = 0;
            bool broke = false;
            foreach (var item in collection) {
                var c = item.MustSatisfy(condition);
                if (!c.IsKept) {
                    broke = true;
                    break;
                }
                i++;
            }

            return new Contract<IEnumerable<T>>() {
                IsKept = broke,
                BrokenMessage = $"The {i.ToOrdinalString()} item in collection doesn't satisfy {conditionDescrption ?? conditionDescrption.ToString()}",
                Value = collection,
            };
        }

        /// <summary>
        /// 每一项都必须满足某条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contract"></param>
        /// <param name="condition">应满足的条件</param>
        /// <param name="conditionDescrption">条件描述</param>
        /// <returns>集合满足条件, Contract的IsKept为true.</returns>
        public static Contract<IEnumerable<T>> AndMustEachSatisfy<T>(this Contract<IEnumerable<T>> contract, Predicate<T> condition, string conditionDescrption = null)
            => contract.ThrowsWhenBroken().MustEachSatisfy(condition, conditionDescrption);

        /// <summary>
        /// 每个元素都应该等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">等于</param>
        /// <returns>每个元素都等于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> MustEachEquals<T>(this IEnumerable<T> collection, T other)
            => collection.MustEachSatisfy((i) => i.Equals(other), $"must equals to {other.ToString()}");

        /// <summary>
        /// 每个元素都应该等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">等于</param>
        /// <returns>每个元素都等于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> AndMustEachEquals<T>(this Contract<IEnumerable<T>> collection, T other)
            => collection.ThrowsWhenBroken().MustEachEquals(other);

        /// <summary>
        /// 每个元素都应该不等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">等于</param>
        /// <returns>每个元素都不等于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> MustEachNotEquals<T>(this IEnumerable<T> collection, T other)
            => collection.MustEachSatisfy((i) => !i.Equals(other), $"must equals to {other.ToString()}");

        /// <summary>
        /// 每个元素都应该不等于指定值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="other">等于</param>
        /// <returns>每个元素都不等于指定值, Contract的IsKept为true.</returns>

        public static Contract<IEnumerable<T>> AndMustEachNotEquals<T>(this Contract<IEnumerable<T>> collection, T other)
            => collection.ThrowsWhenBroken().MustEachNotEquals(other);

        

    }
}