using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace ijw.Next {
    /// <summary>
    /// 提供对string类型的若干扩展方法
    /// </summary>
    public static class StringExt {
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
        /// 添加短格式的当前时间前缀, 使用[20121221 132355]这样的形式.
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="beforePrefix">前缀之前的字符串, 默认是"["</param>
        /// <param name="afterPrefix">前缀之前的字符串, 默认是"] "</param>
        /// <returns>添加短格式时间前缀后的字符串</returns>
        [Obsolete("use PrefixShortDateTime instead")]
        public static string PrefixWithNowShortTimeLabel(this string astring, string beforePrefix = "[", string afterPrefix = "] ") {
            var now = DateTime.Now;
            return $"{beforePrefix}{now.Year:D4}{now.Month:D2}{now.Day:D2} {now.Hour:D2}{now.Minute:D2}{now.Second:D2}{afterPrefix}{astring}";
        }


        /// <summary>
        /// 添加可简单定制化的当前日期时间前缀, 使用[2012-12-21 13:23:55]这样的形式.
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="seperatorDate">日期分隔符, 默认是"-"</param>
        /// <param name="seperatorTime">时间分隔符, 默认是":"</param>
        /// <param name="seperatorBetween">日期与时间之间的分隔符, 默认是一个空格</param>
        /// <param name="beforePrefix">前缀开始符, 默认是"["</param>
        /// <param name="afterPrefix">前缀结束符, 默认是"]"加一个空格</param>
        /// <returns>添加短格式时间前缀后的字符串</returns>
        public static string PrefixShortDateTime(this string astring, string seperatorDate = "-", string seperatorTime = ":", string seperatorBetween = " ", string beforePrefix = "[", string afterPrefix = "] ") {
            var now = DateTime.Now;
            return $"{beforePrefix}{now.Year:D4}{seperatorDate}{now.Month:D2}{seperatorDate}{now.Day:D2}{seperatorBetween}{now.Hour:D2}{seperatorTime}{now.Minute:D2}{seperatorTime}{now.Second:D2}{afterPrefix}{astring}";
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
        /// 获取指定的字符子串(不包含)后面的字符串. 如"1234".SubstringAfter("2"), 返回"34".
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="afterThis">指定的目标子串</param>
        /// <returns>从目标子串最后一个字符之后开始一直到结尾的所有字符组成的字符串. 如无子串抛出异常.</returns>
        public static string SubstringAfter(this string astring, string afterThis) {
            var index = astring.IndexOf(afterThis);
            if (index == -1) throw new ArgumentOutOfRangeException(nameof(afterThis));
            return astring.Substring(index + afterThis.Length);
        }

        /// <summary>
        /// 获取指定的字符子串(不包含)的第一个实例的前面的字符串. 如"1234".SubstringAfter("3"), 返回"12".
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="beforeThis">指定的目标子串</param>
        /// <returns>从第一个字符到目标子串地一个字符之前的所有字符组成的字符串. 如无子串抛出异常.</returns>
        public static string SubstringBefore(this string astring, string beforeThis) {
            var index = astring.IndexOf(beforeThis);
            if (index == -1) throw new ArgumentOutOfRangeException(nameof(beforeThis));
            return astring.Substring(0, index);
        }

        /// <summary>
        /// 获取指定的字符子串(不包含)之间的字符串. 如"12345".SubstringAfter("2", "4"), 返回"3".
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="afterThis">指定的起始目标子串</param>
        /// <param name="beforeThis">指定的终止目标子串</param>
        /// <returns>从起始子串后面到终止子串前面, 之间所有的字符组成的字符串. 如无子串抛出异常.</returns>
        public static string SubstringBetween(this string astring, string afterThis, string beforeThis) {
            var start = astring.IndexOf(afterThis);
            if (start == -1) throw new ArgumentOutOfRangeException(nameof(afterThis));
            var end = astring.IndexOf(beforeThis);
            if (end == -1) throw new ArgumentOutOfRangeException(nameof(beforeThis));

            return astring.Substring(start + afterThis.Length, end - start - afterThis.Length);
        }

        /// <summary>
        /// 重复指定次数. 如"Abc".Repeat(3) 返回 "AbcAbcAbc".
        /// </summary>
        /// <param name="astring"></param>
        /// <param name="times">重复次数, 必须大于0</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(this string astring, int times) {
            times.ShouldNotLessThan(0);
            return times switch
            {
                0 => string.Empty,
                1 => astring,
                _ => repeat(astring, times)
            };

            static string repeat(string astring, int times) {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < times; i++) {
                    result.Append(astring);
                }
                return result.ToString();
            }
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
        public static string Remove(this string theString, string toRemove) 
            => toRemove.IsNullOrEmpty() ? theString : theString.Replace(toRemove, string.Empty);

        /// <summary>
        /// 移除指定的字符串
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="toRemove">一系列欲移除的字符串</param>
        /// <returns>如果存在字串, 将字串用空替换后返回；反之, 返回原字符串</returns>
        public static string Remove(this string theString, params string[] toRemove) {
            string result = theString;
            foreach (var s in toRemove) {
                if (s.IsNullOrEmpty()) continue;
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
                if (endString.IsNullOrEmpty()) continue;
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
            number.ShouldBeNotLessThanZero();
            if (number == 0) {
                return aString;
            }
            else {
                return aString.Remove(aString.Length - number, number);
            }
        }

        /// <summary>
        /// 统计含有多少指定的子字符串
        /// </summary>
        /// <param name="parentString"></param>
        /// <param name="subString">字符串</param>
        /// <returns>含有的数量</returns>
        public static int ContainsHowMany(this string parentString, string subString) {
            parentString.ShouldBeNotNull();
            subString.ShouldBeNotNullArgument(nameof(subString));

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
            aString.ShouldBeNotNull();
            aChar.ShouldBeNotNullArgument(nameof(aChar));

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
        /// 将中文数字转换阿拉伯数字
        /// </summary>
        /// <param name="cnum">汉字数字</param>
        /// <returns>长整型阿拉伯数字</returns>
        public static long ParseChineseNumberToInt(this string cnum) {
            cnum = cnum.Replace("零十", "零一十");
            cnum = Regex.Replace(cnum, "\\s+", "");
            if (cnum.StartsWith("十")) {
                cnum = $"一{cnum}";
            }
            long firstUnit = 1;//一级单位                
            long secondUnit = 1;//二级单位 
            long result = 0;//结果
            for (var i = cnum.Length - 1; i > -1; --i)//从低到高位依次处理
            {
                var tmpUnit = charToUnit(cnum[i]);//临时单位变量
                if (tmpUnit > firstUnit)//判断此位是数字还是单位
                {
                    firstUnit = tmpUnit;//是的话就赋值,以备下次循环使用
                    secondUnit = 1;
                    if (i == 0)//处理如果是"十","十一"这样的开头的
                    {
                        result += firstUnit * secondUnit;
                    }
                    continue;//结束本次循环
                }
                if (tmpUnit > secondUnit) {
                    secondUnit = tmpUnit;
                    continue;
                }
                var num = charToNumber(cnum[i]);
                // if (firstUnit == 10 && num == 0) num = 1;
                result += firstUnit * secondUnit * num;//如果是数字,则和单位想乘然后存到结果里
            }
            return result;

            static long charToNumber(char c) => c switch
            {
                '一' => 1,
                '二' => 2,
                '三' => 3,
                '四' => 4,
                '五' => 5,
                '六' => 6,
                '七' => 7,
                '八' => 8,
                '九' => 9,
                '零' => 0,
                _ => -1,
            };

            static long charToUnit(char c) => c switch
            {
                '十' => 10,
                '百' => 100,
                '千' => 1000,
                '万' => 10000,
                '亿' => 100000000,
                '兆' => 1000000000000,
                _ => 1,
            };
        }
        /// <summary>
        /// 找到所有中文数字字符串. 贪婪策略, 即会尽量找到最长的数字再找下一个.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>所有的中文数字字符串集合</returns>
        public static IEnumerable<string> FindAllChineseNumberStrings(this string s) {
            var n = "万亿兆";
            string numStr = string.Empty;
            int preU2 = -1;
            int currU2 = -1;

            for (int j = 0; j < s.Length; j++) {
                var curr = s[j];
                currU2 = n.IndexOf(curr);

                if (preU2 == -1 && currU2 == -1) { //如果之前没有2级单位, 当前也不是2级单位
                    if (!numStr.IsNullOrEmpty()) yield return numStr;
                    numStr = Regex.Match(s.Substring(j), _PATTERN_XXXX).Value;
                    if (numStr.IsNullOrEmpty()) yield break;
                    j = s.IndexOf(numStr, j) + numStr.Length - 1;
                }
                else if (preU2 == -1 && currU2 != -1) { //当前是2级单位
                    if (numStr.IsNullOrEmpty()) { //前面没有数字, 2级单位无效.
                        continue;
                    }
                    else {
                        numStr += curr; //是一个有效的2级单位.
                        string c = search(s, j, curr);
                        if (c.IsNullOrEmpty()) {
                            yield return numStr;//数字结束了, 直接返回
                            numStr = string.Empty; //清空缓存
                            preU2 = -1;
                        }
                        else {
                            numStr += c;
                            preU2 = currU2;
                            j += c.Length;
                        }
                    }
                }
                else if (preU2 != -1 && currU2 == -1) {
                    yield return numStr;//数字结束了, 直接返回
                    numStr = string.Empty; //清空缓存
                    preU2 = -1;
                }
                else if (preU2 != -1 && currU2 != -1) {
                    if (currU2 >= preU2) { //2级单位次序不对
                        yield return numStr; //数字结束了, 直接返回
                        numStr = string.Empty; //清空缓存
                        preU2 = -1;
                    }
                    else {
                        numStr += curr; //是一个有效的2级单位.
                        string c = search(s, j, curr);
                        if (c.IsNullOrEmpty()) {
                            yield return numStr;//数字结束了, 直接返回
                            numStr = string.Empty; //清空缓存
                            preU2 = -1;
                        }
                        else {
                            numStr += c;
                            preU2 = currU2;
                            j += c.Length;
                        }
                    }
                }
            }

            if (!numStr.IsNullOrEmpty()) yield return numStr;

            //local func:
            static string search(string s, int j, char curr)
                => (curr == '万' ? Regex.Match(s.Substring(j + 1), _PATTERN_1XXXX) : Regex.Match(s.Substring(j + 1), _PATTERN_0_XXXX))
                .Value;
        }

        /// <summary>
        /// 表示缩进. 三个空格组成
        /// </summary>
        private static readonly string _INDENT = "   ";

        private static readonly string _PATTERN_XXXX = ("@千@百@十@?|" + //1234, 1230
                                               "@千@百(零@)?|" + //1204, 1200
                                       "@千零@?十@?|" + //1034, 1030
                                       "@千(零@)?|" + //1004, 1000

                                       "@百@?十@?|" + //234, 230
                                       "@百(零@)?|" + //204

                                       "@?十@?|" + //34, 30 

                                       "@")  //4
                                       .Replace("@", "[一二三四五六七八九]");

        private static readonly string _PATTERN_1XXXX = ($"^(" +
                                                "@千@百@十@?|" + //1234, 1230
                                        "@千@百(零@)?|" + //1204, 1200
                                        "@千零@?十@?|" + //1034, 1030
                                        "@千(零@)?|" + //1004, 1000

                                        "零@百@?十@?|" + //0234, 0230
                                        "零@百(零@)?|" + //0204

                                        "零@?十@?|" + //034, 030 

                                        "零@)")  //04
                                        .Replace("@", "[一二三四五六七八九]");

        private static readonly string _PATTERN_0_XXXX = $"^零?({_PATTERN_XXXX})";
    }
}