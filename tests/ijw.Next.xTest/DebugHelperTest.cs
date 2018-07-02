using System;
using Xunit;

namespace ijw.Next.xTest {
    public class DebugHelperTest {
#if NET452
        [Fact]
        public void TestGetCallerName() {
            var n = test();
            Assert.Equal("DebugHelperTest.TestGetCallerName", n);
        }

        private static string test() {
            return DebugHelper.GetCallerName();
        }
#endif
    }
}