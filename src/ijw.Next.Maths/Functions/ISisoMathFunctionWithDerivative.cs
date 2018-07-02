namespace ijw.Next.Maths {
    /// <summary>
    /// 可以计算导数的数学函数
    /// </summary>
    public interface ISisoMathFunctionWithDerivative : ISisoMathFunction {
        /// <summary>
        /// 计算指定输入的导数
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>导数</returns>
        double CalculateDerivative(double input);
    }
}
