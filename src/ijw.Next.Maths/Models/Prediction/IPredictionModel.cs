using System.Collections.Generic;

namespace ijw.Next.Maths {
    /// <summary>
    /// 预测模型
    /// </summary>
    public interface IPredictionModel {
        /// <summary>
        /// 计算绝对误差
        /// </summary>
        /// <returns></returns>
        double GetError();

        /// <summary>
        /// 计算相对误差
        /// </summary>
        /// <returns></returns>
        double GetRelativeError();
    }
}