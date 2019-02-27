using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ijw.Next.Collection.xTest {
    public class IEnumerableNumberExtUnitTest {
        [Fact]
        public void MedianFilterForOddCountCollectionTest() {
            double[] s = { 0d, 1d, 2d, 3d, 6.5d, 5d, 6d, 3d, 8d };

            var filtered = s.FilterWithWindowMedian(3).ToArray();

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
            double[] s = { 0d, 1d, 2d, 3d, 6.5d, 5d, 6d, 3d, 8d, 9d };

            var filtered = s.FilterWithWindowMedian(4).ToArray();

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

        [Fact]
        public void LimitingNaNFilter() {
            float[] r = { 1.2f, 2, 3.1f, 5, 0.5f, float.NaN, 2, 1 };
            r = r.FilteringNaN().ToArray();
            float[] exp = { 1.2f, 2, 3.1f, 5, 0.5f, 2, 1 };

            Assert.Equal(exp, r);
        }

        [Fact]
        public void FilterWithAmplifyLimitationTest() {
            float[] r = { 1.2f, 2, 3.1f, 5, 0.5f, 2, 1 };
            r = r.FilterWithAmplifyLimitation(3).ToArray();
            float[] exp = { 1.2f, 2, 3.1f, 5, 5, 2, 1 };

            Assert.Equal(exp, r);
        }

        [Fact]
        public void VarianceTest() {
            var set = ijw.Next.ML.Samples.SampleHelper.LoadSampleSetFrom(@"testdata\顺北1-8H-整米录井数据.csv");
            var v = set.Columns[0].Variance();
        }
    }
}
