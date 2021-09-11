using System.IO;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// StreamReader的帮助类
    /// </summary>
    public class StreamReaderHelper {
        /// <summary>
        /// 获取流读取器
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码. null为系统默认编码</param>
        /// <returns></returns>
        public static StreamReader NewStreamReaderFromStream(Stream stream, Encoding? encoding = null)
            => encoding is null ? new StreamReader(stream) : new StreamReader(stream, encoding);

        /// <summary>
        /// 使用指定的编码打开指定文件.请使用using.
        /// </summary>
        /// <param name="fileinfo">文件信息</param>
        /// <param name="encoding">编码</param>
        /// <returns>流读取器</returns>
        public static StreamReader NewStreamReaderFromFileInfo(FileInfo fileinfo, Encoding? encoding = null) 
            => NewStreamReaderFromFile(fileinfo.FullName, encoding);

        /// <summary>
        /// 使用指定的编码方式打开指定文件.请使用using.
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="encoding">文本编码方式</param>
        /// <returns>流读取器</returns>
        public static StreamReader NewStreamReaderFromFile(string filepath, Encoding? encoding = null) {
            FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            return NewStreamReaderFromStream(stream, encoding);
        }
    }
}