using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var cls = new Program();

            switch (Console.ReadKey(true).KeyChar.ToString())
            {
                case "1":
                    cls.shori1();
                    break;
                case "2":
                    Sample2.Start();
                    break;
                case "3":
                    var tasks = Enumerable.Range(1, 10).Select(x => Task.Run(() => { Thread.Sleep(1000); return x; }));
                    var all = Task.WhenAll(tasks);
                    //①
                    //foreach (int element in all.Result)
                    //{
                    //    Console.WriteLine(element);
                    //}

                    //②
                    Array.ForEach(all.Result,new Action<int>(Console.WriteLine));

                    //Original
                    //all.Result.ForEach(Console.WriteLine);

                    //Task.WaitAll は、渡された Task 配列に格納された全ての Task が完了するまで、現在のスレッドをブロックします。
                    //Task#WaitXX は現在のスレッドをブロックして Task の完了を待ちますが、対して Task#WhenXX は、渡された複数の Task が完了するのを待つ Task を生成します。
                    break;
                case "4":
                    var form1 = new Form1();
                    form1.ShowDialog();
                    break;
                default:
                    break;
            }
                    
            Console.WriteLine("サンプル終了");
            Console.ReadKey(true);
        }

        private void shori1()
        {
            var cls = new Sample1();

            Console.WriteLine("shori");
            

            Console.ReadKey();
        }
    }


    class Sample1
    {
        public async Task RunAsync()
        { 
            Task t1 = Task.Run(() => Count(1));
            Task t2 = Task.Run(() => Count(2));

            await Task.WhenAll(t1, t2);

            Console.WriteLine("shoriowari");
        }

        public void Count(int n)
        {
            for(int i = 0;i < 50; i++)
            {
                Console.WriteLine($"Task{n}: {i}");
            }
        }
    }

    class Sample2
    {
        public static void Start()
        {
            var task = AsyncMethod();
            Console.WriteLine("Started");
            task.Wait();
            Console.WriteLine("Completed");
        }

        static async Task AsyncMethod()
        {
            await Task.Delay(1000);
            Console.WriteLine("AsyncMethod");
            await Task.Delay(1000);
        }
    }

    //並列で処理する
    class Sample3
    {
        public async static void Start()
        {
            var t1 = CalcAsync("aa");
            var t2 = CalcAsync("aa");
            var t3 = CalcAsync("aa");
            await Task.WhenAll(t1, t2, t3);
        }

        /// <summary>
        /// 重い処理
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static async Task CalcAsync(string str)
        {
            Console.WriteLine(@"wait...");
            await Task.Run(() => Thread.Sleep(3000));
            Console.WriteLine(@"done.");
        }
    }

}
