using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ファイル読み込みに必要です
using System.IO;
// Json.net用に必要です
using Newtonsoft.Json;
using System.Reflection;

namespace Json
{

    public class JsonRead
    {
        public static void aa()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var iconPath = Path.Combine(outPutDirectory, "Folder\\Img.jpg");
        }

        /// <summary>
        /// 読み込みサンプル
        /// https://tech-and-investment.com/json2/
        /// </summary>
        public static void Read()
        {
            try
            {
                var account = new Func<Account?>(() =>
                {
                    // jsonファイルを読み込みます
                    using StreamReader file = File.OpenText(@"C:\Users\rendy\Desktop\CSharpSample1\CSharpSample1\Json\test.json");
                    // デシリアライズ関数に読み込んだファイルと、データ用クラスの名称(型)を指定します。
                    // デシリアライズされたデータは、自動的にaccountのメンバ変数に格納されます 
                    return (Account?)new JsonSerializer().Deserialize(file, typeof(Account));
                })();

                // 表示
                Console.WriteLine("Email : " + account?.Email);
                Console.WriteLine("Active: " + account?.Active);
                Console.WriteLine("Date  : " + account?.CreatedDate);
                foreach (string role in account?.Roles ?? Enumerable.Empty<string>())
                {
                    Console.WriteLine("Roles : " + role);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ファイル読み込み時に例外が発生しました。:{ex.Message}");
            }
        }

        /// <summary>
        /// 書き込みサンプル
        /// https://tech-and-investment.com/json3/
        /// </summary>
        public static void Write()
        {
            // データ用のクラスオブジェクトを作成します。
            Account account = new Account
            {
                Email = "james@example.com",
                Active = true,
                CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Roles = new List<string>
                {
                    "User",
                    "Admin"
                }
            };
            // 作成したオブジェクトのデータをJSONにシリアライズします
            // 一つ目に引数に、シリアライズするオブジェクトを指定します
            // 二つ目に引数に、インデントの有無を指定します。
            string json = JsonConvert.SerializeObject(account, Formatting.Indented);
            // シリアライズ済みデータ(文字列)をファイルに書き込みます
            File.WriteAllText("Account.json", json);
        }

        /// <summary>
        /// デシリアライズ用のデータクラスです
        /// </summary>
        private class Account
        {
            public string Email { get; set; }
            public bool Active { get; set; }
            public DateTime CreatedDate { get; set; }
            public IList<string> Roles { get; set; }
        }
    }
}
