using System;
using System.IO;

namespace ijw.Next.IO.FileSystem.Watcher {
    /// <summary>
    /// 单文件的监视器
    /// </summary>
    public class SingleFileChangeMonitor {
        private FileSystemWatcher _watcher = new FileSystemWatcher();
        /// <summary>
        /// 文件发生变化时触发的事件
        /// </summary>
        public event Action Changed;
        /// <summary>
        /// 发现多重Invoke的处理方式
        /// </summary>
        public FileMonitorMultipleInvokingOption MultipleInvokingOption { get; set; }

        /// <summary>
        /// 开始监视指定的文件
        /// </summary>
        /// <param name="fullFileName">欲监视的文件全路径名</param>
        public void StartMonitoring(string fullFileName) {
            FileInfo fi = null;
            try {
                fi = new FileInfo(fullFileName);
            }
            catch {
                throw new Exception("Invaild file path or name. Or cannot access.");
            }
            StartMonitoring(fi);
        }

        /// <summary>
        /// 开始监视指定的文件
        /// </summary>
        /// <param name="fileInfo">欲监视的文件的FileInfo</param>
        public void StartMonitoring(FileInfo fileInfo) {
            _watcher = new FileSystemWatcher
            {
                EnableRaisingEvents = false,
                Filter = fileInfo.Name,
                Path = fileInfo.DirectoryName,
                NotifyFilter = NotifyFilters.LastWrite
            };
            _watcher.Changed += _watcher_Changed;
            _watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// 停止监视
        /// </summary>
        public void StopMonitoring() {
            this._watcher.EnableRaisingEvents = false;
        }

        void _watcher_Changed(object sender, FileSystemEventArgs e) {
            if (this.Changed == null) return;
            switch (this.MultipleInvokingOption) {
                case FileMonitorMultipleInvokingOption.DoNothing:
                    this.Changed();
                    break;
                case FileMonitorMultipleInvokingOption.MergeDoubleEvent:
                    doubleEvent();
                    break;
                case FileMonitorMultipleInvokingOption.OffAndOn:
                    offAndOn(e);
                    break;
                default:
                    break;
            }
        }

        private int _times = 0;
        private void doubleEvent() {
            _times++;
            if (_times == 2) {
                this._times = 0;
                this.Changed();
            }
        }

        private void offAndOn(FileSystemEventArgs e) {
            try {
                _watcher.EnableRaisingEvents = false;
                FileInfo objFileInfo = new FileInfo(e.FullPath);
                if (!objFileInfo.Exists) return;
                this.Changed();
            }
            catch{
                throw;
            }
            finally {
                _watcher.EnableRaisingEvents = true;
            }
        }
    }
}
