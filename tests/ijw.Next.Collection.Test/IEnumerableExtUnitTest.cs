using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ijw.Next.Collection.Test {
    [TestClass]
    public class IEnumerableExtUnitTest {
        [TestMethod]
        [DataRow(new string[] { "a", "b", "c", "d", "e", "f" }, 3, 0, new string[] { "a", "b", "c" })]
        [DataRow(new string[] { "a", "b", "c", "d", "e", "f" }, 3, 1, new string[] { "b", "c", "d" })]
        [DataRow(new string[] { "a", "b", "c", "d", "e", "f" }, 3, 2, new string[] { "c", "d", "e" })]
        [DataRow(new string[] { "a", "b", "c", "d", "e", "f" }, 3, 3, new string[] { "d", "e", "f" })]
        public void ForEachWindowTest(string[] s, int windowLength, int windowIndex, string[] slice) {
            var windows = s.ForEachWindow(windowLength).ToArray();
            Assert.AreEqual(windows[windowIndex], slice);
        }
    }
}