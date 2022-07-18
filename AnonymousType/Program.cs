using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousType
{
    internal class Program
    {
        // 動的に作成するプロパティ、値の組み合わせをDictionaryに登録
        private Dictionary<string, string> dic = new Dictionary<string, string>
        {
            { "key1", "キー1" },
            { "key2", "キー2" }
        };

        static void Main(string[] args)
        {
            new Program().Execute();
        }

        private void Execute()
        {
            void Pattern1()
            {
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (var item in dic)
                {
                    expando[item.Key] = item.Value;
                }
                dynamic d = expando;
                Console.WriteLine(d.key1);
                Console.WriteLine(d.key2);
            }
            void Pattern1_1()
            {
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (var item in dic)
                {
                    expando.Add(item.Key, item.Value);
                }
                dynamic d = expando;
                Console.WriteLine(d.key1);
                Console.WriteLine(d.key2);
            }
            Pattern1_2();
            void Pattern1_2()
            {
                dynamic d = new Func<IDictionary<string, object>>(() =>
                {
                    IDictionary<string, object> expando = new ExpandoObject();
                    foreach (var item in dic)
                    {
                        expando.Add(item.Key, item.Value);
                    }
                    return expando;
                }).Invoke();
                Console.WriteLine(d.key1);
                Console.WriteLine(d.key2);
            }
            Pattern1_3();
            void Pattern1_3()
            {
                dynamic d = (IDictionary<string, object>)dic.ToDictionary(s => s.Key, aa => (object)aa.Value);
                // キーでのアクセスは効くので、linqを使う限りdictionaryでしか返されない。
                Console.WriteLine(d["key1"]);
                Console.WriteLine(d["key2"]);

                ////Console.WriteLine(d.key1);
                ////Console.WriteLine(d.key2);
            }


            void Pattern2()
            {
                dynamic expando = new ExpandoObject();
                IDictionary<string, object> dictionary = (IDictionary<string, object>)expando;
                dictionary.Add("FirstName", "Bob");
                dictionary.Add("LastName", "Smith");
                Console.WriteLine(expando.FirstName + " " + expando.LastName);
            }

            void Pattern2_2()
            {
                dynamic expando = new Func<ExpandoObject>(() =>
                {
                    IDictionary<string, object> dictionary = new ExpandoObject();
                    dictionary.Add("FirstName", "Bob");
                    dictionary.Add("LastName", "Smith");
                    return (ExpandoObject)dictionary;
                })();
                Console.WriteLine(expando.FirstName + " " + expando.LastName);
            }
            void Pattern2_3()
            {
                dynamic expando = new Func<IDictionary<string, object>>(() =>
                {
                    IDictionary<string, object> dictionary = new ExpandoObject();
                    dictionary.Add("FirstName", "Bob");
                    dictionary.Add("LastName", "Smith");
                    return dictionary;
                })();
                Console.WriteLine(expando.FirstName + " " + expando.LastName);
            }
            void Pattern3()
            {
                // 匿名型をDictionaryから作成
                dynamic expando = new ExpandoObject();
                foreach (var item in dic)
                {
                    ((IDictionary<string, object>)expando).Add(item.Key, item.Value);
                }
                Console.WriteLine(expando.key1);  // キー1
                Console.WriteLine(expando.key2);  // キー2
            }

            void Pattern4()
            {
                var obj = new ExpandoObject();
                var type = obj.GetType();
                var ctor = type.GetConstructors()[0];
                var xxxx = ctor.GetParameters().Select(p => dic[p.Name]).ToArray();
                var obj2 = ctor.Invoke(xxxx);
            }

            void Pattern5()
            {
                Student student = new Student()
                {
                    StudentId = "1",
                    StudentName = "Cnillincy"
                };
                var props = student
                     .GetType()
                     .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     .Select(s => (key: s.Name, value: s.GetValue(student)))
                     .ToList();
                IDictionary<string, object> anonymousType = new ExpandoObject();
                foreach (var (key, value) in props)
                {
                    anonymousType.Add(key, value);
                }
                dynamic dynamic = anonymousType;
                Console.WriteLine(dynamic.key1);  // キー1
                Console.WriteLine(dynamic.key2);  // キー2
            }
            void Pattern5_1()
            {
                Student student = new Student()
                {
                    StudentId = "1",
                    StudentName = "Cnillincy"
                };
                var props = student
                     .GetType()
                     .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     .Select(s => (key: s.Name, value: s.GetValue(student)))
                     .ToList();
                dynamic dynamic = new Func<IDictionary<string, object>>(() =>
                {
                    IDictionary<string, object> anonymousType = new ExpandoObject();
                    foreach (var (key, value) in props)
                    {
                        anonymousType.Add(key, value);
                    }
                    return anonymousType;
                }).Invoke();
                Console.WriteLine(dynamic.key1);  // キー1
                Console.WriteLine(dynamic.key2);  // キー2
            }
        }
    }


    public class Student
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
    }

}
