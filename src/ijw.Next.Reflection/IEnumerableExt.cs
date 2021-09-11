using System;
using System.Collections;
using System.Collections.Generic;

namespace ijw.Next.Reflection {
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableExt {
        /// <summary>
        /// 生成泛型List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public static IList ToGenericList<T>(this IEnumerable<T> enumerable, Type itemType) {
            enumerable.ShouldBeNotNull();
            return makeGenericList(itemType, enumerable);
        }

        private static IList makeGenericList<T>(Type itemType, IEnumerable<T> items) {
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

            if (list is not IList ilist) throw new CannotCreateGenericListException(itemType);

            foreach (var o in items)
                ilist.Add(o);

            return ilist;
        }
    }
}
