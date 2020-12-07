using System;

namespace ijw.Next {
    /// <summary>
    /// 标注数据项的索引
    /// </summary>
    public class DataIndexAttribute : Attribute {
        /// <summary>
        /// 构建数据项的索引
        /// </summary>
        /// <param name="index"></param>
        public DataIndexAttribute(int index) => Index = index;

        /// <summary>
        /// 数据项的索引索引
        /// </summary>
        public int Index { get; }
    }
}
