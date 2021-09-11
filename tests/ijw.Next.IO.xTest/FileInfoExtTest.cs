using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

using Xunit;

namespace ijw.Next.IO.xTest {
    public class FileInfoExtTest {
        [Fact]
        public void GetSHA1ForFileSelfTest() {
            string binPath = Assembly.GetExecutingAssembly().Location;
            var fi = new FileInfo(binPath);
            var sha1 = fi.GetSHA1();
        }
    }
}