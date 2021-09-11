using Newtonsoft.Json;
using System.IO;
using ijw.Next.IO;
using System.Text;
using System;

namespace ijw.Next.Serialization.Json {
    /// <summary>
    /// 提供若干对象序列化Helper方法
    /// </summary>
    public class JsonSerializationHelper {
        #region Load Object from string
        /// <summary>
        /// 把 json 字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="str">json字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T LoadJsonObject<T>(string str) => JsonConvert.DeserializeObject<T>(str);

        /// <summary>
        /// 把 json 字符串反序列化为对象
        /// </summary>
        /// <param name="str">json字符串</param>
        /// <param name="objectType">对象类型</param>
        /// <returns>反序列化后的对象</returns>
        public static object LoadJsonObject(string str, Type objectType) => JsonConvert.DeserializeObject(str, objectType);

        #endregion

        #region Load object from file
        /// <summary>
        /// 把 json 文本文件反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="filepath">全路径文件名</param>
        /// <param name="encoding">使用的编码</param>
        /// <returns>反序列化后的对象</returns>
        public static T LoadJsonObjectFromFile<T>(string filepath, Encoding? encoding = null)
            => LoadJsonObjectFromFile<T>(filepath.AsFileInfo(), encoding);

        /// <summary>
        /// 把 json 文本文件反序列化为对象, 使用UTF8编码
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fileinfo">全路径文件名</param>
        /// <param name="encoding">使用的编码</param>
        /// <returns>反序列化后的对象</returns>
        public static T LoadJsonObjectFromFile<T>(FileInfo fileinfo, Encoding? encoding = null)
            => LoadJsonObject<T>(fileinfo.ReadToEnd(encoding));

        /// <summary>
        /// 把 json 文本文件反序列化为对象
        /// </summary>
        /// <param name="filepath">全路径文件名</param>
        /// <param name="objectType">对象类型</param>
        /// <param name="encoding">使用的编码</param>
        /// <returns>反序列化后的对象</returns>
        public static object LoadJsonObjectFromFile(string filepath, Type objectType, Encoding? encoding = null)
            => LoadJsonObjectFromFile(filepath.AsFileInfo(), objectType, encoding);
        
        /// <summary>
        /// 把 json 文本文件反序列化为对象
        /// </summary>
        /// <param name="fileinfo">全路径文件名</param>
        /// <param name="objectType"></param>
        /// <param name="encoding">使用的编码</param>
        /// <returns>反序列化后的对象</returns>
        public static object LoadJsonObjectFromFile(FileInfo fileinfo, Type objectType, Encoding? encoding = null)
            => LoadJsonObject(fileinfo.ReadToEnd(encoding), objectType);
        #endregion

        #region Save object into json string
        /// <summary>
        /// 把对象序列化为JSON字符串
        /// </summary>
        /// <param name="objToSave">欲序列化的对象</param>
        public static string SaveObjectToJsonString(object objToSave) {
            var jstring = JsonConvert.SerializeObject(objToSave);
            DebugHelper.WriteLine("Object serialized in json successfully: " + jstring);
            return jstring;
        }
        #endregion

        #region Save object into json file
        /// <summary>
        /// 把对象序列化到Json文件中
        /// </summary>
        /// <param name="objToSave">欲序列化的对象</param>
        /// <param name="filepath">文件全路径名</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static long SaveObjectToJsonFile(object objToSave, string filepath, Encoding? encoding = null) {
            string jstring = SaveObjectToJsonString(objToSave);
            jstring.WriteToFile(filepath, false, encoding);
            DebugHelper.WriteLine("into text file: " + filepath);
            return jstring.Length;
        } 
        #endregion
    }
}