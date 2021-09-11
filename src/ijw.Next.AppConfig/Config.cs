namespace ijw.Next.AppConfig {
    /// <summary>
    /// 配置的基类
    /// </summary>
    public abstract class Config {
        /// <summary>
        /// 配置的名字
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 配置的
        /// </summary>
        internal AppConfigs AppConfig { get; set; }

        /// <summary>
        /// 更新配置内容
        /// </summary>
        public void Update() {
            AppConfig.AddOrUpdateConfig(this);
        }
    }
}