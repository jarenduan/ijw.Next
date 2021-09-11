using System;
using System.IO;
using System.Threading;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace ijw.Next.IO.xTest {
    public class FileHelperUnitest {
        [Fact]
        public void DeleteFileBefore_Test() {
            string folder = @$"{Environment.CurrentDirectory}\testfiles_forDeleteFileBefore_Test";
            if (Directory.Exists(folder)) Directory.Delete(folder, true);
            Directory.CreateDirectory(folder);

            for (int i = 0; i < 5; i++) {
                Thread.Sleep(1000);
                i.ToString().WriteToFile(@$"{folder}\{i}.txt");
            }
            var del = FileHelper.DeleteFiles(folder, TimeSpan.FromSeconds(2));
            Assert.Equal(3, del);
            if (Directory.Exists(folder)) Directory.Delete(folder, true);
        }

        [Fact]
        public void CopyFileTest() {
            string sourceFolder = @$"{Environment.CurrentDirectory}\testfiles_forCopyFile_Test_source";
            if (Directory.Exists(sourceFolder)) Directory.Delete(sourceFolder, true);
            Directory.CreateDirectory(sourceFolder);

            for (int i = 0; i < 5; i++) {
                i.ToString().WriteToFile(@$"{sourceFolder}\{i}.txt");
            }

            string sourceSubFolder = @$"{Environment.CurrentDirectory}\testfiles_forCopyFile_Test_source\subfolder";
            Directory.CreateDirectory(sourceSubFolder);

            for (int i = 0; i < 5; i++) {
                i.ToString().WriteToFile(@$"{sourceFolder}\{i}.txt");
            }

            for (int i = 0; i < 5; i++) {
                i.ToString().WriteToFile(@$"{sourceSubFolder}\{i}.txt");
            }

            string destFolder = @$"{Environment.CurrentDirectory}\testfiles_forCopyFile_Test_dest";
            if (Directory.Exists(destFolder)) Directory.Delete(destFolder, true);
            Directory.CreateDirectory(destFolder);

            var orderedCopiedFiles = FileHelper.CopyFiles(sourceFolder,
                                                     destFolder,
                                                     "*.*",
                                                     SearchOption.AllDirectories)
                                               .Select(n => n.Replace(destFolder, sourceFolder))
                                               .OrderBy(n => n);

            var orderedSourceFiles = Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories)
                                              .OrderBy(n => n);

            Assert.Equal(orderedSourceFiles, orderedCopiedFiles); 
        }
    }
}