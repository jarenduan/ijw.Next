namespace ijw.Next.Grid {
    /// <summary>
    /// 表中的一列
    /// </summary>
    /// <typeparam name="T">列中每个单元格容纳的元素的类型</typeparam>
    public class Column<T> : IndexedViewBase<T>{
        /// <summary>
        /// 总列数
        /// </summary>
        public override int Dimension => _grid.ColumnCount;

        /// <summary>
        /// 指定索引处单元格中的元素
        /// </summary>
        /// <param name="index">行数索引</param>
        /// <returns></returns>
        public override T this[int index] {
            get => _grid._cells[index, Index];
            set => _grid._cells[index, Index] = value;
        }

        /// <summary>
        /// 无参数构造函数, 仅供Grid类初始化时内部使用
        /// </summary>
        internal Column(Grid<T> grid, int index) : base(grid, index) {
        }
    }
}