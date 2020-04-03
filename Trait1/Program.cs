using System;

namespace Trait1
{
    class Program
    {
        /// <summary>
        /// C# トレイト再現用サンプル
        /// C#ではトレイト的動作をインターフェースのデフォルト実装として実装している模様
        /// https://www.infoq.com/jp/articles/default-interface-methods-cs8/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IDefaultInterFaceMethod anyClass = new AnyClass();
            anyClass.DefaultMethod();

            // こっちはエラーになる。当のクラスはデフォルトメソッドのことを知らないから。
            //var anyClass2 = new AnyClass();
            //anyClass2.DefaultMethod();

            //AnyClass型でDefaultMethodを使いたければキャストアップしなければならない。
            AnyClass anyClass2 = new AnyClass();
            ((IDefaultInterFaceMethod)anyClass2).DefaultMethod();
        }
    }

    /// <summary>
    /// Trait再現用インターフェース
    /// </summary>
    /// <remarks>
    /// .NetCore3.x : C# 8.0からでないと内部に処理を定義できない。
    /// .Net 4.7.2 C# 7.Xではエラーになることを確認した。
    /// https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/configure-language-version
    /// </remarks>
    interface IDefaultInterFaceMethod
    {
        /// <summary>
        /// インターフェースのデフォルト実装
        /// </summary>
        public void DefaultMethod()
        {
            Console.WriteLine("I am a default method in the interface!");
        }

        public void TestMethod()
        {
            Console.WriteLine("Test");
        }
    }

    /// <summary>
    /// インターフェースを実装したクラス
    /// </summary>
    class AnyClass : IDefaultInterFaceMethod
    {
    }
}
