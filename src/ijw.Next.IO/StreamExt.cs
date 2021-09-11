using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 扩展Stream类, 以支持一些简便的读写功能.
    /// </summary>
    public static class StreamExt {
        #region Get reader & writer
        /// <summary>
        /// 获取流读取器
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码. null为系统默认编码</param>
        /// <returns></returns>
        public static StreamReader GetStreamReader(this Stream stream, Encoding? encoding = null)
            => StreamReaderHelper.NewStreamReaderFromStream(stream, encoding);

        /// <summary>
        /// 获取流读取器
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码. null为系统默认编码</param>
        /// <returns></returns>
        public static StreamWriter GetStreamWriter(this Stream stream, Encoding? encoding = null)
            => StreamWriterHelper.NewStreamWriterToStream(stream, encoding);

        /// <summary>
        /// 获取流读取器
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码. null为系统默认编码</param>
        /// <returns></returns>
        public static BinaryReader GetBinaryReader(this Stream stream, Encoding? encoding = null)
            => BinaryReaderHelper.NewBinaryReaderFromStream(stream, encoding);

        /// <summary>
        /// 获取流读取器
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码. null为系统默认编码</param>
        /// <returns></returns>
        public static BinaryWriter GetBinaryWriter(this Stream stream, Encoding? encoding = null)
            => BinaryWriterHelper.NewBinaryWriterToStream(stream, encoding);
        #endregion

        #region Read & Write String

        /// <summary>
        /// 使用指定的编码方式,调用StreamReader从流中读取全部的字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">编码方式</param>
        /// <returns>读取全部的字符串</returns>
        public static string ReadStringAndDispose(this Stream stream, Encoding? encoding = null) {
            using var r = stream.GetStreamReader(encoding);
            return r.ReadToEnd();
        }

        /// <summary>
        /// 用指定编码把字符串写入流, 并Dispose流.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="aString">欲写入的字符串</param>
        /// <param name="encoding">所用的编码</param>
        public static void WriteStringAndDispose(this Stream stream, string aString, Encoding? encoding = null) {
            using var writer = stream.GetStreamWriter(encoding);
            writer.Write(aString);
        }

        #endregion

        #region Read Bytes

        /// <summary>
        /// 使用指定的编码方式, 调用BinaryReader从流中读取指定长度的二进制数据
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="length">读取长度</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>读取的二进制数组</returns>
#if NETSTANDARD1_4
        public static byte[] ReadBytesAndDispose(this Stream stream, int length, Encoding? encoding = null) {
#else
        public static byte[] ReadBytesAndDispose(this Stream stream, long length, Encoding? encoding = null) {
#endif
            using var reader = stream.GetBinaryReader();
            return reader.ReadBytes(length);
        }

        /// <summary>
        /// 使用BinaryReader用指定的编码方式从流中读取全部的二进制数据.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding">使用的编码方式. null 为系统默认编码.</param>
        /// <returns>读取的字节数</returns>
        public static List<byte> ReadBytesAndDispose(this Stream stream, Encoding? encoding = null) {
            using var reader = stream.GetBinaryReader();
            return reader.ReadBytes();
        }

        #endregion

        #region Write Text File

        /// <summary>
        /// 使用指定的编码方式调用ReadStringByStreamReader方法读取流中的全部字符串, 然后使用指定编码覆盖或者追加到指定文件.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filename">写入的文件</param>
        /// <param name="append">是否追加. 默认是false</param>
        /// <param name="readEncoding">读取流用的编码</param>
        /// <param name="writeEncoding">写入文件的编码方式</param>
        public static int WriteToTextFileAndDispose(this Stream stream, string filename, bool append = false, Encoding? readEncoding = null, Encoding? writeEncoding = null) {
            var s = stream.ReadStringAndDispose(readEncoding);
            s.WriteToFile(filename, append, writeEncoding);
            return s.Length;
        }

        #endregion

        #region Write Binary File
        /// <summary>
        /// 使用指定的编码方式调用ReadBytesByBinaryReader方法读取流中指定长度的二进制数据, 然后使用指定编码覆盖(或追加)到指定文件.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filename">写入的文件</param>
        /// <param name="length">读取数据字节数</param>
        /// <param name="readEncoding">读取流时使用的编码方式</param>
        /// <param name="writeEncoding">写入文件时使用的编码方式</param>
        /// <param name="append">是否追加. 默认是false</param>
        /// <returns>读取到的二进制数组</returns>
#if NETSTANDARD1_4
        public static byte[] WriteToBinaryFile(this Stream stream, string filename, int length, Encoding readEncoding, Encoding writeEncoding, bool append = false) {
#else
        public static byte[] WriteToBinaryFileAndDispose(this Stream stream, string filename, long length, Encoding readEncoding, Encoding writeEncoding, bool append = false) {
#endif
            byte[] content = stream.ReadBytesAndDispose(length, readEncoding);
            content.WriteToFile(filename, append, writeEncoding);
            return content;
            //TODO: reimplement it, down below.
        }

        /// <summary>
        /// 使用指定的编码方式调用ReadBytesByBinaryReader方法读取流中的全部二进制数据, 然后使用指定编码覆盖或者追加到指定文件.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filename">写入的文件</param>
        /// <param name="readEncoding">读取流时使用的编码方式</param>
        /// <param name="writeEncoding">写入文件时使用的编码方式</param>
        /// <param name="append">是否追加. 默认是false</param>
        public static long WriteToBinaryFileAndDispose(this Stream stream, string filename, Encoding readEncoding, Encoding writeEncoding, bool append = false) {
            FileMode filemode = append ? FileMode.Append : FileMode.Create;
            long length = 0;
            FileStream file = new FileStream(filename, filemode);
            using (var writer = new BinaryWriter(file, writeEncoding)) {
                using (var reader = new BinaryReader(stream, readEncoding)) {
                    byte[] buffer = reader.ReadBytes(256);
                    //int i = 0;
                    while (buffer.Length > 0) {
                        length += buffer.Length;
                        writer.Write(buffer);
                        buffer = reader.ReadBytes(256);// read next 256 bytes
                        //Console.Write(i++);
                    }
                }
                writer.Flush();
            }
            return length;
        }
#endregion
    }
}