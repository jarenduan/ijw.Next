using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableNumberExt {
        #region Statistic

        #region Average
        /// <summary>
        /// 计算均值，可指定是否忽略最大值最小值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ignoreMaxMinValue">是否忽略最大值最小值</param>
        /// <returns>均值</returns>
        /// 
        public static double Average(this IEnumerable<double> values, bool ignoreMaxMinValue) {
            if (!ignoreMaxMinValue) {
                return values.Average();
            }
            else {
                double max = double.NegativeInfinity, min = double.PositiveInfinity, sum = 0d;
                int count = 0;
                foreach (var v in values) {
                    max = Math.Max(max, v);
                    min = Math.Min(min, v);
                    sum += v;
                    count = checked(count + 1);
                }
                sum = sum - max - min;
                return sum / (count - 2);
            }
        }

        /// <summary>
        /// 计算均值，可指定是否忽略最大值最小值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ignoreMaxMinValue">是否忽略最大值最小值</param>
        /// <returns>均值</returns>
        public static decimal Average(this IEnumerable<decimal> values, bool ignoreMaxMinValue) {
            if (!ignoreMaxMinValue) {
                return values.Average();
            }
            else {
                decimal max = values.First(), min = values.First(), sum = 0m;
                int count = 0;
                foreach (var v in values) {
                    max = Math.Max(max, v);
                    min = Math.Min(min, v);
                    sum += v;
                    count = checked(count + 1);
                }
                sum = sum - max - min;
                return sum / (count - 2);
            }
        }

        /// <summary>
        /// 计算均值，可指定是否忽略最大值最小值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ignoreMaxMinValue">是否忽略最大值最小值</param>
        /// <returns>均值</returns>
        public static float Average(this IEnumerable<float> values, bool ignoreMaxMinValue) {
            if (!ignoreMaxMinValue) {
                return values.Average();
            }
            else {
                float max = float.NegativeInfinity, min = float.PositiveInfinity, sum = 0f;
                int count = 0;
                foreach (var v in values) {
                    max = Math.Max(max, v);
                    min = Math.Min(min, v);
                    sum += v;
                    count = checked(count + 1);
                }
                sum = sum - max - min;
                return sum / (count - 2);
            }
        }

        /// <summary>
        /// 计算均值，可指定是否忽略最大值最小值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ignoreMaxMinValue">是否忽略最大值最小值</param>
        /// <returns>均值</returns>
        /// 
        public static double Average(this IEnumerable<long> values, bool ignoreMaxMinValue) {
            if (!ignoreMaxMinValue) {
                return values.Average();
            }
            else {
                double max = double.NegativeInfinity, min = double.PositiveInfinity, sum = 0d;
                int count = 0;
                foreach (var v in values) {
                    max = Math.Max(max, v);
                    min = Math.Min(min, v);
                    sum += v;
                    count = checked(count + 1);
                }
                sum = sum - max - min;
                return sum / (count - 2);
            }
        }

        /// <summary>
        /// 计算均值，可指定是否忽略最大值最小值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ignoreMaxMinValue">是否忽略最大值最小值</param>
        /// <returns>均值</returns>
        /// 
        public static double Average(this IEnumerable<int> values, bool ignoreMaxMinValue) {
            if (!ignoreMaxMinValue) {
                return values.Average();
            }
            else {
                double max = double.NegativeInfinity, min = double.PositiveInfinity, sum = 0d;
                int count = 0;
                foreach (var v in values) {
                    max = Math.Max(max, v);
                    min = Math.Min(min, v);
                    sum += v;
                    count = checked(count + 1);
                }
                sum = sum - max - min;
                return sum / (count - 2);
            }
        }
        #endregion

        #region Variance
        /// <summary>
        /// 获得方差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double Variance(this IEnumerable<double> values, bool isAllData = false) {
            var mean = values.Average();
            var (count, sum) = values.SumAndCount(v => Math.Sqrt(v - mean)); //get count and sum within only one iteration.
            count = isAllData ? count : count - 1;
            return sum / count;
        }

        /// <summary>
        /// 获得方差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double Variance(this IEnumerable<decimal> values, bool isAllData = false) {
            var mean = values.Average();
            var (count, sum) = values.SumAndCount(v => Math.Sqrt(Convert.ToDouble(v - mean))); //get count and sum within only one iteration.
            count = isAllData ? count : count - 1;
            return sum / count;
        }

        /// <summary>
        /// 获得方差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double Variance(this IEnumerable<float> values, bool isAllData = false) {
            var mean = values.Average();
            var (count, sum) = values.SumAndCount(v => Math.Sqrt(v - mean)); //get count and sum within only one iteration.
            count = isAllData ? count : count - 1;
            return sum / count;
        }

        /// <summary>
        /// 获得方差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double Variance(this IEnumerable<long> values, bool isAllData = false) {
            var mean = values.Average();
            var (count, sum) = values.SumAndCount(v => Math.Sqrt(v - mean)); //get count and sum within only one iteration.
            count = isAllData ? count : count - 1;
            return sum / count;
        }

        /// <summary>
        /// 获得方差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double Variance(this IEnumerable<int> values, bool isAllData = false) {
            var mean = values.Average();
            var (count, sum) = values.SumAndCount(v => Math.Sqrt(v - mean)); //get count and sum within only one iteration.
            count = isAllData ? count : count - 1;
            return sum / count;
        }
        #endregion

        #region StandardVariance
        /// <summary>
        /// 获得标准差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double StandardVariance(this IEnumerable<double> values, bool isAllData = false)
           => Math.Sqrt(values.Variance(isAllData));

        /// <summary>
        /// 获得标准差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double StandardVariance(this IEnumerable<decimal> values, bool isAllData = false)
           => Math.Sqrt(values.Variance(isAllData));

        /// <summary>
        /// 获得标准差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double StandardVariance(this IEnumerable<float> values, bool isAllData = false)
           => Math.Sqrt(values.Variance(isAllData));

        /// <summary>
        /// 获得标准差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double StandardVariance(this IEnumerable<long> values, bool isAllData = false)
           => Math.Sqrt(values.Variance(isAllData));

        /// <summary>
        /// 获得标准差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double StandardVariance(this IEnumerable<int> values, bool isAllData = false)
           => Math.Sqrt(values.Variance(isAllData));
        #endregion

        #region MedianValue
        /// <summary>
        /// 获取集合的中位值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>奇数个元素，返回中间元素的值，偶数个元素返回中间两元素的平均值。</returns>
        public static double Median(this IEnumerable<double> values) {
            int count = values.GetCount();

            count.ShouldBeNotZero();

            double midValue;
            var ordered = values.OrderBy(d => d).ToArray();

            if (count.IsOdd()) {
                midValue = ordered[(count - 1) / 2];
            }
            else {
                int afterMidIndex = count / 2;
                var itemBeforeMid = ordered[afterMidIndex - 1];
                var itemAfterMid = ordered[afterMidIndex];
                midValue = (itemBeforeMid + itemAfterMid) / 2;
            }

            return midValue;
        }

        /// <summary>
        /// 获取集合的中位值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>奇数个元素，返回中间元素的值，偶数个元素返回中间两元素的平均值。</returns>
        public static decimal Median(this IEnumerable<decimal> values) {
            int count = values.GetCount();

            count.ShouldBeNotZero();

            decimal midValue;
            var ordered = values.OrderBy(d => d).ToArray();

            if (count.IsOdd()) {
                midValue = ordered[(count - 1) / 2];
            }
            else {
                int afterMidIndex = count / 2;
                var itemBeforeMid = ordered[afterMidIndex - 1];
                var itemAfterMid = ordered[afterMidIndex];
                midValue = (itemBeforeMid + itemAfterMid) / 2;
            }

            return midValue;
        }

        /// <summary>
        /// 获取集合的中位值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>奇数个元素，返回中间元素的值，偶数个元素返回中间两元素的平均值。</returns>
        public static float Median(this IEnumerable<float> values) {
            int count = values.GetCount();

            count.ShouldBeNotZero();

            float midValue;
            var ordered = values.OrderBy(d => d).ToArray();

            if (count.IsOdd()) {
                midValue = ordered[(count - 1) / 2];
            }
            else {
                int afterMidIndex = count / 2;
                var itemBeforeMid = ordered[afterMidIndex - 1];
                var itemAfterMid = ordered[afterMidIndex];
                midValue = (itemBeforeMid + itemAfterMid) / 2;
            }

            return midValue;
        }

        /// <summary>
        /// 获取集合的中位值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>奇数个元素，返回中间元素的值，偶数个元素返回中间两元素的平均值。</returns>
        public static double Median(this IEnumerable<long> values) {
            int count = values.GetCount();

            count.ShouldBeNotZero();

            double midValue;
            var ordered = values.OrderBy(d => d).ToArray();

            if (count.IsOdd()) {
                midValue = ordered[(count - 1) / 2];
            }
            else {
                int afterMidIndex = count / 2;
                var itemBeforeMid = ordered[afterMidIndex - 1];
                var itemAfterMid = ordered[afterMidIndex];
                midValue = (itemBeforeMid + itemAfterMid) / 2;
            }

            return midValue;
        }

        /// <summary>
        /// 获取集合的中位值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>奇数个元素，返回中间元素的值，偶数个元素返回中间两元素的平均值。</returns>
        public static double Median(this IEnumerable<int> values) {
            int count = values.GetCount();

            count.ShouldBeNotZero();

            double midValue;
            var ordered = values.OrderBy(d => d).ToArray();

            if (count.IsOdd()) {
                midValue = ordered[(count - 1) / 2];
            }
            else {
                int afterMidIndex = count / 2;
                var itemBeforeMid = ordered[afterMidIndex - 1];
                var itemAfterMid = ordered[afterMidIndex];
                midValue = (itemBeforeMid + itemAfterMid) / 2;
            }

            return midValue;
        }
        #endregion

        #region MiddleValue
        /// <summary>
        /// 获取集合的中值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>返回最大最小的平均值。</returns>
        public static double Middle(this IEnumerable<double> values) => (values.Max() + values.Min()) / 2;

        /// <summary>
        /// 获取集合的中值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>返回最大最小的平均值。</returns>
        public static decimal Middle(this IEnumerable<decimal> values) => (values.Max() + values.Min()) / 2;

        /// <summary>
        /// 获取集合的中值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>返回最大最小的平均值。</returns>
        public static float Middle(this IEnumerable<float> values) => (values.Max() + values.Min()) / 2;

        /// <summary>
        /// 获取集合的中值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>返回最大最小的平均值。</returns>
        public static double Middle(this IEnumerable<long> values) => (values.Max() + values.Min()) / 2;

        /// <summary>
        /// 获取集合的中值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>返回最大最小的平均值。</returns>
        public static double Middle(this IEnumerable<int> values) => (values.Max() + values.Min()) / 2;
        #endregion

        #endregion

        #region Normalize

        #region Normalize
        /// <summary>
        /// 对浮点集合中的值逐一进行归一化
        /// </summary>
        /// <param name="enumerable">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>归一化后的集合</returns>
        public static List<double> Normalize(this IEnumerable<double> enumerable, IEnumerable<double> maxValues, IEnumerable<double> minValues)
            => (enumerable, maxValues, minValues).ForEachThreeSelect((x, max, min) => x.NormalizeMaxMin(min, max)).ToList();

        /// <summary>
        /// 对浮点集合中的值逐一进行归一化
        /// </summary>
        /// <param name="enumerable">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>归一化后的集合</returns>
        public static List<decimal> Normalize(this IEnumerable<decimal> enumerable, IEnumerable<decimal> maxValues, IEnumerable<decimal> minValues)
            => (enumerable, maxValues, minValues).ForEachThreeSelect((x, max, min) => x.NormalizeMaxMin(min, max)).ToList();

        /// <summary>
        /// 对浮点集合中的值逐一进行归一化
        /// </summary>
        /// <param name="enumerable">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>归一化后的集合</returns>
        public static List<float> Normalize(this IEnumerable<float> enumerable, IEnumerable<float> maxValues, IEnumerable<float> minValues)
            => (enumerable, maxValues, minValues).ForEachThreeSelect((x, max, min) => x.NormalizeMaxMin(min, max)).ToList();

        /// <summary>
        /// 对浮点集合中的值逐一进行归一化
        /// </summary>
        /// <param name="enumerable">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>归一化后的集合</returns>
        public static List<double> Normalize(this IEnumerable<long> enumerable, IEnumerable<long> maxValues, IEnumerable<long> minValues)
            => (enumerable, maxValues, minValues).ForEachThreeSelect((x, max, min) => x.NormalizeMaxMin(min, max)).ToList();

        /// <summary>
        /// 对浮点集合中的值逐一进行归一化
        /// </summary>
        /// <param name="enumerable">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>归一化后的集合</returns>
        public static List<double> Normalize(this IEnumerable<int> enumerable, IEnumerable<int> maxValues, IEnumerable<int> minValues)
            => (enumerable, maxValues, minValues).ForEachThreeSelect((x, max, min) => x.NormalizeMaxMin(min, max)).ToList();

        #endregion

        #region Denormalize

        /// <summary>
        /// 对浮点集合中的值逐一进行反归一化
        /// </summary>
        /// <param name="enumerable">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>反归一化后的集合</returns>
        public static List<double> Denormalize(this IEnumerable<double> enumerable, IEnumerable<double> maxValues, IEnumerable<double> minValues)
            => (enumerable, maxValues, minValues).ForEachThreeSelect((x, max, min) => x.DenormalizeMaxMin(min, max)).ToList();

        /// <summary>
        /// 对浮点集合中的值逐一进行反归一化
        /// </summary>
        /// <param name="enumerable">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>反归一化后的集合</returns>
        public static List<decimal> Denormalize(this IEnumerable<decimal> enumerable, IEnumerable<decimal> maxValues, IEnumerable<decimal> minValues)
            => (enumerable, maxValues, minValues).ForEachThreeSelect((x, max, min) => x.DenormalizeMaxMin(min, max)).ToList();

        /// <summary>
        /// 对浮点集合中的值逐一进行反归一化
        /// </summary>
        /// <param name="enumerable">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>反归一化后的集合</returns>
        public static List<float> Denormalize(this IEnumerable<float> enumerable, IEnumerable<float> maxValues, IEnumerable<float> minValues)
            => (enumerable, maxValues, minValues).ForEachThreeSelect((x, max, min) => x.DenormalizeMaxMin(min, max)).ToList();

        #endregion

        #endregion

        #region Filters

        #region NaN Filters

        /// <summary>
        /// 去除序列中的NaN
        /// </summary>
        /// <param name="values"></param>
        /// <returns>去除后的序列</returns>
        public static IEnumerable<double> FilteringNaN(this IEnumerable<double> values)
            => values.Where(v => !double.IsNaN(v));

        /// <summary>
        /// 去除序列中的NaN
        /// </summary>
        /// <param name="values"></param>
        /// <returns>去除后的序列</returns>
        public static IEnumerable<float> FilteringNaN(this IEnumerable<float> values)
            => values.Where(v => !float.IsNaN(v));
        #endregion

        #region Limiting Diff Filters

        /// <summary>
        /// 限制波动对集合进行过滤. 用前一个样本+波动幅度代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新集合</return>
        public static IEnumerable<double> FilteringWithLimitingDiff(this IEnumerable<double> values, double diff) {
            diff.ShouldLargerThan(0d);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingDiff(prev, d));
        }

        /// <summary>
        /// 限制波动对集合进行过滤. 用前一个样本+波动幅度代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新集合</return>
        public static IEnumerable<decimal> FilteringWithLimitingDiff(this IEnumerable<decimal> values, decimal diff) {
            diff.ShouldLargerThan(0);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingDiff(prev, d));
        }

        /// <summary>
        /// 限制波动对集合进行过滤. 用前一个样本+波动幅度代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新集合</return>
        public static IEnumerable<float> FilteringWithLimitingDiff(this IEnumerable<float> values, float diff) {
            diff.ShouldLargerThan(0f);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingDiff(prev, d));
        }

        /// <summary>
        /// 限制波动对集合进行过滤. 用前一个样本+波动幅度代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新集合</return>
        public static IEnumerable<long> FilteringWithLimitingDiff(this IEnumerable<long> values, long diff) {
            diff.ShouldLargerThan(0L);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingDiff(prev, d));
        }

        /// <summary>
        /// 限制波动对集合进行过滤. 用前一个样本+波动幅度代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新集合</return>
        public static IEnumerable<int> FilteringWithLimitingDiff(this IEnumerable<int> values, int diff) {
            diff.ShouldLargerThan(0);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingDiff(prev, d));
        }

        #endregion

        #region Limiting Amplify Filters
        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns>过滤后的新集合</returns>
        public static IEnumerable<double> FilteringWithLimitingAmplify(this IEnumerable<double> values, double diff) {
            diff.ShouldLargerThan(0d);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingAmplify(prev, diff));
        }

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns>过滤后的新集合</returns>
        public static IEnumerable<decimal> FilteringWithLimitingAmplify(this IEnumerable<decimal> values, decimal diff) {
            diff.ShouldLargerThan(0m);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingAmplify(prev, diff));
        }

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns>过滤后的新集合</returns>
        public static IEnumerable<float> FilteringWithLimitingAmplify(this IEnumerable<float> values, float diff) {
            diff.ShouldLargerThan(0f);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingAmplify(prev, diff));
        }

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns>过滤后的新集合</returns>
        public static IEnumerable<long> FilteringWithLimitingAmplify(this IEnumerable<long> values, long diff) {
            diff.ShouldLargerThan(0L);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingAmplify(prev, diff));
        }

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替. 
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns>过滤后的新集合</returns>
        public static IEnumerable<int> FilteringWithLimitingAmplify(this IEnumerable<int> values, int diff) {
            diff.ShouldLargerThan(0);
            return values.PreviousBasedFilter(diff, (curr, prev, d) => curr.LimitingAmplify(prev, diff));
        }

        #endregion

        #region Window Median Filters
        /// <summary>
        /// 在指定窗口长度内进行中位值滤波。例如{1,2,8,5}.MedianFilter(3), 返回:{1,2,5,5}.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<double> FilteringWithWindowMedian(this IEnumerable<double> values, int windowLength) {
            return values.WindowBasedFilter(windowLength, w => w.Median());
        }

        /// <summary>
        /// 在指定窗口长度内进行中位值滤波。例如{1,2,8,5}.MedianFilter(3), 返回:{1,2,5,5}.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<decimal> FilteringWithWindowMedian(this IEnumerable<decimal> values, int windowLength) {
            return values.WindowBasedFilter(windowLength, w => w.Median());
        }

        /// <summary>
        /// 在指定窗口长度内进行中位值滤波。例如{1,2,8,5}.MedianFilter(3), 返回:{1,2,5,5}.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<float> FilteringWithWindowMedian(this IEnumerable<float> values, int windowLength) {
            return values.WindowBasedFilter(windowLength, w => w.Median());
        }

        /// <summary>
        /// 在指定窗口长度内进行中位值滤波。例如{1,2,8,5}.MedianFilter(3), 返回:{1,2,5,5}.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<double> FilteringWithWindowMedian(this IEnumerable<long> values, int windowLength) {
            return values.WindowBasedFilter(windowLength, v => Convert.ToDouble(v), w => w.Median());
        }

        /// <summary>
        /// 在指定窗口长度内进行中位值滤波。例如{1,2,8,5}.MedianFilter(3), 返回:{1,2,5,5}.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<double> FilteringWithWindowMedian(this IEnumerable<int> values, int windowLength) {
            return values.WindowBasedFilter(windowLength, v => Convert.ToDouble(v), w => w.Median());
        }

        #endregion

        #region Window Mean Filters
        /// <summary>
        /// 算术平均值过滤. 窗口长度内取平均值.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="ignoreMaxMinValue">计算均值时是否忽略最大值最小值</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<double> FilteringWithWindowMean(this IEnumerable<double> values, int windowLength, bool ignoreMaxMinValue = false) {
            return values.WindowBasedFilter(windowLength, w => w.Average(ignoreMaxMinValue));
        }

        /// <summary>
        /// 算术平均值过滤. 窗口长度内取平均值.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="ignoreMaxMinValue">计算均值时是否忽略最大值最小值</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<decimal> FilteringWithWindowMean(this IEnumerable<decimal> values, int windowLength, bool ignoreMaxMinValue = false) {
            return values.WindowBasedFilter(windowLength, w => w.Average(ignoreMaxMinValue));
        }

        /// <summary>
        /// 算术平均值过滤. 窗口长度内取平均值.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="ignoreMaxMinValue">计算均值时是否忽略最大值最小值</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<float> FilteringWithWindowMean(this IEnumerable<float> values, int windowLength, bool ignoreMaxMinValue = false) {
            return values.WindowBasedFilter(windowLength, (IEnumerable<float> w) => w.Average(ignoreMaxMinValue));
        }

        /// <summary>
        /// 算术平均值过滤. 窗口长度内取平均值.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="ignoreMaxMinValue">计算均值时是否忽略最大值最小值</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<double> FilteringWithWindowMean(this IEnumerable<long> values, int windowLength, bool ignoreMaxMinValue = false) {
            return values.WindowBasedFilter(windowLength, v => Convert.ToDouble(v), w => w.Average(ignoreMaxMinValue));
        }

        /// <summary>
        /// 算术平均值过滤. 窗口长度内取平均值.
        /// </summary>
        /// <param name="values">原集合</param>
        /// <param name="windowLength">窗口长度</param>
        /// <param name="ignoreMaxMinValue">计算均值时是否忽略最大值最小值</param>
        /// <returns>新的样本集</returns>
        public static IEnumerable<double> FilteringWithWindowMean(this IEnumerable<int> values, int windowLength, bool ignoreMaxMinValue = false) {
            return values.WindowBasedFilter(windowLength, v => Convert.ToDouble(v), w => w.Average(ignoreMaxMinValue));
        }

        #endregion

        #endregion
    }
}