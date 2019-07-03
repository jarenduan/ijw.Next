using System;
using System.Collections;
using System.Collections.Generic;
using ijw.Next.Collection;

namespace ijw.Next.Grid {
    /// <summary>
    /// 行列集合的基类, 提供共有的索引器/枚举器实现, 无法实例化.
    /// </summary>
    /// <typeparam name="TElement">元素类型</typeparam>
    /// <typeparam name="TRowOrColumn">行/列类型</typeparam>
    abstract public class IndexedViewCollectionBase<TElement, TRowOrColumn> : IEnumerable<TRowOrColumn>
        where TRowOrColumn : IndexedViewBase<TElement> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TRowOrColumn this[int index] => _data[index];

        internal IndexedViewCollectionBase(Grid<TElement> grid, int count) {
            grid.ShouldBeNotNullArgument(nameof(grid));
            count.ShouldBeNotZero();

            _grid = grid;
            _data = ArrayHelper.NewArrayWithValue(count, index => createIndexedView(grid, index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_grid"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected abstract TRowOrColumn createIndexedView(Grid<TElement> _grid, int index);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TRowOrColumn> GetEnumerator() => ((IEnumerable<TRowOrColumn>)_data).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TRowOrColumn>)_data).GetEnumerator();

        /// <summary>
        /// 对grid的引用
        /// </summary>
        protected Grid<TElement> _grid;
        private readonly TRowOrColumn[] _data;
    }
}