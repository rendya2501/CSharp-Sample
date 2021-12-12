using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqGroupBy
{
    enum GenderType
    {
        Male,
        Female
    }

    class Program
    {
        static List<(string Name, GenderType Gender, int Age)> tupleList = new List<(string Name, GenderType Gender, int Age)>
            {
                ("井村 由宇",GenderType.Female,58),
                ("脇田 大",GenderType.Male,30),
                ("平 千佳子",GenderType.Female,75),
                ("江川 那奈",GenderType.Female,66),
                ("永島 美幸",GenderType.Female,80),
                ("中塚 明日",GenderType.Female,55),
                ("黒木 りえ",GenderType.Female,52),
                ("戎 杏",GenderType.Female,45),
                ("菅 亮",GenderType.Male,79),
                ("池本 しぼり",GenderType.Female,38),
                ("本多 路子",GenderType.Female,52),
                ("山内 博明",GenderType.Male,21),
                ("木下 さやか",GenderType.Female,27),
                ("市川 竜也",GenderType.Male,61),
                ("山城 りえ",GenderType.Female,41),
                ("長田 窈",GenderType.Female,79),
                ("井手 秀樹",GenderType.Male,35),
                ("松井 明慶",GenderType.Male,68),
                ("立石 たまき",GenderType.Female,79),
                ("とよた さやか",GenderType.Female,51),
                ("小木 美月",GenderType.Female,77),
                ("今西 まなみ",GenderType.Female,66),
                ("河本 友也",GenderType.Male,74),
                ("北条 ヒカル",GenderType.Female,24),
                ("天野 瑠璃亜",GenderType.Female,68),
                ("大塚 浩正",GenderType.Male,69),
                ("真田 大五郎",GenderType.Male,57),
                ("堀井 京子",GenderType.Female,76),
                ("渡辺 美佐",GenderType.Female,51),
                ("浅川 美帆",GenderType.Female,75),
                ("岩井 賢治",GenderType.Male,20),
                ("村瀬 莉沙",GenderType.Female,31),
                ("市川 研二",GenderType.Male,57),
                ("河原 美幸",GenderType.Female,27),
                ("黒岩 憲史",GenderType.Male,30),
                ("薬師丸 美嘉",GenderType.Female,62),
                ("阿部 禄郎",GenderType.Male,50),
                ("吉野 公顕",GenderType.Male,48),
                ("八十田 隼士",GenderType.Male,40),
                ("清田 美和子",GenderType.Female,59),
                ("矢口 あさみ",GenderType.Female,26),
                ("米沢 明宏",GenderType.Male,80),
                ("神野 礼子",GenderType.Female,55),
                ("辻 三郎",GenderType.Male,29),
                ("百瀬 有海",GenderType.Female,32),
                ("村瀬 俊介",GenderType.Male,73),
                ("金丸 寿明",GenderType.Male,43),
                ("小寺 勝久",GenderType.Male,25),
                ("今泉 由宇",GenderType.Female,64),
                ("岡田 大",GenderType.Male,40),
            };


        static void Main(string[] args)
        {
            Do(5);
            Do2(5);
        }


        static void Do(int chunkSize = 1)
        {
            var male = new List<(string test, int count)>();
            var female = new List<(string test, int count)>();

            (string test, int count) CreateT(List<(string Name, GenderType Gender, int Age)> s, string name) =>
                (test: name, count: s.Count());

            var timer = new Stopwatch();
            timer.Start();

            // 25万ms
            Enumerable.Range(0, 150)
                .Select((v, i) => (v, i))
                .GroupBy(x => x.i / chunkSize)
                .ToList()
                .ForEach(s =>
                {
                    var from = s.Min(m => m.v);
                    var to = s.Max(m => m.v);
                    var fromTo = from.ToString() + "～" + to.ToString();

                    male.AddRange(
                        tupleList
                            .GroupBy(g => (Gender: g.Gender == GenderType.Male, Age: g.Age >= from && g.Age <= to))
                            .Where(w => w.Key.Gender && w.Key.Age && w.Any())
                            .Select(ss => CreateT(ss.ToList(), "男性 " + fromTo))
                            .ToList()
                    );
                    female.AddRange(
                        tupleList
                            .GroupBy(g => (Gender: g.Gender == GenderType.Female, Age: g.Age >= from && g.Age <= to))
                            .Where(w => w.Key.Gender && w.Key.Age && w.Any())
                            .Select(ss => CreateT(ss.ToList(), "女性 " + fromTo))
                            .ToList()
                    );
                });

            timer.Stop();
            male.Clear();
            female.Clear();
            timer.Restart();

            // 2万ms
            foreach (var chunk in Enumerable.Range(0, 150)
                .Select((v, i) => (v, i))
                .GroupBy(x => x.i / chunkSize)
                .Select(g => g.Select(x => x.v))
                .ToList())
            {
                var from = chunk.Min();
                var to = chunk.Max();
                var fromTo = from.ToString() + "～" + to.ToString();

                male.AddRange(
                    tupleList
                        .GroupBy(g => (Gender: g.Gender == GenderType.Male, Age: g.Age >= from && g.Age <= to))
                        .Where(w => w.Key.Gender && w.Key.Age && w.Any())
                        .Select(ss => CreateT(ss.ToList(), "男性 " + fromTo))
                        .ToList()
                );
                female.AddRange(
                    tupleList
                        .GroupBy(g => (Gender: g.Gender == GenderType.Female, Age: g.Age >= from && g.Age <= to))
                        .Where(w => w.Key.Gender && w.Key.Age && w.Any())
                        .Select(ss => CreateT(ss.ToList(), "女性 " + fromTo))
                        .ToList()
                );
            }


            timer.Stop();
            male.Clear();
            female.Clear();
            timer.Restart();

            // 1万ms
            {
                var from = 0;
                // 間隔分インクリメントされる。
                for (var to = chunkSize - 1; to < 150; to += chunkSize)
                {
                    // ここの間は0,1,2,3,4  5,6,7,8,9 になってる。
                    var fromTo = from.ToString() + "～" + to.ToString();

                    male.AddRange(
                        tupleList
                            .GroupBy(g => (Gender: g.Gender == GenderType.Male, Age: g.Age >= from && g.Age <= to))
                            .Where(w => w.Key.Gender && w.Key.Age && w.Any())
                            .Select(ss => CreateT(ss.ToList(), "男性 " + fromTo))
                            .ToList()
                    );
                    female.AddRange(
                        tupleList
                            .GroupBy(g => (Gender: g.Gender == GenderType.Female, Age: g.Age >= from && g.Age <= to))
                            .Where(w => w.Key.Gender && w.Key.Age && w.Any())
                            .Select(ss => CreateT(ss.ToList(), "女性 " + fromTo))
                            .ToList()
                    );
                    // fromは6,10,16みたいな感じで常にto + 1の値にする。
                    from = to + 1;
                }
            }

            timer.Stop();
            timer.Restart();
        }


        static void Do2(int chunkSize = 1)
        {
            var male = new List<(string test, int count)>();
            var female = new List<(string test, int count)>();

            (string test, int count) CreateT(List<(string Name, GenderType Gender, int Age)> s, string name) =>
                (test: name, count: s.Count());

            var timer = new Stopwatch();
            timer.Start();
            // 2万ms
            foreach (var chunk in Enumerable.Range(0, 150)
                .Select((v, i) => (v, i))
                .GroupBy(x => x.i / chunkSize)
                .Select(g => g.Select(x => x.v))
                .ToList())
            {
                var from = chunk.Min();
                var to = chunk.Max();
                var fromTo = from.ToString() + "～" + to.ToString();

                male.AddRange(
                    tupleList
                        .Where(w => w.Gender == GenderType.Male && (w.Age >= from && w.Age <= to))
                        .GroupBy(w => (w.Gender, w.Age))
                        .Select(ss => CreateT(ss.ToList(), "男性 " + fromTo))
                        .ToList()
                );
                female.AddRange(
                    tupleList
                        .Where(w => w.Gender == GenderType.Female && (w.Age >= from && w.Age <= to))
                        .GroupBy(w => (w.Gender, w.Age))
                        .Select(ss => CreateT(ss.ToList(), "女性 " + fromTo))
                        .ToList()
                );
            }
            timer.Stop();
            timer.Restart();
        }



        static void BenchiMark()
        {
            var timer = new Stopwatch();
            timer.Start();

            // N 個ずつの N
            var chunkSize = 5;
            var chunks = Enumerable.Range(0, 100)
                .Select((v, i) => (v, i))
                .GroupBy(x => x.i / chunkSize)
                .Select(g => g.Select(x => x.v))
                .ToList();
            // 動作確認
            // 15万ms
            foreach (var chunk in chunks)
            {
                var a222a = tupleList
                    .GroupBy(g => g.Age >= chunk.Min() && g.Age <= chunk.Max())
                    .Where(w => w.Key)
                    .Select(s => s + "女性 生年月日無し")
                    .ToList();
            }

            timer.Stop();
            timer.Reset();
            timer.Start();
            // 500ms
            var chunks2 = Enumerable.Range(0, 100)
                .Select((v, i) => (v, i))
                .GroupBy(x => x.i / chunkSize)
                .Select(g =>
                {
                    return tupleList
                       .GroupBy(g2 => g2.Age >= g.Min(m => m.v) && g2.Age <= g.Max(m => m.v))
                       .Where(w => w.Key)
                       .Select(s => s + "女性 生年月日無し")
                       .ToList();
                });
            timer.Stop();
            timer.Reset();
        }

        static void Create()
        {
            var query = Enumerable.Range(0, 100)
                .Select((x, index) => (x, index))
                .GroupBy(e => e.x - e.index * 2, e => e.x * 2);
            foreach (var a in query)
            {
                Console.WriteLine(string.Join(", ", a));
            }
            foreach (var i in Enumerable.Range(0, 10).Select(x => x * 3))
            {
                Console.WriteLine(i);
            }
        }

        static void Botu()
        {
            var kankaku = 5;
            var from = 0;
            // 間隔分インクリメントされる。
            for (var to = kankaku - 1; to < 150; to += kankaku)
            {
                // ここの間は0,1,2,3,4  5,6,7,8,9 になってる。

                // fromは6,10,16みたいな感じで常にto + 1の値にする。
                from = to + 1;
            }
        }

        static void CreateSquence()
        {
            List<(int, int)?> chunks2 = Enumerable.Range(0, 100)
                .Select((v, i) => (v, i))
                .GroupBy(x => x.i / 5)
                .Select(g => ((int, int)?)(g.Min(m => m.v), g.Max(m => m.v)))
                .ToList();
            chunks2.Insert(0, null);
        }
    }
}
