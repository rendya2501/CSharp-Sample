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
            var client = new WebClient();
            var result = await client.DownloadStringTaskAsync("https://codezine.jp/");
            Console.WriteLine("1");
            Console.WriteLine(result);
            Console.WriteLine("2");
        }
    }
}
