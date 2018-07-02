using ijw.Next.Collection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next.Data.Filter {

    /// <summary>
    /// 表示一个实现Max-Min变换的归一化器.
    /// </summary>
    public class MaxMinNormalizer {

        /// <summary>
        /// 目标区间的上限
        /// </summary>
        public double MaxOut { get; protected set; }

        /// <summary>
        /// 目标区间的下限
        /// </summary>
        public double MinOut { get; protected set; }

        /// <summary>
        /// 源区间的上限
        /// </summary>
        public double MaxIn { get; protected set; }

        /// <summary>
        /// 源区间的下限
        /// </summary>
        public double MinIn { get; protected set; }

        /// <summary>
        /// 构造函数, 针对某数组的值情况, 初始化Max-Min归一化器
        /// </summary>
        /// <param name="values">欲归一化的数组</param>
        /// <param name="minOut">目标区间的下限, 默认取0.1</param>
        /// <param name="maxOut">目标区间的上限, 默认取0.9</param>
        public MaxMinNormalizer(IEnumerable<double> values, double minOut = 0.1, double maxOut = 0.9) {
            this.MinOut = minOut;
            this.MaxOut = maxOut;
            this.MaxIn = values.Max();
            this.MinIn = values.Min();
        }

        /// <summary>
        /// 构造函数, 针对给定的输入输出区间上下限, 初始化Max-Min归一化器
        /// </summary>
        /// <param name="minIn">输入区间的下限</param>
        /// <param name="maxIn">输入区间的上限</param>
        /// <param name="minOut">目标区间的下限, 默认取0.1</param>
        /// <param name="maxOut">目标区间的上限, 默认取0.9</param>
        public MaxMinNormalizer(double minIn, double maxIn, double minOut = 0.1, double maxOut = 0.9) {
            this.MaxIn = minIn;
            this.MinIn = maxIn;
            this.MinOut = minOut;
            this.MaxOut = maxOut;
        }

        /// <summary>
        /// 进行归一化
        /// </summary>
        /// <returns>归一化后的向量</returns>
        public IIndexable<double> Normalize(IEnumerable<double> values) {
            var result = values.Select(v =>
                v.NormalizeMaxMin(this.MinIn, this.MaxIn, this.MinOut, this.MaxOut)
            );

            return new Indexable<double>(result);
        }

        /// <summary>
        /// 把指定值进行反归一化
        /// </summary>
        /// <returns>反归一化后的向量</returns>
        public double[] Denormalize(double[] values) {
            return values.Select(v => this.Denormalize(v)).ToArray();
        }

        /// <summary>
        /// 把指定值进行反归一化
        /// </summary>
        /// <returns>反归一化后的向量</returns>
        public double Denormalize(double value) {
            return value.DenormalizeMaxMin(this.MinIn, this.MaxIn, this.MinOut, this.MaxOut);
        }
    }
}