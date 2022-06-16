using System;

namespace SwitchExpression1
{
    class Program
    {
        /// <summary>
        /// スイッチ式サンプル1
        /// スイッチ式使ってActionを受け取って実行って事ができると、処理をifで切り替えずに済むのでは？と思ってやってみた。
        /// 昔やったときは出来かったはずだが、できるようになってるっぽい。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 適当な分岐のつもり
            int flag = 1;

            // 1.Actionデリゲートとして受け取るタイプ
            Action result = flag switch
            {
                1 => Test1,
                2 => Test2,
                _ => throw new InvalidOperationException()
            };
            result.Invoke();

            // 2.そもそも関数として切り離したタイプ
            Action(flag).Invoke();

            // 3.どれか1つでもキャストするとvarでもいけることがわかった。
            // でもって、こっちだと.Invoke()で起動できる。
            // 勘違いだ。a()とするかa.Invoke()とするかの違いでしかなかった。
            var a = flag switch
            {
                1 => (Action)Test1,
                2 => Test2,
                _ => throw new InvalidOperationException()
            };
            a.Invoke();

            // 4.そもそもActionとして受け取らないで即時実行するタイプ
            (flag switch
            {
                1 => (Action)Test1,
                2 => Test2,
                _ => throw new InvalidOperationException()
            }).Invoke();
            // 何とか1行に出来なくはないが・・・。ないだろうなぁ。
            (flag switch { 1 => (Action)Test1, 2 => Test2, _ => throw new Exception() }).Invoke();
        }

        /// <summary>
        /// 2.そもそも関数として切り離したタイプ
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        static Action Action(int flag) => flag switch
        {
            1 => Test1,
            2 => Test2,
            _ => throw new InvalidOperationException()
        };

        /// <summary>
        /// テストメソッド1
        /// </summary>
        static void Test1() => Console.WriteLine("test1");
        /// <summary>
        /// テストメソッド2
        /// </summary>
        static void Test2() => Console.WriteLine("test2");
    }
}
