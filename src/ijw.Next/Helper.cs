using System;
using System.Collections.Generic;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 一些不好分类的帮助方法
    /// </summary>
    public static class Helper {
        /// <summary>
        /// 获取python风格的起止索引值
        /// </summary>
        /// <param name="length">总长度</param>
        /// <param name="start">计算得到的C#风格的起始索引</param>
        /// <param name="end">计算得到的C#风格的结束索引</param>
        /// <param name="startAtPython">启始索引. 该处字符将包括在返回结果中. 0代表第一个字符, 负数代表倒数第几个字符(-1表示倒数第一个字符), null等同于0. 默认值是0</param>
        /// <param name="endAtPython">结束索引. 该处字符将不包括在返回结果中. 0代表第一个字符, 负数代表倒数第几个字符(-1表示倒数第一个字符), null代表结尾. 默认值为null.</param>
        public static void PythonStartEndCalculator(int length, out int start, out int end, int? startAtPython = 0, int? endAtPython = null) {
            //endAt.ShouldNotEquals(0);
            //TODO: tuple returns in c# 7.0
            if (startAtPython == null) {
                startAtPython = 0;
            }
            else if (startAtPython < 0) {
                startAtPython = length + startAtPython;
            }

            if (endAtPython == null) {
                endAtPython = length - 1;
            }
            else if (endAtPython < 0) {
                endAtPython = length + endAtPython - 1;
            }
            else {
                endAtPython--;
            }

            //if (startAt > endAt) {
            //    throw new Exception("start index > end index.");
            //}

            start = startAtPython.Value;
            end = endAtPython.Value;
        }

        /// <summary>
        /// 输出一个包含集合中所有元素的字符串, 默认形如[a1, a2, a3, [a41, a42, a43], a5]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">指定的集合</param>
        /// <param name="separator">元素之间的分隔符, 默认是", "</param>
        /// <param name="prefix">第一个元素之前的字符串, 默认是"["</param>
        /// <param name="postfix">最后一个元素之后的字符串, 默认是"]"</param>
        /// <param name="transform">对于每个元素, 输出字符串之前进行一个操作.默认为null, 代表调用ToString().</param>
        /// <returns></returns>
        public static string ToAllEnumStrings<T>(IEnumerable<T> collection, string separator = ", ", string prefix = "[", string postfix = "]", Func<T, string> transform = null) {
            StringBuilder sb = new StringBuilder(prefix);
            foreach (var item in collection) {
                if (item is IEnumerable<T> ienum) {
                    sb.Append(ToAllEnumStrings(ienum, separator, prefix, postfix));
                }
                else {
                    string s = transform == null ? item.ToString() : transform(item);
                    sb.Append(s);
                }
                sb.Append(separator);
            }
            sb.RemoveLast(separator.Length);
            sb.Append(postfix);
            return sb.ToString();
        }
    }
}