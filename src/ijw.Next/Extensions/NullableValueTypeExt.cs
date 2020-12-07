namespace ijw.Next {
    /// <summary>
    /// 
    /// </summary>
    public static class NullableValueTypeExt {
        /// <summary>
        /// 获取可空值类型变量的值, 空值用默认值代替 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <param name="defaultValue">默认值, 缺省为default</param>
        /// <returns>内部值</returns>
        public static T GetValueAnyway<T>(this T? v, T defaultValue = default) where T : struct {
            return v is null ? defaultValue : v.Value;
        }
    }
}
