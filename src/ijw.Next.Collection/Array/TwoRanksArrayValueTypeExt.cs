using System;


namespace ijw.Next.Collection {
    /// <summary>
    /// 值类型二维数组的扩展方法
    /// </summary>
    public static class TwoRanksArrayValueTypeExt {
        /// <summary>
        /// 根据指定的方法设置每个单元格中的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="function"></param>
        public static void SetEach<T>(this T[,] array, Func<int, int, T> function) where T : struct {
            array.ForEachRefWithIndex(delegate (ref T item, int i, int j) {
                item = function(i, j);
            });
        }

        /// <summary>
        /// 根据指定的方法设置每个单元格中的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="function"></param>
        public static void SetEach<T>(this T?[,] array, Func<int, int, T?> function) where T : struct {
            array.ForEachRefWithIndex(delegate (ref T? item, int i, int j) {
                item = function(i, j);
            });
        }

        /// <summary>
        /// 清空数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void Clear<T>(this T[,] array) where T : struct {
            array.ForEachRef(delegate (ref T item) {
                item = default;
            });
        }

        /// <summary>
        /// 清空数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void Clear<T>(this T?[,] array) where T : struct {
            array.ForEachRef(delegate (ref T? item) {
                item = default;
            });
        }
    }
}