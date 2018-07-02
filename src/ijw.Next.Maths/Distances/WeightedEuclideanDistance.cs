#if !NET35
using ijw.Next.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Maths {
    /// <summary>
    /// 计算向量间的加权欧氏距离
    /// </summary>
    public class WeightedEuclideanDistance : WeightedMinkowskiDistance {
        /// <summary>
        /// 构造一个加权欧氏距离计算器
        /// </summary>
        /// <param name="weights">权重集合</param>
        public WeightedEuclideanDistance(ICollection<double> weights) : base(2, weights) { }

        /// <summary>
        /// 构造一个加权欧氏距离计算器
        /// </summary>
        /// <param name="weights">权重集合</param>
        public WeightedEuclideanDistance(IIndexable<double> weights) : base(2, weights) { }
    }
}
#endif