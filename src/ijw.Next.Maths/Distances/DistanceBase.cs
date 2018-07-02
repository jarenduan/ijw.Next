#if !NET35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ijw.Next.Collection;

namespace ijw.Next.Maths {
    /// <summary>
    /// 距离计算器基类
    /// </summary>
    public abstract class DistanceBase : IDistanceBase {
        /// <summary>
        /// 获取两个向量间的距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>距离</returns>
        public virtual double GetDistance(ICollection<double> v1, ICollection<double> v2) {
            v1.ShouldBeNotNullArgument(nameof(v1));
            v2.ShouldBeNotNullArgument(nameof(v2));
            v2.Count.ShouldEquals(v1.Count).ThrowsWhenBroke<DimensionNotMatchException>(); ;
            return getDistance(v1, v2);
        }

        /// <summary>
        /// 获取两个向量间的距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>距离</returns>
        public virtual double GetDistance(IIndexable<double> v1, IIndexable<double> v2) {
            v1.ShouldBeNotNullArgument(nameof(v1));
            v2.ShouldBeNotNullArgument(nameof(v2));
            v2.Count.ShouldEquals(v1.Count()).ThrowsWhenBroke<DimensionNotMatchException>();
            return getDistance(v1, v2);
        }

        /// <summary>
        /// 获取两个向量间的距离
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>距离</returns>
        public virtual double GetDistance(IEnumerable<double> v1, IEnumerable<double> v2) {
            v1.ShouldBeNotNullArgument(nameof(v1));
            v2.ShouldBeNotNullArgument(nameof(v2));
            v2.Count().ShouldEquals(v1.Count()).ThrowsWhenBroke<DimensionNotMatchException>();
            return getDistance(v1, v2);
        }

#pragma warning disable IDE1006 // 命名样式
        /// <summary>
        /// 获取两个向量间的距离，不用检查向量维度是否匹配
        /// </summary>
        /// <param name="v1">第一个向量</param>
        /// <param name="v2">第二个向量</param>
        /// <returns>距离</returns>
        protected abstract double getDistance(IEnumerable<double> v1, IEnumerable<double> v2);
#pragma warning restore IDE1006 // 命名样式
    }
}
#endif