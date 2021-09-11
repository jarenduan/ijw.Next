using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ijw.Next.Collection.xTest {
    public class IEnumerableExtUnitTest {
        [Fact]
        public void ForEachWindowTest() {
            string[] s = { "a", "b", "c", "d", "e" , "f"};
            var windows = s.ForEachWindow(3).ToArray();
            Assert.Equal(4, windows.Length);
            Assert.Equal(windows[0], new string[] { "a", "b", "c" });
            Assert.Equal(windows[1], new string[] { "b", "c", "d" });
            Assert.Equal(windows[2], new string[] { "c", "d", "e" });
            Assert.Equal(windows[3], new string[] { "d", "e", "f" });
        }

       

        [Fact]
        public void NullFilterTest() {
            string[] s = { "a", "b", "c", "d", "e", "f" };
            var r = s.SkipNull().ToArray();

            string?[] ss = { "a", "b", "c", "d", null, "f" };
            var rr = ss.SkipNull().ToArray();

            int?[] sss = { 1, 2, 3, null, 5 };
            var rrr = sss.SkipNull().ToArray();

            //for non-null value Type, it is invalid to call NullFilter method.
            //Uncomment below lines, you'll get errors.
            //int[] i = { 2, 3, 4 };
            //var rrrr = i.NullFilter().ToArray();

            Assert.Equal(6, r.Length);
            Assert.Equal(5, rr.Length);
            Assert.Equal(4, rrr.Length);
        }
    }
}