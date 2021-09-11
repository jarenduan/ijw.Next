using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ijw.Next.Collection.Async {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentBorrowableCollection<T> {
        /// <summary>
        /// 元素数量
        /// </summary>
        public int Count => _queue.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Append(T item) {
            _queue.Enqueue(item);
            resumeIfWaiting();
            raiseCountChangedEvent(Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consumeFunc"></param>
        /// <returns></returns>
        public async Task<bool> TryConsumeOneAsync(Func<T, Task<bool>> consumeFunc) {
            T item = borrowAvailableOne();
            //T item = await borrowAvailableOneAsync();
            bool success = false;
            try {
                success = await consumeFunc(item);
            }
            catch {
                success = false;
            }

            if (!success) {
                Append(item); //return back.
            }

            return success;
        }

        private T borrowAvailableOne() {
            while (true) {
                if (_queue.TryDequeue(out var item)) {
                    raiseCountChangedEvent(Count);
                    return item;
                }
                else {
                    DebugHelper.WriteLine("Collection falls asleep!");
                    _dataArrivedSignal.Wait();
                    DebugHelper.WriteLine("Collection is awaken!");
                }
            }
        }

        private async Task<T> borrowAvailableOneAsync() {
            while (true) {
                if (_queue.TryDequeue(out var item)) {
                    raiseCountChangedEvent(Count);
                    return item;
                }
                else {
                    DebugHelper.WriteLine("Collection falls asleep!");
                    await _semaphore.WaitAsync();
                    DebugHelper.WriteLine("Collection is awaken!");
                }
            }
        }

        private void resumeIfWaiting() {
            if (_dataArrivedSignal.IsSet) {
                DebugHelper.WriteLine("Collection is not sleeping!");
            }
            DebugHelper.WriteLine("Try to wake collection up!");
            _dataArrivedSignal.Set();
            DebugHelper.WriteLine("Collection waked up!");

            //if (_semaphore.CurrentCount == 0) {
            //    lock (_syncWaking) {
            //        if (_semaphore.CurrentCount == 0) {
            //            _semaphore.Release();
            //            DebugHelper.WriteLine("Collection waked up!");
            //        }
            //    }
            //}
            //else {
            //    DebugHelper.WriteLine("Collection is not sleeping!");
            //}
        }

        private void raiseCountChangedEvent(int count) 
            => ItemCountChanged?.Invoke(this, new ItemCountChangedEventArgs() { ItemCount = count });

        /// <summary>
        /// 内部元素数量发生变化
        /// </summary>
        public event EventHandler<ItemCountChangedEventArgs>? ItemCountChanged;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly ManualResetEventSlim _dataArrivedSignal = new ManualResetEventSlim();

        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
    }
}