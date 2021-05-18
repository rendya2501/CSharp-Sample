using System;

namespace Dispose1
{
    /// <summary>
    /// IDisposable実装サンプル
    /// Disposeで本当にガベージコレクトされているのか気になったのでやってみた。
    /// 後、純粋にDisposeの実装の仕方を知りたかったから。
    /// https://clickan.click/idisposable/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            using var aa = new SomeClass();
            aa.Hoge();

            using (var bb = new DisposeSample())
            {
            }
        }
    }
}
