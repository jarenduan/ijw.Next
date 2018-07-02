namespace ijw.Next.Maths {
    /// <summary>
    /// 一个输入与输出的模型
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IModel<TInput, TOutput> {
        /// <summary>
        /// 输入维度
        /// </summary>
        int InputDimension { get; }

        /// <summary>
        /// 输出维度
        /// </summary>
        int OutputDimension { get; }

        /// <summary>
        /// 输入
        /// </summary>
        TInput Input { get; set; }

        /// <summary>
        /// 输出
        /// </summary>
        TOutput Output { get; }

        /// <summary>
        /// 根据当前的输入进行计算，更新输出
        /// </summary>
        void Calculate();
    }

    //public interface ISimpleCalculationModel : IModel<double, double> { }

    //public interface ISimpleCalculationModelWithDerivative : ISimpleCalculationModel {
    //    double Derivative { get; }
    //    void CalculateDerivative();
    //}
}
