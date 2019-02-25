using System;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 提供对string类型的若干扩展方法
    /// </summary>
    public static class StringExt {
        /// <summary>
        /// 添加短格式的当前时间前缀, 使用[20121221 132355]这样的形式.
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="beforePrefix">前缀之前的字符串, 默认是"["</param>
        /// <param name="afterPrefix">前缀之前的字符串, 默认是"] "</param>
        /// <returns>添加短格式时间前缀后的字符串</returns>
        public static string PrefixWithNowShortTimeLabel(this string astring, string beforePrefix = "[", string afterPrefix = "] ") {
            var now = DateTime.Now;
            return $"{beforePrefix}{now.Year.ToString("D4")}{now.Month.ToString("D2")}{now.Day.ToString("D2")} {now.Hour.ToString("D2")}{now.Minute.ToString("D2")}{now.Second.ToString("D2")}{afterPrefix}{astring}";
        }

        /// <summary>
        /// 获取Python风格的子字符串
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="startIndex">启始索引. 该处字符将包括在返回结果中. 0代表第一个字符, 负数代表倒数第几个字符(-1表示倒数第一个字符), null等同于0. 默认值是0</param>
        /// <param name="endIndex">结束索引. 该处字符将不包括在返回结果中. 0代表第一个字符, 负数代表倒数第几个字符(-1表示倒数第一个字符), null代表结尾. 默认值为null.</param>
        /// <returns>子字符串</returns>
        public static string SubstringPythonStyle(this string astring, int? startIndex = null, int? endIndex = null) {
            IjwHelper.PythonStartEndCalculator(astring.Length, out int startAt, out int endAt, startIndex, endIndex);
            if (endAt < 0) {
                return string.Empty;
            }
            else {
                return astring.SubStringFromTo(startAt, endAt);
            }
        }

        /// <summary>
        /// 获取子串
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="fromIndex">起始索引（该位置字符也包括在字串中）</param>
        /// <param name="toIndex">结束索引（该位置字符也包括在字串中）</param>
        /// <returns>子字符串</returns>
        public static string SubStringFromTo(this string astring, int fromIndex, int toIndex) {
            fromIndex.ShouldBeNotLessThanZero();
            toIndex.ShouldBeNotLessThanZero();
            toIndex.ShouldLessThan(astring.Length);
            if (toIndex < fromIndex) {
                return string.Empty;
            }
            char[] result = new char[toIndex - fromIndex + 1];
            int j = 0;
            for (int i = fromIndex; i <= toIndex; i++) {
                result[j] = astring[i];
                j++;
            }
            return new string(result);
        }

        /// <summary>
        /// 重复指定次数. 如"Abc".Repeat(3) 返回 "AbcAbcAbc".
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="times">重复次数, 必须大于0</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(this string astring, int times) {
            times.ShouldLargerThan(0);
            if (times == 1) return astring;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < times; i++) {
                result.Append(astring);
            }
            return result.ToString();
        }

        /// <summary>
        /// 返回格式化后的json序列化字符串
        /// </summary>
        /// <param name="json"></param>
        /// <returns>格式化后的json序列化字符串</returns>
        public static String FormatJson(this String json) {
            StringBuilder result = new StringBuilder();
            string newLine = Environment.NewLine;
            int length = json.Length;
            int number = 0;
            bool inQuotion = false;
            char currChar = ' ';
            char? nextChar = ' ';

            //遍历输入字符串.  
            for (int i = 0; i < length; i++) {
                //获取当前字符和下一个字符.  
                currChar = json[i];
                if ((i + 1) < length) {
                    nextChar = json[i + 1];
                }
                else {
                    nextChar = null;
                }

                //双引号需要控制开合, 内部字符直接输出
                if(currChar == '\"') {
                    inQuotion = !inQuotion;
                }

                if(inQuotion) {
                    result.Append(currChar);
                    continue;
                }

                //如果当前字符是前方括号、前花括号做如下处理：  
                if ((currChar == '[') || (currChar == '{')) {
                    //（1）如果前面还有字符, 并且字符为“：”, 打印：换行和缩进字符字符串.  
                    //if ((i - 1 > 0) && (json[i - 1] == ':')) {
                    //    result.Append('\n');
                    //    result.Append(indent(number));
                    //}

                    //打印当前字符.  
                    result.Append(currChar);

                    //"{"、"["后面必须换行.  
                    result.Append(newLine);

                    //每出现一次开括号, 缩进次数增加一次.  
                    number++;
                    
                    //打印缩进.
                    result.Append(_INDENT.Repeat(number));

                    //进行下一次循环.  
                    continue;
                }

                //如果当前字符是闭括号  
                if ((currChar == ']') || (currChar == '}')) {
                    //"]"和"}"前面必须换行. 
                    result.Append(newLine);

                    //比括号出现, 缩进次数减少一次.  
                    number--;

                    //打印缩进.
                    result.Append(_INDENT.Repeat(number));

                    //打印当前字符.  
                    result.Append(currChar);

                    //如果当前字符后面还有字符, 并且字符不为“, ”, 打印：换行.  
                    if (nextChar != ',' && nextChar != '}' && nextChar != ']' && nextChar != null) {
                        result.Append(newLine);
                    }

                    //继续下一次循环.  
                    continue;
                }

                //如果当前字符是逗号.逗号后面换行, 并缩进, 不改变缩进次数.  
                if ((currChar == ',')) {
                    result.Append(currChar);
                    result.Append(newLine);
                    result.Append(_INDENT.Repeat(number));
                    continue;
                }

                //如果是":", 在后面输出一个空格
                if ((currChar == ':')) {
                    result.Append(currChar);
                    result.Append(' ');
                    continue;
                }

                //打印当前字符.  
                result.Append(currChar);
            }

            return result.ToString();
        }

        /// <summary>
        /// 删除指定的字符串
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="toRemove">欲删除的子串</param>
        /// <returns>如果存在字串, 将字串用空替换后返回；反之, 返回原字符串</returns>
        public static string Remove(this string theString, string toRemove) {
            return theString.Replace(toRemove, string.Empty);
        }

        /// <summary>
        /// 移除指定的字符串
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="toRemove">一系列欲移除的字符串</param>
        /// <returns>如果存在字串, 将字串用空替换后返回；反之, 返回原字符串</returns>
        public static string Remove(this string theString, params string[] toRemove) {
            string result = theString;
            foreach (var s in toRemove) {
                result = result.Remove(s);
            }
            return result;
        }

        /// <summary>
        /// 如果尾部是指定字符串之一, 则移除掉, 否则不做更改.常用于更动字符串中的文件扩展名.
        /// </summary>
        /// <param name="aString"></param>
        /// <param name="toRemove">指定的一系列字符串</param>
        /// <returns>移除尾部指定字符串的结果</returns>
        public static string RemoveLast(this string aString, params string[] toRemove) {
            foreach (var endString in toRemove) {
                if (aString.EndsWith(endString)) {
                    return aString.RemoveLast(endString.Length);
                }
            }
            return aString;
        }

        /// <summary>
        /// 从后向前删除指定数量的字符
        /// </summary>
        /// <param name="aString"></param>
        /// <param name="number">删除数量</param>
        /// <returns></returns>
        public static string RemoveLast(this string aString, int number) {
            return aString.Remove(aString.Length - number, number);
        }

        #region To Other Type Anyway

        /// <summary>
        /// 尝试转换成int. 如果失败将返回defaultNumer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultNumer">转换失败时返回的值, 默认是0</param>
        /// <returns>转换后的int</returns>
        public static int ToIntAnyway(this string s, int defaultNumer = 0) {
            if (int.TryParse(s, out int i)) {
                return i;
            }
            else {
                return defaultNumer;
            }
        }

        /// <summary>
        /// 尝试转换成float. 如果NaN或转换失败将返回defaultNumer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultNumer">转换失败时返回的值, 默认是0</param>
        /// <returns>转换后的float</returns>
        public static float ToFloatAnyway(this string s, float defaultNumer = 0f) {
            var result = defaultNumer;
            if (float.TryParse(s, out float i)) {
                if (!float.IsNaN(i)) {
                    result = i;
                }
            }
            return result;
        }

        /// <summary>
        /// 尝试转换成double. 如果NaN或转换失败将返回defaultNumer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultNumer">转换失败时返回的值, 默认是0</param>
        /// <returns>转换后的float</returns>
        public static double ToDoubleAnyway(this string s, double defaultNumer = 0d) {
            var result = defaultNumer;
            if (double.TryParse(s, out double i)) {
                if (!double.IsNaN(i)) {
                    result = i;
                }
            }
            return result;
        }

        /// <summary>
        /// 尝试转换成double. 如果NaN或转换失败将返回defaultNumer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultNumer">转换失败时返回的值, 默认是0</param>
        /// <returns>转换后的float</returns>
        public static decimal ToDecimalAnyway(this string s, decimal defaultNumer = 0m) {
            var result = defaultNumer;
            if (decimal.TryParse(s, out decimal i)) {
                result = i;
            }
            return result;
        }

        /// <summary>
        /// 把字符串转换成指定的枚举, 如果转换失败返回指定的缺省值.
        /// </summary>
        /// <typeparam name="T">转换的枚举类型</typeparam>
        /// <param name="aString">此字符串</param>
        /// <param name="ignoreCase">转换时是否忽略大小写, 默认不忽略</param>
        /// <param name="defaultValue">可选参数, 表示转换失败的时候所取的缺省值, 默认是枚举的0值</param>
        /// <returns>转换后的枚举值</returns>
        /// <exception cref="ArgumentException">
        /// 指定的类型不是枚举类型时, 将抛出此异常. (Wish C# support "where T: Enum" to avoid this at compilation time)
        /// </exception>
        public static T ToEnumAnyway<T>(this string aString, bool ignoreCase = false, T defaultValue = default) where T : struct {
            T result = defaultValue;
#if (NET35)
            Type t = typeof(T);
            if (!t.IsEnum) {
                throw new ArgumentException($"{t.Name} is not a enumeration type.");
            }
            try {
                result = (T)Enum.Parse(typeof(T), aString);
            }
            catch {
                result = defaultValue;
            }
#else
            if (Enum.TryParse<T>(aString, out T e)) {
                result = e;
            }
#endif
            return result;
        }

        /// <summary>
        /// 把字符串转换成指定的枚举, 如果转换失败返回指定的缺省值.
        /// </summary>
        /// <typeparam name="T">转换的枚举类型</typeparam>
        /// <param name="aString">此字符串</param>
        /// <param name="ignoreCase">转换时是否忽略大小写, 默认不忽略</param>
        /// <returns>转换后的枚举值</returns>
        ///// <exception cref="ArgumentException">
        ///// 指定的类型不是枚举类型时, 将抛出此异常. (Wish C# support "where T: Enum" to avoid this at compilation time)
        ///// </exception>
        public static T ToEnum<T>(this string aString, bool ignoreCase = false) where T : Enum 
            => (T)Enum.Parse(typeof(T), aString, ignoreCase);
        #endregion

        #region  Is other type
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

        #region Is Null Or Empty Or space

        /// <summary>
        /// 判断是否是Null或者空字符串
        /// </summary>
        /// <param name="aString"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string aString) {
            return string.IsNullOrEmpty(aString);
        }

        /// <summary>
        /// 判断是否是Null或空字符串或全是空格
        /// </summary>
        /// <param name="aString"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string aString) {
            if (aString is null) {
                return true;
            }
            for (int i = 0; i < aString.Length; i++) {
                if (!char.IsWhiteSpace(aString[i])) {
                    return false;
                }
            }
            return true;
        }

        #endregion New Region

        /// <summary>
        /// 统计含有多少指定的子字符串
        /// </summary>
        /// <param name="parentString"></param>
        /// <param name="subString">字符串</param>
        /// <returns>含有的数量</returns>
        public static int ContainsHowMany(this string parentString, string subString) {
            parentString.ShouldBeNotNullArgument();
            subString.ShouldBeNotNullArgument();

            int pLength = parentString.Length;
            if (pLength == 0) {
                return 0;
            }
            int sLength = subString.Length;
            if (sLength == 0) {
                return 0;
            }

            int count = 0;

            for (int i = 0; i < pLength; i += sLength) {
                bool isContains = false;
                for (int j = 0; j < sLength; j++) {
                    if (i + j >= pLength) {
                        break;
                    }
                    isContains = parentString[i + j] == subString[j];
                    if (!isContains) {
                        break;
                    }
                }
                if (isContains) {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// 统计含有多少指定的字符
        /// </summary>
        /// <param name="aString"></param>
        /// <param name="aChar">字符</param>
        /// <returns>含有的数量</returns>
        public static int ContainsHowMany(this string aString, char aChar) {
            aString.ShouldBeNotNullArgument();

            int pLength = aString.Length;
            if (pLength == 0) {
                return 0;
            }

            int count = 0;

            for (int i = 0; i < pLength; i++) {
                if (aString[i] == aChar) {
                    count++;
                }
            }

            return count;
        }

        internal static string AppendOrdinalPostfix(this string s) {
            switch (s) {
                case "11":
                    return "11th";
                case "12":
                    return "12th";
                case "13":
                    return "13th";
                default:
                    break;
            }
            if (s.EndsWith("1")) {
                return s + "st";
            }
            else if (s.EndsWith("2")) {
                return s + "nd";
            }
            else if (s.EndsWith("3")) {
                return s + "rd";
            }
            else {
                return s + "th";
            }
        }

        /// <summary>
        /// 表示缩进. 三个空格组成
        /// </summary>
        private static readonly string _INDENT = "   ";
    }
}