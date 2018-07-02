#if !NET35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ijw.Next.Collection;

namespace ijw.Next.Maths {
    /// <summary>
    /// 计算向量间的加权闵氏距离
    /// </summary>
    public class WeightedMinkowskiDistance : WeightedDistanceBase {
        /// <summary>
        /// 构造一个加权闵氏距离计算器
        /// </summary>
        /// <param name="lambda">距离幂值</param>
        /// <param name="weights">权重集合</param>
        public WeightedMinkowskiDistance(int lambda, ICollection<double> weights) : base(weights) {
            this.Lambda = lambda;
        }

        /// <summary>
        /// 构造一个加权闵氏距离计算器
        /// </summary>
        /// <param name="lambda">距离幂值</param>
        /// <param name="weights">权重集合</param>
        public WeightedMinkowskiDistance(int lambda, IIndexable<double> weights) : base(weights) {
            this.Lambda = lambda;
        }

        /// <summary>
        /// 距离幂值
        /// </summary>
        public int Lambda { get; protected set; }

        /// <summary>
        /// 计算两个向量的的加权曼哈顿距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>加权曼哈顿距离</returns>
        protected override double getDistance(IEnumerable<double> v1, IEnumerable<double> v2) {
            var sum = (v1, v2, Weights).ForEachThreeSelect((u, v, w) => w * (Math.Pow(Math.Abs(u - v), Lambda)))
                                        .Sum();
            return Math.Pow(sum, 1d / Lambda);
        }
    }
}
#endif