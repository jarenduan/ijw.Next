using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ijw.Next.xTest {
    public class ContractTest {
        [Fact]
        public void ShouldBeNotNullTest() {
            //Value type never will be null, uncomment line below, you'll get errors.
            //int i = 10;
            //i.ShouldBeNotNull();

            int? ii = 10;
            ii.ShouldBeNotNull();

            string s = "";
            s.ShouldBeNotNull();

            string? ss = null;
            Assert.Throws<NullReferenceException>(() => ss.ShouldBeNotNull());
        }

        [Fact]
        public void MustBeNotNullTest() {
            string s = "";
            s.MustNotNull();
        }
    }
}
