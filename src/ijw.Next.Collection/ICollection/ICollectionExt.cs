using System.Collections.Generic;

namespace ijw.Next.Collection {
    /// <summary>
    /// 对ICollection{T}的一些扩展方法
    /// </summary>
    public static class ICollectionExt {
        /// <summary>
        /// 将一个集合分割为两个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="ratioOfFirstGroup">一个整数, 第一个集合占的比例</param>
        /// <param name="ratioOfSecondGroup">一个整数, 第二个集合占的比例</param>
        /// <param name="method">分割方法</param>
        /// <param name="firstGroup">第一个集合</param>
        /// <param name="secondGroup">第二个集合</param>
        /// <remarks>
        /// Returning IList is bad design, so this is not recommended in net40+, use tuple-returning instead.
        /// </remarks>
        public static void DivideByRatioAndMethod<T>(this ICollection<T> collection, int ratioOfFirstGroup, int ratioOfSecondGroup, CollectionDividingMethod method, out IList<T> firstGroup, out IList<T> secondGroup) =>
            //List<T> first = new(), second = new();
            //divide(collection, method, ratioOfFirstGroup, ratioOfSecondGroup, first, second);
            //firstGroup = first;
            //secondGroup = second;
            (firstGroup, secondGroup) = DivideByRatioAndMethod(collection, method, ratioOfFirstGroup, ratioOfSecondGroup);

        /// <summary>
        /// 将一个集合分割为两个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="method">分割方法</param>
        /// <param name="ratioOfFirstGroup">一个整数, 第一个集合占的比例</param>
        /// <param name="ratioOfSecondGroup">一个整数, 第二个集合占的比例</param>
        /// <returns></returns>
        public static (List<T> firstGroup, List<T> secondGroup) DivideByRatioAndMethod<T>(this ICollection<T> collection, CollectionDividingMethod method, int ratioOfFirstGroup, int ratioOfSecondGroup) {
            List<T> first = new(), second = new();
            divide(collection, method, ratioOfFirstGroup, ratioOfSecondGroup, first, second);
            return (first, second);

            void divide<T>(IEnumerable<T> source, CollectionDividingMethod method, int ratioOfFirstGroup, int ratioOfSecondGroup, List<T> first, List<T> second) {
                if (method == CollectionDividingMethod.Random) {
                    source = source.Shuffle();
                }

                source.ForEach((element, index) => {
                    if (index % (ratioOfFirstGroup + ratioOfSecondGroup) < ratioOfFirstGroup) {
                        first.Add(element);
                    }
                    else {
                        second.Add(element);
                    }
                });
            }
        }
    }
}