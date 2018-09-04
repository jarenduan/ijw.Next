using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection.Indexable {
    /// <summary>
    /// 实现Indexable的List{T}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class IndexableList<T> : List<T>, IIndexable<T> {
    }
}
