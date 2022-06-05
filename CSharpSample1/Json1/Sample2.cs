using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using static System.Console;

namespace Json1
{
    public static class JsonUtility
    {
        /// <summary>
        /// 任意のオブジェクトをJSON文字列にシリアライズします。
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string Serialize(object graph)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(graph.GetType());
                serializer.WriteObject(stream, graph);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
        /// <summary>
        /// JSON文字列を任意のオブジェクトにデシリアライズします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string message)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }
    }

    class Sample2
    {
        public void Start()
        {
            //データの作成
            var p_1 = new Person2()
            {
                ID = 0,
                Name = "Taka",
            };
            p_1.Attributes.Add("key1", "value1");
            p_1.Attributes.Add("key2", "value2");
            p_1.Attributes.Add("key3", "value3");

            var p_2 = new Person2()
            {
                ID = 1,
                Name = "PG",
            };
            p_2.Attributes.Add("keyAA", "valueAA");
            p_2.Attributes.Add("keyBB", "valueBB");
            p_2.Attributes.Add("keyCC", "valueCC");
            //リストをシリアライズ
            string json = JsonUtility.Serialize(new List<Person2>() { p_1, p_2 });
            WriteLine(json);
            //デシリアライズ
            var pDeserializeList = JsonUtility.Deserialize<IList<Person2>>(json);
            //内容の出力
            foreach (var p in pDeserializeList)
            {
                WriteLine("ID = " + p.ID);
                WriteLine("Name = " + p.Name);
                foreach (var att in p.Attributes)
                {
                    WriteLine(att.Key + " = " + att.Value);
                }
            }
        }
    }

    [DataContract]
    public class Person2
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public IDictionary<string, string> Attributes { get; private set; }

        public Person2()
        {
            this.Attributes = new Dictionary<string, string>();
        }
    }
}
