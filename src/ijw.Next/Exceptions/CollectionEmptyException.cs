using System;
using System.Collections.Generic;

namespace ijw.Next {
    public class CollectionEmptyException<T> : Exception {
        private readonly IEnumerable<T> _collection;

        public CollectionEmptyException(IEnumerable<T> collection) {
            this._collection = collection;
        }

        public CollectionEmptyException(string message) : base(message) {
        }

        public CollectionEmptyException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}