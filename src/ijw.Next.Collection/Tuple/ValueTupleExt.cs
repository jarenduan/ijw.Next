#pragma warning disable CS0612 // IEnumerableHelper类型或成员已过时
using System;
using System.Collections.Generic;

namespace ijw.Next.Collection {
    /// <summary>
    /// 值元组的扩展方法
    /// </summary>
    public static class ValueTupleExt {
        #region Tuple of IEnumerables
        #region For each pair
        /// <summary>
        /// 对元组中两个集合的每一对元素执行指定的计算, 返回迭代器.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <typeparam name="TResult">函数计算返回值得类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="func">欲执行的计算. 接受两个集合的元素对作为参数. </param>
        /// <returns>计算结果形成的集合（仅为迭代器, 即并未即时进行计算. ）</returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">第1个集合的元素数大于第2个集合的元素数时会抛出 CountNotMatchException 异常.</exception>
        public static IEnumerable<TResult> ForEachPairSelect<T1, T2, TResult>(this (IEnumerable<T1>, IEnumerable<T2>) tuple, Func<T1, T2, TResult> func) {
            return IEnumerableHelper.ForEachPairSelect(tuple.Item1, tuple.Item2, (i1, i2) => func(i1, i2));
        }

        /// <summary>
        /// 对元组中两个集合的每一对元素执行指定的计算, 返回迭代器. 传递对应索引作为参数之一. 
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <typeparam name="TResult">函数计算返回值得类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="func">欲执行的计算, 接受两个集合的元素对以及当前索引作为参数. </param>
        /// <returns>计算结果形成的集合（仅为迭代器, 即并未即时进行计算. ）</returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">第1个集合的元素数大于第2个集合的元素数时会抛出 CountNotMatchException 异常.</exception>
        public static IEnumerable<TResult> ForEachPairSelect<T1, T2, TResult>(this (IEnumerable<T1>, IEnumerable<T2>) tuple, Func<T1, T2, int, TResult> func) {
            return IEnumerableHelper.ForEachPairSelect(tuple.Item1, tuple.Item2, (i1, i2, i) => func(i1, i2, i));
        }

        /// <summary>
        /// 对元组中两个集合的每一对元素执行指定的操作
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="action">欲执行的操作, 接受两个集合的元素对作为参数. </param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">第1个集合的元素数大于第2个集合的元素数时会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(this (IEnumerable<T1>, IEnumerable<T2>) tuple, Action<T1, T2> action) {
            IEnumerableHelper.ForEachPair(tuple.Item1, tuple.Item2, (i1, i2) => action(i1, i2));
        }

        /// <summary>
        /// 对元组中两个集合的每一对元素执行指定的操作, 传递对应索引作为参数之一. 
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="action">欲执行的操作, 接受两个集合的元素对以及当前索引作为参数. </param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">第1个集合的元素数大于第2个集合的元素数时会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(this (IEnumerable<T1>, IEnumerable<T2>) tuple, Action<T1, T2, int> action) {
            IEnumerableHelper.ForEachPair(tuple.Item1, tuple.Item2, (i1, i2, i) => action(i1, i2, i));
        }



        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行操作.根据操作返回的值决定是否提前结束迭代.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="dowhile">需要对每对元素执行的操作, 接受两个集合的元素作为参数. </param>
        /// <param name="forceDimensionMatching">为true, 会在操作后检查两个集合元素数是否不相等. 为false, 不检查.</param>
        /// <returns>执行了操作的最后一对元素的索引. 注意如果迭代是在到达第1个集合结尾之前就break出来的话, 此项为相应的负值. 因此可通过返回值的正负来判断是否进行完整的迭代. </returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">当1)集合2的元素数量小于集合1的元素数量, 或2)<paramref name="forceDimensionMatching"/>为true, 且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>

        public static int ForEachPairWhile<T1, T2>(this (IEnumerable<T1>, IEnumerable<T2>) tuple, Func<T1, T2, bool> dowhile, bool forceDimensionMatching = false) {
            return IEnumerableHelper.ForEachPairWhile(tuple.Item1, tuple.Item2, dowhile, forceDimensionMatching);
        }

        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行操作.根据操作返回的值决定是否提前结束迭代.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="dowhile">需要对每对元素执行的操作, 接受两个集合的元素对以及当前索引作为参数. </param>
        /// <param name="forceDimensionMatching">为true, 会在操作后检查两个集合元素数是否不相等. 为false, 不检查.</param>
        /// <returns>执行了操作的最后一对元素的索引. 注意如果迭代是在到达第1个集合结尾之前就break出来的话, 此项为相应的负值. 因此可通过返回值的正负来判断是否进行完整的迭代. </returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">当1)集合2的元素数量小于集合1的元素数量, 或2)<paramref name="forceDimensionMatching"/>为true, 且两个集合元素数不相等时, 会抛出 CountNotMatchException 异常.</exception>

        public static int ForEachPairWhile<T1, T2>(this (IEnumerable<T1>, IEnumerable<T2>) tuple, Func<T1, T2, int, bool> dowhile, bool forceDimensionMatching = false) {
            return IEnumerableHelper.ForEachPairWhile(tuple.Item1, tuple.Item2, dowhile, forceDimensionMatching);
        }
        #endregion

        #region For each two in between
        /// <summary>
        /// 对元组中两个集合的每一对组合对执行指定操作. 例如对于{1,2}和{a,b,c}, 将依次针对(1,a), (1,b), (1,c), (2,a), (2,b), (2,c)六种组合执行操作.
        /// <para>
        /// 可以理解为嵌套for循环.
        /// </para>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="tuple"></param>
        /// <param name="action">想要执行的操作. 接受两个参数, 第一个参数来自第一个集合, 第二个参数来自第二个集合.</param>
        public static void ForEachTwoInBetween<T1, T2>(this (IEnumerable<T1>, IEnumerable<T2>) tuple, Action<T1, T2> action) {
            IEnumerableHelper.ForEachTwoInBetween(tuple.Item1, tuple.Item2, action);
        }

        /// <summary>
        /// 对元组中两个集合的每一对组合对执行指定查询. 例如对于{1,2}和{a,b,c}, 将依次针对(1,a), (1,b), (1,c), (2,a), (2,b), (2,c)六种组合执行操作.
        /// <para>
        /// 可以理解为嵌套for循环.
        /// </para>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="tuple"></param>
        /// <param name="func">想要执行的计算. 接受两个参数, 第一个参数来自第一个集合, 第二个参数来自第二个集合.</param>
        /// <returns>每一个组合的计算结果组成的序列迭代器.</returns>
        public static IEnumerable<TResult> ForEachTwoInBetweenSelect<T1, T2, TResult>(this (IEnumerable<T1>, IEnumerable<T2>) tuple, Func<T1, T2, TResult> func) {
            return IEnumerableHelper.ForEachTwoInBetween(tuple.Item1, tuple.Item2, func);
        }
        #endregion

        #region For Each Three
        /// <summary>
        /// 对三个集合进行同步迭代, 对每一组（三个）元素进行指定操作.
        /// </summary>
        /// <typeparam name="T1">第一个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第二个个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第三个个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="doWork">执行的操作</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">集合1元素数大于集合2或集合3的元素数时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachThree<T1, T2, T3>(this (IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3) tuple, Action<T1, T2, T3> doWork) {
            tuple.collection1.ShouldBeNotNullArgument(nameof(tuple.collection1));
            tuple.collection2.ShouldBeNotNullArgument(nameof(tuple.collection2));
            tuple.collection3.ShouldBeNotNullArgument(nameof(tuple.collection3));

            IEnumerator<T2> iter2 = tuple.collection2.GetEnumerator();
            IEnumerator<T3> iter3 = tuple.collection3.GetEnumerator();

            foreach (var e1 in tuple.collection1) {
                if (!iter2.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       tuple.collection1,
                       tuple.collection2,
                       tuple.collection3
                   );
                if (!iter3.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       tuple.collection1,
                       tuple.collection2,
                       tuple.collection3
                   );
                doWork(e1, iter2.Current, iter3.Current);
            }
        }

        /// <summary>
        /// 对三个集合进行同步迭代, 对每一组（三个）元素以及索引进行指定操作.
        /// </summary>
        /// <typeparam name="T1">第一个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第二个个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第三个个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="doWork">执行的操作</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">集合1元素数大于集合2或集合3的元素数时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachThree<T1, T2, T3>(this (IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3) tuple, Action<T1, T2, T3, int> doWork) {
            tuple.collection1.ShouldBeNotNullArgument(nameof(tuple.collection1));
            tuple.collection2.ShouldBeNotNullArgument(nameof(tuple.collection2));
            tuple.collection3.ShouldBeNotNullArgument(nameof(tuple.collection3));

            IEnumerator<T2> iter2 = tuple.collection2.GetEnumerator();
            IEnumerator<T3> iter3 = tuple.collection3.GetEnumerator();
            int index = 0;
            foreach (var e1 in tuple.collection1) {
                if (!iter2.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       tuple.collection1,
                       tuple.collection2,
                       tuple.collection3
                   );
                if (!iter3.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                       tuple.collection1,
                       tuple.collection2,
                       tuple.collection3
                   );
                doWork(e1, iter2.Current, iter3.Current, index);
                index = checked(index + 1);
            }
        }

        /// <summary>
        /// 对三个集合进行同步迭代, 对每一组（三个）元素进行指定函数计算
        /// </summary>
        /// <typeparam name="T1">第一个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第二个个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第三个个集合里面元素的类型</typeparam>
        /// <typeparam name="TResult">函数计算返回值得类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="func">需要计算的函数</param>
        /// <returns>返回的结果迭代器</returns>
        /// <exception cref="TwoIEnumerableCountNotMatchException">集合1元素数大于集合2或集合3的元素数时, 会抛出 CountNotMatchException 异常.</exception>
        /// <remarks>
        /// 本函数只返回一个迭代器.计算将延迟在对结果的迭代访问时进行.
        /// </remarks>
        public static IEnumerable<TResult> ForEachThreeSelect<T1, T2, T3, TResult>(this (IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3) tuple, Func<T1, T2, T3, TResult> func) {
            tuple.collection1.ShouldBeNotNullArgument(nameof(tuple.collection1));
            tuple.collection2.ShouldBeNotNullArgument(nameof(tuple.collection2));
            tuple.collection3.ShouldBeNotNullArgument(nameof(tuple.collection3));

            IEnumerator<T2> iter2 = tuple.collection2.GetEnumerator();
            IEnumerator<T3> iter3 = tuple.collection3.GetEnumerator();

            foreach (var e1 in tuple.collection1) {
                if (!iter2.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                        tuple.collection1,
                        tuple.collection2, 
                        tuple.collection3
                    );
                if (!iter3.MoveNext())
                    throw new ThreeIEnumerableCountNotMatchException(
                        tuple.collection1,
                        tuple.collection2, 
                        tuple.collection3
                    );
                yield return func(e1, iter2.Current, iter3.Current);
            }
        }
        #endregion
        #endregion

        #region Tuple of ILists
        /// <summary>
        /// 对元组中两个集合的每一对元素执行指定的操作
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="action">欲执行的操作, 接受两个集合的元素引用作为参数. </param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">第1个集合的元素数大于第2个集合的元素数时会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(this (IList<T1>, IList<T2>) tuple, ActionWithRef<T1, T2> action) {
            IListHelper.ForEachPair(tuple.Item1, tuple.Item2, (ref T1 i1, ref T2 i2) => action(ref i1, ref i2));
        }

        /// <summary>
        /// 对元组中两个集合的每一对元素执行指定的操作, 传递对应索引作为参数之一. 
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="action">欲执行的操作, 接受两个集合的元素引用以及当前索引作为参数. </param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">第1个集合的元素数大于第2个集合的元素数时会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(this (IList<T1>, IList<T2>) tuple, ActionWithRef<T1, T2, int> action) {
            IListHelper.ForEachPair(tuple.Item1, tuple.Item2, (ref T1 i1, ref T2 i2, ref int i) => action(ref i1, ref i2, ref i));
        }

        /// <summary>
        /// 对三个集合进行同步迭代, 对每一组（三个）元素进行指定操作.
        /// </summary>
        /// <typeparam name="T1">第一个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第二个个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第三个个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="doWork">执行的操作</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">集合1元素数大于集合2或集合3的元素数时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachThree<T1, T2, T3>(this (IList<T1> collection1, IList<T2> collection2, IList<T3> collection3) tuple, ActionWithRef<T1, T2, T3> doWork) {
            IListHelper.ForEachThree(tuple.collection1, tuple.collection2, tuple.collection3, (ref T1 i1, ref T2 i2, ref T3 i3) => {
                doWork(ref i1, ref i2, ref i3);
            });
        }

        /// <summary>
        /// 对三个集合进行同步迭代, 对每一组（三个）元素以及索引进行指定操作.
        /// </summary>
        /// <typeparam name="T1">第一个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第二个个集合里面元素的类型</typeparam>
        /// <typeparam name="T3">第三个个集合里面元素的类型</typeparam>
        /// <param name="tuple"></param>
        /// <param name="doWork">执行的操作</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">集合1元素数大于集合2或集合3的元素数时, 会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachThree<T1, T2, T3>(this (IList<T1> collection1, IList<T2> collection2, IList<T3> collection3) tuple, ActionWithRef<T1, T2, T3, int> doWork) {
            IListHelper.ForEachThree(tuple.collection1, tuple.collection2, tuple.collection3, (ref T1 i1, ref T2 i2, ref T3 i3, ref int index) => {
                doWork(ref i1, ref i2, ref i3, ref index);
            });
        }
        #endregion
    }
}
#pragma warning restore CS0612 // 类型或成员已过时