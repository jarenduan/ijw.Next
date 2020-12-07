using System.IO;

namespace ijw.Next {
    /// <summary>
    /// string类与契约相关的扩展方法
    /// </summary>
    public static class StringContractShouldExt {
        /// <summary>
        /// 应该存在这样的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns>存在指定路径的文件, 返回true. 否则抛出FileNotFoundException异常.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static FileInfo ShouldExistSuchFile(this string path) {
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists) {
                throw new FileNotFoundException("File doesn't exist.", fi.FullName);
            }
            return fi;
        }

        /// <summary>
        /// 应该是有效的绝对路径名
        /// </summary>
        /// <param name="path"></param>
        /// <returns>是有效的绝对路径名, 返回true. 否则抛出FileNotFoundException异常.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static FileInfo ShouldBeValidAbsoluteName(this string path) {
            var result = path.Length > 3 && path[1] == ':' && path[2] == '\\';
            if (!result) {
                throw new ContractBrokenException($"{path} is not an absolute path.");
            }
            return new FileInfo(path);
        }
    }
}
