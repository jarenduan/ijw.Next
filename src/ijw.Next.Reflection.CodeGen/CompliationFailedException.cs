using System;
using System.Runtime.Serialization;

namespace ijw.Next.Reflection.CodeGen {
    [Serializable]
    public class CompliationFailedException : Exception {
        public CompliationFailedException() {
        }

        public CompliationFailedException(string message) : base(message) {
        }

        public CompliationFailedException(string message, Exception innerException) : base(message, innerException) {
        }

        protected CompliationFailedException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}