using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ijw.Next.Collection {
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableStringExt {
        /// <summary>
        /// 将一组字符串连接起来, 形成新的字符串
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="connector"></param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> strings, string connector = "") {
            StringBuilder sb = new();
            
            foreach (var s in strings) {
                sb.Append(s);
                sb.Append(connector);
            }

            if (strings.Any()) {
                sb.RemoveLast(connector);
            }

            return sb.ToString();
        }
    }
}