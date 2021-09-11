using System;

namespace ijw.Next.Reflection {
    public class TypeNotSupportException : Exception {

        public TypeNotSupportException(Type type) => Type = type;

        public Type Type { get; }
    }
}