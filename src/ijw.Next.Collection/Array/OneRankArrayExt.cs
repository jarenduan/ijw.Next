using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next.Collection {
    /// <summary>
    /// 提供对一维数组的扩展方法
    /// </summary>
    public static class OneRankArrayExt {
        /// <summary>
        /// 使用一个初始化函数进行数组的初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="initializer"></param>
        public static void Initialize<T>(this T[] array, Func<int, T> initializer) {
            for (int i = 0; i < array.Length; i++) {
                array[i] = initializer(i);
            }
        }

        /// <summary>
        /// 获得强类型的枚举器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static IEnumerator<T> GetEnumeratorGenerics<T>(this T[] array) {
            return array.AsEnumerable().GetEnumerator();
        }

        /// <summary>
        /// 以从前向后的顺序, 移除数组中所有具有指定值的元素, 结果将保存在新的数组中返回.
        /// 数组维数不变, 其中可能出现的剩余空间将设为类型的默认值.
        /// 如: 对整形数组{3, 1, 1, 0, 4, 1, 2, 2, 0} 调用 RemoveAll(1) 将得到新数组: {3, 0, 4, 2, 2, 0, 0, 0, 0).
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="source"></param>
        /// <param name="toRemove">指定的值</param>
        /// <returns></returns>
        public static T[] RemoveAll<T>(this T[] source, T toRemove) {
            int len = source.Length;
            T[] result = new T[len];
            int index = 0;
            for (int i = 0; i < len; i++) {
                var curr = source[i];
                if (curr is null && toRemove is null || !(curr is null) && curr.Equals(toRemove))
                    continue;
                result[index] = source[i];
                index = checked(index + 1);
            }
            return result;
        }

        /// <summary>
        /// 以从前向后的顺序, 移除数组中所有具有指定值的元素, 结果将保存在新的数组中返回. 新数组维数会发生变化.
        /// 如: 对整形数组{3, 1, 1, 0, 4, 1, 2, 2, 0} 调用 RemoveAll(1) 将得到新数组: {3, 0, 4, 2, 2).
        /// 内部使用了Linq实现.
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="source"></param>
        /// <param name="toRemove">指定的值</param>
        /// <returns></returns>
        public static T[] ShrinkByRemoving<T>(this T[] source, T toRemove) {
            var r = from item in source
                    where !item.Equals(toRemove)
                    select item;
            return r.ToArray();
        }

        /// <summary>
        /// 对数组中的元素进行替换. 返回新数组.
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="source"></param>
        /// <param name="replace">要替换的值</param>
        /// <param name="with">替换成的值</param>
        /// <returns>新数组</returns>
        public static T[] ReplaceAll<T>(this T[] source, T replace, T with = default) {
            int len = source.Length;
            T[] result = new T[len];
            for (int i = 0; i < len; i++) {
                var item = source[i];
                if ((item is null && replace is null) || (item != null && item.Equals(replace)))
                    result[i] = with;
                else
                    result[i] = source[i];
            }
            return result;
        }

        #region Set Values
        /// <summary>
        /// 根据指定的一系列索引, 设置数组中的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index">指定的索引, 一组整数</param>
        /// <param name="values">指定的值</param>
        public static void SetValuesForTheIndexes<T>(this T[] array, IEnumerable<int> index, IEnumerable<T> values) {
            if (index.Count() != values.Count())
                throw new TwoIEnumerableCountNotMatchException(index, values);
            Dictionary<int, T> dict = new Dictionary<int, T>();
            (index, values).ForEachPair((i, v) => 
                    dict.Add(i, v)
            );
            SetValuesForTheIndexes(array, dict);
        }

        /// <summary>
        /// 为数组设置指定索引处的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="values"></param>
        public static void SetValuesForTheIndexes<T>(this T[] array, Dictionary<int, T> values) {
            for (int i = 0; i < array.Length; i++) {
                if (values.TryGetValue(i, out var value)) {
                    array[i] = value;
                }
            }
        }
        #endregion

        /// <summary>
        /// 随机打乱数组中元素的排列顺序, 返回新数组.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this T[] numbers) {
            int count = numbers.Length;
            if (count == 0) {
                return numbers;
            }
            Random r = new Random();
            int i = r.Next(count / 2, count);
            i.Times(() => {
                int i1 = r.Next(count);
                int i2 = r.Next(count);
                var temp = numbers[i1];
                numbers[i1] = numbers[i2];
                numbers[i2] = temp;
            });
            return numbers;
        }
    }
}