using System;
using static System.Math;

namespace ijw.Next {
    /// <summary>
    /// 提供对Float、Double等浮点类型的若干扩展方法
    /// </summary>
    public static class FloatingNumberExt {
        #region Truncate
        /// <summary>
        /// 用上限截断
        /// </summary>
        /// <param name="f"></param>
        /// <param name="upperBound">上限</param>
        /// <returns>大于上限, 返回上限值; 反之返回自身</returns>
        public static float TruncateTop(this float f, float upperBound = 99999f)
            => (f > upperBound) ? upperBound : f;

        /// <summary>
        /// 用下限截断
        /// </summary>
        /// <param name="f"></param>
        /// <param name="lowerBound">下限</param>
        /// <returns>小于下限, 返回下限值; 反之返回自身</returns>
        public static float TruncateBottom(this float f, float lowerBound = -99999f)
            => (f < lowerBound) ? lowerBound : f;

        /// <summary>
        /// 用上限截断
        /// </summary>
        /// <param name="f"></param>
        /// <param name="upperBound">上限</param>
        /// <returns>大于上限, 返回上限值; 反之返回自身</returns>
        public static double TruncateTop(this double f, double upperBound = 99999d)
            => (f > upperBound) ? upperBound : f;

        /// <summary>
        /// 用下限截断
        /// </summary>
        /// <param name="f"></param>
        /// <param name="lowerBound">下限</param>
        /// <returns>小于下限, 返回下限值; 反之返回自身</returns>
        public static double TruncateBottom(this double f, double lowerBound = -99999d)
            => (f < lowerBound) ? lowerBound : f;

        /// <summary>
        /// 用上限截断
        /// </summary>
        /// <param name="f"></param>
        /// <param name="upperBound">上限</param>
        /// <returns>大于上限, 返回上限值; 反之返回自身</returns>
        public static decimal TruncateTop(this decimal f, decimal upperBound = 99999m)
            => (f > upperBound) ? upperBound : f;

        /// <summary>
        /// 用下限截断
        /// </summary>
        /// <param name="f"></param>
        /// <param name="lowerBound">下限</param>
        /// <returns>小于下限, 返回下限值; 反之返回自身</returns>
        public static decimal TruncateBottom(this decimal f, decimal lowerBound = -99999m)
            => (f < lowerBound) ? lowerBound : f;
        #endregion

        #region Pow

        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static double Pow(this double number, int power) => Math.Pow(number, power);

        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static double Pow(this float number, int power) => Math.Pow(number, power);

        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static double Pow(this double number, float power) => Math.Pow(number, power);

        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static double Pow(this float number, float power) => Math.Pow(number, power);

        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static double Pow(this double number, double power) => Math.Pow(number, power);
        
        /// <summary>
        /// 幂计算
        /// </summary>
        /// <param name="number"></param>
        /// <param name="power">幂</param>
        /// <returns></returns>
        public static double Pow(this float number, double power) => Math.Pow(number, power);

        #endregion

        #region Normalization

        #region NormalizeMaxMin

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
        /// 使用指定的最大值/最小值进行归一化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="minOut">目标区间最小值</param>
        /// <param name="maxOut">目标区间最大值</param>
        /// <returns>归一化后的值</returns>
        public static decimal NormalizeMaxMin(this decimal x, decimal min, decimal max, decimal minOut = 0.1m, decimal maxOut = 0.9m) {
            return (x - min) / (max - min) * (maxOut - minOut) + minOut;
        }

        /// <summary>
        /// 使用指定的最大值/最小值进行归一化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="minOut">目标区间最小值</param>
        /// <param name="maxOut">目标区间最大值</param>
        /// <returns>归一化后的值</returns>
        public static float NormalizeMaxMin(this float x, float min, float max, float minOut = 0.1f, float maxOut = 0.9f) {
            return (x - min) / (max - min) * (maxOut - minOut) + minOut;
        }

        /// <summary>
        /// 使用指定的最大值/最小值进行归一化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="minOut">目标区间最小值</param>
        /// <param name="maxOut">目标区间最大值</param>
        /// <returns>归一化后的值</returns>
        public static double NormalizeMaxMin(this long x, long min, long max, double minOut = 0.1, double maxOut = 0.9) {
            return (x - min) / (max - min) * (maxOut - minOut) + minOut;
        }

        /// <summary>
        /// 使用指定的最大值/最小值进行归一化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="minOut">目标区间最小值</param>
        /// <param name="maxOut">目标区间最大值</param>
        /// <returns>归一化后的值</returns>
        public static double NormalizeMaxMin(this int x, int min, int max, double minOut = 0.1, double maxOut = 0.9) {
            return (x - min) / (max - min) * (maxOut - minOut) + minOut;
        }
        #endregion

        #region DenormalizeMaxMin

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

        /// <summary>
        /// 使用指定的最大值/最小值进行反归一化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <param name="minOut">目标区间最小值</param>
        /// <param name="maxOut">目标区间最大值</param>
        /// <returns></returns>
        public static decimal DenormalizeMaxMin(this decimal x, decimal min, decimal max, decimal minOut = 0.1m, decimal maxOut = 0.9m) {
            return (x - minOut) / (maxOut - minOut) * (max - min) + min;
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
        public static float DenormalizeMaxMin(this float x, float min, float max, float minOut = 0.1f, float maxOut = 0.9f) {
            return (x - minOut) / (maxOut - minOut) * (max - min) + min;
        }
        #endregion

        #endregion

        #region Filters

        #region Limiting Diff

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
        /// 限制波动过滤. 用前一个值+波动幅度代替. 
        /// </summary>
        /// <param name="curr">欲过滤的值</param>
        /// <param name="prev">前一个值</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新值</return>
        public static decimal LimitingDiff(this decimal curr, decimal prev, decimal diff) {
            decimal r;
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
        /// 限制波动过滤. 用前一个值+波动幅度代替. 
        /// </summary>
        /// <param name="curr">欲过滤的值</param>
        /// <param name="prev">前一个值</param>
        /// <param name="diff">波动幅度限制</param>
        /// <return>过滤后的新值</return>
        public static float LimitingDiff(this float curr, float prev, float diff) {
            float r;
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

        #endregion

        #region LimitingAmplify

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

        /// <summary>
        /// 限幅过滤. 放弃掉波动过大的数值, 用前一个数值代替.  
        /// </summary>
        /// <param name="curr">欲过滤的值</param>
        /// <param name="prev">前一个值</param>
        /// <param name="diff">波动最大值绝对值</param>
        /// <return>过滤后的新值</return>
        public static decimal LimitingAmplify(this decimal curr, decimal prev, decimal diff) {
            decimal r;
            if (Abs(curr - prev) > diff) {
                r = prev;
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
        public static float LimitingAmplify(this float curr, float prev, float diff) {
            float r;
            if (Abs(curr - prev) > diff) {
                r = prev;
            }
            else {
                r = curr;
            }
            return r;
        }

        #endregion

#endregion
    }
}
