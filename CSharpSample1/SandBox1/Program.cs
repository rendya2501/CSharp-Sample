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


            Button_Click();
            Button_Click_1();
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
