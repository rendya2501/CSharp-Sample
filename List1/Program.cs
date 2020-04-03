using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp5
{
    /// <summary>
    /// いつぞや伊林さんからの質問
    /// 再帰的に中身を取り出しているときに、Listを格納する方法を訪ねられたので
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>
            {
                "a",
                "b",
                "c"
            };
            Test(list);
        }

        static void Test<T> (T arg) //where T : new()
        {
            if (typeof(List<>).IsAssignableFrom(arg.GetType().GetGenericTypeDefinition()))
            {
                IList c = (IList)arg;

                foreach (object o in c)
                {
                    Console.WriteLine(o.ToString()); // -> A
                }
            }
        }
    }
}
