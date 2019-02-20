using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ijw.Next.IO.CsvReader {
    /// <summary>
    /// Csv文件的各列标题
    /// </summary>
    public class CsvHeader {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="headerTexts"></param>
        public CsvHeader(IEnumerable<string> headerTexts) {
            this._headers = new List<string>(headerTexts);
        }

        /// <summary>
        /// 列名
        /// </summary>
        public IEnumerable<string> HeaderTexts => _headers;

        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount => _headers.Count;

        /// <summary>
        /// 列名索引器
        /// </summary>
        /// <param name="index">列的索引</param>
        /// <returns>对应列的列名</returns>
        public string this[int index] => _headers[index];

        /// <summary>
        /// 列索引的索引器
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns>该列的索引</returns>
        public int this[string columnName] => _headers.IndexOf(columnName);

        private readonly List<string> _headers;
    }
}