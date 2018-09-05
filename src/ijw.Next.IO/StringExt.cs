using System.IO;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 
    /// </summary>
    public static class StringExt {
        /// <summary>
        /// 获取字符串代表的FileInfo. 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>指代的FileInfo</returns>
        /// <exception cref="FileNotFoundException">字符串不是有效的文件路径时, 抛出此异常. </exception>
        /// <remarks>即使返回有效的FileInfo, 也并不保证之后文件仍继续存在, 应考虑使用try-catch进行后续操作. </remarks>
        public static FileInfo AsFileInfo(this string filepath) {
            filepath.MustExistSuchFile();
            FileInfo fi = new FileInfo(filepath);
            return fi;
        }

        /// <summary>
        /// 使用指定编码将字符串写入指定文本文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filepath">写入的文件</param>
        /// <param name="encoding">写入使用的编码方式</param>
        /// <param name="append">是否追加, true追加, false新建或覆盖</param>
        public static void WriteToFile(this string content, string filepath, Encoding encoding, bool append = false) {
            FileHelper.WriteStringToFile(filepath, content, encoding, append);
        }

        /// <summary>
        /// 使用Unicode编码将字符串写入指定文本文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filepath">写入的文件</param>
        /// <param name="append">是否追加, true追加, false新建或覆盖</param>
        public static void WriteToFile(this string content, string filepath, bool append = false) {
            FileHelper.WriteStringToFile(filepath, content, append);
        }
    }
}
