using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionPropertySet
{
    class Program
    {
        /// <summary>
        /// リフレクションを使ったプロパティへの代入はやっぱり遅いけど、1回20msなら別に気にする必要なくないか？
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //var testClass = new Test();
            //var test2 = new Test2() { T2 = "Test" };
            //testClass.SetTest(test2);


            //var main = new Test();
            //var a1 = new Test1() { T1 = "Test1" };
            //SetTest(main, a1);
            //var a2 = new Test2() { T2 = "Test2" };
            //SetTest(main, a2);

            //Benchmark(1);


            // 匿名型でも行けるー。すげー。
            var main = new Test();
            var a1 = new { T1 = "Test1" , T2 = "Test2" };
            SetValue(main, a1);
        }

        public static void SetValue<T1, T2>(T1 targetObj, T2 value)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = value
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            foreach (var prop in props)
            {
                var propValue = prop.GetValue(value);
                typeof(T1).GetProperty(prop.Name).SetValue(targetObj, propValue);
            }
        }


        static void Benchmark(int n)
        {
            var sw = new System.Diagnostics.Stopwatch();

            var parent = new Parent()
            {
                Prop1 = 1,
                Prop2 = "2",
                Prop3 = "Obj"
            };

            sw.Start();
            for (int i = 0; i < n; i++)
            {
                //var main = new Test();
                //var a1 = new Test1() { T1 = "Test1" };
                //var a2 = new Test2() { T2 = "Test2" };
                //main.T1 = a1.T1;
                //main.T2 = a2.T2;

                _ = new Child1(parent);
            }
            sw.Stop();
            Console.WriteLine($"Point             = {sw.ElapsedMilliseconds}ms");

            sw.Restart();
            for (int i = 0; i < n; i++)
            {
                //var main = new Test();
                //var a1 = new Test1() { T1 = "Test1" };
                //SetTest(main, a1);
                //var a2 = new Test2() { T2 = "Test2" };
                //SetTest(main, a2);

                _ = new Child2(parent);
            }
            sw.Stop();
            Console.WriteLine($"Point(OverLoad)   = {sw.ElapsedMilliseconds}ms");
        }
    }


    public class Parent
    {
        public int Prop1 { get; set; }

        public string Prop2 { get; set; }

        public object Prop3 { get; set; }
    }

    // 子クラス
    public class Child1 : Parent
    {
        public Child1(Parent parent)
        {
            this.Prop1 = parent.Prop1;
            this.Prop2 = parent.Prop2;
            this.Prop3 = parent.Prop3;
        }

        // 子クラスで付け足すプロパティ
        public object OriginalProp { get; set; }

        // 子クラスで付け足すメソッド
        public void OriginalMethod()
        {
            // Do something
        }
    }

    // 子クラス
    public class Child2 : Parent
    {
        public Child2(Parent parent)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = parent
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                var propValue = prop.GetValue(parent);
                typeof(Child2).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }

        // 子クラスで付け足すプロパティ
        public object OriginalProp { get; set; }

        // 子クラスで付け足すメソッド
        public void OriginalMethod()
        {
            // Do something
        }
    }

    class Test1
    {
        public string T1 { get; set; }
    }

    class Test2
    {
        public string T2 { get; set; }
    }

    class Test
    {
        public string T1 { get; set; }
        public string T2 { get; set; }

        public void SetTest(Test2 test2)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = test2
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                var propValue = prop.GetValue(test2);
                typeof(Test).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    interface ITest11
    {
        public string Test1 { get; set; }
    }
    interface ITest2
    {
        public string Test2 { get; set; }
    }
    class Sample : ITest11, ITest2
    {
        public string Test1 { get; set; }
        public string Test2 { get; set; }
    }
}
