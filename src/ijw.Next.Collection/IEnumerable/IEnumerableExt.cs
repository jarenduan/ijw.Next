using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 提供了IEnumerable的一系列扩展方法
    /// </summary>
    public static class IEnumerableExt {
        #region For Each

        /// <summary>
        /// 在集合上遍历执行指定操作. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action">调用的函数</param>
        /// <returns>集合元素总数</returns>
        public static int ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
            int index = 0;

            foreach (var item in collection) {
                index = checked(index + 1);
                action(item);
            }

            return index;
        }

        /// <summary>
        /// 在集合上遍历执行指定操作, 提供元素和索引同时作为参数, 索引从0开始.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <returns>集合的元素个数</returns>
        public static int ForEach<T>(this IEnumerable<T> collection, Action<T, int> action) {
            int index = 0;

            foreach (var element in collection) {
                action(element, index);
                index = checked(index + 1);
            }

            return index;
        }

        /// <summary>
        /// 在集合上遍历调用某个函数.函数返回值可以控制是否继续迭代.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="doWhile">调用的函数, 返回false则不继续迭代</param>
        public static int ForEachWhile<T>(this IEnumerable<T> collection, Func<T, bool> doWhile) {
            int index = 0;

            foreach (var item in collection) {
                if (!doWhile(item)) {
                    break;
                }
                index = checked(index + 1);
            }

            return index;
        }

        /// <summary>
        /// 在集合上遍历调用某个函数, 提供元素和索引同时作为参数, 索引从0开始. 函数返回值可以控制是否break循环.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="doWhile">返回TRUE继续循环, 返回false则break退出</param>
        /// <returns>执行到元素的索引</returns>
        /// <remarks>
        /// <para>与TakeWhile有一定差别：本函数直接运行，不返回迭代器。</para>
        /// </remarks>
        public static int ForEachWhile<T>(this IEnumerable<T> collection, Func<T, int, bool> doWhile) {
            int index = 0;
            foreach (var element in collection) {
                if (!doWhile(element, index))
                    break;
                index = checked(index + 1);
            }
            return index;
        }

        /// <summary>
        /// 根据指定长度迭代返回每一个窗口。例如{a,b,c,d,e,f}.ForEachWindow(3)将依次返回数组：[a,b,c]、[b,c,d]、[c,d,e]、[d,e,f].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="windowLength">指定长度</param>
        /// <returns>窗口序列</returns>
        public static IEnumerable<T[]> ForEachWindow<T>(this IEnumerable<T> collection, int windowLength) {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext()) yield break;

            //make the 1st window.
            T[] lastWindow = new T[windowLength];
            for (int i = 0; i < windowLength; i++) {
                lastWindow[i] = enumerator.Current;
                if (!enumerator.MoveNext()) yield break;
            }
            yield return lastWindow;

            while (true) {
                T[] currWindow = new T[windowLength];
                for (int i = 0; i < windowLength - 1; i++) {
                    currWindow[i] = lastWindow[i + 1];
                }
                currWindow[windowLength - 1] = enumerator.Current;
                yield return currWindow;
                if (!enumerator.MoveNext()) yield break;
                lastWindow = currWindow;
            }
        }

        #endregion

        #region For Each and the Next

        /// <summary>
        /// 遍历返回每一个元素和下一个元素组成的元组.例如对于集合[a,b,c,d], 则遍历返回(a,b)、(b,c)、(c,d).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns>返回每一个和下一个组成的元组</returns>
        public static IEnumerable<(T curr, T next)> ForEachAndNext<T>(this IEnumerable<T> collection) {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext()) {
                yield break;
            }
            var prev = enumerator.Current;
            while (enumerator.MoveNext()) {
                var curr = enumerator.Current;
                yield return (prev, curr);
                prev = curr;
            }
        }

        /// <summary>
        /// 对集合中的每一个元素和下一个元素执行指定操作.例如对于集合[a,b,c,d]指定action, 则遍历执行action(a,b)、action(b,c)、action(c,d).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action">指定的操作, 接受两个参数</param>
        /// <returns>最后执行处的元素索引.-1表示没有执行.</returns>
        public static int ForEachAndNext<T>(this IEnumerable<T> collection, Action<T, T> action) {
            var enumerator = collection.GetEnumerator();
            int index = -1;
            if (enumerator.MoveNext()) {
                var prev = enumerator.Current;
                while (enumerator.MoveNext()) {
                    index = checked(index + 1);
                    var curr = enumerator.Current;
                    action(prev, curr);
                    prev = curr;
                }
            }
            return index;
        }

        /// <summary>
        /// 对每一个元素和下一个元素以及前者的索引执行指定操作.例如对于集合[a,b,c,d]指定action, 则遍历执行action(a,b,0)、action(b,c,1)、action(c,d,2).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action">指定的操作, 接受两个参数</param>
        /// <returns>最后执行处的元素索引.-1表示没有执行.</returns>
        public static int ForEachAndNext<T>(this IEnumerable<T> collection, Action<T, T, int> action) {
            var enumerator = collection.GetEnumerator();
            int index = -1;
            if (enumerator.MoveNext()) {
                var prev = enumerator.Current;
                while (enumerator.MoveNext()) {
                    index = checked(index + 1);
                    var curr = enumerator.Current;
                    action(prev, curr, index);
                    prev = curr;
                }
            }
            return index;
        }

        /// <summary>
        /// 对每一个元素和下一个元素调用指定函数.例如对于集合[a,b,c,d]指定func, 则遍历调用func(a,b)、func(b,c)、func(c,d).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">指定的函数, 接受两个参数</param>
        /// <returns>计算结果组成的序列</returns>
        public static IEnumerable<TResult> ForEachAndNext<T, TResult>(this IEnumerable<T> collection, Func<T, T, TResult> func) {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext()) {
                yield break;
            }
            var prev = enumerator.Current;
            while (enumerator.MoveNext()) {
                var curr = enumerator.Current;
                yield return func(prev, curr);
                prev = curr;
            }
        }

        /// <summary>
        /// 对每一个元素和下一个元素以及前者的索引调用指定函数.例如对于集合[a,b,c,d]指定func, 则遍历调用func(a,b,0)、func(b,c,1)、func(c,d,2).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">指定的函数, 接受三个参数</param>
        /// <returns>计算结果组成的序列</returns>
        public static IEnumerable<TResult> ForEachAndNext<T, TResult>(this IEnumerable<T> collection, Func<T, T, int, TResult> func) {
            var enumerator = collection.GetEnumerator();
            int index = 0;
            if (!enumerator.MoveNext()) {
                yield break;
            }
            var prev = enumerator.Current;
            while (enumerator.MoveNext()) {
                index = checked(index + 1);
                var curr = enumerator.Current;
                yield return func(prev, curr, index);
                prev = curr;
            }
        }

        /// <summary>
        /// 对每一个元素和下一个元素执行指定操作.操作返回值控制是否继续遍历.例如对于集合[a,b,c,d]指定func, 则遍历调用func(a,b,0)、func(b,c,1)、func(c,d,2).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="doWhile">指定的函数, 接受2个元素类型参数, 返回false则停止迭代</param>
        /// <returns>最后执行处的元素索引(连续两元素的第一个元素的索引), -1表示没有执行.</returns>
        public static int ForEachAndNextWhile<T>(this IEnumerable<T> collection, Func<T, T, bool> doWhile) {
            var enumerator = collection.GetEnumerator();
            int index = -1;
            if (!enumerator.MoveNext()) {
                return index;
            }
            index = checked(index + 1);
            var prev = enumerator.Current;
            while (enumerator.MoveNext()) {
                var curr = enumerator.Current;
                if (!doWhile(prev, curr)) {
                    break;
                }
                index = checked(index + 1);
                prev = curr;
            }
            return index;
        }

        /// <summary>
        /// 对每一个元素和下一个元素以及前者的索引调用指定函数.例如对于集合[a,b,c,d]指定func, 则遍历调用func(a,b,0)、func(b,c,1)、func(c,d,2).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="doWhile">指定的函数, 接受三个参数, 返回false则停止迭代</param>
        /// <returns>最后执行处的元素索引(连续两元素的第一个元素的索引), -1表示没有执行.</returns>
        public static int ForEachAndNextWhile<T>(this IEnumerable<T> collection, Func<T, T, int, bool> doWhile) {
            var enumerator = collection.GetEnumerator();
            int index = -1;
            if (!enumerator.MoveNext()) {
                return index;
            }
            index = checked(index + 1);
            var prev = enumerator.Current;
            while (enumerator.MoveNext()) {
                var curr = enumerator.Current;
                if (!doWhile(prev, curr, index)) {
                    break;
                }
                prev = curr;
                index = checked(index + 1);
            }
            return index;
        }
        #endregion

        #region Item Equals
        /// <summary>
        /// 元素值比较.即依次调用Equals方法比较数组中每个元素是否相等.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns>元素数量相等且每个元素相等, 返回true, 否则返回false.</returns>
        [Obsolete("use SequenceEqual method instead.")]
        public static bool ItemEquals<T>(this IEnumerable<T> source, IEnumerable<T> other) {
            return source.ItemEquals(other, (d1, d2) => d1.Equals(d2));
        }

        /// <summary>
        /// 调用指定函数元素值比较.即依次调用指定接口比较数组中每个元素是否相等.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="comparer">用于比较相等</param>
        /// <returns>元素数量相等且每个元素相等, 返回true, 否则返回false.</returns>
        [Obsolete("use SequenceEqual method instead.")]
        public static bool ItemEquals<T>(this IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer) {
            return source.ItemEquals(other, (x, y) => comparer.Equals(x, y));
        }

        /// <summary>
        /// 调用指定函数元素值比较.即依次调用指定方法比较数组中每个元素是否相等.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="comparer">指定的用于比较相等的方法</param>
        /// <returns>元素数量相等且每个元素相等, 返回true, 否则返回false.</returns>
        [Obsolete("use SequenceEqual method instead.")]
        public static bool ItemEquals<T>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, T, bool> comparer) {
            if (other == null) return false;

            try {
                return (source, other).ForEachPairWhile(dowhile: comparer, forceDimensionMatching: true) > 0;
            }
            catch (CountNotMatchException) {
                return false;
            }
        }

        /// <summary>
        /// 调用指定函数元素值比较.即依次调用指定方法比较数组中每个元素是否相等.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="comparer">指定的用于比较相等的方法</param>
        /// <returns>元素数量相等且每个元素相等, 返回true, 否则返回false.</returns>
        [Obsolete("use SequenceEqual method instead.")]
        public static bool ItemEquals<T>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, T, int, bool> comparer) {
            if (other == null) return false;

            try {
                return (source, other).ForEachPairWhile(dowhile: comparer, forceDimensionMatching: true) > 0;
            }
            catch (CountNotMatchException) {
                return false;
            }
        }

        /// <summary>
        /// 调用指定函数元素值比较.即依次调用指定方法比较数组中每个元素是否相等.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="comparer">指定的用于比较相等的方法</param>
        /// <returns>元素数量相等且每个元素相等, 返回true, 否则返回false.</returns>
        public static bool SequenceEqual<T>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, T, bool> comparer) {
            if (other == null) return false;

            try {
                return (source, other).ForEachPairWhile(dowhile: comparer, forceDimensionMatching: true) > 0;
            }
            catch (CountNotMatchException) {
                return false;
            }
        }

        /// <summary>
        /// 调用指定函数元素值比较.即依次调用指定方法比较数组中每个元素是否相等.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="comparer">指定的用于比较相等的方法</param>
        /// <returns>元素数量相等且每个元素相等, 返回true, 否则返回false.</returns>
        public static bool SequenceEqual<T>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, T, int, bool> comparer) {
            if (other == null) return false;

            try {
                return (source, other).ForEachPairWhile(dowhile: comparer, forceDimensionMatching: true) > 0;
            }
            catch (CountNotMatchException) {
                return false;
            }
        }
        #endregion

        #region Take
        /// <summary>
        /// 给定起止索引, 提取范围内的元素, 包括起止处的元素.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="fromIndex">起始索引</param>
        /// <param name="toIndex">终止索引</param>
        /// <returns></returns>
        public static IEnumerable<T> Take<T>(this IEnumerable<T> collection, int fromIndex, int toIndex) {
            fromIndex.ShouldNotLessThan(0);
            toIndex.ShouldNotLessThan(0);
            fromIndex.ShouldNotLargerThan(toIndex);
            collection.Take(1);
            return collection.Where((ele, index) =>
                index >= fromIndex && index <= toIndex
            );
        }

        /// <summary>
        /// 从0开始反复提取指定数目的元素, 每次增加指定步长.提取元素形成新集合, 内部使用了yield return.
        /// 如果步长和提取量相等, 则元素全部被提取.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="step">步长, 每隔多少个元素进行提取, 1代表相邻的下一个. 例如step设为2, 则每次提取的起始索引是：0, 2, 4...</param>
        /// <param name="takeEachTime">每次提取量, 应小于等于步长</param>
        /// <returns></returns>
        public static IEnumerable<T> TakeEveryOther<T>(this IEnumerable<T> collection, int step, int takeEachTime) {
            step.ShouldLargerThan(0);
            takeEachTime.ShouldLargerThan(0);
            takeEachTime.ShouldNotLargerThan(step);

            return collection.Where((item, index) =>
                index % step < takeEachTime
            );
        }

        /// <summary>
        /// 类python风格的取子集. 如: 对[1,2,3,4,5], <see cref="TakePythonStyle"/>(0, -1)返回[1,2,3,4]; <see cref="TakePythonStyle"/>(-3, -1)返回[3, 4]; <see cref="TakePythonStyle"/>(1,2)返回[2]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="startAt">启始索引. 该处元素将包括在返回结果中. 0 = 第一个元素, -n = 倒数第n个元素, null = 0. 默认值是0</param>
        /// <param name="endAt">结束索引. 该处元素将不包括在返回结果中. 0 = 第一个元素, -n = 倒数第n个元素, null = 结尾. 默认值为null. </param>
        /// <returns>子集</returns>
        public static IEnumerable<T> TakePythonStyle<T>(this IEnumerable<T> collection, int? startAt = 0, int? endAt = null) {
            int count = collection.Count();
            Helper.PythonStartEndCalculator(count, out int startAtPython, out int endAtPython, startAt, endAt);
            if (startAtPython > endAtPython) {
                return new List<T>();
            }
            return collection.Take(startAtPython, endAtPython);
        }
        #endregion

        #region GetCount
        /// <summary>
        /// 获取集合中的元素数量（主要考虑IIndexable）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns>集合中的元素数量</returns>
        internal static int GetCount<T>(this IEnumerable<T> collection) =>
#if !NET35
            collection is IIndexable<T> indexable ? indexable.Count :
#endif    
            collection.Count();
        #endregion

        #region IndexOf
        /// <summary>
        /// 查找指定元素在集合第一次出现位置的索引
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection"></param>
        /// <param name="value">指定元素</param>
        /// <returns>如果集合中不存在, 返回-1;</returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, T value) {
            if (collection is IList<T> list) {
                return list.IndexOf(value);
            }

            if (collection is ICollection<T> ilist) {
                return ilist.IndexOf(value);
            }

            int index = -1;
            collection.ForEachWhile((v, i) => {
                if (v.Equals(value)) {
                    index = i;
                    return false;
                }
                else {
                    return true;
                }
            });
            return index;
        }

        /// <summary>
        /// 在IEnumerable&lt;T&gt;查找第一个符合谓词的元素对象的索引
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">谓词, 为真则立即返回索引</param>
        /// <returns>返回第一个符合谓词的元素的索引, 如果没有符合的将会返回-1</returns>
        /// <remarks>
        /// 方法从后向前遍历集合, 因此时间复杂度是O(index), 即如果目标元素是第一个, 则只需要一次迭代.
        /// 此方法适用于预期元素处于列表中排位靠后的情况. 如果预期元素在较前的位置, 应该使用LastIndexOf&lt;T&gt;扩展方法.
        /// </remarks>
        public static int IndexOf<T>(this IEnumerable<T> collection, Predicate<T> predicate) {
            int index = 0;
            foreach (var item in collection) {
                if (predicate(item)) {
                    return index;
                }
                index = checked(index + 1);
            }
            return -1;
        }

        /// <summary>
        /// 在IEnumerable&lt;T&gt;查找最后一个出现的元素对象索引
        /// </summary>
        public static int LastIndexOf<T>(this IEnumerable<T> collection, T item) {
            if (collection is List<T> list) {
                return list.LastIndexOf(item);
            }
            return collection.Reverse().IndexOf(item);
        }
        #endregion

        #region Elements At

        /// <summary>
        /// 从集合中按指定索引处, 提取相应的元素们形成新的集合. （输出按照元素在集合中的顺序, 而非指定索引的顺序）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="indexes">指定的索引, 一组整数</param>
        /// <returns></returns>
        public static IEnumerable<T> ElementsAt<T>(this IEnumerable<T> collection, IEnumerable<int> indexes) {
            int i = 0;
            foreach (var e in collection) {
                if (indexes.Contains(i)) {
                    yield return e;
                }
                i++;
            }
        }

        /// <summary>
        /// 提取集合中指定索引处的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="index">指定的索引</param>
        /// <returns></returns>
        internal static T GetElementsAt<T>(this IEnumerable<T> collection, int index) =>
#if !NET35
            collection is IIndexable<T> indexable ? indexable[index] :
#endif    
            collection.ElementAt(index);

        #endregion

        #region Each (with index as a return)
        /// <summary>
        /// 返回一个由元素及其相应索引组成的元组集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<(T item, int index)> EachWithIndex<T>(this IEnumerable<T> collection) {
            int index = 0;
            foreach (var element in collection) {
                yield return (element, index);
                index = checked(index + 1);
            }
        }
        #endregion

        #region Divide

        /// <summary>
        /// 按指定的比例把集合分拆成两部分
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="ratioOfFirstGroup">第一部分的占比</param>
        /// <param name="ratioOfSecondGroup">第二部分的占比</param>
        /// <param name="firstGroup">分拆出的第一部分</param>
        /// <param name="secondGroup">分拆出的第二部分</param>
        /// <remarks>net40+请使用返回元组的版本,  out return 版本不推荐使用</remarks>
        public static void DivideByRatio<T>(this IEnumerable<T> source, int ratioOfFirstGroup, int ratioOfSecondGroup, out List<T> firstGroup, out List<T> secondGroup) {
            var first = new List<T>();
            var second = new List<T>();

            source.ForEach((element, index) => {
                if (index % (ratioOfFirstGroup + ratioOfSecondGroup) < ratioOfFirstGroup) {
                    first.Add(element);
                }
                else {
                    second.Add(element);
                }
            });

            firstGroup = first;
            secondGroup = second;
        }

        /// <summary>
        /// 把一个集合按指定的比率和方式分成两部分
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="method">切分方式</param>
        /// <param name="ratioOfFirstGroup">第一部分的比例</param>
        /// <param name="ratioOfSecondGroup">第二把部分的比例</param>
        /// <param name="firstGroup">切分后的第一部分</param>
        /// <param name="secondGroup">切分后的第二部分</param>
        /// <remarks>net40+请使用返回元组的版本,  out return 版本不推荐使用</remarks>
        public static void DivideByRatioAndMethod<T>(this IEnumerable<T> source, int ratioOfFirstGroup, int ratioOfSecondGroup, CollectionDividingMethod method, out List<T> firstGroup, out List<T> secondGroup) {
            if (method == CollectionDividingMethod.Random) {
                source = source.Shuffle();
            }
            source.DivideByRatio(ratioOfFirstGroup, ratioOfSecondGroup, out firstGroup, out secondGroup);
        }

#if !NET35
                /// <summary>
                /// 按指定的比例把集合分拆成两部分
                /// </summary>
                /// <typeparam name="T">元素类型</typeparam>
                /// <param name="source">源集合</param>
                /// <param name="ratioOfFirstGroup">第一部分的占比</param>
                /// <param name="ratioOfSecondGroup">第二部分的占比</param>
                /// <returns>元组（分拆出的第一部分, 分拆出的第二部分）</returns>
                /// <remarks>使用返回元组的版本,  out return 版本不推荐使用</remarks>
                public static (List<T> firstGroup, List<T> secondGroup) DivideByRatio<T>(this IEnumerable<T> source, int ratioOfFirstGroup, int ratioOfSecondGroup) {
                    source.DivideByRatio(ratioOfFirstGroup, ratioOfSecondGroup, out var firstGrouop, out var secondGroup);
                    return (firstGrouop, secondGroup);
                }

                /// <summary>
                /// 把一个集合按指定的比率和方式分成两部分
                /// </summary>
                /// <typeparam name="T">元素类型</typeparam>
                /// <param name="source">源集合</param>
                /// <param name="method">切分方式</param>
                /// <param name="ratioOfFirstGroup">第一部分的比例</param>
                /// <param name="ratioOfSecondGroup">第二把部分的比例</param>
                /// <returns>元组（切分后的第一部分, 切分后的第二部分）</returns>
                /// <remarks>使用返回元组的版本,  out return 版本不推荐使用</remarks>
                public static (List<T> firstGroup, List<T> secondGroup) DivideByRatioAndMethod<T>(this IEnumerable<T> source, CollectionDividingMethod method, int ratioOfFirstGroup, int ratioOfSecondGroup) {
                    source.DivideByRatioAndMethod(ratioOfFirstGroup, ratioOfSecondGroup, method, out var firstGrouop, out var secondGroup);
                    return (firstGrouop, secondGroup);
                }
#endif

        #endregion

        #region Random

        /// <summary>
        /// 返回随机打乱顺序的序列. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns>随机打乱顺序后的序列</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection) {
            int[] order = 0.ToTotal(collection.Count()).ToArray().Shuffle();
            for (int i = 0; i < order.Length; i++) {
                yield return collection.ElementAt(order[i]);
            }
        }
        #endregion

        #region First (with index as parameter)

        /// <summary>
        /// 返回第一个满足条件的元素, 过滤条件使用元素和索引作为参数.无则抛出<see cref="System.InvalidOperationException"/>异常.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="pred"></param>
        /// <returns></returns>
        public static T First<T>(this IEnumerable<T> collection, Func<T, int, bool> pred)
            => collection.Where((item, index) => pred(item, index)).First();

        /// <summary>
        /// 返回第一个满足条件的元素, 过滤条件使用元素和索引作为参数.无则返回null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="pred">过滤条件, 为真则返回.</param>
        /// <returns></returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Func<T, int, bool> pred)
            => collection.Where((item, index) => pred(item, index)).FirstOrDefault();

        #endregion

        #region Max/Min (with Index as a parameters)
        /// <summary>
        /// 用元素和索引作为参数, 使用指定的方法比较, 找到最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">比较的方法, 接受元素及其索引作为参数.</param>
        /// <returns>最大值及其索引</returns>
        public static (int index, V maxValue) Max<T, V>(this IEnumerable<T> collection, Func<T, int, V> func) where V : IComparable {
            collection.ShouldNotBeEmpty();

            V maxValue = func(collection.First(), 0);
            int maxIndex = 0;
            int index = 0;

            foreach (var item in collection) {
                var v = func(item, index);
                if (v.CompareTo(maxValue) > 0) {
                    maxValue = v;
                    maxIndex = index;
                }
                index = checked(index + 1);
            }

            return (maxIndex, maxValue);
        }

        /// <summary>
        /// 用元素和索引作为参数, 使用指定的方法比较, 找到最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">比较的方法, 接受元素及其索引作为参数.</param>
        /// <returns>最小值及其索引</returns>
        public static (int index, V minValue) Min<T, V>(this IEnumerable<T> collection, Func<T, int, V> func) where V : IComparable {
            collection.ShouldNotBeEmpty();

            V minValue = func(collection.First(), 0);
            int minIndex = 0;
            int index = 0;

            foreach (var item in collection) {
                var v = func(item, index);
                if (v.CompareTo(minValue) < 0) {
                    minValue = v;
                    minIndex = index;
                }
                index = checked(index + 1);
            }

            return (minIndex, minValue);
        }
        #endregion

        #region Sum and Count
        /// <summary>
        /// 用元素作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, double sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, double> func) {
            collection.ShouldNotBeEmpty();

            var result = 0d;
            int index = 0;

            foreach (var item in collection) {
                result += func(item);
                index = checked(index + 1);
            }

            return (index, result);
        }

        /// <summary>
        /// 用元素作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, decimal sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, decimal> func) {
            collection.ShouldNotBeEmpty();

            var result = 0m;
            int index = 0;

            foreach (var item in collection) {
                result += func(item);
                index = checked(index + 1);
            }

            return (index, result);
        }

        /// <summary>
        /// 用元素作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, float sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, float> func) {
            collection.ShouldNotBeEmpty();

            var result = 0f;
            int index = 0;

            foreach (var item in collection) {
                result += func(item);
                index = checked(index + 1);
            }

            return (index, result);
        }
        /// <summary>
        /// 用元素作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, int sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, int> func) {
            collection.ShouldNotBeEmpty();

            var result = 0;
            int index = 0;

            foreach (var item in collection) {
                result += func(item);
                index = checked(index + 1);
            }

            return (index, result);
        }
        /// <summary>
        /// 用元素作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, long sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, long> func) {
            collection.ShouldNotBeEmpty();

            var result = 0L;
            int index = 0;

            foreach (var item in collection) {
                result += func(item);
                index = checked(index + 1);
            }

            return (index, result);
        }
        #endregion

        #region Sum and Count (with index as parameters)
        /// <summary>
        /// 用元素和索引作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, double sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, int, double> func) {
            collection.ShouldNotBeEmpty();

            var result = 0d;
            int index = 0;

            foreach (var item in collection) {
                result += func(item, index);
                index = checked(index + 1);
            }

            return (index, result);
        }

        /// <summary>
        /// 用元素和索引作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, float sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, int, float> func) {
            collection.ShouldNotBeEmpty();

            var result = 0f;
            int index = 0;

            foreach (var item in collection) {
                result += func(item, index);
                index = checked(index + 1);
            }

            return (index, result);
        }

        /// <summary>
        /// 用元素和索引作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, int sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, int, int> func) {
            collection.ShouldNotBeEmpty();

            var result = 0;
            int index = 0;

            foreach (var item in collection) {
                result += func(item, index);
                index = checked(index + 1);
            }

            return (index, result);
        }

        /// <summary>
        /// 用元素和索引作为参数, 使用指定的方法计算后, 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func">计算的方法, 接受元素及其索引作为参数.</param>
        /// <returns>所求到和与进行求和的元素总数</returns>
        public static (int count, long sum) SumAndCount<T>(this IEnumerable<T> collection, Func<T, int, long> func) {
            collection.ShouldNotBeEmpty();

            var result = 0L;
            int index = 0;

            foreach (var item in collection) {
                result += func(item, index);
                index = checked(index + 1);
            }

            return (index, result);
        }
        #endregion

        #region ToStrings
        /// <summary>
        /// 输出形如[a1, a2 ... an]的带省略号的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="maxDisplayNumber">字符串中最多显示几个元素</param>
        /// <param name="postfix">前缀字符串, 显示在第一个元素前面的字符串</param>
        /// <param name="prefix">后缀字符串, 显示在最后一个元素后面的字符串</param>
        /// <param name="seperator">每个元素之间的分割字符串</param>
        /// <returns></returns>
        public static string ToSimpleEnumStrings<T>(this IEnumerable<T> collection, int maxDisplayNumber = 3, string prefix = "[", string postfix = "]", string seperator = ", ") {
            if (maxDisplayNumber <= 0)
                maxDisplayNumber = 3;
            int count = collection.Count();
            if (count <= maxDisplayNumber) {
                return collection.ToAllEnumStrings();
            }
            else {
                StringBuilder sb = new StringBuilder(prefix);

                void appendSimpleStringIfPossible(T item) {
                    if (item is IEnumerable<T> ienum) {
                        sb.Append(ienum.ToSimpleEnumStrings());
                    }
                    else {
                        sb.Append(item.ToString());
                    }
                    sb.Append(seperator);
                }

                foreach (var item in collection.Where((item, index) => index <= maxDisplayNumber - 2)) {
                    appendSimpleStringIfPossible(item);
                }

                //collection.ForEachWithIndexAndBreak((item, index) => {
                //    if(index <= maxDisplayNumber - 2) {
                //        appendSimpleStringIfPossible<T>(sb, item);
                //        return true;
                //    }
                //    else {
                //        return false;
                //    }
                //});

                sb.Append("...").Append(seperator);
                appendSimpleStringIfPossible(collection.Last());
                sb.Append(postfix);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 输出包含所有元素的字符串, 默认形如[a1, a2, a3, [a41, a42, a43], a5]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="separator">元素之间的分隔符, 默认是", "</param>
        /// <param name="prefix">第一个元素之前的字符串, 默认是"["</param>
        /// <param name="postfix">最后一个元素之后的字符串, 默认是"]"</param>
        /// <param name="transform">对于每个元素, 输出字符串之前进行一个操作.默认为null, 代表调用ToString().</param>
        /// <returns></returns>
        public static string ToAllEnumStrings<T>(this IEnumerable<T> collection, string separator = ", ", string prefix = "[", string postfix = "]", Func<T, string> transform = null) {
            return Helper.ToAllEnumStrings(collection, separator, prefix, postfix, transform);
        }

        #endregion

        #region Filters
        /// <summary>
        /// 基于移动窗口进行计算处理。每个当前值在当前窗口的：1）中间（窗口长度奇数时）2）或者在窗口中间 + 1处（窗口长度偶数时）
        /// 序列首尾（不在窗口中的）值将不进行任何计算，直接输出.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="func">一个计算函数，输入参数是当前窗口，输出是计算后的值</param>
        /// <returns>过滤后的序列</returns>
        public static IEnumerable<T> WindowBasedFilter<T>(this IEnumerable<T> values, int windowLength, Func<IEnumerable<T>, T> func) {
            return WindowBasedFilter(values, windowLength, (v) => v, func);
        }

        /// <summary>
        /// 基于移动窗口进行计算处理。每个当前值在当前窗口的：1）中间（窗口长度奇数时）2）或者在窗口中间 + 1处（窗口长度偶数时）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="values"></param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="funcWithoutWindow">一个计算函数，输入参数是无法在窗口中计算的值，输出是计算后的值</param>
        /// <param name="funcWithWindow">一个计算函数，输入参数是当前窗口，输出是计算后的值</param>
        /// <returns>过滤后的序列</returns>
        public static IEnumerable<TResult> WindowBasedFilter<T, TResult>(this IEnumerable<T> values, int windowLength, Func<T,TResult> funcWithoutWindow, Func<IEnumerable<T>, TResult> funcWithWindow) {
            windowLength.ShouldBeNotLessThanZero();
            windowLength.ShouldNotLargerThan(values.Count());

            var enumerator = values.GetEnumerator();

            //The values in the first half of the 1st window could not be filtered, so yield return directly.
            for (int i = 0; i < windowLength / 2; i++) {
                if (!enumerator.MoveNext()) {
                    yield break;
                }
                else {
                    yield return funcWithoutWindow(enumerator.Current);
                }
            }

            //use window to filter.
            var windows = values.ForEachWindow(windowLength);
            foreach (var w in windows) {
                yield return funcWithWindow(w);
                if (!enumerator.MoveNext()) {
                    yield break;
                }
            }

            //no more window to filter with, yield return rest directly.
            for (int i = 0; i < windowLength / 2; i++) {
                if (!enumerator.MoveNext()) {
                    yield break;
                }
                else {
                    yield return funcWithoutWindow(enumerator.Current);
                }
            }
        }

        /// <summary>
        /// 去除序列中的Null
        /// </summary>
        /// <param name="values"></param>
        /// <returns>去除后的序列</returns>
        public static IEnumerable<T> NullFilter<T>(this IEnumerable<T?> values) where T : struct {
            var query = from v in values
                        where v != null
                        select v.Value;
            return query;
        }

        /// <summary>
        /// 这个函数签名有点复杂，使用起来学习成本太高。暂时不进行public，仅供内部使用吧
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="diff"></param>
        /// <param name="diffLimitingFunc"></param>
        /// <returns></returns>
        internal static IEnumerable<T> PreviousBasedFilter<T>(this IEnumerable<T> values, T diff, Func<T, T, T, T> diffLimitingFunc) {
            values.ShouldNotBeNullOrEmpty();

            yield return values.First();

            var result = values.ForEachAndNext((prev, curr) => diffLimitingFunc(curr, prev, diff));

            foreach (var item in result) {
                yield return item;
            }
        }
        #endregion
    }
}