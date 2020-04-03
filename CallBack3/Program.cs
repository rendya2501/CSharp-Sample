using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CallBack3
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpRequest("https://www.yahoo.co.jp/", (res) =>
            {
                Console.WriteLine(res.Headers);
            }).Wait();


            Callback(
                false,
                () => Console.WriteLine("Hello"),
                () => Console.WriteLine("World")
            );
        }

        async static Task HttpRequest(string url, Action<HttpResponseMessage> action)
        {
            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage res = await httpClient.GetAsync(url);
            action(res);
        }


        static void Callback(bool result, Action TrueAction, Action FalseAction)
        {
            if (result)
            {
                TrueAction();
            }
            else
            {
                FalseAction();
            }
        }
    }
}
