using System;
using System.IO;
using System.Linq;
using Xunit;

namespace ijw.Next.IO.CsvReader.xTest {
    public class CsvFileReaderUnitTest {
        [Fact]
        public void ReadObjectsTest() {
            CsvFileReader cfr = new CsvFileReader();
            var dir = Environment.CurrentDirectory + "\\..\\..\\..\\testfiles";
            var files = Directory.GetFiles(dir, "test2.csv");
            if (files == null || files.Length == 0) {
                throw new FileNotFoundException(dir + " has no test2.csv");
            }
            else {
                cfr.CsvFilePath = files[0];
                cfr.IsFirstLineHeader = true;
                var vec = cfr.ReadObjects<CsvTestClass>();
                Assert.Equal(47, vec.Count());
            }

            dir = Environment.CurrentDirectory + "\\..\\..\\..\\testfiles";
            files = Directory.GetFiles(dir, "test1.csv");
            if (files == null || files.Length == 0) {
                throw new FileNotFoundException(dir + " has no test1.csv");
            }
            else {
                cfr.CsvFilePath = files[0];
                cfr.IsFirstLineHeader = true;
                var vec = cfr.ReadObjects<CsvTestClass>();
                Assert.Equal(27, vec.Count());
            }
        }
    }
}