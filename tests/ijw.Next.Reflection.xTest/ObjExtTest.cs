using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ijw.Next.Reflection.xTest {
    public class ObjExtTest {
        [Fact]
        public void SetPropertyTest() {
            TestClassA a = new TestClassA();

            a.SetPropertyValue("Name", "B");

            Assert.Equal("B", a.Name);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                a.SetPropertyValue("name", "b")
            );
        }

        private class TestClassA {
            public string Name { get; set; } = "A";
            public int Age { get; set; } = 12;
            public void SayHello() {
                Console.WriteLine("Hello");
            }
        }
    }
}
