using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    internal static class IListHelper {
        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素的引用进行操作.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="action">需要对每对元素执行的操作, 接受两个集合的元素引用作为参数. </param>
        /// <param name="forceDimensionMatching">如果为true, 会在操作后检查两个集合元素数是否不相等. 为false, 不检查.</param>
        /// <param name="ifCheckDimensionFirst">如果为true, 会在迭代之前检查集合元素数量是否匹配. 可与<paramref name="forceDimensionMatching"/>配合使用</param>
        /// <exception cref="CollectionCountNotMatchException{T1, T2}">当1)集合2的元素数量小于集合1的元素数量, 或2)<paramref name="forceDimensionMatching"/>为true, 且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(IList<T1> collection1, IList<T2> collection2, ActionWithRef<T1, T2> action, bool forceDimensionMatching = false, bool ifCheckDimensionFirst = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            if (ifCheckDimensionFirst) {
                checkDimension(collection1,collection2,forceDimensionMatching);
            }

            for (int i = 0; i < collection1.Count; i++) {
                if (i >= collection2.Count) {
                    throw new CollectionCountNotMatchException<T1, T2>(collection1, collection2);
                }

                var item1 = collection1[i];
                var item2 = collection2[i];

                action(ref item1, ref item2);

                collection1[i] = item1;
                collection2[i] = item2;
            }

            checkDimension(collection1, collection2, forceDimensionMatching);
        }

        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素的引用以及当前索引进行操作.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="action">需要对每对元素执行的操作, 接受两个集合的元素引用以及当前索引作为参数. </param>
        /// <param name="forceDimensionMatching">为true, 会在操作后检查两个集合元素数是否不相等. 为false, 不检查.</param>
        /// <param name="ifCheckDimensionFirst">如果为true, 会在迭代之前检查集合元素数量是否匹配. 可与<paramref name="forceDimensionMatching"/>配合使用</param>
        /// <exception cref="CollectionCountNotMatchException{T1, T2}">当1)集合2的元素数量小于集合1的元素数量, 或2)<paramref name="forceDimensionMatching"/>为true, 且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(IList<T1> collection1, IList<T2> collection2, ActionWithRef<T1, T2, int> action, bool forceDimensionMatching = false, bool ifCheckDimensionFirst = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            if (ifCheckDimensionFirst) {
                checkDimension(collection1, collection2, forceDimensionMatching);
            }

            for (int i = 0; i < collection1.Count; i++) {
                if (i >= collection2.Count) {
                    throw new CollectionCountNotMatchException<T1, T2>(collection1, collection2);
                }

                var item1 = collection1[i];
                var item2 = collection2[i];

                action(ref item1, ref item2, ref i);

                collection1[i] = item1;
                collection2[i] = item2;
            }

            checkDimension(collection1, collection2, forceDimensionMatching);
        }

        private static void checkDimension<T1, T2>(IList<T1> collection1, IList<T2> collection2, bool forceDimensionMatching) {
            if (forceDimensionMatching) {
                collection1.MustCountEquals(collection2);
            }
            else {
                collection1.MustCountNotMoreThan(collection2);
            }
        }

        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素的引用进行操作.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第3个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="collection3">第3个集合</param>
        /// <param name="action">需要对每对元素执行的操作, 接受两个集合的元素引用作为参数. </param>
        /// <param name="forceDimensionMatching">为true, 会在操作后检查两个集合元素数是否不相等. 为false, 不检查.</param>
        /// <param name="ifCheckDimensionFirst">如果为true, 会在迭代之前检查集合元素数量是否匹配. 可与<paramref name="forceDimensionMatching"/>配合使用</param>
        /// <exception cref="CollectionCountNotMatchException{T1, T2}">当1)集合2的元素数量小于集合1的元素数量, 或2)<paramref name="forceDimensionMatching"/>为true, 且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachThree<T1, T2, T3>(IList<T1> collection1, IList<T2> collection2, IList<T3> collection3, ActionWithRef<T1, T2, T3> action, bool forceDimensionMatching = false, bool ifCheckDimensionFirst = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            if (ifCheckDimensionFirst) {
                checkDimensionForThreeCollections(collection1, collection2, collection3, forceDimensionMatching);
            }

            for (int i = 0; i < collection1.Count; i++) {
                if (i >= collection2.Count || i > collection3.Count) {
                    throw new CollectionCountNotMatchException<T1, T2, T3>(collection1, collection2, collection3);
                }

                var item1 = collection1[i];
                var item2 = collection2[i];
                var item3 = collection3[i];

                action(ref item1, ref item2, ref item3);

                collection1[i] = item1;
                collection2[i] = item2;
                collection3[i] = item3;
            }

            checkDimensionForThreeCollections(collection1, collection2, collection3, forceDimensionMatching);
        }
        
        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素的引用以及当前索引进行操作.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第3个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="collection3">第3个集合</param>
        /// <param name="action">需要对每对元素执行的操作, 接受两个集合的元素引用以及当前索引作为参数. </param>
        /// <param name="forceDimensionMatching">为true, 会在操作后检查两个集合元素数是否不相等. 为false, 不检查.</param>
        /// <param name="ifCheckDimensionFirst">如果为true, 会在迭代之前检查集合元素数量是否匹配. 可与<paramref name="forceDimensionMatching"/>配合使用</param>
        /// <exception cref="CollectionCountNotMatchException{T1, T2, T3}">当1)集合2的元素数量小于集合1的元素数量, 或2)<paramref name="forceDimensionMatching"/>为true, 且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachThree<T1, T2, T3>(IList<T1> collection1, IList<T2> collection2, IList<T3> collection3, ActionWithRef<T1, T2, T3, int> action, bool forceDimensionMatching = false, bool ifCheckDimensionFirst = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            if (ifCheckDimensionFirst) {
                checkDimensionForThreeCollections(collection1, collection2, collection3, forceDimensionMatching);
            }

            for (int i = 0; i < collection1.Count; i++) {
                if (i >= collection2.Count || i > collection3.Count) {
                    throw new CollectionCountNotMatchException<T1, T2, T3>(collection1, collection2, collection3);
                }

                var item1 = collection1[i];
                var item2 = collection2[i];
                var item3 = collection3[i];

                action(ref item1, ref item2, ref item3, ref i);

                collection1[i] = item1;
                collection2[i] = item2;
                collection3[i] = item3;
            }

            checkDimensionForThreeCollections(collection1, collection2, collection3, forceDimensionMatching);
        }

        private static void checkDimensionForThreeCollections<T1, T2, T3>(IList<T1> collection1, IList<T2> collection2, IList<T3> collection3, bool forceDimensionMatching) {
            if (forceDimensionMatching) {
                if (collection1.Count != collection2.Count || collection1.Count != collection3.Count) {
                    throw new CollectionCountNotMatchException<T1, T2, T3>(collection1, collection2, collection3);
                }
            } else {
                if (collection1.Count > collection2.Count || collection1.Count > collection3.Count) {
                    throw new CollectionCountNotMatchException<T1, T2, T3>(collection1, collection2, collection3);
                }
            }
        }
    }
}
