using System;
using System.Collections.Generic;
using System.Linq;

namespace SelectMany
{
    /// <summary>
    /// SelectManyサンプル
    /// https://www.urablog.xyz/entry/2018/05/28/070000
    /// </summary>
    class Program
    {
        private class Parameter
        {
            public string Name { get; set; }
            public int[] Numbers { get; set; }
        }

        static void Main(string[] args)
        {

        }

        static void sample1()
        {
            //Parameter[] parameters = new Parameter[]
            //{
            //    new Parameter() { Name = "正一郎", Numbers = new int[] { 1, 2, 3 } },
            //    new Parameter() { Name = "清次郎", Numbers = new int[] { 1, 3, 5 } },
            //    new Parameter() { Name = "誠三郎", Numbers = new int[] { 2, 4, 6 } },
            //    new Parameter() { Name = "征史郎", Numbers = new int[] { 9, 8, 7 } },
            //};

            // SelectManyのサンプルというより、タプルのサンプルになってしまった。
            var parameters = new List<(string Name, int[] Numbers)> {
                (Name: "正一郎", Numbers: new int[] {1, 2, 3 }),
                (Name: "清次郎", Numbers: new int[] {1, 3, 5 }),
                (Name: "誠三郎", Numbers: new int[] {2, 4, 6 }),
                (Name: "征史郎", Numbers: new int[] {9, 8, 7 })
            };

            // parametersの配列のインデックスをIndexAに、Numbersの配列のインデックスをIndexBに変換した、匿名型を取得する。
            var results = parameters.SelectMany((value, indexA) =>
               value.Numbers.Select((number, indexB) =>
                  new { Number = number, IndexA = indexA, IndexB = indexB }));

            // 表示用の文字列作成
            string text = string.Empty;
            foreach (var value in results)
            {
                text += string.Format("[{0}:{1}]{2}, ", value.IndexA, value.IndexB, value.Number);
            }
            Console.WriteLine(text);



            text = string.Empty;
            // こっちでもいい。
            var parameters2 = new[] {
                (Name: "正一郎", Numbers: new int[] {1, 2, 3 }),
                (Name: "清次郎", Numbers: new int[] {1, 3, 5 }),
                (Name: "誠三郎", Numbers: new int[] {2, 4, 6 }),
                (Name: "征史郎", Numbers: new int[] {9, 8, 7 })
            };
            // 全てのNumbersの情報が一つのコレクションにまとまって取得できる。
            var results2 = parameters2.SelectMany(value => value.Numbers);
            foreach (int value in results2)
            {
                text += string.Format("{0}, ", value);
            }
            Console.WriteLine(text);
            // 入力待ち用
            Console.ReadKey();
        }

        static void sample2()
        {
            var authors = CreateAuthors();

            var bookNames = authors.SelectMany(
                author =>
                {
                    return author.Books.Select(book => book.Name);
                },
                (author, bookName) =>
                {
                    return $"{bookName}/{author.Name}";
                }
            );

            Console.WriteLine(string.Join(", ", bookNames));
        }

        class Author
        {
            public string Name { get; set; }
            public Book[] Books { get; set; }
        }

        class Book
        {
            public string Name { get; set; }
        }

        static Author[] CreateAuthors() =>
            new[] {
                new Author()
                {
                    Name = "芥川龍之介",
                    Books = new[] {
                        new Book(){Name = "羅生門"},
                        new Book(){Name = "蜘蛛の糸"},
                        new Book(){Name = "河童"},
                    },
                },
                new Author()
                {
                    Name = "江戸川乱歩",
                    Books = new[] {
                        new Book(){Name = "人間椅子"},
                        new Book(){Name = "怪人二十面相"},
                    },
                },
                new Author()
                {
                    Name = "川端康成",
                    Books = new[] {
                        new Book(){Name = "雪国"},
                        new Book(){Name = "伊豆の踊り子"},
                    },
                },
            };
    }
}
