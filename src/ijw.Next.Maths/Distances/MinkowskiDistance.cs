#if !NET35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ijw.Next.Collection;

namespace ijw.Next.Maths {
    /// <summary>
    /// 计算向量间的闵氏距离
    /// </summary>
    public class MinkowskiDistance : DistanceBase {
        /// <summary>
        /// 构造一个闵氏距离计算器
        /// </summary>
        /// <param name="lambda"></param>
        public MinkowskiDistance(int lambda) {
            this.Lambda = lambda;
        }

        /// <summary>
        /// 距离幂值
        /// </summary>
        public int Lambda { get; protected set; }

        /// <summary>
        /// 计算向量间的闵氏距离
        /// </summary>
        protected override double getDistance(IEnumerable<double> v1, IEnumerable<double> v2) {
            var sum = (v1, v2).ForEachPairSelect((u, v) => Math.Pow(Math.Abs(u - v), Lambda))
                              .Sum();
            return Math.Pow(sum, 1d / Lambda);
        }
    }
}
#endif