using ijw.Next.Collection;
using ijw.Next.Collection.Indexable;

namespace ijw.Next.Grid {
    public class Header<T> : Indexable<T> {
        public Header(T[] data) : base(data) {
        }
    }
}