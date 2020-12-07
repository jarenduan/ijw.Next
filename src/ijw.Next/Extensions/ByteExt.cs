using System;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 
    /// </summary>
    public static class ByteExt {
        /// <summary>
        /// 连接另一个byte数组
        /// </summary>
        /// <param name="head"></param>
        /// <param name="body">另一个数组</param>
        /// <returns>连接后的新的数组</returns>
        public static byte[] Combine(this byte[] head, byte[] body) {
            var buffer = new byte[head.Length + body.Length];
            Buffer.BlockCopy(head, 0, buffer, 0, head.Length);
            Buffer.BlockCopy(body, 0, buffer, head.Length, body.Length);
            return buffer;
        }

        /// <summary>
        /// 输出成16进制字符串, 形如: "FF 00 12 EF".
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>16进制字符串</returns>
        public static string ToHexString(this byte[] buffer) {
            var sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++) {
                sb.Append(buffer[i].ToString("X2")).Append(" ");
            }
            return sb.ToString();
        }
    }
}
