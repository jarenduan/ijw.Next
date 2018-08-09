using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection
{
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableNumberFilterExt {
        /// <summary>
        /// 限制波动对集合进行过滤. 用前一个样本+波动幅度代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新集合</return>
        public static IEnumerable<double> LimitingDiffFilter(this IEnumerable<double> values, double diff) {
            diff.ShouldLargerThan(0);
            values.ShouldNotBeNullOrEmpty();

            yield return values.First();
            
            var result = values.ForEachAndNext((prev, curr) => curr.LimitingDiff(prev, diff));

            foreach (var item in result) {
                yield return item;
            }
        }

        /// <summary>
        /// 限制波动对集合进行过滤. 用前一个样本+波动幅度代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新集合</return>
        public static IEnumerable<float> LimitingDiffFilter(this IEnumerable<float> values, float diff) {
            diff.ShouldLargerThan(0);
            values.ShouldNotBeNullOrEmpty();

            yield return values.First();

            var result = values.ForEachAndNext((prev, curr) => curr.LimitingDiff(prev, diff));

            foreach (var item in result) {
                yield return item;
            }
        }

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns>过滤后的新集合</returns>
        public static IEnumerable<double> LimitingAmplifyFilter(this IEnumerable<double> values, double diff) {
            diff.ShouldLargerThan(0);
            values.ShouldNotBeNullOrEmpty();

            yield return values.First();

            var result = values.ForEachAndNext((prev, curr) => curr.LimitingAmplify(prev, diff));

            foreach (var item in result) {
                yield return item;
            }
        }

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns>过滤后的新集合</returns>
        public static IEnumerable<float> LimitingAmplifyFilter(this IEnumerable<float> values, float diff) {
            diff.ShouldLargerThan(0);
            values.ShouldNotBeNullOrEmpty();

            yield return values.First();

            var result = values.ForEachAndNext((prev, curr) => curr.LimitingAmplify(prev, diff));

            foreach (var item in result) {
                yield return item;
            }
        }

        /// <summary>
        /// 在指定窗口长度内进行中位值滤波。例如{1,2,8,5}.MedianFilter(3), 返回:{1,2,5,5}.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<double> MedianFilter(this IEnumerable<double> values, int windowLength) {
            return values.filterWithWindow(windowLength, w => w.GetMedianValue());
        }

        /// <summary>
        /// 在指定窗口长度内进行中位值滤波。例如{1,2,8,5}.MedianFilter(3), 返回:{1,2,5,5}.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<float> MedianFilter(this IEnumerable<float> values, int windowLength) {
            return values.filterWithWindow(windowLength, w => w.GetMedianValue());
        }

        /// <summary>
        /// 算术平均值过滤. 窗口长度内取平均值.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="ignoreMaxMinValue">计算均值时是否忽略最大值最小值</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<double> MeanFilter(this IEnumerable<double> values, int windowLength, bool ignoreMaxMinValue = false) {
            return values.filterWithWindow(windowLength, w => w.Average(ignoreMaxMinValue));
        }

        /// <summary>
        /// 算术平均值过滤. 窗口长度内取平均值.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="ignoreMaxMinValue">计算均值时是否忽略最大值最小值</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<float> MeanFilter(this IEnumerable<float> values, int windowLength, bool ignoreMaxMinValue = false) {
            return values.filterWithWindow(windowLength, w => w.Average(ignoreMaxMinValue));
        }

        private static IEnumerable<T> filterWithWindow<T>(this IEnumerable<T> values, int windowLength, Func<IEnumerable<T>, T> func) { 
            windowLength.ShouldBeNotLessThanZero();
            windowLength.ShouldNotLargerThan(values.Count());

            var enumerator = values.GetEnumerator();

            //The values in the first half of the 1st window could not be filtered, so yield return directly.
            for (int i = 0; i < windowLength / 2; i++) {
                if (!enumerator.MoveNext()) {
                    yield break;
                }
                else {
                    yield return enumerator.Current;
                }
            }

            //use window to filter.
            var windows = values.ForEachWindow(windowLength);
            foreach (var w in windows) {
                yield return func(w);
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
                    yield return enumerator.Current;
                }
            }
        }
    }
}