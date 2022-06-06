using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SandBox1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tupleList = new List<(int Index, bool? flag)>
                {
                    (1, true),
                    (2, false),
                    (3, null),
                };


            var nullList = new List<(int Index, bool? flag)>();

            var v1 = nullList.Select(a => a.flag == true);
            var v2 = v1.Distinct();
            var v3 = v2.OrderBy(o => o);
            var v4 = v3.FirstOrDefault();

            //Button_Click();
            //Button_Click_1();
        }
        static private void Button_Click()
        {
            //Task.Delay(2000).GetAwaiter().GetResult();
            //Console.WriteLine("owata1");
            Console.WriteLine($"1:Before await. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine($"2:In task run. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            }).GetAwaiter().GetResult();
            Console.WriteLine($"3:After await. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        static private async void Button_Click_1()
        {
            Console.WriteLine($"1:Before await. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() => Console.WriteLine($"2:In task run. Thread Id: {Thread.CurrentThread.ManagedThreadId}"));
            Console.WriteLine($"3:After await. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            //await Task.Delay(2000);
            //Console.WriteLine("owata2");
        }
    }

}
