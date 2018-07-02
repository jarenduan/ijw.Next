#if !NET35
using ijw.Next.Collection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ijw.Next.Maths {
    /// <summary>
    /// 向量
    /// </summary>
    public class Vector : Indexable<double>, IEquatable<Vector> {

        #region Construct

        /// <summary>
        /// 构造一个空向量，各维度为0.
        /// </summary>
        /// <param name="dimension"></param>
        internal Vector(int dimension) : base(dimension) {
        }

        /// <summary>
        /// 用双精度浮点数的数组构造一个向量
        /// </summary>
        /// <param name="data">双精度浮点数的数组</param>
        public Vector(IEnumerable<double> data) : base(data) {
        }

        /// <summary>
        /// 用一系列双精度浮点数构造一个向量
        /// </summary>
        public Vector(params double[] data) : base(data) {
        }
        #endregion Construct

        #region Properties

        /// <summary>
        /// 向量维度
        /// </summary>
        public int Dimension => Count;

        #endregion Properties

        #region Initialization Methods

        /// <summary>
        /// 生成一个单位向量
        /// </summary>
        /// <param name="dimension">向量维度</param>
        /// <returns>生成的单位向量</returns>
        public static Vector One(int dimension) {
            var result = new Vector(dimension)
            {
                _data = ArrayHelper.NewArrayWithValue(dimension, 1.0d)
            };
            return result;
        }

        /// <summary>
        /// 生成一个零向量
        /// </summary>
        /// <param name="dimension">向量维度</param>
        /// <returns>生成的零向量</returns>
        public static Vector Zero(int dimension) {
            var result = new Vector(dimension)
            {
                _data = ArrayHelper.NewArrayWithValue(dimension, 0d)
            };
            ;
            return result;
        }

        /// <summary>
        /// 生成一个随机的向量
        /// </summary>
        /// <param name="dimension">向量维度</param>
        /// <returns>新生成的向量</returns>
        public static Vector RandomNew(int dimension) {
            Random r = new Random();
            var result = new Vector(dimension);
            for (int i = 0; i < dimension; i++) {
                result._data[i] = r.NextDouble();
            }
            return result;
        }

        #endregion Initialization Methods

        #region Calculation Methods

        /// <summary>
        /// 向量加法. 此方法不返回新的向量, 将会改变自身的值
        /// </summary>
        /// <param name="addend"></param>
        public void AddToSelf(Vector addend) {
            if (addend.Dimension != this.Dimension) {
                throw new DimensionNotMatchException();
            }
            for (int i = 0; i < this._data.Length; i++) {
                this._data[i] += addend._data[i];
            }
        }

        /// <summary>
        /// 向量加法，返回新的向量
        /// </summary>
        /// <param name="addend">加数</param>
        /// <returns>新的向量，表示和</returns>
        public Vector Plus(Vector addend) {
            if (this.Dimension != addend.Dimension) {
                throw new DimensionNotMatchException();
            }
            Vector result = new Vector(this.Dimension);
            for (int i = 0; i < result._data.Length; i++) {
                result._data[i] = this._data[i] + addend._data[i];
            }
            return result;
        }

        /// <summary>
        /// 向量减法，返回新的向量
        /// </summary>
        /// <param name="anotherVector">减去</param>
        /// <returns>新的向量，表示差</returns>
        public Vector Minus(Vector anotherVector) {
            this.Dimension.ShouldEquals(anotherVector.Dimension);
            Vector result = new Vector(this.Dimension);
            for (int i = 0; i < result._data.Length; i++) {
                result._data[i] = this._data[i] - anotherVector._data[i];
            }
            return result;
        }

        /// <summary>
        /// 向量乘法，返回新的向量
        /// </summary>
        /// <param name="number">乘以</param>
        /// <returns>新的向量，表示向量和数字的乘积</returns>
        public Vector Multipy(double number) {
            Vector result = new Vector(this.Dimension);
            for (int i = 0; i < result._data.Length; i++) {
                result._data[i] = this._data[i] * number;
            }
            return result;
        }

        /// <summary>
        /// 向量点积，返回新的向量
        /// </summary>
        /// <param name="anotherVector">另一个向量</param>
        /// <returns>新的向量，表示向量的点积</returns>
        public double GetDotProduct(Vector anotherVector) {
            this.Dimension.ShouldEquals(anotherVector.Dimension);
            return (this._data, anotherVector._data).ForEachPairSelect((i, j) => i * j).Sum();
        }

        /// <summary>
        /// 向量叉乘，返回新的向量
        /// </summary>
        /// <param name="anotherVector">另一个向量</param>
        /// <returns>新的向量，表示向量的叉乘</returns>
        public Vector GetCrossProduct(Vector anotherVector) {
            if (this.Dimension != 3 || anotherVector.Dimension != 3) {
                throw new NotSupportedException("Cross product calculation only support 3-dimensional vectors.");
            }

            Vector result = new Vector(3);

            result._data[0] = this[1] * anotherVector[2] - this[2] * anotherVector[1];
            result._data[1] = this[2] * anotherVector[0] - this[0] * anotherVector[2];
            result._data[2] = this[0] * anotherVector[1] - this[1] * anotherVector[0];

            return result;
        }

        #endregion Calculation Methods

        #region Operators Overload

        /// <summary>
        ///  + 操作符重载, 执行向量加法
        ///  注意, 该方法行为像值类型一样, 每次都返回新的浮点向量
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector operator +(Vector a, Vector b) {
            return a.Plus(b);
        }

        /// <summary>
        ///  - 操作符重载, 执行向量减法
        ///  注意, 该方法行为像值类型一样, 每次都返回新的浮点向量
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector operator -(Vector a, Vector b) {
            return a.Minus(b);
        }

        /// <summary>
        /// 操作符 * 重载, 实现向量和浮点数的乘法
        ///  注意, 该方法行为像值类型一样, 每次都返回新的浮点向量
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector operator *(Vector a, double b) {
            return a.Multipy(b);
        }

        /// <summary>
        /// 操作符 * 重载, 实现向量和浮点数的乘法.
        ///  注意, 该方法行为像值类型一样, 每次都返回新的浮点向量
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector operator *(double b, Vector a) {
            return a * b;
        }

        /// <summary>
        /// 操作符 / 重载, 实现向量除以浮点数的除法
        ///  注意, 该方法行为像值类型一样, 每次都返回新的浮点向量
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector operator /(Vector a, double b) {
            return 1 / b * a;
        }

        #endregion Operators Overload

        #region Distance Methods
        /// <summary>
        /// 计算向量间的加权欧式距离
        /// </summary>
        /// <param name="anotherVector">另一个向量</param>
        /// <returns>向量间的加权欧式距离</returns>
        public double EuclideanDistanceFrom(Vector anotherVector) {
            return new EuclideanDistance().GetDistance(this, anotherVector);
        }

        /// <summary>
        /// 计算向量间的加权曼哈顿距离
        /// </summary>
        /// <param name="anotherVector">另一个向量</param>
        /// <param name="weights">各维度的权值</param>
        /// <returns>向量间的加权曼哈顿距离</returns>
        public double EuclideanDistanceFrom(Vector anotherVector, Vector weights) {
            return new WeightedEuclideanDistance(weights).GetDistance(this, anotherVector);

        }

        /// <summary>
        /// 计算向量间的曼哈顿距离
        /// </summary>
        /// <param name="anotherVector">另一个向量</param>
        /// <returns>向量间的曼哈顿距离</returns>
        public double ManhattanDistanceFrom(Vector anotherVector) {
            return new ManhattanDistance().GetDistance(this, anotherVector);
        }

        /// <summary>
        /// 计算向量间的加权曼哈顿距离
        /// </summary>
        /// <param name="anotherVector">另一个向量</param>
        /// <param name="weights">各维度的权值</param>
        /// <returns>向量间的加权曼哈顿距离</returns>
        public double ManhattanDistanceFrom(Vector anotherVector, Vector weights) {
            return new WeightedManhattanDistance(weights).GetDistance(this, anotherVector);
        }

        /// <summary>
        /// 计算向量间的闵氏距离
        /// </summary>
        /// <param name="anotherVector">另一个向量</param>
        /// <param name="lambda">分量的幂值</param>
        /// <returns>向量间的闵氏距离</returns>
        public double MinkowskiDistanceFrom(Vector anotherVector, int lambda) {
            return new MinkowskiDistance(lambda).GetDistance(this, anotherVector);
        }

        /// <summary>
        /// 计算向量间的加权闵氏距离
        /// </summary>
        /// <param name="anotherVector">另一个向量</param>
        /// <param name="lambda">分量的幂值</param>
        /// <param name="weights">各维度的权值</param>
        /// <returns>向量间的加权闵氏距离</returns>
        public double MinkowskiDistanceFrom(Vector anotherVector, int lambda, Vector weights) {
            return new WeightedMinkowskiDistance(lambda, weights).GetDistance(this, anotherVector);
        }

        #endregion Distance Methods

        #region Equals Methods

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="obj">比较的对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj) {
            return this.Equals(obj as Vector);
        }

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="other">比较的对象</param>
        /// <returns>是否相等</returns>
        public bool Equals(Vector other) {
            return this.ItemEquals(other);
        }

        /// <summary>
        /// 容忍精度的相等比较
        /// </summary>
        /// <param name="other">比较的向量</param>
        /// <param name="errorTolerance">容忍的精度</param>
        /// <returns>差别是否小于指定的精度</returns>
        public bool Equals(Vector other, double errorTolerance) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.Dimension != other.Dimension) return false;
            if (double.IsNaN(errorTolerance)) {
                return this.ItemEquals(other);
            }
            else {
                return this.ItemEquals(other, (d1, d2) => {
                    return Math.Abs(d1 - d2) <= errorTolerance;
                });
            }
        }

        /// <summary>
        /// 容忍精度的相等比较
        /// </summary>
        /// <param name="other">比较的向量</param>
        /// <param name="errorTolerance">容忍的精度</param>
        /// <returns>差别是否小于指定的精度</returns>
        public bool Equals(Vector other, IIndexable<double> errorTolerance) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.Dimension != other.Dimension) return false;



            return this.ItemEquals(other, (d1, d2, i) => Math.Abs(d1 - d2) <= errorTolerance[i]);
        }

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="other">比较的向量</param>
        /// <param name="equalityComparer">比较器</param>
        /// <returns>是否相等</returns>
        public bool Equlas(Vector other, IEqualityComparer<Vector> equalityComparer) {
            equalityComparer.ShouldBeNotNullArgument(nameof(equalityComparer));
            return equalityComparer.Equals(this, other);
        }

        
        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <returns>hash值</returns>
        public override int GetHashCode() {
            // Don't ask me why, it comes from Math.Net.
            var hashNum = Math.Min(this.Dimension, 25);
            int hash = 17;
            unchecked {
                for (var i = 0; i < hashNum; i++) {
                    hash = hash * 31 + this[i].GetHashCode();
                }
            }
            return hash;
        }

        #endregion Equals Methods

        #region ToString

        /// <summary>
        /// 生成字符串
        /// </summary>
        /// <returns>表示自身部分内容的字符串</returns>
        public override string ToString() {
            return this.ToSimpleEnumStrings(5, "<", ">");
        }

        #endregion ToString
    }
}
#endif