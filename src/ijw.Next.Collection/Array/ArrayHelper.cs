using System;

namespace ijw.Next.Collection {
    /// <summary>
    /// 数组Helper类
    /// </summary>
    public static class ArrayHelper {
        #region NewArray with values
        /// <summary>
        /// 创建数组, 并使用指定的值填充数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dimension">数组维度</param>
        /// <param name="value">指定的值</param>
        /// <returns>创建好的数组</returns>
        public static T[] NewArrayWithValue<T>(int dimension, T value) {
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
		/// <param name="dimension">数组维度</param>
		/// <param name="computer">值计算函数, 数组索引作为传入参数</param>
		/// <returns>创建好的数组</returns>
		public static T[] NewArrayWithValue<T>(int dimension, Func<T> computer) {
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
        /// <param name="dimension">数组维度</param>
        /// <param name="computer">值计算函数, 数组索引作为传入参数</param>
		/// <returns>创建好的数组</returns>
        public static T[] NewArrayWithValue<T>(int dimension, Func<int, T> computer) {
            T[] result = new T[dimension];
            for (int i = 0; i < result.Length; i++) {
                result[i] = computer(i);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 对两个集合进行同步迭代, 对每一对元素进行操作.
        /// </summary>
        /// <typeparam name="T1">第1个集合里面元素的类型</typeparam>
        /// <typeparam name="T2">第2个集合里面元素的类型</typeparam>
        /// <param name="collection1">第1个集合</param>
        /// <param name="collection2">第2个集合</param>
        /// <param name="actionWithRef">对每对元素的引用所执行的操作</param>
        /// <exception cref="TwoIEnumerableCountNotMatchException">第1个集合的元素数大于第2个集合的元素数时会抛出 CountNotMatchException 异常.</exception>
        public static void ForEachPair<T1, T2>(T1[] collection1, T2[] collection2, ActionWithRef<T1, T2> actionWithRef) {
            for (int i = 0; i < collection1.Length; i++) {
                if (i > collection2.Length - 1) throw new DimensionNotMatchException();
                actionWithRef(ref collection1[i], ref collection2[i]);
            }
        }

    }
}
