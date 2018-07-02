namespace ijw.Next.IO.FileSystem.Watcher {
    /// <summary>
    /// 文件监视器发现多次Invoke的处理方式
    /// </summary>
    public enum FileMonitorMultipleInvokingOption {
        /// <summary>
        /// 什么也不做
        /// </summary>
        DoNothing = 0,
        /// <summary>
        /// 合并两个Invoke
        /// </summary>
        MergeDoubleEvent, 
        /// <summary>
        /// 忽略一个
        /// </summary>
        OffAndOn
    }
}
