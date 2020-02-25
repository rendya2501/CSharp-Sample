using System;
using System.Threading.Tasks;

namespace AsyncAwait1
{
    class Program
    {
        static void Main(string[] args)
        {
            // ①非同期メソッドを呼び出す
            Task T = RunAsync(); //③処理が戻る
            Console.WriteLine("...他の処理...");
            T.Wait();
        }

        /// <summary>
        /// 非同期メソッドの定義
        /// </summary>
        /// <returns></returns>
        private static async Task RunAsync()
        {
            await Task.Run(() => Count(1)); // ②呼び出し元に処理を戻す
            Console.WriteLine("処理が終了しました。"); // Countメソッドが終了してから実行される
        }

        static void Count(int n)
        {
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"Task{n}: {i}");
            }
        }
    }
}
