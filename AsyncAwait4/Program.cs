using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait4
{
    class Program
    {
        /// <summary>
        /// https://tech-lab.sios.jp/archives/15711
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Task<string> task = HeavyMethod1(); // ①処理の実行
            // ④処理が戻るのでHeavyMethod2が実行される
            HeavyMethod2();
            Console.WriteLine(task.Result);
            Console.ReadLine();
        }

        static async Task<string> HeavyMethod1()
        {
            Console.WriteLine("すごく重い処理その1(´・ω・`)始まり");
            // ②重い処理の実行
            // ③一度実行もとに戻る。その間HeavyMethod1の処理は続行される(バックグラウンド)。
            await Task.Delay(3000); 
            Console.WriteLine("すごく重い処理その1(´・ω・`)終わり");
            // ⑤バックグラウンド(別スレッド)で動いているHeavyMethod1の重い処理が終了すると、
            // HeavyMethod2が実行中でもHeavyMethod1に戻り、hogeを返す。
            return "hoge";
        }

        static void HeavyMethod2()
        {
            Console.WriteLine("すごく重い処理その2(´・ω・`)始まり");
            Thread.Sleep(3000);
            Console.WriteLine("すごく重い処理その2(´・ω・`)終わり");
        }
    }
}
