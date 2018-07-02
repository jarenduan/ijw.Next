#if !NET35
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexableArray<T> : IIndexable<T> {
        protected T[] _Array;
        public IndexableArray(int length) {
            this._Array = new T[length];
        }

        public T this[int i] {
            get => this._Array[i];
            set => this._Array[i] = value;
        }

        public int Count => this._Array.Length;

        public IEnumerator<T> GetEnumerator() {
            return this._Array.GetEnumeratorGenerics();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _Array.GetEnumerator();
        }
    }
}
#endif