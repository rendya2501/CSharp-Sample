using System;

namespace Tuple1
{
    class Program
    {
        /// <summary>
        /// 普通のタプル
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var a = new Program();
            var t = a.GetMaxMin(15, 13);
            Console.WriteLine(t.max);
            Console.WriteLine(t.min);

            // 変数の宣言を分解Var
            var (max, min) = a.GetMaxMin(15, 13);
            Console.WriteLine(max);
            Console.WriteLine(min);

            var t2 = a.GetTuple(15, 13);
            Console.WriteLine(t2.Item1);
            Console.WriteLine(t2.Item2);


            var t3 = a.GetP(15, 13);
            Console.WriteLine(t3.Item1);
            Console.WriteLine(t3.Item2);

            var vt = (x2: 1, y2: "vt");

        }

        //public (int max, int min) GetMaxMin(int x, int y)
        //{
        //    return x >= y ? (x, y) : (y, x);
        //}

        /// <summary>
        /// ここまで短くかける。
        /// タプルの肝は戻り値を2つ定義するところだ
        /// なんとこれがValueTupleっぽいですね。
        /// 本ではサラリとタプルと言っていたので、混乱しました。
        /// とはいえ、これが一番わかりやすくていいんじゃないですかね？
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public (int max, int min) GetMaxMin(int x, int y) => x > y ? (x, y) : (y, x);


        /// <summary>
        /// ここまでやってようやくただのTupleになるらしい。
        /// まぁ、小難しくやるくらいなら動作も軽いValueTuple使えばいいだけの話だが。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tuple<int, int> GetTuple(int x, int y)
        {
            return x > y ? Tuple.Create(x, y) : Tuple.Create(y, x);
        }

        /// <summary>
        /// ValueTuple
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public (int,int) GetTuple2(int x, int y) => x > y ? (x, y) : (y, x);

        /// <summary>
        /// ValueTuple構造体の<int,int>型だから名前ついてそうだけど、そんなことはない。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ValueTuple<int, int> GetP(int x, int y) => x > y ? (max: x, min: y) : (max: y, min: x);
    }
}
