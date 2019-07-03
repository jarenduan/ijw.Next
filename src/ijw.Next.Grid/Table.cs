using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next.Grid {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBodyCell"></typeparam>
    /// <typeparam name="THeaderCell"></typeparam>
    public class Table<TBodyCell, THeaderCell> : Grid<TBodyCell> {
        /// <summary>
        /// 
        /// </summary>
        public Header<THeaderCell> ColumnHeader { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnHeader"></param>
        public Table(TBodyCell[,] data, THeaderCell[] columnHeader) : base(data) {
            columnHeader.ShouldBeNotNullArgument(nameof(columnHeader));
            columnHeader.Length.ShouldEquals(data.Length);

            ColumnHeader = new Header<THeaderCell>(columnHeader);
        }
    }
}
