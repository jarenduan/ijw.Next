using System.Collections.Generic;

namespace ijw.Next.Maths {
    /// <summary>
    /// 预测模型
    /// </summary>
    public interface ISOPredictionModel : IPredictionModel {
        /// <summary>
        /// 期望输出
        /// </summary>
        double DesireOutput { get; set; }
    }
}