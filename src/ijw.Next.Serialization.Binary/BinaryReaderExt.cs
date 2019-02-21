using System;
using System.IO;

namespace ijw.Next.Serialization.Binary {
    /// <summary>
    /// BinaryReader的扩展方法
    /// </summary>
    public static class BinaryReaderExt {
        /// <summary>
        /// 使用前4个字节作为对象长度从流中取回二进制形式序列化的对象. 没有解析成功, 会抛出异常.
        /// </summary>
        /// <typeparam name="T">取回对象的类型</typeparam>
        /// <param name="reader"></param>
        /// <returns>返回解析到的对象.</returns>
        public static T RetrieveBinaryObject<T>(this BinaryReader reader) {
            var size = getObjectSize(reader);
            return reader.RetrieveBinaryObject<T>(size);
        }

        /// <summary>
        /// 使用指定长度从流中取回二进制形式序列化的对象. 没有解析成功, 会抛出异常.
        /// </summary>
        /// <typeparam name="T">取回对象的类型</typeparam>
        /// <param name="reader"></param>
        /// <param name="length">指定取回的对象大小.</param>
        /// <returns>返回解析到的对象.</returns>
        public static T RetrieveBinaryObject<T>(this BinaryReader reader, int length) {
            length.ShouldLargerThan(0);
            DebugHelper.WriteLine(string.Format("Try reading object (length: {0})...", length));
            byte[] objBytes = reader.ReadBytes(length);
            var result = BinarySerializationHelper.Deserialize<T>(objBytes);
            DebugHelper.WriteLine("Object retrieved.");
            return result;
        }

        private static int getObjectSize(BinaryReader reader) {
            DebugHelper.Write("Read 4 bytes as object size...");
            byte[] objLenBytes = reader.ReadBytes(4);
            return BitConverter.ToInt32(objLenBytes, 0);
        }
    }
}