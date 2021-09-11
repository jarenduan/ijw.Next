using System.IO;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// StreamWriter的帮助类
    /// </summary>
    public class StreamWriterHelper {
        /// <summary>
        /// 根据指定的文件路径和编码创建StreamWriter.
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="append">是否追加模式</param>
        /// <param name="encoding">编码</param>
        /// <returns>创建的StreamWriter</returns>
        public static StreamWriter NewStreamWriterByFilepath(string filepath, bool append = false, Encoding? encoding = null) {
            FileStream fs = new FileStream(filepath, append ? FileMode.Append : FileMode.Create);
            return NewStreamWriterToStream(fs, encoding);
        }

        /// <summary>
        /// 根据指定的流和编码创建StreamWriter.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码</param>
        /// <returns>创建的StreamWriter</returns>
        public static StreamWriter NewStreamWriterToStream(Stream stream, Encoding? encoding = null) {
            encoding ??= Encoding.Unicode;
            return new StreamWriter(stream, encoding);
        }
    }
}