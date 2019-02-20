using ijw.Next.Collection;
using ijw.Next.Reflection;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ijw.Next.IO.CsvReader {
    /// <summary>
    /// CSV文件读取
    /// </summary>
    public class CsvFileReader {
        /// <summary>
        /// 构造一个csv文件读取器实例
        /// </summary>
        /// <param name="csvfilePath"></param>
        public CsvFileReader(string csvfilePath) {
            CsvFilePath = csvfilePath;
        }

        #region Csv file

        /// <summary>
        /// CSV文件路径
        /// </summary>
        public string CsvFilePath { get; set; }

        /// <summary>
        /// 文件的编码
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.Unicode;

        /// <summary>
        /// 首行是否是表头
        /// </summary>
        public bool IsFirstLineHeader { get; set; } = true;

        /// <summary>
        /// 分隔符
        /// </summary>
        public char[] Separators { get; set; } = { ',' };

        #endregion

        #region Methods
        /// <summary>
        /// 读取文件标题行
        /// </summary>
        /// <returns>Csv标题</returns>
        public CsvHeader ReadHeader() {
            CsvFilePath.ShouldExistSuchFile();
            Encoding.ShouldBeNotNullReference();
            return ReadHeader(CsvFilePath, Encoding, Separators);
        }

        /// <summary>
        /// 依次将每行分隔, 返回字符串数组
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string[]> ReadStrings() {
            CsvFilePath.ShouldExistSuchFile();
            Encoding.ShouldBeNotNullReference();
            return ReadStrings(CsvFilePath, Encoding, IsFirstLineHeader, Separators);
        }

        /// <summary>
        /// 将每行分隔后, 返回字符串数组和行号
        /// </summary>
        public IEnumerable<(string[] values, int lineNumber)> ReadStringsWithLineNumber() {
            CsvFilePath.ShouldExistSuchFile();
            Encoding.ShouldBeNotNullReference();
            return ReadStringsWithLineNumber(CsvFilePath, Encoding, IsFirstLineHeader, Separators);
        }
        /// <summary>
        /// 从csv文件里面读取对象
        /// </summary>
        /// <typeparam name="T">读取对象的类型</typeparam>
        /// <returns>对象集合的迭代器</returns>
        public IEnumerable<T> ReadObjects<T>() where T : class, new() {
            CsvFilePath.ShouldExistSuchFile();
            Encoding.ShouldBeNotNullReference();
            IsFirstLineHeader.ShouldEquals(true);

            return ReadObjects<T>(CsvFilePath, Encoding, Separators);
        }
#if !NETSTANDARD1_4
        /// <summary>
        /// 读取标题行和全部行, 形成一个DataTable
        /// </summary>
        /// <param name="hasHeader">是否有标题行</param>
        /// <returns>包含全部数据的DataTable</returns>
        public DataTable ReadDataTable(bool hasHeader = true) {
            DataTable dt = new DataTable();
            if (hasHeader) {
                ReadHeader().HeaderTexts.ForEach(h =>
                {
                    dt.Columns.Add(h);
                });
            }
            ReadStringsWithLineNumber().ForEach(l => {
                dt.Rows.Add(l.values);
            });
            return dt;
        }

        /// <summary>
        /// 将指定页的数据读取出来, 形成一个DataTable
        /// </summary>
        /// <param name="pageIndex">从1开始的页码</param>
        /// <param name="pageSize">每页数据行数</param>
        /// <param name="hasHeader">是否有标题行</param>
        /// <returns>包含指定页数据的DataTable</returns>
        public DataTable ReadDataTable(int pageIndex, int pageSize = 20, bool hasHeader = true) {
            DataTable dt = new DataTable();
            if (hasHeader) {
                ReadHeader().HeaderTexts.ForEach(h => {
                    dt.Columns.Add(h);
                });
            }
            ReadStringsWithLineNumber()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ForEach(l => {
                    dt.Rows.Add(l.values);
                });
            return dt;
        }
#endif

        #endregion

        #region static methods
        /// <summary>
        /// 将每行用指定字符分隔后, 返回字符串数组. 使用指定的编码方式读取文件.
        /// </summary>
        /// <param name="csvFilepath">csv文件的路径</param>
        /// <param name="encoding">csv文件的编码</param>
        /// <param name="isFirstLineHeader">第一行是否是header</param>
        /// <param name="separators">使用的分隔符</param>
        /// <returns>一个元组的集合,每个元组包含: 行号、该行分隔出的字符串数组</returns>
        public static IEnumerable<string[]> ReadStrings(string csvFilepath, Encoding encoding, bool isFirstLineHeader = false, char[]? separators = null) {
            csvFilepath.ShouldExistSuchFile();

            char[] with = separators ?? new char[] { ',' };

            using (var reader = StreamReaderHelper.NewStreamReaderFrom(csvFilepath, encoding)) {
                foreach (var (line, lineNum) in reader.ReadLinesWithLineNumber()) {
                    if (lineNum == 1 && isFirstLineHeader) continue;
                    string[] values = line.Split(with).Select(s => s.Trim()).ToArray();
                    yield return values;
                }
            }
        }

        /// <summary>
        /// 将每行用指定字符分隔后, 返回字符串数组和行号. 使用指定的编码方式读取文件.
        /// </summary>
        /// <param name="csvFilepath">csv文件的路径</param>
        /// <param name="encoding">csv文件的编码</param>
        /// <param name="isFirstLineHeader">第一行是否是header</param>
        /// <param name="separators">使用的分隔符</param>
        /// <returns>一个元组的集合,每个元组包含: 行号、该行分隔出的字符串数组</returns>
        public static IEnumerable<(string[] line, int lineNum)> ReadStringsWithLineNumber(string csvFilepath, Encoding encoding, bool isFirstLineHeader = false, char[]? separators = null) {
            csvFilepath.ShouldExistSuchFile();

            char[] with = separators ?? new char[] { ',' };

            using (var reader = StreamReaderHelper.NewStreamReaderFrom(csvFilepath, encoding)) {
                foreach (var (line, lineNum) in reader.ReadLinesWithLineNumber()) {
                    if (lineNum == 1 && isFirstLineHeader) continue;
                    string[] values = line.Split(with).Select(s => s.Trim()).ToArray();
                    yield return (values, lineNum);
                }
            }
        }

        /// <summary>
        /// 读取文件标题行
        /// </summary>
        /// <param name="csvFilepath">csv文件的路径</param>
        /// <param name="encoding">csv文件的编码</param>
        /// <param name="separators">使用的分隔符</param>
        /// <returns>Csv标题</returns>
        public static CsvHeader ReadHeader(string csvFilepath, Encoding encoding, char[]? separators = null) {
            csvFilepath.ShouldExistSuchFile();

            char[] with = separators ?? new char[] { ',' };

            using (var reader = StreamReaderHelper.NewStreamReaderFrom(csvFilepath, encoding)) {
                var (line, _) = reader.ReadLinesWithLineNumber().FirstOrDefault();
                var values = line.Split(with).Select(s => s.Trim()).ToArray(); ;
                CsvHeader header = new CsvHeader(values);
                return header;
            }
        }

        /// <summary>
        /// 从csv文件里面读取对象
        /// </summary>
        /// <typeparam name="T">读取对象的类型</typeparam>
        /// <param name="csvFilepath">csv文件的路径</param>
        /// <param name="encoding">csv文件的编码</param>
        /// <param name="separators">csv分隔符. 默认为null, 将使用逗号作为分隔. </param>
        /// <returns></returns>
        public static IEnumerable<T> ReadObjects<T>(string csvFilepath, Encoding encoding, char[]? separators = null) where T : class, new() {
            csvFilepath.ShouldExistSuchFile();
            encoding.ShouldBeNotNullReference();

            string[] propertyNames = { };

            foreach (var (line, lineNum) in ReadStringsWithLineNumber(csvFilepath, encoding, separators: separators)) {
                if (lineNum == 1) {
                    propertyNames = line;
                }
                else {
                    T? obj = default;
                    try {
                        obj = ReflectionHelper.CreateNewInstance<T>(propertyNames, line);
                    }
                    catch (Exception ex) {
                        throw new Exception($"Parsing exception happened at the {lineNum.ToOrdinalString()} line: {line.ToSimpleEnumStrings()}", ex);
                    }
                    yield return obj;
                }
            }
        }
#endregion
    }
}