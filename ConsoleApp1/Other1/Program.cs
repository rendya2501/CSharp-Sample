using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Other1
{
    class Program
    {
        /// <summary>
        /// https://qiita.com/mounntainn/items/e8b8f94a15ec4fee12bf
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //リスト作成
            List<SampleData> dataList = new List<SampleData>
            {
                new SampleData { Code1 = "1", Code2 = "1", Value = "てすと1-1" },
                new SampleData { Code1 = "1", Code2 = "2", Value = "てすと1-2" },
                new SampleData { Code1 = "2", Code2 = "1", Value = "てすと2-1" },
                new SampleData { Code1 = "2", Code2 = "2", Value = "てすと2-2" }
            };


            string code1 = Console.ReadLine();
            string code2 = Console.ReadLine();


            SampleData result1 = dataList.Find(new Predicate<SampleData>(s => s.Code1 == code1 && s.Code2 == code2));
            // こっちでも問題はない
            SampleData result2 = dataList.FirstOrDefault(s => s.Code1 == code1 && s.Code2 == code2);
            // Predicate使わなくてもいける
            SampleData resutl3 = dataList.Find(s => s.Code1 == code1 && s.Code2 == code2);

            //②.First：一致するデータがないとExceptionが発生するので注意。
            //  .FirstOrDefault, .Find：参照型ならデフォルト値としてnullを返します。
            //  個人的にはFindの方が可読性的に好みですね。
            //  (ただし、パフォーマンスはFirstの方が上のようです。)
            //  FirstOrDefault, .Findについてコメントをいただきました。

            //  .FirstOrDefault：IEnumerableのメソッド
            //  .Find：Listのメソッド
            //なので前者の方が統一的に使えるようです。
            //.FirstOrDefault vs.Find ですが
            //これ、宣言されている場所がそれぞれ違います。
            //FindはListが持っているメンバメソッドです。なのでList以外では"使えません"。
            //一方FirstOrDefaultはLINQというかIEnumerableに（拡張メソッドとして）実装されているのでどんなリストの型でも使えます。


            if (result1 == null)
            {
                Console.WriteLine("検索条件：コード1=" + code1 + "　コード2=" + code2 + "　のデータは存在しません。");
            }
            else
            {
                Console.WriteLine("検索条件：コード1=" + code1 + "　コード2=" + code2 + "　の値は「" + result1.Value + "」です。");
                var aa = result2.Value;
            }
        }
    }

    public class SampleData
    {
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string Value { get; set; }
    }

}
