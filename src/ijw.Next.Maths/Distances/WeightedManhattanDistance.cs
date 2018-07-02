#if !NET35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ijw.Next.Collection;

namespace ijw.Next.Maths
{
    /// <summary>
    /// 计算向量间的加权曼哈顿距离
    /// </summary>
    public class WeightedManhattanDistance : WeightedDistanceBase {
        /// <summary>
        /// 构造一个加权曼哈顿距离计算器
        /// </summary>
        /// <param name="weights">权重集合</param>
        public WeightedManhattanDistance(ICollection<double> weights) : base(weights) { }

        /// <summary>
        /// 构造一个加权曼哈顿距离计算器
        /// </summary>
        /// <param name="weights">权重集合</param>
        public WeightedManhattanDistance(IIndexable<double> weights) : base(weights) { }

        /// <summary>
        /// 计算两个向量的的加权曼哈顿距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>加权曼哈顿距离</returns>
        protected override double getDistance(IEnumerable<double> v1, IEnumerable<double> v2) {
            return (v1, v2, this.Weights).ForEachThreeSelect((u, v, w) => w * (u - v)).Sum();
        }
    }
}
#endif