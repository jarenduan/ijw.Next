using System.IO;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 
    /// </summary>
    public static class BinaryWriterHelper {
        /// <summary>
        /// 根据指定的流和编码创建StreamWriter.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码</param>
        /// <returns>创建的StreamWriter</returns>
        public static BinaryWriter NewBinaryWriterToStream(Stream stream, Encoding? encoding = null)
            => encoding is null ? new BinaryWriter(stream) : new BinaryWriter(stream, encoding);
        /// <summary>
        /// 根据指定的文件路径和编码创建BinaryWriter.
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="append">是否追加模式</param>
        /// <param name="encoding">编码</param>
        /// <returns>创建的BinaryWriter</returns>
        public static BinaryWriter NewBinaryWriterByFilepath(string filepath, bool append = false, Encoding? encoding = null) {
            var fs = new FileStream(filepath, append ? FileMode.Append : FileMode.Create);
            return NewBinaryWriterToStream(fs);
        }
    }
}
