namespace ijw.Next.Maths {
    /// <summary>
    /// 矩阵
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Matrix<T> {
        /// <summary>
        /// 列数
        /// </summary>
        public int CountOfX { get; private set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int CountOfY { get; private set; }

        /// <summary>
        /// 内部数据
        /// </summary>
        public T[,] Data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="countOfX"></param>
        /// <param name="countOfY"></param>
        public Matrix(int countOfX, int countOfY) {
            this.CountOfX = countOfX;
            this.CountOfY = countOfY;
            this.Data = new T[countOfX, countOfY];
        }
    }
}