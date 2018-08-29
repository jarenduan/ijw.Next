using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ijw.Next;

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
    }
}
