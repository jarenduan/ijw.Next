using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    public static class ICollectionContractMustExt {
        /// <summary>
        /// 集合元素数必须等于指定集合的元素数
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="collection"></param>
        /// <param name="otherCollection"></param>
        /// <returns>两个集合元素数必须相等的契约</returns>
        public static Contract<ICollection<T1>> MustCountEquals<T1, T2>(this ICollection<T1> collection, ICollection<T2> otherCollection) {
            var isKept = collection.Count == otherCollection.Count;
            if (!isKept) {
                throw new CollectionCountNotMatchException<T1, T2>(collection, otherCollection);
            }
            else {
                return new Contract<ICollection<T1>>(collection, "Two Collection Count NOT Match");
            }
        }

        /// <summary>
        /// 集合元素数必须等于指定集合的元素数
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="contract"></param>
        /// <param name="otherCollection"></param>
        /// <returns>两个集合元素数必须相等的契约</returns>
        public static Contract<ICollection<T1>> AndMustCountMatch<T1, T2>(this Contract<ICollection<T1>> contract, ICollection<T2> otherCollection)
            => contract.Value.MustCountEquals(otherCollection);

        /// <summary>
        /// 集合元素数必须大于指定集合的元素数
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="collection"></param>
        /// <param name="otherCollection"></param>
        /// <returns>两个集合元素数的契约</returns>
        public static Contract<ICollection<T1>> MustCountMoreThan<T1, T2>(this ICollection<T1> collection, ICollection<T2> otherCollection) {
            var isKept = collection.Count > otherCollection.Count;
            if (!isKept) {
                throw new CollectionCountNotMatchException<T1, T2>(collection, otherCollection);
            }
            else {
                return new Contract<ICollection<T1>>(collection, "First Collection Count More");
            }
        }

        /// <summary>
        /// 集合元素数必须大于指定集合的元素数
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="collection"></param>
        /// <param name="otherCollection"></param>
        /// <returns>两个集合元素数的契约</returns>
        public static Contract<ICollection<T1>> MustCountNotMoreThan<T1, T2>(this ICollection<T1> collection, ICollection<T2> otherCollection) {
            var isKept = collection.Count <= otherCollection.Count;
            if (!isKept) {
                throw new CollectionCountNotMatchException<T1, T2>(collection, otherCollection);
            }
            else {
                return new Contract<ICollection<T1>>(collection, "First Collection Count NOT More");
            }
        }

        /// <summary>
        /// 集合元素数必须大于指定集合的元素数
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="contract"></param>
        /// <param name="otherCollection"></param>
        /// <returns>两个集合元素数必须相等的契约</returns>
        public static Contract<ICollection<T1>> AndMustCountMoreThan<T1, T2>(this Contract<ICollection<T1>> contract, ICollection<T2> otherCollection)
            => contract.Value.MustCountMoreThan(otherCollection);

        /// <summary>
        /// 集合元素数必须小于指定集合的元素数
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="collection"></param>
        /// <param name="otherCollection"></param>
        /// <returns>两个集合元素数的契约</returns>
        public static Contract<ICollection<T1>> MustCountLessThan<T1, T2>(this ICollection<T1> collection, ICollection<T2> otherCollection) {
            var isKept = collection.Count < otherCollection.Count;
            if (!isKept) {
                throw new CollectionCountNotMatchException<T1, T2>(collection, otherCollection);
            }
            else {
                return new Contract<ICollection<T1>>(collection, "Two Collection Count NOT Match");
            }
        }

        /// <summary>
        /// 集合元素数必须小于指定集合的元素数
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="contract"></param>
        /// <param name="otherCollection"></param>
        /// <returns>两个集合元素数的契约</returns>
        public static Contract<ICollection<T1>> AndMustCountLessThan<T1, T2>(this Contract<ICollection<T1>> contract, ICollection<T2> otherCollection)
            => contract.Value.MustCountLessThan(otherCollection);
    }
}
