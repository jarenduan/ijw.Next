namespace ijw.Next.IO.FileSystem.Watcher {
    /// <summary>
    /// �ļ����������ֶ��Invoke�Ĵ���ʽ
    /// </summary>
    public enum FileMonitorMultipleInvokingOption {
        /// <summary>
        /// ʲôҲ����
        /// </summary>
        DoNothing = 0,
        /// <summary>
        /// �ϲ�����Invoke
        /// </summary>
        MergeDoubleEvent, 
        /// <summary>
        /// ����һ��
        /// </summary>
        OffAndOn
    }
}
