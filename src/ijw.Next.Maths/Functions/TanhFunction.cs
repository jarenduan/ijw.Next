using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Maths.Functions
{
    /// <summary>
    /// 双曲正切函数
    /// </summary>
    public class TanHFunction : ISisoMathFunctionWithDerivative {
        /// <summary>
        /// 计算输出值
        /// </summary>
        /// <param name="input">指定的输入,</param>
        /// <returns>输出，属于[-1,1]</returns>
        public double Calculate(double input) {
            var ex = Math.Pow(Math.E, input);
            var e_x = 1 / ex;
            return (ex - e_x) / (ex + e_x);
        }

        /// <summary>
        /// 计算指定输入的导数
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>导数</returns>
        public double CalculateDerivative(double input) => 1 - Math.Sqrt(Calculate(input));
    }
}