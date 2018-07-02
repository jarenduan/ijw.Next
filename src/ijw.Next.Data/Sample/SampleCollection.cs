using ijw.Next.Collection;
using ijw.Next.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Math;

namespace ijw.Next.Data.Sample {
    /// <summary>
    /// 表示样本的集合, 非线程安全. 此集合始终非空，至少包含一条样本数据。
    /// </summary>
    public class SampleCollection : IEnumerable<Sample> {
        #region Constructors
        protected SampleCollection() { }

        /// <summary>
        /// 从double数组的集合创建一个样本集，样本集将直接引用这些数组，这意味着对样本集的一切编辑将导致这些数组的直接变更。
        /// </summary>
        /// <param name="samples">原始数据数组</param>
        /// <param name="outputDimension">输出维度，可选，默认是0，无输出</param>
        /// <param name="fieldNames">列名，可选</param>
        public SampleCollection(IEnumerable<double[]> samples, int outputDimension = 0, IEnumerable<string> fieldNames = null) {
            samples.ShouldBeNotNullArgument(nameof(samples));
            samples.ShouldNotBeNullOrEmpty();
            int totalDimension = samples.ElementAt(0).Length;
            outputDimension.ShouldNotLargerThan(totalDimension);
            fieldNames.Count().ShouldEquals(totalDimension);

            this.FieldNames = fieldNames.ToArray();
            this.InputDimensionCount = totalDimension - outputDimension;
            this.OutputDimensionCount = outputDimension;

            this._data = new double[samples.Count()][];
            samples.ForEach((row, index) => {
                row.Length.ShouldEquals(totalDimension);
                _data[index] = row;
                this._samples.Add(new Sample(row, outputDimension, this.FieldNames));
            });
            initializeDimensionColumns();
        }

        /// <summary>
        /// 从一个Sample集合samples创建一个新的样本集。会导致值的复制。创建的样本集不引用samples，二者可以相互更改。
        /// </summary>
        /// <param name="samples">样本的集合</param>
        public SampleCollection(IEnumerable<Sample> samples) {
            samples.ShouldBeNotNullArgument(nameof(samples));
            samples.ShouldNotBeNullOrEmpty();

            var firstSample = samples.ElementAt(0);
            int totalDimension = firstSample.Dimension;
            this.OutputDimensionCount = firstSample.OutputDimension;
            this.InputDimensionCount = firstSample.InputDimension;
            this.FieldNames = firstSample.Fields;

            this._data = new double[samples.Count()][];
            samples.ForEach((s, index) => {
                s.Dimension.ShouldEquals(totalDimension);
                s.InputDimension.ShouldEquals(this.InputDimensionCount);
                s.Fields.Count().ShouldEquals(totalDimension);

                //新建一行样本数据
                var line = new double[totalDimension];
                s.ForEach((v, jndex) => {
                    line[jndex] = v;
                });
                this._data[index] = line;

                //新建一个样本元素对象，目的是不影响传入的Sample对象
                this._samples.Add(new Sample(line, OutputDimensionCount, this.FieldNames));
            });
            initializeDimensionColumns();
        }

        private void initializeDimensionColumns() {
            for (int i = 0; i < this.TotalDimensionCount; i++) {
                this._columns.Add(new SampleCollectionColumn(this._data, i));
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// 样本输入维度
        /// </summary>
        public int InputDimensionCount { get; protected set; }

        /// <summary>
        /// 样本输出维度
        /// </summary>
        public int OutputDimensionCount { get; protected set; }

        /// <summary>
        /// 样本数据总维度
        /// </summary>
        public int TotalDimensionCount => InputDimensionCount + OutputDimensionCount;

        /// <summary>
        /// 样本总数
        /// </summary>
        public int Count { get { return this._data.Length; } }

        /// <summary>
        /// 样本集合
        /// </summary>
        public IEnumerable<Sample> Samples => this._samples;

        /// <summary>
        /// 列集合
        /// </summary>
        public IEnumerable<SampleCollectionColumn> Columns => this._columns;

        /// <summary>
        /// 按索引访问样本
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sample this[int index] => _samples[index];

        /// <summary>
        /// 按列名访问样本列
        /// </summary>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        public SampleCollectionColumn this[string fieldname] => _columns[GetColumnIndex(fieldname)];
       
        /// <summary>
        /// 字段名称
        /// </summary>
        public string[] FieldNames { get; protected set; }
        #endregion

        #region Members

        /// <summary>
        /// 行视图集合内部存储
        /// </summary>
        protected List<Sample> _samples = new List<Sample>();

        /// <summary>
        /// 列视图集合内部存储
        /// </summary>
        private List<SampleCollectionColumn> _columns = new List<SampleCollectionColumn>();

        /// <summary>
        /// 内部数据实际存储
        /// </summary>
        protected double[][] _data;
        
        #endregion

        #region Collection Element Maintanence

        /// <summary>
        /// 向集合添加一个样本. 
        /// 添加时会保证样本与集合内样本的输入输出维度一致.
        /// </summary>
        /// <param name="sample">待添加的样本</param>
        protected void Add(Sample sample) {
            if (this._samples.Count != 0 && (sample.InputDimension != this.InputDimensionCount || sample.OutputDimension != this.OutputDimensionCount)) {
                throw new DimensionNotMatchException();
            }
            this._samples.Add(sample);
        }

        /// <summary>
        /// 将样本集划分为两个子集
        /// </summary>
        /// <param name="ratioOfFirstGroup">第一个子集的占比</param>
        /// <param name="ratioOfSecondGroup">第二个子集的占比</param>
        /// <param name="method">切分方法</param>
        /// <param name="firstGroup">第一个子集</param>
        /// <param name="secondGroup">第二个子集</param>
        public void DivideIntoTwo(int ratioOfFirstGroup, int ratioOfSecondGroup, CollectionDividingMethod method, out SampleCollection firstGroup, out SampleCollection secondGroup) {
            this.DivideByRatioAndMethod(ratioOfFirstGroup, ratioOfSecondGroup, method, out List<Sample> samples1, out List<Sample> samples2);

            firstGroup = new SampleCollection(samples1);
            secondGroup = new SampleCollection(samples2);
        }
       
        #endregion

        #region Methods

        #region IEnumerable Implementation
        public IEnumerator<Sample> GetEnumerator() {
            return this._samples.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this._samples.GetEnumerator();
        }
        #endregion
        /// <summary>
        /// 检查字符串是否是样本集中的输入字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>是字段名返回真，反之假</returns>
        public bool IsField(string fieldName) {
            var index = GetColumnIndex(fieldName);
            return index >= 0;
        }

        /// <summary>
        /// 检查字符串是否是样本集中的输入字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>是返回真，反之假</returns>
        public bool IsInputField(string fieldName) {
            var index = GetColumnIndex(fieldName);
            return index >= 0 && index < InputDimensionCount;
        }

        /// <summary>
        /// 检查字符串是否是样本集中的输出字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>是返回真，反之假</returns>
        public bool IsOutputField(string fieldName) {
            var index = GetColumnIndex(fieldName);
            return index >=0 && index >= InputDimensionCount;
        }

        /// <summary>
        /// 获取字符串代表的样本集中的字段序号
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>存在则返回字段所在列的索引号，反之-1</returns>
        public int GetColumnIndex(string fieldname) => this.FieldNames?.IndexOf(fieldname) ?? -1;

        /// <summary>
        /// 获取字符串代表的样本集中的字段序号
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>存在则返回字段所在列的索引号，反之-1</returns>
        public SampleCollectionColumn GetColumn(string fieldname) {
            var index = GetColumnIndex(fieldname);
            if (index < 0) {
                throw new ArgumentOutOfRangeException(nameof(fieldname));
            }
            return GetColumnAt(index);
        }

        /// <summary>
        /// 获取指定索引处的维度列
        /// </summary>
        /// <param name="index">指定的索引</param>
        /// <returns>维度列</returns>
        public SampleCollectionColumn GetColumnAt(int index) {
            return this._columns[index];
        }

        public double[][] ToArray() {
            double[][] array = new double[this._data.Length][];
            for (int i = 0; i < array.Length; i++) {
                array[i] = new double[this.TotalDimensionCount];
                for (int j = 0; j < this.TotalDimensionCount; j++) {
                    array[i][j] = this._data[i][j];
                }
            }
            return array;
        }

        public SampleCollection Clone() {
            double[][] cloneData = ToArray();
            return new SampleCollection(cloneData, this.OutputDimensionCount, this.FieldNames);
        }

        #region Filters
        
        #region 限制波动过滤
        /// <summary>
        /// 复制样本集，并限制波动进行样本集过滤。用前一个样本+波动幅度代替。
        /// </summary>
        /// <param name="diffLimitations">波动最大值绝对值的向量.</param>
        /// <returns>新的样本集</returns>
        public SampleCollection LimitingDiffFilter(IEnumerable<double> diffLimitations) {
            diffLimitations.Count().ShouldEquals(this.TotalDimensionCount);
            diffLimitations.ShouldEachSatisfy((item) => item > 0);

            SampleCollection result = this.Clone();

            CollectionHelper.ForEachThree(
                this.Columns,
                result.Columns,
                diffLimitations,
                (srcCol, resultCol, diff) => {
                    LimitingDiffFilter(srcCol, resultCol, diff);
                });

            return result;
        }

        /// <summary>
        /// 限制波动对集合进行过滤。用前一个样本+波动幅度代替。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="diff"></param>
        protected void LimitingDiffFilter(IIndexable<double> values, IIndexable<double> result, double diff) {
            for (int i = 1; i < values.Count; i++) {
                var curr = values[i];
                var prev = values[i - 1];
                result[i] = limitationByDiff(diff, curr, prev);
            }
        }

        protected double limitationByDiff(double diff, double curr, double prev) {
            if ((curr - prev) > diff) {
                return prev + diff;
            }
            else if ((prev - curr) > diff) {
                return prev - diff;
            }
            else {
                return curr;
            }
        }
        #endregion

        #region 限幅过滤
        /// <summary>
        /// 限幅过滤。放弃掉波动过大的样本，用前一个样本代替。
        /// </summary>
        /// <param name="diffLimitations">波动最大值绝对值的向量. 都必须大于0</param>
        /// <returns>新的样本集</returns>
        public SampleCollection LimitingAmplifyFilter(IEnumerable<double> diffLimitations) {
            diffLimitations.Count().ShouldEquals(this.TotalDimensionCount);
            diffLimitations.ShouldEachSatisfy((item) => item > 0);

            SampleCollection result = this.Clone();
            CollectionHelper.ForEachThree(
                this.Columns,
                result.Columns,
                diffLimitations,
                (srcCol, resultCol, diff) => {
                    LimitingAmplifyFilter(srcCol, resultCol, diff);
                });

            return result;
        }

        /// <summary>
        /// 限幅过滤。放弃掉波动过大的数值，用前一个数值代替。
        /// </summary>
        /// <param name="diff">波动最大值绝对值</param>
        /// <returns></returns>
        protected void LimitingAmplifyFilter(IIndexable<double> values, IIndexable<double> result, double diff) {
            for (int i = 1; i < values.Count; i++) {
                var curr = values[i];
                var prev = values[i - 1];
                result[i] = limitingAmplification(diff, curr, prev);
            }
        }

        protected double limitingAmplification(double diff, double curr, double prev) {
            if (Abs(curr - prev) > diff) {
                return prev;
            }
            else {
                return curr;
            }
        }

        #endregion

        #region 中位值过滤
        /// <summary>
        /// 中位值过滤。针对每个维度, 在窗口长度内取中位值.
        /// </summary>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public SampleCollection MedianFilter(int windowLength = 5) {
            windowLength.ShouldLargerThan(0);
            windowLength.ShouldBeOdd();
            windowLength.ShouldNotLargerThan(this.Count());

            int[] medians = CollectionHelper.NewArrayWithValue(this.TotalDimensionCount, windowLength);

            return MedianFilter(medians);
        }

        /// <summary>
        /// 中位值过滤。窗口长度内取中位值
        /// </summary>
        /// <param name="windowLengths">各维度的窗口长度</param>
        /// <returns>新的样本集</returns>
        public SampleCollection MedianFilter(int[] windowLengths) {
            windowLengths.ShouldEachSatisfy((m) => m.ShouldLargerThan(0));
            windowLengths.ShouldEachSatisfy((m) => m.ShouldBeOdd());
            windowLengths.Length.ShouldNotLargerThan(this.Count());

            SampleCollection result = this.Clone();
            CollectionHelper.ForEachThree(
                this.Columns,
                result.Columns,
                windowLengths,
                (srcCol, resultCol, winlength) => {
                    LimitingAmplifyFilter(srcCol, resultCol, winlength);
                });

            return result;
        }


        protected void MedianFilter(IIndexable<double> values, IIndexable<double> result, int windowLength) {
            int half = windowLength / 2;
            for (int i = half; i < values.Count - half; i++) {
                double[] window = values.TakePythonStyle(i - half, i + half + 1).OrderBy((e) => e).ToArray();
                result[i] = window[half + 1];
            }
        }
        #endregion

        #region 算术平均值过滤
        /// <summary>
        /// 算术平均值过滤。窗口长度内取平均值
        /// </summary>
        /// <param name="windowLength">窗口长度</param>
        /// <returns>新的样本集</returns>
        public SampleCollection MeanFilter(int windowLength) {
            windowLength.ShouldBeNotLessThanZero();
            windowLength.ShouldNotLargerThan(this.Count());

            int[] medians = CollectionHelper.NewArrayWithValue(this.TotalDimensionCount, windowLength);

            return MedianFilter(medians);
        }

        /// <summary>
        /// 算术平均值过滤。窗口长度内取平均值
        /// </summary>
        /// <param name="windowLengths">各维度的窗口长度</param>
        /// <returns>新的样本集</returns>
        public SampleCollection MeanFilter(int[] windowLengths) {
            windowLengths.ShouldEachSatisfy((m) => m.ShouldLargerThan(0) && m.ShouldNotLargerThan(this.Count()));
            windowLengths.Length.ShouldEquals(this.TotalDimensionCount);

            SampleCollection result = this.Clone();
            CollectionHelper.ForEachThree(
               this.Columns,
               result.Columns,
               windowLengths,
               (srcCol, resultCol, winlength) => {
                   LimitingAmplifyFilter(srcCol, resultCol, winlength);
               });
            return result;
        }

        protected void MeanFilter(IIndexable<double> values, IIndexable<double> result, int windowLength) {
            int half = windowLength / 2;
            for (int i = half; i < values.Count - half; i++) {
                result[i] = values.TakePythonStyle(i - half, i + half + 1)
                          .ToArray()
                          .Sum((e) => e) / windowLength;
            }
        }

        #endregion

        #endregion New Region

#endregion

        #region Methods
        /// <summary>
        /// 检查字符串是否是样本集中的字段名
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns>是字段名返回真，反之假</returns>
        public static SampleCollection LoadFromCSVFile(StreamReader reader, int outputDimension = 0, bool ignoreInvalidLine = false, bool firstLineFieldName = true) {
            SampleCollection sc = new SampleCollection();
            int lineNum = 0;
            string[] fieldNames = null;
            bool validLine;
            int totalDimension = -1;
            foreach (var line in reader.ReadLines()) {
                lineNum++;
                validLine = true;
                var strValues = line.Replace(" ", "").Split(',');
                if (totalDimension == -1) {
                    totalDimension = strValues.Length;
                }
                else {
                    if (strValues.Length != totalDimension) {
                        if (ignoreInvalidLine) {
                            validLine = false;
                            continue;
                        }
                        else {
                            throw new DimensionNotMatchException("Dimension not match in line:" + lineNum.ToString());
                        }
                    }
                }
                if (lineNum == 1 && firstLineFieldName) {
                    fieldNames = strValues;
                    continue;
                }

                var values = new double[totalDimension];
                for (int i = 0; i < strValues.Length; i++) {
                    if (!double.TryParse(strValues[i], out double value)) {
                        if (ignoreInvalidLine) {
                            validLine = false;
                            break;
                        }
                        else {
                            throw new Exception("Has non-double value in line:" + lineNum.ToString());
                        }
                    }
                    values[i] = value;
                }
                if (validLine) {
                    sc.Add(new Sample(values, outputDimension, fieldNames));
                }
            }

            return sc;
        }
        #endregion


        //#region Normalization

        ///// <summary>
        ///// 是否归一化
        ///// </summary>
        //public bool IsNormalized { get { return Normalizer != null; } }
        ///// <summary>
        ///// 样本归一化器
        ///// </summary>
        //public SampleMaxMinNormalizer Normalizer { get; protected set; }
        ///// <summary>
        ///// 把当前样本集归一化
        ///// </summary>
        ///// <returns>归一化后的新样本集</returns>
        //public SampleCollection Normalize() {
        //    if (this.IsNormalized) 
        //        throw new AlreadyNormalizedExcpetion();
        //    else
        //        this.Normalizer = new SampleMaxMinNormalizer(this);
        //    var n = new SampleCollection(this._sampleData.Select(s => this.Normalizer.Normalize(s)));
        //    n.Normalizer = this.Normalizer;
        //    return n;
        //}

        ///// <summary>
        ///// 把当前样本集反归一化
        ///// </summary>
        ///// <returns>反归一化后的新样本集</returns>
        //public SampleCollection Denormalize(SampleCollection samples) {
        //    if (!samples.IsNormalized ) throw new NonNormalizedException();
        //    var n = new SampleCollection(this._sampleData.Select(s => this.Normalizer.DeNormalize(s)));
        //    n.Normalizer = this.Normalizer;
        //    return n;
        //}

        ///// <summary>
        ///// 针对输入向量中的某一维度的值进行反归一化
        ///// </summary>
        ///// <param name="value">某一维度的输入值</param>
        ///// <param name="index">在输入向量中处于第几维</param>
        ///// <returns></returns>
        //public double DenormalizeInput(double value, int index) {
        //    return value.DenormalizeMaxMin(this.Normalizer.MinInput.ElementAt(index), this.Normalizer.MaxInput.ElementAt(index));
        //}

        ///// <summary>
        ///// 针对输出向量中的某一维度的值进行反归一化
        ///// </summary>
        ///// <param name="value">某一维度的输入值</param>
        ///// <param name="index">在输出向量中处于第几维</param>
        ///// <returns></returns>
        //public double DenormalizeOutput(double value, int index = 0) {
        //    return value.DenormalizeMaxMin(this.Normalizer.MinInput.ElementAt(index), this.Normalizer.MaxOutput.ElementAt(index));
        //}
        //#endregion
    }
}