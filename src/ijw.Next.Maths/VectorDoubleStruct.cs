using System;
using System.Text;

namespace ijw.Next.Maths {
    /// <summary>
    /// 向量值类型
    /// </summary>
    public struct VectorDoubleStruct {
        /// <summary>
        /// 维度
        /// </summary>
        public int Dimension;

        /// <summary>
        /// 内部数据
        /// </summary>
        public double[] Data;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dimension"></param>
        public VectorDoubleStruct(int dimension) {
            Dimension = dimension;
            Data = new double[Dimension];
        }

        /// <summary>
        /// 字符串表示
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder("{");
            foreach (var item in this.Data) {
                sb.Append(item.ToString());
                sb.Append(", ");
            }
            if (sb.Length >= 2) sb.Remove(sb.Length - 2, 2);
            sb.Append("}");
            return sb.ToString();
        }
    }
}