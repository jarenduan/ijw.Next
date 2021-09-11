using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ijw.Next.Collection;

namespace ijw.Next.Reflection.CodeGen {
    /// <summary>
    /// Code generation helper class.
    /// </summary>
    public class CodeGenHelper {
        /// <summary>
        /// 动态编译
        /// </summary>
        /// <param name="code">需要动态编译的代码</param>
        /// <returns>动态生成的程序集</returns>
        public static Assembly GenerateAssemblyFromCode(string code) {
            // 丛代码中转换表达式树
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            // 随机程序集名称
            var assemblyName = Path.GetRandomFileName();
            // 引用
            var references = AppDomain.CurrentDomain.GetAssemblies()
                                                    .Where(x => !x.IsDynamic)
                                                    .Select(x => MetadataReference.CreateFromFile(x.Location));

            // 创建编译对象
            var compilation = CSharpCompilation.Create(assemblyName,
                                                       new[] { syntaxTree },
                                                       references,
                                                       new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using var ms = new MemoryStream();

            // 将编译好的IL代码放入内存流
            var result = compilation.Emit(ms);

            if (result.Success) {
                // 编译成功，从内存中加载编译好的程序集
                ms.Seek(0, SeekOrigin.Begin);
                return Assembly.Load(ms.ToArray());
            }
            else {
                // 编译失败，提示
                var failures = result.Diagnostics.Where(diag => diag.IsWarningAsError || diag.Severity == DiagnosticSeverity.Error);
                throw new CompliationFailedException(failures.ToAllEnumStrings(f => $"{f.Id}: {f.GetMessage()}", separator: Environment.NewLine));
            }
        }
    }
}
