using System;

namespace ijw.Next.Maths {
    /// <summary>
    /// 线性函数
    /// </summary>
    public class LinearFunction : ISisoMathFunctionWithDerivative {
        /// <summary>
        /// 斜率
        /// </summary>
        public double K { get; set; } = 1d;

        /// <summary>
        /// 计算输出值，其与输入相同
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>输出</returns>
        public double Calculate(double input) {
            return input;
        }

        /// <summary>
        /// 计算导数
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>恒等为斜率</returns>
        public double CalculateDerivative(double input) {
            return K;
        }
    }
}
