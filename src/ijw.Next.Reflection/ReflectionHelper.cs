using System;
using System.Linq.Expressions;
using System.Linq;
using ijw.Next.Collection;

namespace ijw.Next.Reflection {
    /// <summary>
    /// 反射功能帮助类
    /// </summary>
    public static class ReflectionHelper {
        /// <summary>
        /// 根据属性名列表和值（字符串形式）列表创建指定类型的新实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyNames">属性名列表</param>
        /// <param name="values">字符串形式的值列表</param>
        /// <returns>创建的新实例</returns>
        public static T CreateNewInstance<T>(string[] propertyNames, string[] values) where T : class, new() {
            propertyNames.ShouldBeNotNullArgument();
            int fieldCount = propertyNames.Count();
            values.ShouldBeNotNullArgument();
            values.ShouldSatisfy(s => s.Count() == fieldCount);

            T obj = new T();
            (values, propertyNames).ForEachPair((s, h) => {
                obj.SetPropertyValue(h, s);
            });

            return obj;
        }

        /// <summary>
        /// 获取属性的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr">实例属性的表达式, 如foo => foo.bar </param>
        /// <returns>属性的名称</returns>
        public static string GetPropertyName<T>(Expression<Func<T, object>> expr) {
            var rtn = "";
            if (expr.Body is UnaryExpression) {
                rtn = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression) {
                rtn = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression) {
                rtn = ((ParameterExpression)expr.Body).Type.Name;
            }
            return rtn;
        }
    }
}
