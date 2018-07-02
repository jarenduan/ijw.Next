using System.Collections.Generic;
using System.Linq;

namespace ijw.Next.Maths {
    /// <summary>
    /// 默认实现
    /// </summary>
    public abstract class MimoModelBase : IMimoMathModel {
        /// <summary>
        /// 输入
        /// </summary>
        public IEnumerable<double> Input {
            get => _input;
            set {
                if (value.Count() != _input.Length)
                    throw new DimensionNotMatchException();
                _input = value.ToArray();
            }
        }

        /// <summary>
        /// 输出
        /// </summary>
        public IEnumerable<double> Output => _output;

        /// <summary>
        /// 输入维度
        /// </summary>
        public int InputDimension => _input.Length; 

        /// <summary>
        /// 输出维度
        /// </summary>
        public int OutputDimension => _output.Length;

        /// <summary>
        /// 使用数组实现的构造函数
        /// </summary>
        /// <param name="inputDimension"></param>
        /// <param name="outputDimension"></param>
        public MimoModelBase(int inputDimension, int outputDimension) {
            this._input = new double[inputDimension];
            this._output = new double[outputDimension];
        }

        /// <summary>
        /// 根据当前的输入进行计算，更新输出
        /// </summary>
        public abstract void Calculate();

        private double[] _input;
        private double[] _output;
    }
}