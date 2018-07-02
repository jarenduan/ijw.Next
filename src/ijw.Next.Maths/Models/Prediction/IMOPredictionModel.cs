using System.Collections.Generic;

namespace ijw.Next.Maths {
    /// <summary>
    /// 预测模型
    /// </summary>
    public interface IMOPredictionModel : IPredictionModel {
        /// <summary>
        /// 期望输出
        /// </summary>
        IEnumerable<double> DesireOutput { get; set; }
    }
}