using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ijw.Next.IO {
    /// <summary>
    /// 
    /// </summary>
    public static class BytesExt {
        /// <summary>
        /// 以默认的Unicode编码写入文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename">文件名</param>
        /// <param name="append">是否是追加模式</param>
        public static void WriteToFile(this byte[] content, string filename, bool append = false) {
            content.WriteToFile(filename, Encoding.Unicode);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename">文件名</param>
        /// <param name="encoding">写入的编码</param>
        /// <param name="append">是否追加模式</param>
        public static void WriteToFile(this byte[] content, string filename, Encoding encoding, bool append = false) {
            FileMode filemode = append ? FileMode.Append : FileMode.Create;
            FileStream file = new FileStream(filename, filemode);
            using (BinaryWriter writer = new BinaryWriter(file, encoding)) {
                writer.Write(content);
                writer.Flush();
            }
        }
    }
}
