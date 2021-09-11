using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ijw.Next.Collection {
    /// <summary>
    /// 提供一个支持较长时间消费操作的线程安全集合.
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <remarks>
    ///  消费者可以随时追加元素, 也可以随时取出元素, 进行消费操作.
    ///  消费后, 可以通过Remove方法控制移除对象, 满足长时间消费操作的需求.
    ///  如果消费者最终无法完成消费操作, 可以调用Return将对象还回集合.
    /// </remarks>
    public class LongTimeConsumerCollection<T> {
        /// <summary>
        /// 内部元素数量发生变化
        /// </summary>
        public event EventHandler<ItemCountChangedEventArgs>? ItemCountChanged;

        /// <summary>
        /// 元素取出策略
        /// </summary>
        public FetchingStrategies ItemGettingStrategy { get; set; }

        /// <summary>
        /// 元素数量.
        /// </summary>
        public int Count => _itemsList.Count;

        /// <summary>
        /// 获取是否存在元素
        /// </summary>
        public bool HasItem => _itemsList.Count != 0;

        /// <summary>
        /// 获取当前是否存在未被提取消费的元素
        /// </summary>
        public bool HasAvailableItems => _itemsList.Exists((tuple) => tuple.Item2 == false);

        /// <summary>
        /// 向集合尾部追加一个元素
        /// </summary>
        /// <param name="item"></param>
        public void Append(T item) {
            if (item is null) {
                DebugHelper.WriteLine("(Appending Item) Item is null, so quit appending.");
                return;
            }
            DebugHelper.WriteLine("(Appending Item) Try to get the lock...");
            int count;
            lock (_syncRoot) {
                DebugHelper.WriteLine("(Appending Item) Got the lock!");
                DebugHelper.Write($"(Appending Item) Item count :{_itemsList.Count}");
                _itemsList.Add(new MutableTuple<T, bool>(item, false));
                count = _itemsList.Count;
                Debug.WriteLine($" => {count}. Item appended!");
            }
            raiseCountChangedEvent(count);
        }

        /// <summary>
        /// 尝试取出一个元素
        /// </summary>
        /// <param name="item">取出的元素</param>
        /// <returns>是否成功</returns>
        public bool TryBorrow(out T item) => tryGetItem(out item, false);

        /// <summary>
        /// 尝试借出一个可用元素
        /// </summary>
        /// <param name="item">取出的元素</param>
        /// <returns>是否成功</returns>
        public bool TryBorrowAvailable(out T item) => tryGetItem(out item, true);

        /// <summary>
        /// 移除某个元素
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item) {
            if (item is null) {
                DebugHelper.WriteLine("(Removing Item) Item is null, so quit removing.");
                return;
            }
            DebugHelper.WriteLine("(Removing Item) Try to get the lock...");
            int index = getIndexOf(item);
            if (index == -1) {
                DebugHelper.Write("(Removing Item) Not Found the Item.");
                return;
            }
            int count;
            lock (_syncRoot) {
                DebugHelper.WriteLine("(Removing Item) Got the lock!");
                DebugHelper.Write($"(Removing Item) Item count :{_itemsList.Count}");
                index = getIndexOf(item);
                if (index == -1) {
                    DebugHelper.Write("(Removing Item) Not Found the Item.");
                    return;
                }
                this._itemsList.RemoveAt(index); //RemoveAt is O(count - index) operation.
                count = _itemsList.Count;
                Debug.WriteLine($" => {count}. Item removed!");
            }
            raiseCountChangedEvent(count);
        }

        /// <summary>
        /// 将指定元素交还给集合
        /// </summary>
        /// <param name="item">欲交还的元素</param>
        public void Return(T item) {
            if (item is null) {
                DebugHelper.WriteLine("(Returning Item Back) Item is null, so quit returning.");
                return;
            }
            int index = getIndexOf(item);
            if (index == -1) {
                DebugHelper.WriteLine("(Returning Item Back) Item is not in collection, so quit returning.");
                return;
            }
            DebugHelper.WriteLine("(Returning Item Back) Try to get the lock...");
            lock (_syncRoot) {
                DebugHelper.WriteLine("(Returning Item Back) Got the lock!");
                index = getIndexOf(item);
                if (index == -1) {
                    DebugHelper.WriteLine("(Returning Item Back) But item is not in collection, so quit returning.");
                    return;
                }
                _itemsList[index].Item2 = false;
                DebugHelper.Write($"(Returning Item Back) Item returned, count:{_itemsList.Count}");
            }
        }

        private int getIndexOf(T item) =>
                    _itemsList.IndexOf(t => (t?.Item1?.Equals(item)) ?? false, ItemGettingStrategy);

        private void raiseCountChangedEvent(int count) => 
            ItemCountChanged?.Invoke(this, new ItemCountChangedEventArgs() { ItemCount = count });
        private bool tryGetItem(out T item, bool onlyGetNotInConsuming = false) {
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
#pragma warning disable CS8601 // 可能的 null 引用赋值。
            item = default;
#pragma warning restore CS8601 // 可能的 null 引用赋值。
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
            if (!HasItem) {
                DebugHelper.WriteLine("(Getting Item) Try getting Item, but no items.");
                return false;
            }
            if (onlyGetNotInConsuming && !HasAvailableItems) {
                DebugHelper.WriteLine("(Getting Item) Try getting Item, but all items are in consuming.");
                return false;
            }
            DebugHelper.WriteLine("(Getting Item) Try to get the lock...");
            int index = -1;
            lock (_syncRoot) {
                DebugHelper.WriteLine("(Getting Item) Got the lock!");
                if (!HasItem) {
                    DebugHelper.WriteLine("(Getting Item) But no items :(");
                    return false;
                }
                if (onlyGetNotInConsuming && !HasAvailableItems) {
                    DebugHelper.WriteLine("(Getting consuming Item) But all in consuming :(");
                    return false;
                }
                if (onlyGetNotInConsuming) {
                    index = _itemsList.IndexOf(t => t.Item2 == false, ItemGettingStrategy);
                    DebugHelper.WriteLine($"(Getting Non-consuming Item) Non-consuming item count: {_itemsList.Count(i => i.Item2 == false)}.");
                    DebugHelper.WriteLine($"(Getting Non-consuming Item) Got one non-consuming item using {ItemGettingStrategy}");
                }
                else {
                    DebugHelper.WriteLine($"(Getting Item) Item count: {_itemsList.Count}.");
                    switch (ItemGettingStrategy) {
                        case FetchingStrategies.FirstFirst:
                            index = 0;
                            DebugHelper.WriteLine("(Getting Item) Got the FIRST one!");
                            break;
                        case FetchingStrategies.LastFirst:
                            index = _itemsList.Count - 1;
                            DebugHelper.WriteLine("(Getting Item), Got the LAST one!");
                            break;
                        default:
                            throw new Exception();
                    }
                }
                if (index == -1) {
                    return false;
                }
                item = _itemsList[index].Item1;
                _itemsList[index].Item2 = true;
            }
            return true;
        }

        //private List<(T, bool)> _itemsList = new List<MutableTuple<T, bool>>();
        private readonly List<MutableTuple<T, bool>> _itemsList = new List<MutableTuple<T, bool>>();
        private readonly object _syncRoot = new object();
    }
}