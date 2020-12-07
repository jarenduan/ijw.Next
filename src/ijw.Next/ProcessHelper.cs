#if !NETSTANDARD1_4
using System.Diagnostics;

namespace ijw.Next {
    /// <summary>
    /// 进程相关的帮助类
    /// </summary>
    public static class ProcessHelper {
        /// <summary>
        /// 尝试获取相同进程.
        /// </summary>
        /// <param name="theSame"></param>
        /// <returns>存在返回true. 反之false.</returns>
        public static bool TryGetSameProcessInstance(out Process? theSame) {
            theSame = null;
            var current = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表  
            foreach (Process process in processes) {
                //如果实例已经存在则忽略当前进程  
                if (process.Id != current.Id) {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (process.MainModule.FileName == current.MainModule.FileName) {
                        //返回已经存在的进程
                        theSame = process;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检测是否有相同进程.
        /// </summary>
        /// <returns>存在返回true. 反之false.</returns>
        public static bool HasSameProcess() => TryGetSameProcessInstance(out _);
    }
}
#endif