using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ijw.Next.Collection {
    /// <summary>
    /// 引用类型二维数组的扩展方法
    /// </summary>
    public static class TwoRanksArrayReferenceExt {
        /// <summary>
        /// 根据指定的方法设置每个单元格中的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="function"></param>
        public static void SetEach<T>(this T[,] array, Func<int, int, T> function) where T : class ? {
            array.ForEachRefWithIndex(delegate (ref T item, int i, int j) {
                item = function(i, j);
            });
        }

        /// <summary>
        /// 把每个单元格设为指定值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value">指定值</param>
        public static void SetEachNullable<T>(this T?[,] array, T? value) where T : class {
            array.ForEachRef(delegate (ref T? item) {
                item = value;
            });
        }

        /// <summary>
        /// 根据指定的方法设置每个单元格中的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="function"></param>
        public static void SetEachNullable<T>(this T?[,] array, Func<int, int, T?> function) where T : class {
            array.ForEachRefWithIndex(delegate (ref T? item, int i, int j) {
                item = function(i, j);
            });
        }
        /// <summary>
        /// 清空数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void Clear<T>(this T?[,] array) where T : class {
            array.ForEachRef(delegate (ref T? item) {
                item = null;
            });
        }
    }
}
