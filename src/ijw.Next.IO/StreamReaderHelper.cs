using System.IO;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// StreamReader的帮助类
    /// </summary>
    public class StreamReaderHelper {
        /// <summary>
        /// 使用 UTF8 编码打开指定文件.请使用using.
        /// </summary>
        /// <param name="fileinfo">文件信息</param>
        /// <returns>流读取器</returns>
        public static StreamReader NewStreamReaderFromFileInfo(FileInfo fileinfo) 
            => NewStreamReaderFromFileInfo(fileinfo, Encoding.UTF8);

        /// <summary>
        /// 使用 UTF8 编码打开指定文件.请使用using.
        /// </summary>
        /// <param name="fileinfo">文件信息</param>
        /// <param name="encoding">编码</param>
        /// <returns>流读取器</returns>
        public static StreamReader NewStreamReaderFromFileInfo(FileInfo fileinfo, Encoding encoding) 
            => NewStreamReaderFromFile(fileinfo.FullName, encoding);

        /// <summary>
        /// 使用 UTF8 编码打开指定文件.请使用using.
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>流读取器</returns>
        public static StreamReader NewStreamReaderFromFile(string filepath) 
            => NewStreamReaderFromFile(filepath, Encoding.UTF8);

        /// <summary>
        /// 使用指定的编码方式打开指定文件.请使用using.
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="encoding">文本编码方式</param>
        /// <returns></returns>
        public static StreamReader NewStreamReaderFromFile(string filepath, Encoding encoding) {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            return new StreamReader(fs, encoding);
        }
    }
}
