#if !NET35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 实现Indexable的List{T}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexableList<T> : List<T>, IIndexable<T> {
    }
}
#endif