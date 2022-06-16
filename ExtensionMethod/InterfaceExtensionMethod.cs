using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public class InterfaceExtensionMethod
    {
        public static void Execute()
        {
            ITest aa = new Test() { TestStr = "1234" };
            Console.WriteLine(aa.Print());
        }
    }
    public class Test : ITest
    {
        public string TestStr { get; set; }
    }

    public interface ITest
    {
        string TestStr { get; set; }
    }

    public static class ITestExtension
    {
        public static string Print(this ITest test)
        {
            return test.TestStr + "_test";
        }
    }
}
