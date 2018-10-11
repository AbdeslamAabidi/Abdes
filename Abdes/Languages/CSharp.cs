using Abdes.Data;
using Abdes.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Abdes.Languages
{
    public class CSharp
    {
        readonly string NameSpace_Class = "Abdes.Program";
        readonly string MainMethod = @"Main";

        public CSharp()
        {
        }

        public dynamic Results(string code)
        {
            // Create Result Object
            var results = new Results();

            if (!Helper.IsRegistredScript(code))
            {
                results.Status = Enum.Status.unregistred;
                results.Message = MessageResult.unregistredscript;

                return results;
            }

            try
            {
                var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);
                MetadataReference[] references = new MetadataReference[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll"))
                };

                string assemblyName = Path.GetRandomFileName();
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
                CSharpCompilation compilation = CSharpCompilation.Create(assemblyName,
                                                                         syntaxTrees: new[] { syntaxTree },
                                                                         references: references,
                                                                         options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                using (var ms = new MemoryStream())
                {
                    EmitResult result = compilation.Emit(ms);

                    if (!result.Success)
                    {
                        IEnumerable<Diagnostic> failures = result.Diagnostics
                                                                 .Where(diagnostic => diagnostic.IsWarningAsError
                                                                        ||
                                                                        diagnostic.Severity == DiagnosticSeverity.Error);

                        results.Status = Enum.Status.error;
                        foreach (Diagnostic diagnostic in failures)
                        {
                            results.Message += string.Format("{0}: {1}\n", diagnostic.Id, diagnostic.GetMessage());
                        }

                        return results;
                    }
                    else
                    {
                        ms.Seek(0, SeekOrigin.Begin);
                        Assembly assembly = Assembly.Load(ms.ToArray());
                        Type type = assembly.GetType(NameSpace_Class);
                        object obj = Activator.CreateInstance(type);

                        results.Data = type.InvokeMember(MainMethod,
                                                         BindingFlags.Default | BindingFlags.InvokeMethod,
                                                         null,
                                                         obj,
                                                         null);

                        results.Status = Enum.Status.success;
                        results.Message = MessageResult.success;
                    }
                }
            }
            catch (Exception ex)
            {
                results.Status = Enum.Status.error;
                results.Message = ex.Message;
            }

            return results;
        }
    }
}
