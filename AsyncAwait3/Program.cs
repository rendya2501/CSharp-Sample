using System;
using System.Net;
using System.Threading.Tasks;

namespace AsyncAwait3
{
    class Program
    {
        /// <summary>
        /// 非同期メソッドの利用例
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            //var client = new WebClient();
            //var result = await client.DownloadStringTaskAsync("https://codezine.jp/");
            //Console.WriteLine("1");
            //Console.WriteLine(result);
            //Console.WriteLine("2");
            var result = await New();
            Console.WriteLine(result);
        }

        private static async Task<string> New()
        {
            var client = new WebClient();
            return await client.DownloadStringTaskAsync("https://codezine.jp/");
        }

        /// <summary>
        /// 従来の非同期通信
        /// </summary>
        private static void Old()
        {
            var client = new WebClient();
            // 非同期処理完了時の処理をイベントに登録
            client.DownloadStringCompleted += (sender, e) =>
            {
                Console.WriteLine(e.Result);
            };

            client.DownloadStringAsync(new Uri("https://codezine.jp"));
            Console.ReadLine();
        }
    }
}
