using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ijw.Next.Reflection.xTest {
    public class UnitTestStringExt {
        [Fact]
        public void TestTo() {
            int i = 100;
            var x1 = "100".To<int>();
            Assert.Equal(i, x1);

            var y1 = "09".ToNullable<int>();
            Assert.Equal(9, y1.Value);

            var y2 = "".ToNullable<int>();
            Assert.Null(y2);
        }
    }
}