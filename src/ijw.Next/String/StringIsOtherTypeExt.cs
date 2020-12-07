using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 
    /// </summary>
    public static class StringIsOtherTypeExt {
        #region  Is number type
        /// <summary>
        /// 是否能解析成32位整数
        /// </summary>
        /// <param name="aString"></param>
        /// <returns>是返回true, 反之返回false</returns>
        public static bool IsInteger32(this string aString) {
            return int.TryParse(aString, out int i);
        }

        /// <summary>
        /// 是否能解析成单精度浮点数
        /// </summary>
        /// <param name="aString"></param>
        /// <returns></returns>
        public static bool IsSingle(this string aString) {
            return float.TryParse(aString, out var f);
        }

        /// <summary>
        /// 是否能解析成双精度浮点数
        /// </summary>
        /// <param name="aString"></param>
        /// <returns></returns>
        public static bool IsDouble(this string aString) {
            return double.TryParse(aString, out var d);
        }

        /// <summary>
        /// 是否能解析成日期时间类型
        /// </summary>
        /// <param name="aString"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string aString) {
            return DateTime.TryParse(aString, out var d);
        }

        #endregion
    }
}
