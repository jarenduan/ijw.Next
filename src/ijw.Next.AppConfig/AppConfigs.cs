using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ijw.Next.IO;
using ijw.Next.Serialization.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ijw.Next.AppConfig {
    /// <summary>
    /// 配置管理器
    /// </summary>
    public class AppConfigs {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configFileFullName">配置文件的路径名</param>
        public AppConfigs(string configFileFullName = null) 
            => ConfigFileFullName = configFileFullName ?? Path.Combine(Environment.CurrentDirectory, "app.config");

        /// <summary>
        /// 配置文件的路径名
        /// </summary>
        public string ConfigFileFullName { get; }

        /// <summary>
        /// 每次有变动, 是否自动保存
        /// </summary>
        public bool AutoSave { get; set; } = true;

        /// <summary>
        /// 装载配置, 如果文件不存在将创建空的配置.
        /// </summary>
        public void LoadOrCreate() {
            string json = null;
            try {
                json = ConfigFileFullName.AsFileInfo().ReadToEnd();
            }
            catch {
                return;
            }
            var jsonDoc = JObject.Parse(json);
            _configJsons.Clear();
            foreach (var c in jsonDoc.Properties()) {
                _configJsons.Add(c.Name, c.Value.ToString());
            }
        }

        /// <summary>
        /// 获取指定的配置
        /// </summary>
        /// <typeparam name="T">配置的类型</typeparam>
        /// <returns>获得的配置, 如果不存在, 返回空的配置</returns>
        public T GetConfig<T>() where T : Config => GetConfig<T>(typeof(T).Name);

        /// <summary>
        /// 获取指定的配置
        /// </summary>
        /// <typeparam name="T">配置的类型</typeparam>
        /// <param name="configName">配置的名称</param>
        /// <returns>获得的配置, 如果不存在, 返回空的配置</returns>
        public T GetConfig<T>(string configName) where T: Config {
            T config = null;
            if (_configJsons.TryGetValue(configName, out var configJson)) {
                config = JsonConvert.DeserializeObject<T>(configJson);
                config.Name = configName;
                config.AppConfig = this;
            }
            return config;
        }

        /// <summary>
        /// 获取指定的配置
        /// </summary>
        /// <typeparam name="T">配置的类型</typeparam>
        /// <param name="configName">配置的名称</param>
        /// <param name="createDefaultFunc">没有配置使用此函数创建默认实例. null则使用new()构造默认的实例.</param>
        /// <returns>获得的配置, 如果不存在, 返回空的配置</returns>
        public T GetOrCreateConfig<T>(string? configName = null, Func<T>? createDefaultFunc = null) where T : Config, new() {
            if (configName is null) {
                configName = typeof(T).Name;
            }
            var config = GetConfig<T>(configName);
            if (config is null) {
                config = createDefaultFunc?.Invoke() ?? new T();
                config.Name = configName;
            }

            config.AppConfig = this;

            return config;
        }

        /// <summary>
        /// 获取指定的配置
        /// </summary>
        /// <typeparam name="T">配置的类型</typeparam>
        /// <param name="configName">配置的名称</param>
        /// <param name="jsonfilename">没有配置使用此json文件创建默认实例. null empty或者则使用new()构造默认的实例.</param>
        /// <returns>获得的配置, 如果不存在, 返回空的配置</returns>
        public T GetOrCreateConfigByFile<T>(string? configName, string jsonfilename) where T : Config, new() {
            if (jsonfilename.IsNullOrEmpty()) {
                return GetOrCreateConfig<T>(configName);
            }
            else {
                return GetOrCreateConfig<T>(configName, () => JsonSerializationHelper.LoadJsonObjectFromFile<T>(jsonfilename));
            }
        }


        /// <summary>
        /// 更新配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        internal void AddOrUpdateConfig<T>(T config) where T: Config {
            var j = JsonConvert.SerializeObject(config);
            _configJsons[config.Name] = j;

            if (AutoSave) {
                Save();
            }
        }

        /// <summary>
        /// 保存到配置文件
        /// </summary>
        public void Save() {
            var sb = new StringBuilder();
            sb.Append("{");
            foreach (var item in _configJsons) {
                sb.Append("\"").Append(item.Key).Append("\":")
                    .Append(item.Value)
                    .Append(",");
            }
            sb.RemoveLast(",");
            sb.Append("}");
            sb.ToString().WriteToFile(ConfigFileFullName);
        }

        /// <summary>
        /// 内部存储
        /// </summary>
        private readonly Dictionary<string, string> _configJsons = new ();
    }
}