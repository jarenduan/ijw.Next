using ijw.Next.Maths;
using System;
using Xunit;

namespace ijw.Next.Maths.xTest {
    public class VectorTest {
        [Fact]
        public void CrossProductTest() {
            Vector v1 = new Vector(1d, 2d, 3d);
            Vector v2 = new Vector(3d, 4d, 5d);

            var v = v1.GetCrossProduct(v2);
            var answer = new Vector(-2, 4, -2);
            Assert.Equal(v, answer);


            Vector v3 = new Vector(2d, 2d, 2d);
            Vector v4 = new Vector(1d, 2d, 4d);

            v = v3.GetCrossProduct(v4);
            answer = new Vector(4, -6, 2);
            Assert.Equal(v, answer);

            Vector v5 = new Vector(2d, 1d, -1d);
            Vector v6 = new Vector(1d, -1d, 2d);

            v = v5.GetCrossProduct(v6);
            answer = new Vector(1, -5, -3);
            Assert.Equal(v, answer);
        }

        [Fact]
        public void DistanceTest() {
            Vector v1 = new Vector(0.1, 0.1, 0.1);
            Vector v2 = new Vector(0.1, 0.2, 0.3);
            Vector w = new Vector(0.3, 0.3, 0.4);

            double d = v1.EuclideanDistanceFrom(v2, w);


            var s = 0.3 * (0.1 - 0.1) * (0.1 - 0.1) + 0.3 * (0.1 - 0.2) * (0.1 - 0.2) + 0.4 * (0.1 - 0.3) * (0.1 - 0.3);
            var answer = Math.Pow(s, 1d / 2);

            Assert.Equal(answer, d, 10);
        }
    }
}
