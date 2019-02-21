using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// A pair of index and value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexValuePair<T> {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="value">Value</param>
        public IndexValuePair(int index, T value) {
            Index = index;
            Value = value;
        }

        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Value
        /// </summary>
        public T Value { get; }
    }
}
