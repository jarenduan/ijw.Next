#if !NET35
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    public static class IIndexableExt {
        #region Filters

        /// <summary>
        /// 限制波动对集合进行过滤. 用前一个样本+波动幅度代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新集合</return>
        public static double[] FilterWithDiffLimitation(this IIndexable<double> values, double diff) {
            diff.ShouldLargerThan(0);

            double[] result = new double[values.Count];

            result[0] = values[0];
            for (int i = 1; i < values.Count; i++) {
                var curr = values[i];
                var prev = values[i - 1];
                result[i] = curr.LimitingDiff(prev, diff);
            }

            return result;
        }

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns></returns>
        public static double[] FilterWithAmplifyLimitation(this IIndexable<double> values, double diff) {
            diff.ShouldLargerThan(0);

            double[] result = new double[values.Count];
            for (int i = 1; i < values.Count; i++) {
                var curr = values[i];
                var prev = values[i - 1];
                result[i] = curr.LimitingAmplify(prev, diff);
            }
            return result;
        }

        /// <summary>
        /// 中位值过滤. 窗口长度内取中位值
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static double[] FilterWithWindowMedian(this IIndexable<double> values, int windowLength) {
            windowLength.ShouldLargerThan(0);
            windowLength.ShouldBeOdd();
            windowLength.ShouldNotLargerThan(values.Count());

            var result = new double[values.Count];
            int half = windowLength / 2;
            for (int i = half; i < values.Count - half; i++) {
                double[] window = values.TakePythonStyle(i - half, i + half + 1).OrderBy((e) => e).ToArray();
                result[i] = window[half + 1];
            }
            return result;
        }

        /// <summary>
        /// 算术平均值过滤. 窗口长度内取平均值
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static double[] FilterWithWindowMean(this IIndexable<double> values, int windowLength) {
            windowLength.ShouldBeNotLessThanZero();
            windowLength.ShouldNotLargerThan(values.Count());

            var result = new double[values.Count];
            int half = windowLength / 2;
            for (int i = half; i < values.Count - half; i++) {
                result[i] = values.TakePythonStyle(i - half, i + half + 1)
                          .ToArray()
                          .Sum((e) => e) / windowLength;
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 获得一个枚举器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexable"></param>
        /// <returns></returns>
        public static IEnumerator<T> GetEnumeratorForIIndexable<T>(this IIndexable<T> indexable) {
            return new IIndexableEnumerator<T>(indexable);
        }

        /// <summary>
        /// 可索引集合的枚举器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class IIndexableEnumerator<T> : IEnumerator, IEnumerator<T> {
            private int _curr = -1;
            private readonly IIndexable<T> _indexable;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="indexable"></param>
            public IIndexableEnumerator(IIndexable<T> indexable) {
                this._indexable = indexable;
            }

            /// <summary>
            /// 当前元素
            /// </summary>
            public T Current {
                get {
                    return this._indexable[_curr];
                }
            }

            object IEnumerator.Current {
                get {
                    return this.Current;
                }
            }

            /// <summary>
            /// 清理资源
            /// </summary>
            public void Dispose() {

            }

            /// <summary>
            /// 迭代
            /// </summary>
            /// <returns></returns>
            public bool MoveNext() {
                this._curr++;
                return (this._curr < this._indexable.Count);
            }

            /// <summary>
            /// 复位
            /// </summary>
            public void Reset() {
                _curr = -1;
            }
        }
    }
}
#endif