using System;
using System.Threading.Tasks;

namespace AsyncAwait1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Task T = RunAsync();
            Console.WriteLine("...他の処理...");
        }

        private static async Task RunAsync()
        {
            await Task.Run(() => Count(1));
            Console.WriteLine("処理が終了しました。");
        }

        static void Count(int n)
        {
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"Task{n}] {i}");
            }
        }
    }
}
