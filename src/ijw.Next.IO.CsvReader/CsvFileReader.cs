using ijw.Next.Reflection;
using ijw.Next;
using ijw.Next.Collection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ijw.Next.IO.CsvReader {
    /// <summary>
    /// CSV文件读取
    /// </summary>
    public class CsvFileReader {

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
        /// 将每行分隔后, 返回字符串数组和行号
        /// </summary>
        public IEnumerable<(string[] values, int lineNumber)> ReadStringsWithLineNumber() {
            this.CsvFilePath.ShouldExistSuchFile();
            this.Encoding.ShouldBeNotNullReference();
            return ReadStringsWithLineNumber(this.CsvFilePath, this.Encoding, this.Separators, this.IsFirstLineHeader);
        }

        /// <summary>
        /// 从csv文件里面读取对象
        /// </summary>
        /// <typeparam name="T">读取对象的类型</typeparam>
        /// <returns>对象集合的迭代器</returns>
        public IEnumerable<T> ReadObjects<T>() where T : class, new() {
            this.CsvFilePath.ShouldExistSuchFile();
            this.Encoding.ShouldBeNotNullReference();
            this.IsFirstLineHeader.ShouldEquals(true);

            return ReadObjects<T>(this.CsvFilePath, this.Encoding, this.Separators);
        } 
        #endregion

        #region static methods

        /// <summary>
        /// 将每行用指定字符分隔后, 返回字符串数组和行号.使用utf-8读取文件.
        /// </summary>
        /// <param name="csvFilepath">csv文件的路径</param>
        /// <param name="encoding">csv文件的编码</param>
        /// <param name="separators">使用的分隔符</param>
        /// <param name="isFirstLineHeader">第一行是否是header</param>
        /// <returns></returns>
        public static IEnumerable<(string[] line, int lineNum)> ReadStringsWithLineNumber(string csvFilepath, Encoding encoding, char[] separators = null, bool isFirstLineHeader = false) {
            csvFilepath.ShouldExistSuchFile();
            separators.ShouldBeNotNullArgument();

            char[] with = separators ?? new char[] { ',' };

            using (var reader = StreamReaderHelper.NewStreamReaderFrom(csvFilepath, encoding)) {
                foreach (var (line, lineNum) in reader.ReadLinesWithLineNumber()) {
                    if (lineNum == 1 && isFirstLineHeader) continue;
                    string[] values = line.Split(with);
                    yield return (values, lineNum);
                }
            }
        }

        /// <summary>
        /// 从csv文件里面读取对象
        /// </summary>
        /// <typeparam name="T">读取对象的类型</typeparam>
        /// <param name="csvFilepath">csv文件的路径</param>
        /// <param name="encoding">csv文件的编码</param>
        /// <param name="separators">csv分隔符。默认为null，将使用逗号作为分隔。</param>
        /// <returns></returns>
        public static IEnumerable<T> ReadObjects<T>(string csvFilepath, Encoding encoding, char[] separators = null) where T : class, new() {
            csvFilepath.ShouldExistSuchFile();
            encoding.ShouldBeNotNullReference();

            string[] propertyNames = null;

            foreach (var (line, lineNum) in ReadStringsWithLineNumber(csvFilepath, encoding, separators)) {
                if (lineNum == 1) {
                    propertyNames = line;
                }
                else {
                    T obj = default;
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