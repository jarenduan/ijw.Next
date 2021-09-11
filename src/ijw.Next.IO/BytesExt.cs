using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 
    /// </summary>
    public static class BytesExt {
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename">文件名</param>
        /// <param name="append">是否追加模式</param>
        /// <param name="encoding">写入的编码</param>
        public static void WriteToFile(this byte[] content, string filename, bool append = false, Encoding? encoding = null)
            => FileHelper.WriteBytesToFile(filename, content, append, encoding);
    }
}
