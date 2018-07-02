using System.Collections.Generic;

namespace ijw.Next.Maths {
    /// <summary>
    /// 表示一个多输入多输出的数学函数
    /// </summary>
    public interface IMimoMathFunction : IFunction<IEnumerable<double>, IEnumerable<double>> {
    }
}
