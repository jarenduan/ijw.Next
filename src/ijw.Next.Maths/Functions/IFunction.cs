namespace ijw.Next.Maths {
    /// <summary>
    /// 表示输入与输出之间的函数
    /// </summary>
    /// <typeparam name="TInput">输入类型</typeparam>
    /// <typeparam name="TOutput">输出类型</typeparam>
    public interface IFunction<TInput, TOutput> {
        /// <summary>
        /// 计算输出值，其与输入相同
        /// </summary>
        /// <param name="input">指定的输入</param>
        /// <returns>输出</returns>
        TOutput Calculate(TInput input);
    }
}