using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ijw.Next.xTest {
    public class IntegerExtTest
    {
        [Fact]
        public void ToTotalTest() {
            int[] numbers = new int[] {0, 1, 2, 3, 4, 5 };
            Assert.Equal(numbers, 0.ToTotal(6));

            numbers = new int[] { 2, 3, 4, 5 };
            Assert.Equal(numbers, 2.ToTotal(4));

            numbers = new int[] { 1, 2, 3, 4, 5 };
            Assert.Equal(numbers, 1.ToTotal(5));

            numbers = new int[] { 1 };
            Assert.Equal(numbers, 1.ToTotal(1));

            numbers = new int[] { 1 };
            Assert.Equal(numbers, 1.ToTotal(1));
        }

        [Fact]
        public void ToTest() {
            int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            Assert.Equal(numbers, 0.To(6));

            numbers = new int[] { 2, 3, 4, 5 };
            Assert.Equal(numbers, 2.To(5));

            numbers = new int[] { 1, 2, 3, 4, 5 };
            Assert.Equal(numbers, 1.To(5));

            numbers = new int[] { 1 };
            Assert.Equal(numbers, 1.To(1));

            numbers = new int[] { 0, 1 };
            Assert.Equal(numbers, 0.To(1));
        }
    }
}
