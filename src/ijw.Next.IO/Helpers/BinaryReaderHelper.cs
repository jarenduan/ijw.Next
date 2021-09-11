using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 
    /// </summary>
    public static class BinaryReaderHelper {
        /// <summary>
        /// 获取流读取器
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码. null为系统默认编码</param>
        /// <returns></returns>
        public static BinaryReader NewBinaryReaderFromStream(Stream stream, Encoding? encoding = null)
            => encoding is null ? new BinaryReader(stream) : new BinaryReader(stream, encoding);

        /// <summary>
        /// 使用指定的编码打开指定文件.请使用using.
        /// </summary>
        /// <param name="fileinfo">文件信息</param>
        /// <param name="encoding">编码, null为使用默认编码</param>
        /// <returns>流读取器</returns>
        public static BinaryReader NewBinaryReaderFromFileInfo(FileInfo fileinfo, Encoding? encoding = null)
            => NewBinaryReaderFromFile(fileinfo.FullName, encoding);

        /// <summary>
        /// 使用指定的编码方式打开指定文件.请使用using.
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="encoding">文本编码方式</param>
        /// <returns>流读取器</returns>
        public static BinaryReader NewBinaryReaderFromFile(string filepath, Encoding? encoding = null) 
            => NewBinaryReaderFromStream(new FileStream(filepath, FileMode.Open, FileAccess.Read));
    }
}
