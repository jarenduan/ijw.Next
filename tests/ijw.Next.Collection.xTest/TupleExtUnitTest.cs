using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ijw.Next.Collection.xTest
{
    public class TupleExtUnitTest {
        [Fact]
        public void TupleForEachTwoRefUnitTest() {
            int[] array1 = { 0, 1 };
            int[] array2 = { 10, 100 };

            (array1, array2).ForEachPair((ref int i1, ref int i2) => {
                i1 = (i1 + 1) * i2;
            });

            Assert.Equal(10, array1[0]);
            Assert.Equal(200, array1[1]);
        }

        [Fact]
        public void TupleForEachThreeRefUnitTest() {
            int[] array1 = { 0, 1 };
            int[] array2 = { 10, 100 };
            int[] array3 = { 4, 40 };

            (array1, array2,array3).ForEachThree((ref int i1, ref int i2, ref int i3) => {
                i1 = (i1 + 1) * i2 - i3;
            });

            Assert.Equal(6, array1[0]);
            Assert.Equal(160, array1[1]);
        }
    }
}