using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SandBox1
{
    class Program
    {
        /// <summary>
        /// リフレクションを使ったプロパティへの代入はやっぱり遅いけど、1回20msなら別に気にする必要なくないか？
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] arr = new int[2][];
            //_ = arr[0].Length;

            var aa = new int [100,512];
            _ = aa.Length;
            _ = aa.GetLength(0);

        }

        static void Test1() => Console.WriteLine("test1");
        static void Test2() => Console.WriteLine("test2");
    }
}
