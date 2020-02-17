using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using static System.Console;

namespace Json1
{
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

            //リストに設定する
            List<Person2> pList = new List<Person2>() { p_1, p_2 };

            //リストをシリアライズ
            string json = JsonUtility.Serialize(pList);

            WriteLine(json);


            //デシリアライズ
            var pDeserializeList = JsonUtility.Deserialize<IList<Person2>>(json);

            //内容の出力
            foreach(var p in pDeserializeList)
            {
                WriteLine("ID = " + p.ID);
                WriteLine("Name = " + p.Name);
                foreach(var att in p.Attributes)
                {
                    WriteLine(att.Key + " = " + att.Value);
                }
            }
        }
    }
}
