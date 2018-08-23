using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    public static class ListExt {
        /// <summary>
        /// 从指定位置开始移除当前及其之后的所有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index">指定的索引处</param>
        /// <returns>移除的数目</returns>
        public static int RemoveRange<T>(this List<T> list, int index) {
            index.ShouldNotLargerThan(list.Count - 1);
            int removeCount = list.Count - index;

            list.RemoveRange(index, removeCount);

            return removeCount;
        }
    }
}
