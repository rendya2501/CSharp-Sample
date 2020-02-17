using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//これがあれば、毎回Console.WriteLineってやる必要がなくなる。WriteLineで十分になる。
//クラス名を書かずに静的メソッドを呼び出す。
using static System.Console;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Json1
{
    /// <summary>
    /// Startメソッドを持たせてForEachで回したいためだけのインタフェース
    /// </summary>
    interface IStart
    {
        void Start();
    }


    class JsonSample3 : IStart
    {
        public void Start()
        {
            WriteLine(this.ToString());

            //ParseメソッドでJSON文字列をJson.NETのオブジェクトに変換
            var obj = JObject.Parse(@"{
                'people' : [
                    {
                        'name' : 'Kato Jun',
                        'age' : 31
                    },
                    {
                        'name' : 'PIKOTARO',
                        'age' : 53
                    },
                    {
                        'name' : 'Kosaka Daimaou',
                        'age' : 43
                    }
                ]
            }");

            //Json.Netのオブジェクトに変換することでLINQが使えるようになる
            var oldestPersonName = obj["people"].OrderByDescending(p => p["age"]).FirstOrDefault();
            WriteLine(oldestPersonName);
        }
    }

    /// <summary>
    /// Json.Netを使ったほうのシリアライズ、デシリアライズ
    /// </summary>
    class JsonSample2 : IStart
    {
        Person p;

        //コンストラクタ
        public JsonSample2(Person p) => this.p = p;        
        public void Start()
        {
            WriteLine(this.ToString());        

            //オブジェクトをJSONにシリアライズ
            var json = JsonConvert.SerializeObject(p);
            WriteLine($"{json}");

            //JSONをオブジェクトにデシリアライズ
            var deserialized = JsonConvert.DeserializeObject<Person>(json);
            WriteLine($"Name: {deserialized.Name}"); // Kato Jun
            WriteLine($"Age : {deserialized.Age}");  // 31
        }
    }


    /// <summary>
    /// DataContractJsonSerializer
    /// .NET Frameworkで提供されている、オブジェクトをJSONにシリアライズ、JSONをオブジェクトにデシリアライズするためのクラスです
    /// </summary>
    class JsonSample1 : IStart
    {
        Person p;

        //コンストラクタ
        public JsonSample1(Person p) => this.p = p;

        public void Start()
        {
            WriteLine(this.ToString());

            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms))
            {
                var serializer = new DataContractJsonSerializer(typeof(Person));
                serializer.WriteObject(ms, this.p);
                ms.Position = 0;

                var json = sr.ReadToEnd();

                WriteLine($"{json}");//{"Age":31,"Name":"Kato Jun"}


                ms.Position = 0;
                var deserialized = (Person)serializer.ReadObject(ms);
                WriteLine($"Name: {deserialized.Name}"); //Kato Jun
                WriteLine($"Age : {deserialized.Age}");  //31
            }
        }
    }
}
