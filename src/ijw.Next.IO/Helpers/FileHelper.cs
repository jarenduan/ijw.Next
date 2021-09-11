using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 文件相关的帮助方法
    /// </summary>
    public class FileHelper {
        /// <summary>
        /// 使用指定的编码向指定的文件写入字符串,可选追加或者创建/覆盖.
        /// </summary>
        /// <param name="filepath">写入文件</param>
        /// <param name="content">写入的内容</param>
        /// <param name="append">是否追加. true为追加, false为创建或覆盖</param>
        /// <param name="encoding">指定的编码方法</param>
        public static void WriteStringToFile(string filepath, string content, bool append = false, Encoding? encoding = null) {
            using var writer = StreamWriterHelper.NewStreamWriterByFilepath(filepath, append, encoding);
            writer.Write(content);
            writer.Flush();
        }

        /// <summary>
        /// 使用指定的编码向指定的文件写入二进制,可选追加或者创建/覆盖.
        /// </summary>
        /// <param name="filepath">写入文件</param>
        /// <param name="content">写入的内容</param>
        /// <param name="append">是否追加. true为追加, false为创建或覆盖</param>
        /// <param name="encoding">指定的编码方法</param>
        public static void WriteBytesToFile(string filepath, byte[] content, bool append = false, Encoding? encoding = null) {
            using var writer = BinaryWriterHelper.NewBinaryWriterByFilepath(filepath, append, encoding);
            writer.Write(content);
            writer.Flush();
        }

        /// <summary>
        /// 按通配符拷贝多个文件.
        /// </summary>
        /// <param name="sourceDir">源文件夹</param>
        /// <param name="destinationDir">目标文件夹</param>
        /// <param name="pattern">通配符</param>
        /// <param name="copyOption">是否递归复制所有子目录中的文件</param>
        /// <param name="overwrite">是否覆盖, 设为false后遇到同名文件会抛出异常</param>
        /// <param name="tryHarderWhenOverwrite">true, 将会尝试重命名源文件后复制, 尽量覆盖被占用的文件.</param>
        /// <returns>字符串数组, 包含拷贝文件的源路径全名称</returns>
        public static string[] CopyFiles(string sourceDir, string destinationDir, string pattern = "*.*", SearchOption copyOption = SearchOption.TopDirectoryOnly, bool overwrite = true, bool tryHarderWhenOverwrite = false) {
            List<string> copied = new();
            try {
                DirectoryInfo srcDir = new(sourceDir);
                var srcDirFullName = srcDir.FullName;

                DirectoryInfo destDir = new(destinationDir);
                var destDirFullName = destDir.FullName;

                var srcfiles = Directory.GetFiles(srcDirFullName, pattern, copyOption);
                //保证目标目录(子目录)存在
                if (srcfiles.Length is 0 && !destDir.Exists) destDir.Create();

                foreach (var srcFullName in srcfiles) {
                    var destFileFullName = srcFullName.Replace(srcDirFullName, destDirFullName);
                    try {
                        var destFileInfo = new FileInfo(destFileFullName);
                        //保证目标目录(子目录)存在
                        var currentDestDir = destFileInfo.Directory;
                        if (!currentDestDir.Exists) currentDestDir.Create();

                        File.Copy(srcFullName, destFileFullName, overwrite);
                        copied.Add(srcFullName);
                    }
                    catch (IOException ex) when (ex.Message.Contains("being used by another process")) {
                        if (tryHarderWhenOverwrite) {
                            string newName = getAvailableName(destFileFullName);
                            File.Move(destFileFullName, newName);
                            File.Copy(srcFullName, destFileFullName, overwrite);
                            copied.Add(srcFullName);
                        }
                    }
                }

                return copied.ToArray();
            }
            catch (Exception ex) { 
                throw new BatchCopyNotFinishedException(copied.ToArray(), ex);
            }
        }

        private static string getAvailableName(string srcFullName) => $"~~{srcFullName}";

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="files">文件全路径名称数组</param>
        /// <returns>实际删除的文件个数</returns>
        public static int DeleteFiles(IEnumerable<string> files) {
            int deleted = 0;
            foreach (var f in files) {
                try {
                    File.Delete(f);
                    deleted++;
                }
                catch {
                    continue;
                }
            }
            return deleted;
        }

        /// <summary>
        /// 删除指定目录的所有符合通配符的文件.
        /// </summary>
        /// <param name="dir">目录名</param>
        /// <param name="pattern">通配符</param>
        /// <returns>删除的文件数量</returns>
        public static int DeleteFiles(string dir, string pattern = "*.*") {
            var files = Directory.GetFiles(dir, pattern);
            return DeleteFiles(files);
        }

        /// <summary>
        /// 删除指定文件夹下面所有指定时间之前的符合通配符的所有文件
        /// </summary>
        /// <param name="dir">指定的文件夹</param>
        /// <param name="olderThan">指定的时间段</param>
        /// <param name="pattern">通配符</param>
        /// <returns>实际删除的文件数量</returns>
        public static int DeleteFiles(string dir, TimeSpan olderThan, string pattern = "*.*") {
            var files = getFileNames(dir, pattern, olderThan);
            return DeleteFiles(files);
        }

        private static string[] getFileNames(string dir, string pattern, TimeSpan olderThan) {
            var files = Directory.GetFiles(dir, pattern);
            var q = from f in files
                    where (DateTime.Now - f.AsFileInfo().LastWriteTime) > olderThan
                    select f;
            return q.ToArray();
        }
    }
}