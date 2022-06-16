using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncAwait2
{
    class Program
    {
        /// <summary>
        /// 値を返す非同期メソッド
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 非同期メソッドの呼び出し
            Task<TimeSpan> t = RunAsync();
            // 非同期メソッドが完了するまでループ
            while (!t.IsCompleted)
            {
                t.Wait(200);
                Console.WriteLine('.');
            }
            // Task型のResultプロパティにアクセスすることで戻り値を取得する
            Console.WriteLine(t.Result);
        }

        /// <summary>
        /// 戻り値のある非同期メソッド
        /// Task<T>型にする
        /// </summary>
        /// <returns></returns>
        private static async Task<TimeSpan> RunAsync()
        {
            var watch = Stopwatch.StartNew();
            await Task.Run(() =>
            {
                // 適当な思い処理
                int c;
                for (int i = 0; i < 1000000000; i++)
                {
                    c =+ i;
                }
            });
            watch.Stop();
            // TimeSpan型を返す
            // Elapsedは経過時間を保持するプロパティ
            return watch.Elapsed;
        }
    }
}
