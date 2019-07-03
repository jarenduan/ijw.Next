namespace ijw.Next.DDD {
    /// <summary>
    /// 属性值变化事件的参数
    /// </summary>
    public class PropertyValueChangeEventArgs {
        /// <summary>
        /// 构造一个属性值变化事件的参数
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        public PropertyValueChangeEventArgs(string propertyName, object? oldvalue, object? newvalue) {
            this.PropertyName = propertyName;
            this.OldValue = oldvalue;
            this.NewValue = newvalue;
        }

        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; protected set; }

        /// <summary>
        /// 旧值
        /// </summary>
        public object? OldValue { get; protected set; }

        /// <summary>
        /// 新值
        /// </summary>
        public object? NewValue { get; protected set; }
    }
}