using System.IO;
using System.IO.Compression;
using System.Text;

namespace ijw.Next.IO.Compression {
    /// <summary>  
    /// 压缩帮助类
    /// </summary>  
    public static class CompressHelper {
        /// <summary>  
        /// Deflate压缩字符串  
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static byte[] CompressDeflate(string str) => CompressDeflate(Encoding.UTF8.GetBytes(str));

        /// <summary>  
        /// Deflate压缩二进制  
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static byte[] CompressDeflate(byte[] str) {
            var ms = new MemoryStream(str) { Position = 0 };
            var outms = new MemoryStream();
            using (var deflateStream = new DeflateStream(outms, CompressionMode.Compress, true)) {
                var buf = new byte[1024];
                int len;
                while ((len = ms.Read(buf, 0, buf.Length)) > 0)
                    deflateStream.Write(buf, 0, len);
            }
            return outms.ToArray();
        }

        /// <summary>  
        /// Deflate解压二进制  
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static byte[] DecompressDeflate(byte[] str) {
            var ms = new MemoryStream(str) { Position = 0 };
            var outms = new MemoryStream();
            using (var deflateStream = new DeflateStream(ms, CompressionMode.Decompress, true)) {
                var buf = new byte[1024];
                int len;
                while ((len = deflateStream.Read(buf, 0, buf.Length)) > 0)
                    outms.Write(buf, 0, len);
            }
            return outms.ToArray();
        }

        /// <summary>
        /// GZip压缩字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] CompressGZip(string input) => CompressGZip(Encoding.UTF8.GetBytes(input));

        /// <summary>
        /// GZip压缩字节数组
        /// </summary>
        /// <param name="inputBytes"></param>
        public static byte[] CompressGZip(byte[] inputBytes) {
            using var outStream = new MemoryStream();
            using var zipStream = new GZipStream(outStream, CompressionMode.Compress, true);
            zipStream.Write(inputBytes, 0, inputBytes.Length);
            close(zipStream);
            return outStream.ToArray();
        }

        private static void close(GZipStream zipStream) {
#if NETSTANDARD1_4
            zipStream.Dispose();
#else
            zipStream.Close(); //很重要，必须关闭，否则无法正确解压
#endif
        }

#if !NET35
        /// <summary>
        /// GZip解压缩字节数组
        /// </summary>
        /// <param name="inputBytes"></param>
        public static byte[] DecompressGZip(byte[] inputBytes) {

            using MemoryStream inputStream = new MemoryStream(inputBytes);
            using MemoryStream outStream = new MemoryStream();
            using GZipStream zipStream = new GZipStream(inputStream, CompressionMode.Decompress);
            zipStream.CopyTo(outStream);
            close(zipStream);
            return outStream.ToArray();
        }

        /// <summary>
        /// GZip压缩文件
        /// </summary>
        /// <param name="fileToCompress"></param>
        public static void CompressGZip(FileInfo fileToCompress) {
            using FileStream originalFileStream = fileToCompress.OpenRead();
            if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz") {
                using var compressedFileStream = File.Create(fileToCompress.FullName + ".gz");
                using var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress);
                originalFileStream.CopyTo(compressionStream);
            }
        }

        /// <summary>
        /// GZip解压缩文件
        /// </summary>
        /// <param name="fileToDecompress"></param>
        public static void DecompressGZip(FileInfo fileToDecompress) {
            using FileStream originalFileStream = fileToDecompress.OpenRead();
            string currentFileName = fileToDecompress.FullName;
            string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

            using var decompressedFileStream = File.Create(newFileName);
            using var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);
            decompressionStream.CopyTo(decompressedFileStream);
        }
#endif
    }
}