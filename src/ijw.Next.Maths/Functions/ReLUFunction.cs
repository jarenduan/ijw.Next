using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Maths
{
    /// <summary>
    /// ReLU函数
    /// </summary>
    public class ReLUFunction : ISisoMathFunctionWithDerivative {
        /// <summary>
        /// 计算输出值，其与输入相同
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>输入大于0，返回输入本身，否则返回0</returns>
        public double Calculate(double input) => Math.Max(0, input);

        /// <summary>
        /// 计算指定输入的导数
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>输入大于0，返回1；反之返回0</returns>
        public double CalculateDerivative(double input) => input > 0 ? 1 : 0;
    }
}
