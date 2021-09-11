using System;

namespace ijw.Next.Reflection {
    internal class CannotCreateGenericListException : Exception {
        public CannotCreateGenericListException(Type itemType) {
        }

        public CannotCreateGenericListException(string message) : base(message) {
        }

        public CannotCreateGenericListException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}