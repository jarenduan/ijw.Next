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
        /// <param name="encoding">编码</param>
        /// <param name="append">是否追加模式</param>
        /// <returns>创建的StreamWriter</returns>
        public static StreamWriter NewStreamWriterByFilepath(string filepath, Encoding encoding, bool append = false) {
            FileStream fs = new FileStream(filepath, append ? FileMode.Append : FileMode.Create);
            return new StreamWriter(fs, encoding);
        }

        /// <summary>
        /// 根据指定的文件路径和默认的Unicode编码创建StreamWriter.
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="append">是否追加模式</param>
        /// <returns>创建的StreamWriter</returns>
        public static StreamWriter NewStreamWriterByFilepath(string filepath, bool append = false) {
            return NewStreamWriterByFilepath(filepath, Encoding.Unicode, append);
        }
    }
}