using System;

namespace ijw.Next {
    /// <summary>
    /// 枚举成员的文字性描述, 可用于UI展示
    /// </summary>
    public class EnumDescriptionAttribute : Attribute {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="description"></param>
        public EnumDescriptionAttribute(string description) => Description = description ?? throw new ArgumentNullException(nameof(description));

        /// <summary>
        /// 文字描述
        /// </summary>
        public string Description { get; }
    }
}