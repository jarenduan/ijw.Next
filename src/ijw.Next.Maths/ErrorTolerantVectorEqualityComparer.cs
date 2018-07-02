#if !NET35
using ijw.Next.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Maths {
    /// <summary>
    /// 误差容忍向量比较器
    /// </summary>
    public class ErrorTolerantVectorEqualityComparer : IEqualityComparer<Vector> {
        /// <summary>
        /// 误差容忍度
        /// </summary>
        public double EquataionPrecision { get; set; } = double.NaN;

        /// <summary>
        /// 考虑误差容忍度的相等比较
        /// </summary>
        /// <param name="x">第1个向量</param>
        /// <param name="y">第2个向量</param>
        /// <returns>相差在误差容忍度内，返回true，反之，返回false.</returns>
        public bool Equals(Vector x, Vector y) {
            if (x == null && y == null) {
                return true;
            }

            if (x == null || y == null) {
                return false;
            }

            if (ReferenceEquals(x, y)) {
                return true;
            }

            if (x.Dimension != y.Dimension) {
                return false;
            }

            if (double.IsNaN(this.EquataionPrecision)) {
                return x.ItemEquals(y);
            }
            else {
                return x.ItemEquals(y, (d1, d2) => {
                    return Math.Abs(d1 - d2) <= this.EquataionPrecision;
                });
            }
        }

        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>hash值</returns>
        public int GetHashCode(Vector obj) {
            return obj.GetHashCode();
        }
    }
}
#endif