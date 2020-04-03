using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Json1
{
    [DataContract]
    public class Person2
    {   
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public IDictionary<string,string> Attributes { get; private set; }

        public Person2()
        {
            this.Attributes = new Dictionary<string, string>();
        }
    }


    public static class JsonUtility
    {
        /// <summary>
        /// 任意のオブジェクトをJSONメッセージへシリアライズします。
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

        public static T Deserialize<T>(string message)
        {
            using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}
