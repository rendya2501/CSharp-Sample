using System;

namespace CS8._0
{
    class Program
    {
        /// <summary>
        /// C#8.0検証プログラム
        /// https://qiita.com/4_mio_11/items/83fb99ec4a03a3b4a8ee
        /// https://ufcpp.net/study/csharp/resource/nullablereferencetype/#opt-in
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            new NullableCheck();
        }
    }

    /// <summary>
    /// null許容参照型
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
    }



    class Person
    {
        public int age;
    }
}
