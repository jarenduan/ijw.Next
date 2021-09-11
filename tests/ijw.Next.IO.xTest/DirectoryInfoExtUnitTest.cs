using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xunit;

namespace ijw.Next.IO.xTest {

    public class DirectoryInfoExtUnitTest {
        [Fact]
        public void GetFileIncludeSubFoldersTest() {
            var testRootPath = "root";
            var rootDir = testRootPath.AsDirectoryInfo();
            if (!rootDir.Exists) {
                Directory.CreateDirectory(testRootPath);
            }
            Directory.SetCurrentDirectory(testRootPath);
            $"file 1 in {testRootPath}".WriteToFile("1.txt");

            var testSubPath = "sub1";
            var SubDir = testSubPath.AsDirectoryInfo();
            if (!SubDir.Exists) {
                Directory.CreateDirectory(testSubPath);
            }
            Directory.SetCurrentDirectory(testSubPath);
            "file 1 in sub1".WriteToFile("1-1.txt");

            var testSubSubPath = "sub1-1";
            var subSubDir = testSubSubPath.AsDirectoryInfo();
            if (!subSubDir.Exists) {
                Directory.CreateDirectory(testSubSubPath);
            }
            Directory.SetCurrentDirectory(testSubSubPath);
            "file 1 in sub1-1".WriteToFile("1-1-1.txt");

            var allFiles = rootDir.GetFilesIncludeSubFolders().ToArray();
            Assert.Equal(3, allFiles.Count());
        }
    }
}
