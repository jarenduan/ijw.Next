#if !NET35
using System;

namespace ijw.Next.Maths {
    
    /// <summary>
    /// 向量距离计算
    /// </summary>
    [Obsolete]
    public class Distance {
        /// <summary>
        /// 计算向量间的欧氏距离
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>向量1与向量2之间的欧氏距离</returns>
        public double EuclideanBetween(Vector v1, Vector v2) {
            return v1.EuclideanDistanceFrom(v2);
        }

        /// <summary>
        /// 计算向量间的欧氏距离
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <param name="weights">权值</param>
        /// <returns>向量1与向量2之间的欧氏距离</returns>
        public double EuclideanBetween(Vector v1, Vector v2, Vector weights) {
            return v1.EuclideanDistanceFrom(v2, weights);
        }

        /// <summary>
        /// 计算向量间的曼哈顿距离
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>向量1与向量2之间的曼哈顿距离</returns>
        public double ManhattanBetween(Vector v1, Vector v2) {
            return v1.ManhattanDistanceFrom(v2);
        }

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        /// <summary>
        /// 计算向量间的曼哈顿距离
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>向量1与向量2之间的曼哈顿距离</returns>
        public double ManhattanBetween(Vector v1, Vector v2, Vector weights) {
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
            return v1.ManhattanDistanceFrom(v2, weights);
        }

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        /// <summary>
        /// 计算向量间的闵氏距离
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>向量1与向量2之间的闵氏距离</returns>
        public double MinkowskiBetween(Vector v1, Vector v2, int lambda) {
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
            return v1.MinkowskiDistanceFrom(v2, lambda);
        }

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        /// <summary>
        /// 计算向量间的闵氏距离
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>向量1与向量2之间的闵氏距离</returns>
        public double MinkowskiBetween(Vector v1, Vector v2, int lambda, Vector weights) {
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
            return v1.MinkowskiDistanceFrom(v2, lambda, weights);
        }
    }
}
#endif