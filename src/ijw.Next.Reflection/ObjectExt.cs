using System;
using System.Linq;
using System.Reflection;

namespace ijw.Next.Reflection {
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExt {
        //TODO: netstandard1.4 impl
#if !NETSTANDARD1_4
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性名</param>
        /// <param name="para">参数名, 默认为null</param>
        /// <returns>属性值</returns>
        public static object? Get(this object obj, string propertyName, object[]? para = null) 
            => obj.GetType().GetProperties().Where(p => p.Name == propertyName).FirstOrDefault()?.GetValue(obj, para);

        /// <summary>
        /// 获取强类型的属性值
        /// </summary>
        /// <typeparam name="T">属性值的类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性名</param>
        /// <param name="para">参数名, 默认为null</param>
        /// <returns>属性值</returns>
        public static T? Get<T>(this object obj, string propertyName, object[]? para = null) where T : class
            => obj.Get(propertyName, para) as T;
#endif
        /// <summary>
        /// 设置指定的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性的名字</param>
        /// <param name="value">属性值</param>
        /// <remarks>
        /// 属性值运行时类型如果不符合, 将会抛出异常
        /// </remarks>
        public static void Set<T>(this T obj, string propertyName, object? value) {
            var pi = obj?.GetType().GetPropertyInfo(propertyName);
            if (pi is null) throw new ArgumentOutOfRangeException(propertyName);
            pi.SetValue(obj, value, null);
        }

#if NETSTANDARD2_0 || NETSTANDARD2_1 
        /// <summary>
        /// 根据JsonElement对象, 设置指定的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性的名字</param>
        /// <param name="jsonValue">JsonElement 属性值</param>
        /// <remarks>
        /// 属性值运行时类型如果不符合, 将会抛出异常
        /// </remarks>
        public static void Set<T>(this T obj, string propertyName, System.Text.Json.JsonElement jsonValue) {
            var pi = obj?.GetType().GetPropertyInfo(propertyName);
            if (pi is null) throw new ArgumentOutOfRangeException(propertyName);
            var type = pi.PropertyType;
            if (type == typeof(string)) {
                pi.SetValue(obj, jsonValue.GetString());
            }
            else if (type == typeof(Int16)) {
                pi.SetValue(obj, jsonValue.GetInt16());
            }
            else if (type == typeof(Int32)) {
                pi.SetValue(obj, jsonValue.GetInt32());
            }
            else if (type == typeof(Int64)) {
                pi.SetValue(obj, jsonValue.GetInt64());
            }
            else if (type == typeof(Single)) {
                pi.SetValue(obj, jsonValue.GetSingle());
            }
            else if (type == typeof(Double)) {
                pi.SetValue(obj, jsonValue.GetDouble());
            }
            else if (type == typeof(Decimal)) {
                pi.SetValue(obj, jsonValue.GetDecimal());
            }
            else if (type == typeof(DateTime)) {
                pi.SetValue(obj, jsonValue.GetDateTime());
            }
            else if (type == typeof(UInt16?)) {
                pi.SetValue(obj, jsonValue.GetUInt16());
            }
            else if (type == typeof(UInt32?)) {
                pi.SetValue(obj, jsonValue.GetUInt32());
            }
            else if (type == typeof(UInt64?)) {
                pi.SetValue(obj, jsonValue.GetUInt64());
            }
            else if (type == typeof(Boolean)) {
                pi.SetValue(obj, jsonValue.GetBoolean());
            }
            else if (type == typeof(Byte)) {
                pi.SetValue(obj, jsonValue.GetByte());
            }
            else if (type == typeof(SByte)) {
                pi.SetValue(obj, jsonValue.GetSByte());
            }
            else if (type == typeof(Guid)) {
                pi.SetValue(obj, jsonValue.GetGuid());
            }
            else {
                throw new TypeNotSupportException(type);
            }
        }
#endif
        /// <summary>
        /// 将字符串尝试转型成属性的类型(用默认的FormatProvider), 并把成功转型后的值设置给指定的属性. 多用于从文本文件中构建对象.
        /// 转型失败将抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性的名字, 必须存在</param>
        /// <param name="stringvalue">属性值</param>
        public static void Set<T>(this T obj, string propertyName, string? stringvalue) {
            var pi = obj?.GetType().GetPropertyInfo(propertyName);
            if (pi is null) throw new ArgumentOutOfRangeException(propertyName);
            if (pi.PropertyType == typeof(string)) {
                pi.SetValue(obj, stringvalue, null);
            }
            else {
                var typedValue = stringvalue?.ToType(pi.PropertyType);
                pi.SetValue(obj, typedValue, null);
            }
        }

        /// <summary>
        /// 将字符串尝试转型成属性的类型（用默认的FormatProvider）, 并把成功转型后的值设置给指定的属性. 多用于从文本文件中构建对象.
        /// 转型失败将抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性的名字, 必须存在</param>
        /// <param name="stringvalue">属性值</param>
        public static void SetPropertyValue<T>(this T obj, string propertyName, string? stringvalue) {
            PropertyInfo pi = typeof(T).GetPropertyInfo(propertyName);
            if (pi is null) throw new ArgumentOutOfRangeException(propertyName);
            if (pi.PropertyType == typeof(string)) {
                pi.SetValue(obj, stringvalue, null);
            }
            else {
                var typedValue = stringvalue?.ToType(pi.PropertyType); 
                pi.SetValue(obj, typedValue, null);
            }
        }

        /// <summary>
        /// 设置指定的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性的名字</param>
        /// <param name="value">属性值</param>
        /// <remarks>
        /// 属性值运行时类型如果不符合, 将会抛出异常
        /// </remarks>
        public static void SetPropertyValue<T>(this T obj, string propertyName, object? value) {
            PropertyInfo pi = typeof(T).GetPropertyInfo(propertyName);
            if (pi is null) throw new ArgumentOutOfRangeException(propertyName);
            pi.SetValue(obj, value, null);
        }

#if !NETSTANDARD1_4
        /// <summary>
        /// 使用指定的方法, 填充基本类型的属性值. 支持int16/32/64, single/double/decimal, datetime, string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="getPropertyValue">获取指定属性名的值, 例如可用IDataRecord[属性名]</param>
        public static T FillPropertiesOfBasicType<T>(this T obj, Func<PropertyInfo, object?> getPropertyValue) where T : class{
            obj.ShouldBeNotNull();

            foreach (var p in typeof(T).GetProperties()) {
                if (!p.CanWrite) continue;

                var name = p.Name;
                var type = p.PropertyType;
                object? value = null;

                try {
                    value = getPropertyValue(p);
                }
                catch {
                    continue;
                }

                if (type == typeof(string)) {
#if NETSTANDARD2_0 || NETSTANDARD2_1
                    if (!p.HasAttribute<IgnoreFillingAttribute>()) {
                        if (p.IsNullable()) {
                            obj.SetPropertyValue(name, value.ToStringNullable());
                        }
                        else {
                            if (value is null) continue;
                            obj.SetPropertyValue(name, value.ToString());
                        }
                    }
#else
                    obj.SetPropertyValue(name, value.ToStringNullable());
#endif
                }
                else if (type == typeof(Int16)) {
                    if (value is null || value is DBNull) continue;
                    obj.SetPropertyValue(name, value.ToInt16());
                }
                else if (type == typeof(Int32)) {
                    if (value is null || value is DBNull) continue;
                    obj.SetPropertyValue(name, value.ToInt32());
                }
                else if (type == typeof(Int64)) {
                    if (value is null || value is DBNull) continue;
                    obj.SetPropertyValue(name, value.ToInt64());
                }
                else if (type == typeof(Single)) {
                    if (value is null || value is DBNull) continue;
                    obj.SetPropertyValue(name, value.ToFloat());
                }
                else if (type == typeof(Double)) {
                    if (value is null || value is DBNull) continue;
                    obj.SetPropertyValue(name, value.ToDouble());
                }
                else if (type == typeof(Decimal)) {
                    if (value is null || value is DBNull) continue;
                    obj.SetPropertyValue(name, value.ToDecimal());
                }
                else if (type == typeof(DateTime)) {
                    if (value is null || value is DBNull) continue;
                    obj.SetPropertyValue(name, value.ToDateTime());
                }
                else if (type == typeof(Int16?)) {
                    obj.SetPropertyValue(name, value.ToInt16Nullable());
                }
                else if (type == typeof(Int32?)) {
                    obj.SetPropertyValue(name, value.ToInt32Nullable());
                }
                else if (type == typeof(Int64?)) {
                    obj.SetPropertyValue(name, value.ToInt64Nullable());
                }
                else if (type == typeof(Single?)) {
                    obj.SetPropertyValue(name, value.ToFloatNullable());
                }
                else if (type == typeof(Double?)) {
                    obj.SetPropertyValue(name, value.ToDoubleNullable());
                }
                else if (type == typeof(Decimal?)) {
                    obj.SetPropertyValue(name, value.ToDecimalNullable());
                }
                else if (type == typeof(DateTime?)) {
                    obj.SetPropertyValue(name, value.ToDateTimeNullable());
                }
                else if (type.IsEnum) {
                    if (value is null || value is DBNull) continue;
                    obj.SetPropertyValue(name, value.ToStringNullable());
                }
                else {

                }
            }

            return obj;
        }
#endif
        /// <summary>
        /// 反射调用某个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static object? InvokeMethod<T>(this T obj, string methodName, params object[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName);
            return mi != null ? mi.Invoke(obj, paras) : throw new ArgumentOutOfRangeException(methodName);
        }

        /// <summary>
        /// 反射调用某个泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="types">泛型参数列表</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static object? InvokeGenericMethod<T>(this T obj, string methodName, Type[] types, params object[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName).MakeGenericMethod(types);
            return mi != null ? mi.Invoke(obj, paras) : throw new ArgumentOutOfRangeException(methodName);
        }

        /// <summary>
        /// 尝试反射调用某个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static bool TryInvokeMethod<T>(this T obj, string methodName, params object?[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName);
            mi?.Invoke(obj, paras);
            return !(mi is null);
        }

        /// <summary>
        /// 尝试反射调用某个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="types">泛型参数列表</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static bool TryInvokeGenericMethod<T>(this T obj, string methodName, Type[] types, params object?[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName).MakeGenericMethod(types);
            mi?.Invoke(obj, paras);
            return !(mi is null);
        }

        /// <summary>
        /// 尝试反射调用某个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="result">返回结果</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static bool TryInvokeMethod<T>(this T obj, string methodName, out object? result, params object?[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName);
            result = mi?.Invoke(obj, paras);
            return !(mi is null);
        }

        /// <summary>
        /// 尝试反射调用某个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="types">泛型参数列表</param>
        /// <param name="result">返回结果</param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public static bool TryInvokeGenericMethod<T>(this T obj, string methodName, Type[] types, out object? result, params object?[] paras) {
            MethodInfo mi = typeof(T).GetMethodInfo(methodName).MakeGenericMethod(types);
            result = mi?.Invoke(obj, paras);
            return !(mi is null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool EqualsNullable<T>(this T obj, T other) =>
            (obj is null && other is null) || (!(obj is null) && obj.Equals(other));
    }
}