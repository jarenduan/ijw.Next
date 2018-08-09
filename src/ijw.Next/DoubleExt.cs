using System;
using static System.Math;

namespace ijw.Next {
    /// <summary>
    /// 提供对Double类型的若干扩展方法
    /// </summary>
    public static class DoubleExt {
        #region Pow

        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static int Pow(this double number, int power) => (int)Math.Pow(number, power);

        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static int Pow(this double number, float power) => (int)Math.Pow(number, power);

        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static int Pow(this double number, double power) => (int)Math.Pow(number, power);

        #endregion 新建 #region

        #region Normalization
        /// <summary>
        /// 使用指定的最大值/最小值进行归一化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="minOut">目标区间最小值</param>
        /// <param name="maxOut">目标区间最大值</param>
        /// <returns>归一化后的值</returns>
        public static double NormalizeMaxMin(this double x, double min, double max, double minOut = 0.1, double maxOut = 0.9) {
            return (x - min) / (max - min) * (maxOut - minOut) + minOut;
        }

        /// <summary>
        /// 使用指定的最大值/最小值进行反归一化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <param name="minOut">目标区间最小值</param>
        /// <param name="maxOut">目标区间最大值</param>
        /// <returns></returns>
        public static double DenormalizeMaxMin(this double x, double min, double max, double minOut = 0.1, double maxOut = 0.9) {
            return (x - minOut) / (maxOut - minOut) * (max - min) + min;
        }
        #endregion

        #region Filters
        /// <summary>
        /// 限制波动过滤. 用前一个值+波动幅度代替. 
        /// </summary>
        /// <param name="curr">欲过滤的值</param>
        /// <param name="prev">前一个值</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新值</return>
        public static double LimitingDiff(this double curr, double prev, double diff) {
            double r;
            if ((curr - prev) > diff) {
                r = prev + diff;
            }
            else if ((prev - curr) > diff) {
                r = prev - diff;
            }
            else {
                r = curr;
            }
            return r;
        }

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替.  
        /// </summary>
        /// <param name="curr">欲过滤的值</param>
        /// <param name="prev">前一个值</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <return>过滤后的新值</return>
        public static double LimitingAmplify(this double curr, double prev, double diff) {
            double r;
            if (Abs(curr - prev) > diff) {
                r = prev;
            }
            else {
                r = curr;
            }
            return r;
        }


        #endregion
    }
}
