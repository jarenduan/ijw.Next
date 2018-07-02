using System;
using ijw.Next.Collection;

namespace ijw.Next.Grid {
    /// <summary>
    /// 行列集合的基类, 提供共有的索引器/枚举器实现, 无法实例化.
    /// </summary>
    /// <typeparam name="TElement">元素类型</typeparam>
    /// <typeparam name="TRowOrColumn">行/列类型</typeparam>
    abstract public class IndexedViewCollectionBase<TElement, TRowOrColumn> : EnumerableBase<TRowOrColumn>
        where TRowOrColumn : IndexedViewBase<TElement> {
        public TRowOrColumn this[int index] => this._data[index];

        internal IndexedViewCollectionBase(Grid<TElement> grid, int count) {
            grid.ShouldBeNotNullArgument();
            count.ShouldBeNotZero();

            this._grid = grid;
            this._data = ArrayHelper.NewArrayWithValue(count, index => CreateIndexedView(this._grid, index));
        }

        protected abstract TRowOrColumn CreateIndexedView(Grid<TElement> _grid, int index);
        protected Grid<TElement> _grid;
    }
}