using System;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    public static class ArrayExt {
        #region NewArray with values
        /// <summary>
        /// 创建数组, 并使用指定的值填充数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="dimension">数组维度</param>
        /// <param name="value">指定的值</param>
        /// <returns>创建好的数组</returns>
        public static T[] NewArrayWithValue<T>(this Array a, int dimension, T value) {
            T[] result = new T[dimension];
            for (int i = 0; i < result.Length; i++) {
                result[i] = value;
            }
            return result;
        }

        /// <summary>
        /// 创建数组, 使用指定的函数填充数组.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="dimension">数组维度</param>
        /// <param name="computer">值计算函数, 数组索引作为传入参数</param>
        /// <returns>创建好的数组</returns>
        public static T[] NewArrayWithValue<T>(this Array a, int dimension, Func<T> computer) {
            T[] result = new T[dimension];
            for (int i = 0; i < result.Length; i++) {
                result[i] = computer();
            }
            return result;
        }

        /// <summary>
        /// 创建数组, 使用指定的函数填充数组.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="dimension">数组维度</param>
        /// <param name="computer">值计算函数, 数组索引作为传入参数</param>
        /// <returns>创建好的数组</returns>
        public static T[] NewArrayWithValue<T>(this Array a, int dimension, Func<int, T> computer) {
            T[] result = new T[dimension];
            for (int i = 0; i < result.Length; i++) {
                result[i] = computer(i);
            }
            return result;
        }
        #endregion

    }
}
