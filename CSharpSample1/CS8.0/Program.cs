using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CS8._0
{
    class Program
    {
        /// <summary>
        /// C#8.0検証プログラム
        /// https://qiita.com/4_mio_11/items/83fb99ec4a03a3b4a8ee
        /// https://ufcpp.net/study/csharp/cheatsheet/ap_ver8/#async-stream
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //new NullableCheck();
            AsyncEnumerable.Main().Wait();
        }
    }

    /// <summary>
    /// null許容参照型
    /// https://ufcpp.net/study/csharp/resource/nullablereferencetype/#opt-in
    /// </summary>
    class NullableCheck
    {
        public NullableCheck()
        {
            Person person = null;
            DisplayPersonAge(ref person);
            // NRT(Nullable Reference Type)を opt-in した時点で警告が出るようになる
            // らしいが.Net4.7でも特に何も出ない。
            string s = null;
            Console.WriteLine(s.Length);

#nullable enable
            E1(null); // 警告が出る

#nullable disable
            E1(null); // 警告が出ない
            D1(null);
            R1(null);
        }


#nullable enable
        static void DisplayPersonAge(ref Person? person)
        {
            Console.WriteLine(person.age);
            if (person != null)
            {
                Console.WriteLine(person.age);
            }

            int age = person?.age ?? 0;
            Console.WriteLine(age);
        }

#nullable enable
        // 有効化したのでここでは string で非 null、string? で null 許容。
        static int E1(string s) => s.Length;
        static int? E2(string? s) => s?.Length;

#nullable disable
        // 無効化したので string に null が入っている可能性あり。
        // string? とは書けない(書くだけで警告になる)。
        static int D1(string s) => s.Length;

#nullable restore
        // 1つ前のコンテキストに戻す。
        // この場合、disable から enable に戻る。
        static int? R1(string? s) => s?.Length;


        class Person
        {
            public int age;
        }
    }


    /// <summary>
    /// switch式
    /// </summary>
    class SwitchFormula
    {
        public int Compare(int? x, int? y) => (x, y) switch
        {
            (int i, int j) => i.CompareTo(j),
            ({ }, null) => 1,
            (null, { }) => -1,
            (null, null) => 0
        };

        public int getAge()
        {
            var pet = "猫";
            return pet switch
            {
                "猫" => 15,
                "犬" => 13,
                "鼠" => 5,
                _ => 0
            };
        }


        private string PetDetails(Animal animal)
        {
            return animal switch
            {
                Cat cat => $"Cat details: {cat.Age} {cat.Cry}",
                Dog dog => $"Dog details: {dog.Age} {dog.Cry}",
                Animal(0, _) => "baby",
                Animal(Age: var age, Cry: var cry) => $"details:{age}{cry}",
                _ => "nothing"
            };
        }

        class Animal
        {
            public int Age;
            public int Cry;
            public Animal(int age, int cry)
            {
                this.Age = age;
                this.Cry = cry;
            }
            /// <summary>
            /// 分解という概念らしい
            /// https://ufcpp.net/study/csharp/datatype/deconstruction/
            /// </summary>
            /// <param name="Age"></param>
            /// <param name="Cry"></param>
            public void Deconstruct(out int Age, out int Cry) => (Age, Cry) = (this.Age, this.Cry);
        }
        private class Cat : Animal
        {
            public Cat(int age, int cry) : base(age, cry) { }
        }
        private class Dog : Animal
        {
            public Dog(int age, int cry) : base(age, cry) { }
        }
    }



    /// <summary>
    /// 非同期ストリーム
    /// 非同期foreach : await foreachという書き方で、非同期なデータ列挙ができる
    /// 非同期using : await usingという書き方で、非同期なリソース破棄ができる
    /// 非同期イテレーター : 非同期メソッド内にyieldをかけるようになる
    /// ざっくりこんな感じ。
    /// 詳しくやるとやベーことになるのでとりあえずこれくらいにしておく。
    /// </summary>
    class AsyncEnumerable
    {
        public static async Task Main()
        {
            await WriteItems(Select(GetData(), x => x * x));
        }

        static async IAsyncEnumerable<int> GetData()
        {
            yield return 1;
            await Task.Delay(1);
            yield return 2;
            await Task.Delay(1);
            yield return 3;
        }

        static async IAsyncEnumerable<int> Select(IAsyncEnumerable<int> source, Func<int, int> selector)
        {
            await foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        static async Task WriteItems(IAsyncEnumerable<int> source)
        {
            await foreach (var item in source)
            {
                Console.WriteLine(item);
            }
        }
    }
}
