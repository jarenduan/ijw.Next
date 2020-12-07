#if !NETSTANDARD1_4
using System;
using System.Linq;

namespace ijw.Next {
    /// <summary>
    /// 枚举项, 可用于绑定在UI控件上
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    public class EnumItem<T> where T : Enum {
#if !NET35
        /// <summary>
        /// 使用<seealso cref="EnumDescriptionAttribute"/>注解(或枚举名), 获取指定枚举类型的所有枚举项.
        /// </summary>
        /// <returns>所有枚举项组成的数组.</returns>
        public static EnumItem<T>[] GetAllItems()
            => GetAllItemsExcept();

        /// <summary>
        /// 获取指定枚举类型的所有枚举项.
        /// </summary>
        /// <param name="getDescriptionFunc">一个函数, 根据枚举值来获取枚举项描述</param>
        /// <returns>所有枚举项(排除项除外)组成的数组.</returns>
        public static EnumItem<T>[] GetAllItems(Func<T, string> getDescriptionFunc)
            => GetAllItemsExcept(getDescriptionFunc);

        /// <summary>
        /// 获取指定枚举类型的所有枚举项.
        /// </summary>
        /// <param name="getEnumItemFunc">一个函数, 根据枚举名称和枚举值来获取枚举项</param>
        /// <returns>所有枚举项组成的数组.</returns>
        public static EnumItem<T>[] GetAllItems(Func<T, EnumItem<T>> getEnumItemFunc) 
            //TODO: add a dict cache for T;
            => GetAllItemsExcept(getEnumItemFunc);

        /// <summary>
        /// 使用<seealso cref="EnumDescriptionAttribute"/>注解(或枚举名), 获取指定枚举类型的所有枚举项(排除项除外).
        /// </summary>
        /// <param name="except">排除项</param>
        /// <returns>所有枚举项(排除项除外)组成的数组.</returns>
        public static EnumItem<T>[] GetAllItemsExcept(params T[] except)
            => GetAllItemsExcept(v => v.GetEnumDescriptionOrName(), except);

        /// <summary>
        /// 获取指定枚举类型的所有枚举项(排除项除外).
        /// </summary>
        /// <param name="getDescriptionFunc">一个函数, 根据枚举值来获取枚举项描述</param>
        /// <param name="except">排除项</param>
        /// <returns>所有枚举项(排除项除外)组成的数组.</returns>
        public static EnumItem<T>[] GetAllItemsExcept(Func<T, string> getDescriptionFunc, params T[] except) 
            => GetAllItemsExcept(v => new EnumItem<T>(getDescriptionFunc(v), v), except);

        /// <summary>
        /// 获取指定枚举类型的所有枚举项.
        /// </summary>
        /// <param name="getEnumItemFunc">一个函数, 根据枚举名称和枚举值来获取枚举项(排除项除外)</param>
        /// <param name="except">排除项</param>
        /// <returns>所有枚举项(排除项除外)组成的数组.</returns>
        public static EnumItem<T>[] GetAllItemsExcept(Func<T, EnumItem<T>> getEnumItemFunc, params T[] except)
            //TODO: add a dict cache for T;
            => EnumHelper.GetAllValue<T>().Except(except).GetItems(getEnumItemFunc);

        /// <summary>
        /// 使用<seealso cref="EnumDescriptionAttribute"/>注解(或枚举名), 获取指定枚举类型的指定枚举项. 
        /// </summary>
        /// <returns>所有枚举项组成的数组.</returns>
        public static EnumItem<T>[] GetItems(params T[] enums) => enums.GetItems();

#endif
        /// <summary>
        /// 构造一个枚举项
        /// </summary>
        /// <param name="description">枚举的文字性描述</param>
        /// <param name="enumValue">枚举的值</param>
        public EnumItem(string description, T enumValue) {
            Type type = typeof(T);
            if (!type.IsEnum) throw new ArgumentOutOfRangeException();
            Description = description ?? throw new ArgumentNullException(nameof(description));
            EnumValue = enumValue;
        }

        /// <summary>
        /// 枚举的文字性描述, 一般用于UI展示
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 枚举的值
        /// </summary>
        public T EnumValue { get; }
    }
}
#endif