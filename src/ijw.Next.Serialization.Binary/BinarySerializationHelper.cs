//#if NET35 || NET40 || NET45 //for netcore is not support binary formatter now, 2016-06-29
//20170614 update to netstandard2.0
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ijw.Next.Serialization.Binary {
    /// <summary>
    /// 二进制序列化帮助类
    /// </summary>
    public class BinarySerializationHelper {
        private static BinaryFormatter _formatter; //cached, in case mutiple creation.

        /// <summary>
        /// 把对象序列化成字节数组
        /// </summary>
        /// <param name="objToSave"></param>
        /// <returns>序列化后的数组</returns>
        public static byte[] Serialize(object objToSave) {
            using (MemoryStream stream = new MemoryStream()) {
                Serialize(objToSave, stream);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 把对象序列化到二进制流当中
        /// </summary>
        /// <param name="objToSave">欲保存的对象(大小&lt;=4GB)</param>
        /// <param name="stream">写入的流</param>
        /// <returns>流的长度</returns>
        public static int Serialize(object objToSave, Stream stream) {
            var formatter = getBinaryFormatter();
            formatter.Serialize(stream, objToSave);
            DebugHelper.WriteLine("Object serialized in binary successfully: " + stream.Length);
            return (int)stream.Length;
        }

        /// <summary>
        /// 把对象序列化到二进制文件中
        /// </summary>
        /// <param name="objToSave">欲保存的对象</param>
        /// <param name="filename">包含路径的文件名</param>
        public static void Serialize(object objToSave, string filename) {
            FileStream fs = null;
            try {
                fs = new FileStream(filename, FileMode.Create);
                Serialize(objToSave, fs);
                DebugHelper.WriteLine("into binary file: " + filename);
            }
            catch (Exception ex) {
                fs?.Close();
                fs?.Dispose();
                try {
                    File.Delete(filename);
                }
                finally {
                    throw ex;
                }
            }
            finally {
                fs?.Close();
                fs?.Dispose();
            }
        }

        /// <summary>
        /// 把二进制数组反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="bytes">存储对象的字节数组</param>
        /// <returns>反序列化后的对象</returns>
        public static T Deserialize<T>(Byte[] bytes) {
            using (MemoryStream mem = new MemoryStream(bytes)) {
                return Deserialize<T>(mem);
            }
        }

        /// <summary>
        /// 从二进制流中反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="stream">二进制流</param>
        /// <returns>反序列化后的对象</returns>
        public static T Deserialize<T>(Stream stream) {
            var formatter = getBinaryFormatter();
            DebugHelper.WriteLine("Object deserializing: " + stream.Length.ToString());
#if DEBUG
            try {
#endif
                T obj = (T)formatter.Deserialize(stream);
                DebugHelper.WriteLine("Object deserialized successfully: " + obj.ToString());
                return obj;
#if DEBUG
            }
            catch (Exception ex) {
                DebugHelper.WriteLine(ex.Message);
                throw ex;
            }
#endif
        }

        /// <summary>
        /// 把二进制文件反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="filename">全路径文件名</param>
        /// <returns>反序列化后的对象</returns>
        public static T Deserialize<T>(string filename) {
            using (FileStream fs = File.Open(filename, FileMode.Open)) {
                var obj = Deserialize<T>(fs);
                DebugHelper.WriteLine("from binary file: " + filename);
                return obj;
            }
        }

        private static BinaryFormatter getBinaryFormatter() {
            if (_formatter == null) {
                _formatter = new BinaryFormatter();
            }
            return _formatter;
        }
    }
}