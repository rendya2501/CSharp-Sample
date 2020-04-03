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
            dynamic script = py.UseFile("C:\\Users\\shun\\source\\repos\\ConsoleApp4\\ConsoleApp4\\myClass.py");
            dynamic clazz = script.MyClass();
            Console.WriteLine(clazz.greet("ヤマダ"));
        }
    }
}
