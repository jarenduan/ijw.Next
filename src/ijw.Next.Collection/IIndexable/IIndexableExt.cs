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
        /// ���Ʋ����Լ��Ͻ��й���. ��ǰһ������+�������ȴ���. 
        /// </summary>
        /// <param name="values">ԭ����</param>
        /// <param name="diff">������������</param>
        /// <return>���˺���¼���</return>
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
        /// �޷�����. �����������������ֵ, ��ǰһ����ֵ����. 
        /// </summary>
        /// <param name="values">ԭ����</param>
        /// <param name="diff">�������ֵ����ֵ</param>
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
        /// ��λֵ����. ���ڳ�����ȡ��λֵ
        /// </summary>
        /// <param name="values">ԭ����</param>
        /// <param name="windowLength">���ڳ���</param>
        /// <returns>�µ�������</returns>
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
        /// ����ƽ��ֵ����. ���ڳ�����ȡƽ��ֵ
        /// </summary>
        /// <param name="values">ԭ����</param>
        /// <param name="windowLength">���ڳ���</param>
        /// <returns>�µ�������</returns>
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
        /// ���һ��ö����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexable"></param>
        /// <returns></returns>
        public static IEnumerator<T> GetEnumeratorForIIndexable<T>(this IIndexable<T> indexable) {
            return new IIndexableEnumerator<T>(indexable);
        }

        /// <summary>
        /// ���������ϵ�ö����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class IIndexableEnumerator<T> : IEnumerator, IEnumerator<T> {
            private int _curr = -1;
            private readonly IIndexable<T> _indexable;

            /// <summary>
            /// ���캯��
            /// </summary>
            /// <param name="indexable"></param>
            public IIndexableEnumerator(IIndexable<T> indexable) {
                this._indexable = indexable;
            }

            /// <summary>
            /// ��ǰԪ��
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
            /// ������Դ
            /// </summary>
            public void Dispose() {

            }

            /// <summary>
            /// ����
            /// </summary>
            /// <returns></returns>
            public bool MoveNext() {
                this._curr++;
                return (this._curr < this._indexable.Count);
            }

            /// <summary>
            /// ��λ
            /// </summary>
            public void Reset() {
                _curr = -1;
            }
        }
    }
}
#endif