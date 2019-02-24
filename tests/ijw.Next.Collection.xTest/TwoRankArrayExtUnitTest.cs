using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ijw.Next.Collection.xTest {
    public class TwoRankArrayExtUnitTest {
        [Fact]
        public void TestSetEachForNonNullValueType() {
            int[,] array = new int[2, 2];

            array.SetEach(0);
            array.SetEach((i,j) => 10);

            //you can't set int[,] with null. uncomment the 2 lines below, you will get error and warning.
            //array.SetEach(null);
            //array.SetEach((i, j) => null);

            array.Clear();
        }

        [Fact]
        public void TestSetEachForNullableValueType() {
            int?[,] array = new int?[2, 2];

            //an int?[,] is just like an int[,].
            array.SetEach(0);
            array.SetEach((i, j) => 10);

            //in addition, you can set null into int?[,] directly,
            array.SetEach(value: null);

            //or with func.
            array.SetEach((i, j) => {
                if (i + j > 2) {
                    return i + j;
                }
                else {
                    return null;
                }
            });

            array.Clear();
        }

        [Fact]
        public void TestSetEachForNonNullRefType() {
            string[,] array = new string[2, 2];

            //you can set a string to each
            array.SetEach("");
            array.SetEach((i, j) => "");

            //but when you set it with null, you get warnings:
            //array.SetEach(value: null);
            //array.SetEach((i, j) => null);

            //for reference types, you could always use explicit methods without warnings:
            array.SetEachNullable(value: null);
            array.SetEachNullable((i, j) => null);

            array.Clear();
        }

        [Fact]
        public void TestSetEachForNullableRefType() {
            string?[,] array = new string?[2, 2];

            //you can do everything with NullableRefType:
            array.SetEach("");
            array.SetEach(value: null);
            array.SetEach((i, j) => "");
            array.SetEach((i, j) => null);

            //the explicit method are still ok, of course:
            array.SetEachNullable(value: null);
            array.SetEachNullable((i, j) => null);

            array.Clear();
        }
    }
}