using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Management;

namespace Client.BulletHell
{
    class Script
    {
        CodeDomProvider provider;
        CompilerParameters cp;

        string source = @"
            using System;
            using Client.Queue;
            using Client.BulletHell;
            using Client.BulletHell.BulletSpawners;

            namespace Client
            {
                class Test
                {
                    void TestWrite()
                    {
                            ActionQueue actionQueue = new ActionQueue();
                            actionQueue.Add(100, delegate()
                            {
                                BulletHell.BulletSpawners.BSpiral spiralb = new BulletHell.BulletSpawners.BSpiral(baseBulletTexture, 1, 20);
                                spiralb.Position = new Vector2f(400, 300);
                            });
                    }
                }

            }
            ";

        public Script()
        {
            provider = CSharpCodeProvider.CreateProvider("CSharp");
            cp = new CompilerParameters()
            {
                GenerateExecutable = false,
                GenerateInMemory = false,
                TreatWarningsAsErrors = false,
                OutputAssembly = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TestClass.dll",
                CompilerOptions = "/target:library /optimize"
            };
            cp.ReferencedAssemblies.Add("System.dll");

            CompilerResults result = provider.CompileAssemblyFromSource(cp, source);

            if (result.Errors.Count > 0)
            {
                Console.Write("H");
            }

            //Run

            AppDomainSetup ads = new AppDomainSetup();
            ads.ShadowCopyFiles = "true";
            AppDomain domain = AppDomain.CreateDomain("domain");

            byte[] rawAssembly =  File.ReadAllBytes("TestClass.dll");
            Assembly assembly = domain.Load(rawAssembly, null);

            
            object o = assembly.CreateInstance("CSharpScripter.TestClass");
            

                
        }
    }
}
