using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

namespace ijw.Next.xTest {
    public class StringExtTest {
        [Fact]
        public void ToFloatAnywayTest() {
            var f = "12.1".ToFloatAnyway();
            Assert.Equal(12.1f, f);

            f = "1e-10".ToFloatAnyway();
            Assert.Equal(1e-10f, f, 8);

            f = float.NaN.ToString().ToFloatAnyway(-1);
            Assert.Equal(-1f, f);

            f = "NaN".ToFloatAnyway(-2);
            Assert.Equal(-2f, f);

            f = "Infinity".ToFloatAnyway(-3);
            Assert.Equal(-3f, f);
        }

        [Fact]

        public void TestFindAllChineseNumberStrings() {
            StringBuilder sb = new StringBuilder();
            foreach (var numStr in GetAllComination()) {
                var cNumStr = numStr.ToInt64Anyway().ToChineseNumberString();
                sb.Append(cNumStr).Append('.');
                var num = numStr.ToInt64Anyway();
                var cNum = cNumStr.ParseChineseNumberToInt();
                if (num != cNum) {
                    Console.Write(numStr + "=>" + cNumStr + ": ");
                    Console.WriteLine($"{num} == {cNum} {num == cNum}");
                }
            }

            var s = sb.ToString();

            var answer = GetAllComination().GetEnumerator();
            answer.MoveNext();
            foreach (var cNumStr in s.FindAllChineseNumberStrings()) {
                answer.MoveNext();
                var answer_cNumStr = answer.Current.ToInt64Anyway().ToChineseNumberString();
                Assert.Equal(answer_cNumStr, cNumStr);
            }

            static IEnumerable<string> GetAllComination() => getAllComination(14);

            static IEnumerable<string> getAllComination(int digits) {
                if (digits == 0) {
                    yield return "";
                    yield break;
                }
                var next = digits - 1;
                foreach (var item in getAllComination(next)) {
                    yield return $"{item}{0}";
                    yield return $"{item}{(digits % 10 == 0 ? 1 : digits % 10)}";
                }
            }
        }
    }
}
