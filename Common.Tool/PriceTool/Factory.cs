using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace Common.Tool.PriceTool
{
    public class PriceMatchFactory
    {
        private static ConcurrentDictionary<string, IPriceMatch> dic = new ConcurrentDictionary<string, IPriceMatch>();

        public static IPriceMatch GetSingle(string name, string code)
        {
            if (dic.ContainsKey(name) && dic[name].Name.Equals(code))
            {
                return dic[name];
            }
            var d = GetFactory(name, code);
            d.Name = code;
            dic.TryAdd(name, d);
            return d;
        }

        public static IPriceMatch GetFactory(string name, string code, out string errorMessage)
        {
            errorMessage = "";
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            try
            {
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("Common.Tool.dll");
                StringBuilder sb = new StringBuilder();
                sb.Append("using System;");
                sb.Append(Environment.NewLine);
                //sb.Append("using System.Collections.Generic;");
                //sb.Append("using System.Linq;");
                //sb.Append("using System.Text;");
                sb.Append("namespace Common.Tool.PriceTool{");
                sb.Append(string.Format("public class {0} : IPriceMatch {{", name));
                sb.Append("public string Name { get; set; }");
                sb.Append(
                    @"public  double GetPrice(double eprice, double commission = 0, int aviebeds = 0, int beds = 0, DateTime sd = default(DateTime), DateTime ed = default(DateTime),DateTime bdate = default(DateTime)){");
                sb.Append(code);
                sb.Append("}}}");
                CompilerResults result = provider.CompileAssemblyFromSource(parameters, sb.ToString());
                var e = result.Errors;
                if (e.HasErrors)
                {
                    foreach (CompilerError err in e)
                    {
                        errorMessage += err.ErrorText + "\r\n";
                    }
                }
                else
                {
                    Assembly assembly = result.CompiledAssembly;
                    object obj = assembly.CreateInstance("Common.Tool.PriceTool." + name);
                    return (IPriceMatch)obj;
                }
            }
            catch (Exception exception)
            {
                errorMessage += exception.ToString();
            }
            return null;

        }

        private static IPriceMatch GetFactory(string name, string code)
        {
            string errorMessage = "";
            return GetFactory(name, code, out errorMessage);
        }

        public static string CheckCode(string code)
        {
            string err;
            GetFactory("test", code, out err);
            return err;
        }
    }
}
