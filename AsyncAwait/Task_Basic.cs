using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    internal static class Task_Basic
    {
        public static void Execute()
        {
            // Taskの実行
            Task.Run(() => {
                Console.WriteLine("task1開始");
                Task.Delay(5000);
                Console.WriteLine("task1終了");
            });

            // Taskの実行
            Task.Run(() => {
                Console.WriteLine("task2開始");
                Task.Delay(3000);
                Console.WriteLine("task2終了");
            });

            Console.WriteLine("tas111111終了");

            //task2開始
            //tas111111終了
            //task1開始
            //task2終了
            //task1終了


            //task1開始
            //task2開始
            //tas111111終了
            //task1終了
            //task2終了
        }
    }
}
