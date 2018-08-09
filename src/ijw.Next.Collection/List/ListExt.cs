using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection
{
    public static class ListExt
    {
        /// <summary>
        /// 从指定位置开始移除当前及其之后的所有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="index">指定的索引处</param>
        /// <returns>移除的数目</returns>
        public static int RemoveRange<T>(this List<T> collection, int index) {
            index.ShouldNotLargerThan(collection.Count - 1);
            int removeCount = collection.Count - index;

            collection.RemoveRange(index, removeCount);

            return removeCount;
        }
    }
}
