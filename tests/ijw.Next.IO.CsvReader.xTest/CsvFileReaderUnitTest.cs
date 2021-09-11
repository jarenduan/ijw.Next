using System;
using System.IO;
using System.Linq;
using Xunit;

namespace ijw.Next.IO.CsvReader.xTest {
    public class CsvFileReaderUnitTest {
        [Fact]
        public void ReadObjectsTest() {
            var dir = Environment.CurrentDirectory + "\\..\\..\\..\\testfiles";
            var files = Directory.GetFiles(dir, "test2.csv");
            if (files is null || files.Length == 0) {
                throw new FileNotFoundException(dir + " has no test2.csv");
            }
            else {
                CsvFileReader cfr = new CsvFileReader(files[0]) {
                    IsFirstLineHeader = true
                };
                var vec = cfr.ReadObjects<CsvTestClass>().ToArray();
                Assert.Equal(47, vec.Length);
            }

            dir = Environment.CurrentDirectory + "\\..\\..\\..\\testfiles";
            files = Directory.GetFiles(dir, "test1.csv");
            if (files is null || files.Length == 0) {
                throw new FileNotFoundException(dir + " has no test1.csv");
            }
            else {
                CsvFileReader cfr = new CsvFileReader(files[0]) {
                    IsFirstLineHeader = true
                };
                var vec = cfr.ReadObjects<CsvTestClass>().ToArray();
                Assert.Equal(27, vec.Length);
            }
        }
    }
}