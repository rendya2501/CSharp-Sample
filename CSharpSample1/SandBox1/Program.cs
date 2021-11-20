using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace SandBox1
{
    //シリアライズ対象のクラス
    public class SampleClass
    {
        public int Number;
        public string Message;
    }

    class Program
    {
        static void Main(string[] args)
        {
            SampleClass cls = new SampleClass
            {
                Message = "テスト",
                Number = 123
            };

            var serializer = new XmlSerializer(typeof(SampleClass));
            var sw = new StringWriter();
            serializer.Serialize(sw, cls);
            Console.WriteLine(sw.ToString());
            sw.Close();

            var ms = new MemoryStream();
            serializer.Serialize(ms, cls);
            ms.Position = 0;
            var i = (SampleClass)serializer.Deserialize(ms);
            Console.WriteLine(i.ToString());
            ms.Close();
        }
    }
}
