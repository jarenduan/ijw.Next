#if !NET35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Maths {
    /// <summary>
    /// 计算向量间的欧氏距离
    /// </summary>
    public class EuclideanDistance : MinkowskiDistance {
        /// <summary>
        /// 构造一个欧式距离计算器
        /// </summary>
        public EuclideanDistance() : base(2) { }
    }
}
#endif
