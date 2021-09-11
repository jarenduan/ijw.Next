using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 
    /// </summary>
    public static class FileInfoExt {
        /// <summary>
        /// Read all content from a file.
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="encoding">read encoding</param>
        /// <returns>all content</returns>
        public static string ReadToEnd(this FileInfo fi, Encoding? encoding = null) {
            using var reader = StreamReaderHelper.NewStreamReaderFromFileInfo(fi, encoding);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// read each line and line number from a file.
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static IEnumerable<(string line, int lineNum)> ReadLines(this FileInfo fi, Encoding? encoding = null) {
            using var reader = StreamReaderHelper.NewStreamReaderFromFileInfo(fi, encoding);
            foreach (var (line, lineNum) in reader.ReadLinesWithLineNumber()) {
                yield return (line, lineNum);
            }
        }

        /// <summary>
        /// 判断文件是否是视频文件. 根据扩展名判断.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>是视频文件, 返回true. 反之返回false.</returns>
        public static bool IsVideo(this FileInfo fileInfo) => fileInfo.ExtensionName().ToLower().In("mkv", "avi", "mp4", "rm", "rmvb", "wmv", "mpg", "mpeg", "mov", "ts");

        /// <summary>
        /// 返回文件扩展名. 没有"."
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>文件扩展名</returns>
        public static string ExtensionName(this FileInfo fileInfo) => fileInfo.Extension.Length == 0? "" : fileInfo.Extension.Substring(1);

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="newName">新文件名. 没有路径.</param>
        public static void Rename(this FileInfo fileInfo, string newName) {
            if (newName.Contains("\\")) {
                throw new ArgumentException("parameter contains '\\'!", nameof(newName));
            }
            File.Move(fileInfo.FullName, Path.Combine(fileInfo.DirectoryName ?? string.Empty, newName));
        }

        /// <summary>
        /// 计算文件的SHA1码
        /// </summary>
        /// <param name="fileinfo">要计算的文件</param>
        /// <returns>SHA1码</returns>
        public static string GetSHA1(this FileInfo fileinfo) {
            var alg = System.Security.Cryptography.SHA1.Create();
            using var s = fileinfo.CreateReadonlyStream();
            var bytes = alg.ComputeHash(s);
            return byteArrayToHexString(bytes);
        }

        /// <summary>
        /// 计算文件的SHA1码
        /// </summary>
        /// <param name="fileinfo">要计算的文件</param>
        /// <returns>SHA1码</returns>
        public static string GetMD5(this FileInfo fileinfo) {
            var alg = System.Security.Cryptography.MD5.Create();
            using var s = fileinfo.CreateReadonlyStream();
            var bytes = alg.ComputeHash(s);
            return byteArrayToHexString(bytes);
        }

        /// <summary>
        /// 字节数组转换为16进制表示的字符串
        /// </summary>
        private static string byteArrayToHexString(byte[] buf) => BitConverter.ToString(buf).Replace("-", "");

        /// <summary>
        /// 检查SHA1码是否匹配
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <param name="sha1">指定的SHA1码</param>
        /// <returns></returns>
        public static bool CheckSHA1(this FileInfo fileinfo, string sha1) 
            => fileinfo.CheckSHA1(sha1, out var _);

        /// <summary>
        /// 检查SHA1码是否匹配
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <param name="sha1">指定的SHA1码</param>
        /// <param name="sha1This">该文件的MD5码</param>
        /// <returns></returns>
        public static bool CheckSHA1(this FileInfo fileinfo, string sha1, out string sha1This)
            => (sha1This = fileinfo.GetSHA1()) == sha1;


        /// <summary>
        /// 检查MD5码是否匹配
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <param name="md5">指定的MD5码</param>
        /// <returns>是否匹配</returns>
        public static bool CheckMD5(this FileInfo fileinfo, string md5) 
            => fileinfo.CheckMD5(md5, out var _);

        /// <summary>
        /// 检查MD5码是否匹配
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <param name="md5">指定的MD5码</param>
        /// <param name="md5This">该文件的MD5码</param>
        /// <returns>是否匹配</returns>
        public static bool CheckMD5(this FileInfo fileinfo, string md5, out string md5This) 
            => (md5This = fileinfo.GetMD5()) == md5;

        /// <summary>
        /// 创建只读流
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>只读流</returns>
        public static Stream CreateReadonlyStream(this FileInfo fileInfo) => 
            new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read,  FileShare.ReadWrite);
    }
}