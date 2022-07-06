using System;
using System.Collections.Generic;
using System.Text;
using IronPython.Hosting;

namespace ConsoleApp4
{
    class PythonCall
    {
        public PythonCall()
        {
            var py = Python.CreateRuntime();
            dynamic script = py.UseFile(System.IO.Path.GetFullPath(@"..\..\..\myClass.py"));
            dynamic clazz = script.MyClass();
            Console.WriteLine(clazz.greet("ヤマダ"));
        }
    }
}
