using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ijw.Next.Collection.Async {
    /// <summary>
    /// 提供基于异步操作的支持长时间消费操作的线程安全集合.
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <remarks>
    ///  消费者可以随时追加元素, 也可以随时取出元素, 进行消费操作.
    ///  如果消费者完成消费操作, 将自动移除对象；反之, 对象将还回集合.
    /// </remarks>
    public class LongTimeConsumingCollection<T> {
        #region Public Members
        /// <summary>
        /// 内部元素数量发生变化
        /// </summary>
        public event EventHandler<ItemCountChangedEventArgs> ItemCountChanged;

        /// <summary>
        /// 元素取出策略
        /// </summary>
        public FetchingStrategies ItemGettngStrategy { get; set; }

        /// <summary>
        /// 元素数量
        /// </summary>
        public int Count => _itemsList.Count;

        /// <summary>
        /// 获取是否存在元素
        /// </summary>
        public bool HasItem => _itemsList.Count != 0;

        /// <summary>
        /// 获取当前是否存在未被提取消费的元素
        /// </summary>
        public bool HasAvailableItems => _itemsList.Exists((itemStorage) => itemStorage.InConsuming == false);
        #endregion

        #region Public Methods
        /// <summary>
        /// 向集合尾部追加一个元素
        /// </summary>
        /// <param name="item"></param>
        public void Append(T item) {
            if (item == null) {
                DebugHelper.WriteLine("(Appending Item) Item is null, so quit appending.");
                return;
            }
            DebugHelper.WriteLine("(Appending Item) Try to get the colleciton-lock...");
            int count;
            lock (_syncCollection) {
                DebugHelper.WriteLine("(Appending Item) Got the colleciton-lock!");
                DebugHelper.Write($"(Appending Item) Item count: {_itemsList.Count}");
                _itemsList.Add(new ItemStorage(item));
                count = _itemsList.Count;
                DebugHelper.WriteLine($" => {count}. Item appended!");
            }
            resumeIfWaiting();
            raiseCountChangedEvent(count);
        }

        /// <summary>
        /// 异步方式取出一个元素, 然后以异步方式进行消费操作. 消费成功后从集合中移除此元素, 反之将元素还回集合.
        /// </summary>
        /// <param name="consumeFunc">异步消费操作</param>
        /// <returns>取出元素并进行消费操作的任务</returns>
        public async Task ConsumeOneAsync(Func<T, Task<bool>> consumeFunc) {
            T item = await borrowAvailableOneAsync();

            bool success = false;
            try {
                success = await consumeFunc(item);
            }
            catch (Exception) {
                success = false;
            }

            if (success) {
                remove(item);
            }
            else {
                returnback(item);
            }
        }

        /// <summary>
        /// 异步方式取出一个元素, 然后以同步方式进行消费操作.消费成功后从集合中移除此元素, 反之将元素还回集合.
        /// </summary>
        /// <param name="consumeFunc">同步消费操作</param>
        /// <returns>取出元素并进行消费操作的任务</returns>
        public async Task ConsumeOneAsync(Func<T, bool> consumeFunc) {
            T item = await borrowAvailableOneAsync();

            bool success = false;
            try {
                success = consumeFunc(item);
            }
            catch (Exception) {
                success = false;
            }

            if (success) {
                remove(item);
            }
            else {
                returnback(item);
            }
        }
        #endregion

        #region Private Methods
        private bool tryBorrowOneItem(out T item) {
            item = default;
            if (!HasItem || !HasAvailableItems) {
                DebugHelper.WriteLine("(Getting Item) Try getting Item, but no available items.");
                return false;
            }

            DebugHelper.WriteLine("(Getting Item) Try to get the colleciton-lock...");

            lock (_syncCollection) {
                DebugHelper.WriteLine("(Getting Item) Got the colleciton-lock!");

                int index = _itemsList.IndexOf(i => !i.InConsuming, ItemGettngStrategy);

                if (index == -1) {
                    DebugHelper.WriteLine("(Getting consuming Item) no available items!");
                    return false;
                }

                DebugHelper.WriteLine($"(Getting Non-consuming Item) Non-consuming item total: {_itemsList.Count(i => !i.InConsuming)}. Got the {index.ToOrdinalString(_itemsList.Count)} item using {ItemGettngStrategy.ToString()}");
                item = _itemsList[index].Item;
                _itemsList[index].InConsuming = true;
            }

            return true;
        }

        private async Task<T> borrowAvailableOneAsync() {
            while (true) {
                await wait();
                if (tryBorrowOneItem(out var item)) {
                    return item;
                }
            }
        }

        /// <summary>
        /// 移除某个元素
        /// </summary>
        private void remove(T item) {
            if (item == null) {
                DebugHelper.WriteLine("(Removing Item) Item is null, so quit removing.");
                return;
            }
            int index = getIndexOf(item);
            if (index == -1) {
                DebugHelper.Write("(Removing Item) Not Found the Item.");
                return;
            }
            int count;
            DebugHelper.WriteLine("(Removing Item) Try to get the colleciton-lock...");
            lock (_syncCollection) {
                DebugHelper.WriteLine("(Removing Item) Got the colleciton-lock!");
                DebugHelper.Write($"(Removing Item) Item count: {_itemsList.Count}");
                if (!_itemsList[index].Item.Equals(item)) {
                    index = getIndexOf(item);
                    if (index == -1) {
                        DebugHelper.WriteLine("(Removing Item) Not Found the Item.");
                        return;
                    }
                }
                _itemsList.RemoveAt(index); //RemoveAt is O(count - index) operation.
                count = _itemsList.Count;
                DebugHelper.WriteLine($" => {count}. Item removed!");
            }
            raiseCountChangedEvent(count);
        }

        /// <summary>
        /// 将指定元素交还给集合
        /// </summary>
        /// <param name="item">欲交还的元素</param>
        private void returnback(T item) {
            if (item == null) {
                DebugHelper.WriteLine("(Returning Item Back) Item is null, quit.");
                return;
            }
            int index = getIndexOf(item);
            if (index == -1) {
                DebugHelper.WriteLine("(Returning Item Back) Item is not in collection, quit.");
                return;
            }
            DebugHelper.WriteLine("(Returning Item Back) Try to get the colleciton-lock...");
            lock (_syncCollection) {
                DebugHelper.WriteLine("(Returning Item Back) Got the colleciton-lock!");
                if (!_itemsList[index].Item.Equals(item)) {
                    index = getIndexOf(item);
                    if (index == -1) {
                        DebugHelper.WriteLine("(Returning Item Back) But item is not in collection, quit.");
                        return;
                    }
                }
                _itemsList[index].InConsuming = false;
                DebugHelper.Write($"(Returning Item Back) Item returned, count:{_itemsList.Count}");
            }
            resumeIfWaiting();
        }

        private int getIndexOf(T item) => _itemsList.IndexOf(t => t.Item.Equals(item), ItemGettngStrategy);

        private void raiseCountChangedEvent(int count) {
            var temp = ItemCountChanged;
            if (temp != null) {
                ItemCountChanged(this, new ItemCountChangedEventArgs() { ItemCount = count });
            }
        }

        private async Task wait() {
            DebugHelper.WriteLine("Collection falls asleep!");
            await _semaphore.WaitAsync();
            DebugHelper.WriteLine("Collection is awaken!");
        }

        private void resumeIfWaiting() {
            DebugHelper.WriteLine("Try to wake up collection...");
            if (_semaphore.CurrentCount == 0) {
                DebugHelper.WriteLine("Collection is sleeping! Try to get the waking-lock...");
                lock (_syncWaking) {
                    DebugHelper.WriteLine("got the waking-lock.");
                    if (_semaphore.CurrentCount == 0) {
                        DebugHelper.WriteLine("Waking up collection...");
                        _semaphore.Release();
                        return;
                    }
                    DebugHelper.WriteLine("release the waking-lock.");
                }
            }
            DebugHelper.WriteLine("Collection is not sleeping!");
        }
        #endregion

        #region Private Members
        private readonly List<ItemStorage> _itemsList = new List<ItemStorage>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly object _syncCollection = new object();
        private readonly object _syncWaking = new object();
        private class ItemStorage {
            public ItemStorage(T item) => Item = item;

            public T Item { get; private set; }

            public bool InConsuming { get; set; } = false;
        }
        #endregion
    }
}