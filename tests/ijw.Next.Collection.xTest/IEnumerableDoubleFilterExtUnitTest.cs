using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ijw.Next.Collection.xTest
{
    public class IEnumerableDoubleFilterExtUnitTest
    {
        [Fact]
        public void MedianFilterForOddCountCollectionTest() {
            double[] s = { 0d, 1d, 2d, 3d, 6.5d, 5d, 6d, 3d, 8d};

            var filtered = s.FilteringWithWindowMedian(3).ToArray();

            Assert.Equal(9, filtered.Length);

            Assert.Equal(0d, filtered[0]);
            Assert.Equal(1d, filtered[1]);
            Assert.Equal(2d, filtered[2]);
            Assert.Equal(3d, filtered[3]);
            Assert.Equal(5d, filtered[4]);
            Assert.Equal(6d, filtered[5]);
            Assert.Equal(5d, filtered[6]);
            Assert.Equal(6d, filtered[7]);
            Assert.Equal(8d, filtered[8]);
        }

        [Fact]
        public void MedianFilterForEvenCountCollectionTest() {
            double[] s = { 0d, 1d, 2d, 3d, 6.5d, 5d, 6d, 3d, 8d, 9d};

            var filtered = s.FilteringWithWindowMedian(4).ToArray();

            Assert.Equal(10, filtered.Length);

            Assert.Equal(0d, filtered[0]);
            Assert.Equal(1d, filtered[1]);
            Assert.Equal(1.5d, filtered[2]);
            Assert.Equal(2.5d, filtered[3]);
            Assert.Equal(4d, filtered[4]);
            Assert.Equal(5.5d, filtered[5]);
            Assert.Equal(5.5d, filtered[6]);
            Assert.Equal(5.5d, filtered[7]);
            Assert.Equal(7d, filtered[8]);
            Assert.Equal(9d, filtered[9]);
        }
    }
}
