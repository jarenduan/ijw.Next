using System;

namespace ijw.Next.Maths {
    /// <summary>
    /// Sigmod函数
    /// </summary>
    public class SigmodFunction  : ISisoMathFunctionWithDerivative{
        /// <summary>
        /// 计算输出值
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>输出, 处于(0, 1)区间</returns>
        public double Calculate(double input) => 1 / (1 + Math.Pow(Math.E, -input));

        /// <summary>
        /// 计算指定输入的导数
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>导数</returns>
        public double CalculateDerivative(double input) {
            double fx = Calculate(input);
            return  fx * (1 - fx);
        }
    }
}