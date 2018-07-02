#if !NET35
using ijw.Next.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Maths {
    /// <summary>
    /// 计算向量间的距离
    /// </summary>

    public interface IDistanceBase {
        /// <summary>
        /// 计算向量间的距离
        /// </summary>
        double GetDistance(ICollection<double> v1, ICollection<double> v2);

        /// <summary>
        /// 计算向量间的距离
        /// </summary>
        double GetDistance(IIndexable<double> v1, IIndexable<double> v2);
    }
}
#endif