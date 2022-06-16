using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncAwait5
{
    /// <summary>
    /// https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/concepts/async/
    /// 非同期メソッドの並列実行サンプル
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            await Sample1();
            await Sample2();
        }

        static async Task Sample1()
        {
            var eggsTask = Task.Run(async () =>
            {
                Console.WriteLine("すごく重い処理その1(´・ω・`)始まり");
                await Task.Delay(3000);
                Console.WriteLine("すごく重い処理その1(´・ω・`)終わり");
            });
            var baconTask = Task.Run(async () =>
            {
                Console.WriteLine("すごく重い処理その2(´・ω・`)始まり");
                await Task.Delay(5000);
                Console.WriteLine("すごく重い処理その2(´・ω・`)終わり");
            });

            await Task.WhenAll(eggsTask, baconTask);
            Console.WriteLine("終わり");
        }

        static async Task Sample2()
        {
            var eggsTask = Task.Run(async () =>
            {
                Console.WriteLine("すごく重い処理その1(´・ω・`)始まり");
                await Task.Delay(3000);
                Console.WriteLine("すごく重い処理その1(´・ω・`)終わり");
            });
            var baconTask = Task.Run(async () =>
            {
                Console.WriteLine("すごく重い処理その2(´・ω・`)始まり");
                await Task.Delay(5000);
                Console.WriteLine("すごく重い処理その2(´・ω・`)終わり");
            });

            var breakfastTasks = new List<Task> { eggsTask, baconTask };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                breakfastTasks.Remove(finishedTask);
            }
            Console.WriteLine("終わり");
        }
    }
}
