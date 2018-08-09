﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableNumberExt {
        /// <summary>
        /// 计算均值，可指定是否忽略最大值最小值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ignoreMaxMinValue">是否忽略最大值最小值</param>
        /// <returns>均值</returns>
        #region Statistic
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
                    count++;
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
                    count++;
                }
                sum = sum - max - min;
                return sum / (count - 2);
            }
        }

        /// <summary>
        /// 获得方差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double GetVariance(this IEnumerable<double> values, bool isAllData = false) {
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
        public static double GetVariance(this IEnumerable<float> values, bool isAllData = false) {
            var mean = values.Average();
            var (count, sum) = values.SumAndCount(v => Math.Sqrt(v - mean)); //get count and sum within only one iteration.
            count = isAllData ? count : count - 1;
            return sum / count;
        }

        /// <summary>
        /// 获得标准差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double GetStandardVariance(this IEnumerable<double> values, bool isAllData = false)
           => Math.Pow(values.GetVariance(isAllData), 0.5);


        /// <summary>
        /// 获得标准差
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isAllData">是否是全部数据. true则分母是数据集count，反之是count-1.</param>
        /// <returns>方差</returns>
        public static double GetStandardVariance(this IEnumerable<float> values, bool isAllData = false)
           => Math.Pow(values.GetVariance(isAllData), 0.5);

        /// <summary>
        /// 获取集合的中位值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>奇数个元素，返回中间元素的值，偶数个元素返回中间两元素的平均值。</returns>
        public static double GetMedianValue(this IEnumerable<double> values) {
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
        public static float GetMedianValue(this IEnumerable<float> values) {
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
        /// 获取集合的中值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>返回最大最小的平均值。</returns>
        public static double GetMiddleValue(this IEnumerable<double> values) => (values.Max() + values.Min()) / 2;

        /// <summary>
        /// 获取集合的中值。
        /// </summary>
        /// <param name="values"></param>
        /// <returns>返回最大最小的平均值。</returns>
        public static float GetMiddleValue(this IEnumerable<float> values) => (values.Max() + values.Min()) / 2;
        #endregion

        #region Doubles' Normalizer

        /// <summary>
        /// 对浮点集合中的值逐一进行归一化
        /// </summary>
        /// <param name="collection">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>归一化后的集合</returns>
        public static List<double> Normalize(this IEnumerable<double> collection, IEnumerable<double> maxValues, IEnumerable<double> minValues) {
            return (collection, maxValues, minValues)
                    .ForEachThreeSelect((x, max, min) => x.NormalizeMaxMin(min, max))
                    .ToList();
        }

        /// <summary>
        /// 对浮点集合中的值逐一进行归一化
        /// </summary>
        /// <param name="collection">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>归一化后的集合</returns>
        public static List<float> Normalize(this IEnumerable<float> collection, IEnumerable<float> maxValues, IEnumerable<float> minValues) {
            return (collection, maxValues, minValues)
                    .ForEachThreeSelect((x, max, min) => x.NormalizeMaxMin(min, max))
                    .ToList();
        }

        /// <summary>
        /// 对浮点集合中的值逐一进行反归一化
        /// </summary>
        /// <param name="collection">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>反归一化后的集合</returns>
        public static List<double> Denormalize(this IEnumerable<double> collection, IEnumerable<double> maxValues, IEnumerable<double> minValues) {
            return (collection, maxValues, minValues)
                    .ForEachThreeSelect((x, max, min) => x.DenormalizeMaxMin(min, max))
                    .ToList();
        }

        /// <summary>
        /// 对浮点集合中的值逐一进行反归一化
        /// </summary>
        /// <param name="collection">浮点数集合</param>
        /// <param name="maxValues">归一化上限值的集合</param>
        /// <param name="minValues">归一化下限值的集合</param>
        /// <returns>反归一化后的集合</returns>
        public static List<float> Denormalize(this IEnumerable<float> collection, IEnumerable<float> maxValues, IEnumerable<float> minValues) {
            return (collection, maxValues, minValues)
                    .ForEachThreeSelect((x, max, min) => x.DenormalizeMaxMin(min, max))
                    .ToList();
        }
        #endregion
    }
}
