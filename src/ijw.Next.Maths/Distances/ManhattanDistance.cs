#if !NET35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ijw.Next.Collection;

namespace ijw.Next.Maths {
    /// <summary>
    /// 计算向量间的曼哈顿距离
    /// </summary>
    public class ManhattanDistance : DistanceBase {
        /// <summary>
        /// 计算两个向量的的曼哈顿距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>曼哈顿距离</returns>
        protected override double getDistance(IEnumerable<double> v1, IEnumerable<double> v2) {
            return (v1, v2).ForEachPairSelect((u, v) => u - v).Sum();
        }
    }
}
#endif