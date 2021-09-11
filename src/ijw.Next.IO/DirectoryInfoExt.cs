using System;
using System.Collections.Generic;
using System.IO;

namespace ijw.Next.IO {
    /// <summary>
    /// 
    /// </summary>
    public static class DirectoryInfoExt {
        /// <summary>
        /// 获取当前文件夹下面的所有文件
        /// </summary>
        /// <param name="rootDirectory"></param>
        /// <returns>元祖序列, (子文件夹info, 相对当前文件夹的路径, 文件info)</returns>
        public static IEnumerable<(DirectoryInfo Dir, string RelativePath, FileInfo fi)> GetFilesIncludeSubFolders(this DirectoryInfo rootDirectory) {
            return forAllFilesRecurion(rootDirectory, ".");

            static IEnumerable<(DirectoryInfo, string RelativePath, FileInfo)> forAllFilesRecurion(DirectoryInfo directory, string relPath) {
                foreach (var f in directory.GetFiles()) {
                    //var relPath = Path.GetRelativePath(rootDirectory.FullName, directory.FullName);
                    yield return (directory, relPath, f);
                }

                foreach (var d in directory.GetDirectories()) {
                    string relPath1 = $"{relPath}\\{d.Name}";
                    foreach (var ff in forAllFilesRecurion(d, relPath1)) {
                        yield return ff;
                    }
                }
            }
        }

        /// <summary>
        /// 对文件夹下面的所有文件执行指定操作
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="action">对文件进行的指定操作</param>
        public static void ForAllFiles(this DirectoryInfo directory, Action<FileInfo> action) {
            action.ShouldBeNotNullArgument(nameof(action));
            foreach (var f in directory.GetFiles()) {
                action(f);
            }
        }

        /// <summary>
        /// 对文件夹及其所有子文件夹的文件执行指定操作
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="action">对文件进行的指定操作</param>
        /// <param name="actionWhenCD">改变目录时执行的指定操作. 包括当前目录. 默认是空. 执行时将自动传递DirectoryInfo和层级序数作为参数. 以初始目录为0级.</param>
        public static void ForAllFilesRecursively(this DirectoryInfo directory, Action<FileInfo> action, Action<DirectoryInfo, int>? actionWhenCD = null) {
            forAllFilesRecurion(directory, 0, action, actionWhenCD);

            static void forAllFilesRecurion(DirectoryInfo directory, int level, Action<FileInfo> action, Action<DirectoryInfo, int>? actionWhenCD = null) {
                actionWhenCD?.Invoke(directory, level);

                directory.ForAllFiles(action);

                foreach (var d in directory.GetDirectories()) {
                    forAllFilesRecurion(d, level + 1, action, actionWhenCD);
                }
            }
        }
    }
}