using System.IO;

namespace ijw.Next.Serialization.Binary {
    /// <summary>
    /// fileInfo Ext
    /// </summary>
    public static class FileInfoExt {
        /// <summary>
        /// 从fileinfo代表的文件中进行二进制反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileInfo"></param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeBinaryObject<T>(this FileInfo fileInfo) {
            return BinarySerializationHelper.Deserialize<T>(fileInfo.FullName);
        }
    }
}
