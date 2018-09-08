namespace ijw.Next.Serialization.Json {
    /// <summary>
    /// 
    /// </summary>
    public static class StringExt {
        /// <summary>
        /// 从Json字符串反序列化出指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns>反序列化出的对象</returns>
        public static T DeserializeJsonObject<T>(this string jsonString) {
            return JsonSerializationHelper.LoadJsonObject<T>(jsonString);
        }
    }
}
