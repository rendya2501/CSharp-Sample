using System;
using System.Threading.Tasks;

namespace AsyncAwait1
{
    class Program
    {
        /// <summary>
        /// 非同期メソッド基本
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // ①非同期メソッドを呼び出す
            //Task T = RunAsync(); //③処理が戻る
            //Console.WriteLine("...他の処理...");
            //T.Wait();

            Button1_Click();
            Console.WriteLine("22");
            Task.Delay(2000).Wait();
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

        private static async void Button1_Click()
        {
            await ExampleMethodAsync();
            Console.WriteLine("\r\nControl returned to Click event handler.\n");
        }

        private static async Task ExampleMethodAsync()
        {
            // The following line simulates a task-returning asynchronous process.
            await Task.Delay(1000);
        }
    }
}
