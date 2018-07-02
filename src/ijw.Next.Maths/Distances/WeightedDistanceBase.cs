#if !NET35
using ijw.Next.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Maths {
    /// <summary>
    /// 计算向量间的加权距离
    /// </summary>
    public abstract class WeightedDistanceBase : DistanceBase {
        /// <summary>
        /// 构造一个加权向量距离计算器
        /// </summary>
        /// <param name="weights">权重集合</param>
        public WeightedDistanceBase(ICollection<double> weights) {
            this.Weights = weights;
            this.Dimention = weights.Count;
        }

        /// <summary>
        /// 构造一个加权向量距离计算器
        /// </summary>
        /// <param name="weights">权重集合</param>
        public WeightedDistanceBase(IIndexable<double> weights) {
            this.Weights = weights;
            this.Dimention = weights.Count;
        }

        /// <summary>
        /// 构造一个加权向量距离计算器
        /// </summary>
        /// <param name="weights">权重集合</param>
        public WeightedDistanceBase(IEnumerable<double> weights) {
            this.Weights = weights;
            this.Dimention = weights.Count();
        }

        /// <summary>
        /// 获取两个向量间的距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>距离</returns>
        public override double GetDistance(ICollection<double> v1, ICollection<double> v2) {
            v1.ShouldBeNotNullArgument(nameof(v1));
            v2.ShouldBeNotNullArgument(nameof(v2));
            v2.Count.ShouldEquals(v1.Count);
            v2.Count.ShouldEquals(this.Dimention);
            return getDistance(v1, v2);
        }

        /// <summary>
        /// 获取两个向量间的距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>距离</returns>
        public override double GetDistance(IIndexable<double> v1, IIndexable<double> v2) {
            v1.ShouldBeNotNullArgument(nameof(v1));
            v2.ShouldBeNotNullArgument(nameof(v2));
            v2.Count.ShouldEquals(v1.Count);
            v2.Count.ShouldEquals(this.Dimention);
            return getDistance(v1, v2);
        }

        /// <summary>
        /// 获取两个向量间的距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>距离</returns>
        public override double GetDistance(IEnumerable<double> v1, IEnumerable<double> v2) {
            v1.ShouldBeNotNullArgument(nameof(v1));
            v2.ShouldBeNotNullArgument(nameof(v2));
            v2.Count().ShouldEquals(v1.Count());
            v2.Count().ShouldEquals(this.Dimention);
            return getDistance(v1, v2);
        }

        /// <summary>`
        /// 各维度权值
        /// </summary>
        public IEnumerable<double> Weights { get; protected set; }

        /// <summary>
        /// 此距离计算器的计算维度
        /// </summary>
        public int Dimention { get; protected set; }
    }
}
#endif