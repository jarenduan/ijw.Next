#if !NET45 && !NETSTANDARD20
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ijw.Next.Collection.Async {
    public class AsyncConcurrentStream<T> {
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();

        public void Append() {

        }

        public async IAsyncEnumerable<T> GetItemsAsync() {
            T nextItem;
            yield return nextItem;
            if (_queue.TryDequeue(out var message)) {
                yield return message;
            }
            else {
                SpinWait.SpinUntil(() => _queue.Count > 0);
            }
        }
    }
}
#endif
