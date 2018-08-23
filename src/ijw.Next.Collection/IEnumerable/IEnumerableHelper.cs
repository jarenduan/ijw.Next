using System;
using System.Collections.Generic;

namespace ijw.Next.Collection {
    /// <summary>
    /// 集合操作的帮助类
    /// </summary>
    [Obsolete]
    public static class IEnumerableHelper {
        #region For Each Pair
        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行操作.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="action">需要对每对元素执行的操作，接受两个集合的元素对作为参数。</param>
        /// <param name="forceDimensionMatching">为true，会在操作后检查两个集合元素数是否不相等. 为false，不检查.</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">当1)集合2的元素数量小于集合1的元素数量，或2)<paramref name="forceDimensionMatching"/>为true，且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2> action, bool forceDimensionMatching = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            IEnumerator<T2> iter = collection2.GetEnumerator();
            foreach (var e1 in collection1) {
                if (!iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
                action(e1, iter.Current);
            }

            if (forceDimensionMatching) {
                //collection2 还有元素的话, 就是异常了
                if (iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
            }
        }
        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行操作. 当前索引将作为参数之一。
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="action">需要对每对元素执行的操作，接受两个集合的元素对以及当前索引作为参数。</param>
        /// <param name="forceDimensionMatching">为true，会在操作后检查两个集合元素数是否不相等. 为false，不检查.</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">当1)集合2的元素数量小于集合1的元素数量，或2)<paramref name="forceDimensionMatching"/>为true，且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2, int> action, bool forceDimensionMatching = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            IEnumerator<T2> iter = collection2.GetEnumerator();
            int index = 0;
            foreach (var e1 in collection1) {
                if (!iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1,collection2);
                action(e1, iter.Current, index);
                index++;
            }

            if (forceDimensionMatching) {
                //collection2 还有元素的话, 就是异常了
                if (iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
            }
        }
        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行指定函数计算（延迟）, 返回迭代器.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <typeparam name="TResult">函数计算返回值得类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="func">执行计算的函数，接受两个集合的元素对作为参数。</param>
        /// <param name="forceDimensionMatching">为true，会在操作后检查两个集合元素数是否不相等. 为false，不检查.</param>
        /// <returns>返回的结果迭代器</returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">当1)集合2的元素数量小于集合1的元素数量，或2)<paramref name="forceDimensionMatching"/>为true，且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        /// <remarks>本函数返回时指定函数计算并没有进行, 本函数只返回一个迭代器.计算将延迟在对结果的迭代访问时进行.</remarks>
        public static IEnumerable<TResult> ForEachPairSelect<T1, T2, TResult>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Func<T1, T2, TResult> func, bool forceDimensionMatching = false) {
            //don't try to test the count(), cos' it might cause iterations.
            //if (collection1.Count() != collection2.Count()) {
            //	throw new CountNotMatchException();
            //}

            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            IEnumerator<T2> iter = collection2.GetEnumerator();
            foreach (var e1 in collection1) {
                if (!iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2); //test during iterations.
                yield return func(e1, iter.Current);
            }
            if (forceDimensionMatching) {
                //collection2 还有元素的话, 就是异常了
                if (iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
            }
        }

        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行指定函数计算（延迟）, 返回迭代器.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <typeparam name="TResult">函数计算返回值得类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="func">执行计算的函数，接受两个集合的元素对以及当前索引作为参数。</param>
        /// <param name="forceDimensionMatching">为true，会在操作后检查两个集合元素数是否不相等. 为false，不检查.</param>
        /// <returns>返回的结果迭代器</returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">第1个集合的元素数大于第2个集合的元素数时会抛出 CountNotMatchException 异常.</exception>
        /// <remarks>本函数返回时指定函数计算并没有进行, 本函数只返回一个迭代器.计算将延迟在对结果的迭代访问时进行.</remarks>
        public static IEnumerable<TResult> ForEachPairSelect<T1, T2, TResult>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Func<T1, T2, int, TResult> func, bool forceDimensionMatching = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            IEnumerator<T2> iter = collection2.GetEnumerator();
            int index = 0;
            foreach (var e1 in collection1) {
                if (!iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1,  collection2);
                yield return func(e1, iter.Current, index);
                index++;
            }
            if (forceDimensionMatching) {
                //collection2 还有元素的话, 就是异常了
                if (iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
            }
        }

        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行指定的操作. 根据操作返回的值决定是否提前结束迭代.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="dowhile">需要对每对元素执行的操作，接受两个集合的元素对作为参数。</param>
        /// <param name="forceDimensionMatching">为true，会在操作后检查两个集合元素数是否不相等. 为false，不检查.</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">当1)集合2的元素数量小于集合1的元素数量，或2)<paramref name="forceDimensionMatching"/>为true，且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        /// <returns>迭代的次数.注意如果迭代是在到达第1个集合结尾之前就break出来的话，此项为相应的负值。因此可通过返回值的正负来判断是否进行完整的迭代。</returns>
        public static int ForEachPairWhile<T1, T2>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Func<T1, T2, bool> dowhile, bool forceDimensionMatching = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            int index = 0;
            IEnumerator<T2> iter = collection2.GetEnumerator();
            foreach (var e1 in collection1) {
                if (!iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
                if (!dowhile(e1, iter.Current)) {
                    index = -index;
                    break;
                }
                index++;
            }

            if (forceDimensionMatching) {
                //collection2 还有元素的话, 就是异常了
                if (iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
            }

            return index;
        }

        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行操作.根据操作返回的值决定是否提前结束迭代.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="dowhile">需要对每对元素执行的操作，接受两个集合的元素对以及当前索引作为参数。</param>
        /// <param name="forceDimensionMatching">为true，会在操作后检查两个集合元素数是否不相等. 为false，不检查.</param>
        /// <returns>迭代的次数. 注意如果迭代是在到达第1个集合结尾之前就break出来的话，此项为相应的负值。因此可通过返回值的正负来判断是否进行完整的迭代。</returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">当1)集合2的元素数量小于集合1的元素数量，或2)<paramref name="forceDimensionMatching"/>为true，且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>
        public static int ForEachPairWhile<T1, T2>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Func<T1, T2, int, bool> dowhile, bool forceDimensionMatching = false) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));

            int index = 0;
            IEnumerator<T2> iter = collection2.GetEnumerator();
            foreach (var e1 in collection1) {
                if (!iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
                if (!dowhile(e1, iter.Current, index)) {
                    index = -index;
                    break;
                }
                index++;
            }

            if (forceDimensionMatching) {
                //collection2 还有元素的话, 就是异常了
                if (iter.MoveNext()) throw new TwoIEnumerableCountNotMatchException(collection1, collection2);
            }

            return index;
        }
        #endregion

        #region For each two
        /// <summary>
        /// 针对每个来自两个集合的元素对执行指定操作. 例如对于{1,2}和{a,b,c}, 将依次针对(1,a), (1,b), (1,c), (2,a), (2,b), (2,c)六种组合执行操作.
        /// <para>
        /// 可以理解为嵌套for循环.
        /// </para>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="collection1">第一个集合</param>
        /// <param name="collection2">第二个集合</param>
        /// <param name="action">想要执行的操作. 接受两个参数, 第一个参数来自第一个集合, 第二个参数来自第二个集合.</param>
        public static void ForEachTwoInBetween<T1, T2>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2> action) {
            collection1.ShouldBeNotNullArgument();
            collection2.ShouldBeNotNullArgument();

            foreach (var i in collection1) {
                foreach (var j in collection2) {
                    action(i, j);
                }
            }
        }

        /// <summary>
        /// 对元组中两个集合的每一对组合对执行指定查询. 例如对于{1,2}和{a,b,c}, 将依次针对(1,a), (1,b), (1,c), (2,a), (2,b), (2,c)六种组合执行操作.
        /// <para>
        /// 可以理解为嵌套for循环.
        /// </para>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult">函数计算返回值得类型</typeparam>
        /// <param name="collection1">第一个集合</param>
        /// <param name="collection2">第二个集合</param>
        /// <param name="func">想要执行的计算. 接受两个参数, 第一个参数来自第一个集合, 第二个参数来自第二个集合.</param>
        /// <returns>每一个组合的计算结果组成的序列迭代器.</returns>
        public static IEnumerable<TResult> ForEachTwoInBetween<T1, T2, TResult>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Func<T1, T2, TResult> func) {
            collection1.ShouldBeNotNullArgument();
            collection2.ShouldBeNotNullArgument();

            foreach (var i in collection1) {
                foreach (var j in collection2) {
                    yield return func(i, j);
                }
            }
        }
        #endregion

        #region For Each Three

        /// <summary>
        /// 对三个集合进行同步迭代, 对每一组（三个）元素进行指定操作.
        /// </summary>
        /// <typeparam name="T1">第一个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第二个个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第三个个集合里面元素的类型</typeparam>
        /// <param name="collection1">第一个集合</param>
        /// <param name="collection2">另二个集合</param>
        /// <param name="collection3">第三个集合</param>
        /// <param name="doWork">执行的操作</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">集合1元素数大于集合2或集合3的元素数时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachThree<T1, T2, T3>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, Action<T1, T2, T3> doWork) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));
            collection3.ShouldBeNotNullArgument(nameof(collection3));

            IEnumerator<T2> iter2 = collection2.GetEnumerator();
            IEnumerator<T3> iter3 = collection3.GetEnumerator();

            foreach (var e1 in collection1) {
                if (!iter2.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       collection1,
                       collection2,
                       collection3
                   );
                if (!iter3.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       collection1,
                       collection2,
                       collection3
                   );
                doWork(e1, iter2.Current, iter3.Current);
            }
        }

        /// <summary>
        /// 对三个集合进行同步迭代, 对每一组（三个）元素及其索引进行指定操作.
        /// </summary>
        /// <typeparam name="T1">第一个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第二个个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第三个个集合里面元素的类型</typeparam>
        /// <param name="collection1">第一个集合</param>
        /// <param name="collection2">另二个集合</param>
        /// <param name="collection3">第三个集合</param>
        /// <param name="doWork">执行的操作</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">集合1元素数大于集合2或集合3的元素数时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachThree<T1, T2, T3>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, Action<T1, T2, T3, int> doWork) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));
            collection3.ShouldBeNotNullArgument(nameof(collection3));

            IEnumerator<T2> iter2 = collection2.GetEnumerator();
            IEnumerator<T3> iter3 = collection3.GetEnumerator();
            int index = 0;
            foreach (var e1 in collection1) {
                if (!iter2.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       collection1,
                       collection2,
                       collection3
                   );
                if (!iter3.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       collection1,
                       collection2,
                       collection3
                   );
                doWork(e1, iter2.Current, iter3.Current, index);
                index++;
            }
        }

        /// <summary>
        /// 对三个集合进行同步迭代, 对每一组（三个）元素进行指定函数计算
        /// </summary>
        /// <typeparam name="T1">第一个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第二个个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第三个个集合里面元素的类型</typeparam>
        /// <typeparam name="TResult">函数计算返回值得类型</typeparam>
        /// <param name="collection1">第一个集合</param>
        /// <param name="collection2">另二个集合</param>
        /// <param name="collection3">第三个集合</param>
        /// <param name="theFunction">需要计算的函数</param>
        /// <returns>返回的结果迭代器</returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">集合1元素数大于集合2或集合3的元素数时, 会抛出 CountNotMatchException 异常.</exception>
        /// <remarks>
        /// 本函数只返回一个迭代器.计算将延迟在对结果的迭代访问时进行.
        /// </remarks>
        public static IEnumerable<TResult> ForEachThreeSelect<T1, T2, T3, TResult>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, Func<T1, T2, T3, TResult> theFunction) {
            collection1.ShouldBeNotNullArgument(nameof(collection1));
            collection2.ShouldBeNotNullArgument(nameof(collection2));
            collection3.ShouldBeNotNullArgument(nameof(collection3));

            IEnumerator<T2> iter2 = collection2.GetEnumerator();
            IEnumerator<T3> iter3 = collection3.GetEnumerator();

            foreach (var e1 in collection1) {
                if (!iter2.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       collection1,
                       collection2,
                       collection3
                   );
                if (!iter3.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       collection1,
                       collection2,
                       collection3
                   );
                yield return theFunction(e1, iter2.Current, iter3.Current);
            }
        }
        #endregion
    }
}