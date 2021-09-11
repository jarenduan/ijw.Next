using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 字符串序列扩展类
    /// </summary>
    public static class IEnumerableStringExt {
        /// <summary>
        /// 多行字符串顺序写入文件
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="filepath">文件路径</param>
        /// <param name="append">是否追加, 默认是不追加</param>
        /// <param name="encoding">写入编码, 默认是Unicode</param>
        public static void WriteToFile(this IEnumerable<string> enumerable, string filepath, bool append = false, Encoding? encoding = null) 
            => enumerable.WriteToFile(filepath.AsFileInfo(), append, encoding);

        /// <summary>
        /// 多行字符串顺序写入文件
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="fileInfo">文件信息</param>
        /// <param name="append">是否追加, 默认是不追加</param>
        /// <param name="encoding">写入编码, 默认是Unicode</param>
        public static void WriteToFile(this IEnumerable<string> enumerable, FileInfo fileInfo, bool append = false, Encoding? encoding = null) {
            using var writer = StreamWriterHelper.NewStreamWriterByFilepath(fileInfo.FullName, append, encoding);
            foreach (var line in enumerable) {
                writer.WriteLine(line);
            }
        }
    }
}