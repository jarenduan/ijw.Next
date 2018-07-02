using System.IO;

namespace ijw.Next.Serialization.Binary {
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExt {
        /// <summary>
        /// 序列化为字节数组
        /// </summary>
        /// <param name="objToSave"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this object objToSave) {
            return BinarySerializationHelper.Serialize(objToSave);
        }
        /// <summary>
        /// 把对象序列化到二进制流当中
        /// </summary>
        /// <param name="objToSave">欲保存的对象(大小&lt;=4GB)</param>
        /// <param name="stream">写入的流</param>
        /// <returns>流的长度</returns>
        public static int ToBinaryStream(this object objToSave, Stream stream) {
            return BinarySerializationHelper.Serialize(objToSave, stream);
        }

        /// <summary>
        /// 把对象序列化到二进制文件中
        /// </summary>
        /// <param name="objToSave">欲保存的对象</param>
        /// <param name="filename">包含路径的文件名</param>
        public static void ToBinaryFile(this object objToSave, string filename) {
            BinarySerializationHelper.Serialize(objToSave, filename);
        }
    }
}