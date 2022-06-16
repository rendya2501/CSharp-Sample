using NullableDictionary2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static NullableDictionary.DictionaryConverter;

namespace NullableDictionary
{
    /// <summary>
    /// http://noriok.hatenadiary.jp/entry/2017/07/17/233146
    /// https://qiita.com/chocolamint/items/9f13fe7e3c6343f898c2
    /// https://qiita.com/RyotaMurohoshi/items/03937297810e7c9aaf8b
    /// https://qiita.com/Temarin/items/27614d879e9376421aae
    /// https://stackoverflow.com/questions/4632945/dictionary-with-null-key
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //先にIntスタック的には下のやつ
            foreach (var item in IntItemsSource.IntItems)
            {
                //Console.WriteLine(item.Key + " " + item.Value);
                Console.WriteLine(Convert(item.Key, IntItemsSource.IntItems));
            }
            foreach (var item in ValidItemsSource.ValidItems)
            {
                //Console.WriteLine(item.Key + " " + item.Value);
                Console.WriteLine(Convert(item.Key, ValidItemsSource.ValidItems));
            }
            foreach (var item in TaxationTypeItemSource.ValidFlagListHasBlank)
            {
                Console.WriteLine(Convert2(item.Key, TaxationTypeItemSource.ValidFlagListHasBlank));
            }
            foreach (var item in ValidFlagItemSource.ValidFlagListHasBlank)
            {
                Console.WriteLine(Convert2(item.Key, ValidFlagItemSource.ValidFlagListHasBlank));
            }

            var aa = new Dictionary<string, string>();
            foreach (var item in aa)
            {
                _ = item.Key;
            }

            var b1 = new Nullable<double>(1);
            _ = (int)b1;

            var c1 = new Nullable<double>(1);
            var c2 = new Nullable<double>(2);
            _ = c1 == c2;

            //nullとint0のハッシュが同じでも、Valueは違うからキーとして認識させるには十分なのでは？
            _ = new Nullable<int?>(null) == new Nullable<int?>(0);

            // IEquatable速度比較
            Benchmark(10_000_000);
        }

        static void Benchmark(int n)
        {
            var sw = new System.Diagnostics.Stopwatch();

            // 通常 : 2635ms
            sw.Start();
            var xs = new[] { new NullableNotEquatable<int?>(null), };
            for (int i = 0; i < n; i++) Array.IndexOf(xs, xs[0]);
            sw.Stop();
            Console.WriteLine($"Point             = {sw.ElapsedMilliseconds}ms");

            // OverLoadのみ : 286ms
            sw.Restart();
            var ys = new[] { new NullableOverLoad<int?>(null), };
            for (int i = 0; i < n; i++) Array.IndexOf(ys, ys[0]);
            sw.Stop();
            Console.WriteLine($"Point(OverLoad)   = {sw.ElapsedMilliseconds}ms");

            // IEquatable実装 : 717ms
            sw.Restart();
            var zs = new[] { new Nullable<int?>(null), };
            for (int i = 0; i < n; i++) Array.IndexOf(zs, zs[0]);
            sw.Stop();
            Console.WriteLine($"Point(IEquatable) = {sw.ElapsedMilliseconds}ms");
        }
    }
}
