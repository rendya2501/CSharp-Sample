using System;

namespace IEquatable1
{
    /// <summary>
    /// IEquatable速度比較サンプル
    /// http://noriok.hatenadiary.jp/entry/2017/07/17/233146
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Benchmark(100_000_000);
        }

        /// <summary>
        /// ノーマル
        /// </summary>
        struct Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        /// <summary>
        /// EqualsメソッドをオーバーロードしたPoint 構造体
        /// </summary>
        struct PointImplementsOverLoad
        {
            public int X { get; }
            public int Y { get; }

            public PointImplementsOverLoad(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object other) =>
                other is PointImplementsIEquatable equatable
                && X == equatable.X && Y == equatable.Y;

            public override int GetHashCode() => X ^ Y;
        }

        /// <summary>
        /// IEquatable<T> を実装した Point 構造体
        /// </summary>
        struct PointImplementsIEquatable : IEquatable<PointImplementsIEquatable>
        {
            public int X { get; }
            public int Y { get; }

            public PointImplementsIEquatable(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Equals(PointImplementsIEquatable other) =>
                X == other.X && Y == other.Y;

            public override bool Equals(object other) =>
                other is PointImplementsIEquatable equatable && Equals(equatable);

            public override int GetHashCode() => X ^ Y;
        }

        static void Benchmark(int n)
        {
            // 3475ms
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var xs = new[] { new Point(1, 2), };
            for (int i = 0; i < n; i++) Array.IndexOf(xs, xs[0]);
            sw.Stop();
            Console.WriteLine($"Point             = {sw.ElapsedMilliseconds}ms");

            // 1605ms
            sw.Restart();
            var ys = new[] { new PointImplementsOverLoad(1, 2), };
            for (int i = 0; i < n; i++) Array.IndexOf(ys, ys[0]);
            sw.Stop();
            Console.WriteLine($"Point(OverLoad)   = {sw.ElapsedMilliseconds}ms");

            // 1970ms
            sw.Restart();
            var zs = new[] { new PointImplementsIEquatable(1, 2), };
            for (int i = 0; i < n; i++) Array.IndexOf(zs, zs[0]);
            sw.Stop();
            Console.WriteLine($"Point(IEquatable) = {sw.ElapsedMilliseconds}ms");
        }
    }
}
