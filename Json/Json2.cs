using Newtonsoft.Json;
using System.Text;

namespace Json
{
    internal class Json2
    {
        /// <summary>
        /// シリアライズサンプル
        /// </summary>
        public static void Serialize()
        {
            Person person = new Person()
            {
                Name = "takahiro",
                Age = 46,
                Weight = 63.5,
            };
            // JSON データにシリアライズ
            var jsonData = JsonConvert.SerializeObject(person);
            // シリアライズされたデータを表示
            Console.WriteLine(jsonData);
        }

        /// <summary>
        /// デシリアライズサンプル
        /// </summary>
        public static void Deseriaize()
        {
            // JSON 形式の文字列を作成
            var jsonData = "{\"Name\":\"takahiro\",\"Age\":46,\"Weight\":63.5}";
            // インスタンス person にデシリアライズ
            Person person = JsonConvert.DeserializeObject<Person>(jsonData);

            Console.WriteLine("Name : " + person?.Name);
            Console.WriteLine("Age: " + person?.Age);
            Console.WriteLine("Weight  : " + person?.Weight);
        }


        public static void Write()
        {
            Person person = new Person()
            {
                Name = "takahiro",
                Age = 46,
                Weight = 1163.5,
            };
            // JSON データにシリアライズ
            var jsonData = JsonConvert.SerializeObject(person);

            new Action(() =>
            {
                // C:¥Work¥test.json を UTF-8 で書き込み用でオープン
                //using var sw = new StreamWriter(@"C:\Work\test.json", false, Encoding.UTF8);
                using var sw = new StreamWriter("test2.json", false, Encoding.UTF8);
                // JSON データをファイルに書き込み
                sw.Write(jsonData);
            })();

            Console.WriteLine("書き込みました。");
        }

        public static void Read()
        {
            Person person = new Func<Person>(() =>
            {
                // C:¥Work¥test.json を UTF-8 で開く
                using var sr = new StreamReader("test2.json", Encoding.UTF8);
                // 変数 jsonData にファイルの内容を代入 
                var jsonData = sr.ReadToEnd();
                // デシリアライズして person にセット
                return JsonConvert.DeserializeObject<Person>(jsonData);
            })();

            Console.WriteLine("Name : " + person?.Name);
            Console.WriteLine("Age: " + person?.Age);
            Console.WriteLine("Weight  : " + person?.Weight);
        }

        // Update?

    }

    //[JsonObject("Person")]
    class Person
    {
        //[JsonProperty("Person Name")]
        public string Name { get; set; }

        //[JsonProperty("Person Age")]
        public int Age { get; set; }

        //[JsonProperty("Person Weight")]
        public double Weight { get; set; }
    }
}
