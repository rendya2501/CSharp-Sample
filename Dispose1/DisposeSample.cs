// セーフハンドルを使用するために必要なusing
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Dispose1
{
    // IDisposableインターフェースを実装
    class DisposeSample : IDisposable
    {
        // 既にメモリが解放されたかどうかをチェックするフラグ
        // true：解放済／false：未解放
        bool disposed = false;
        // セーフハンドルをインスタンス化
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        /// <summary>
        /// デストラクタ
        /// </summary>
        ~DisposeSample()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // true（既に呼ばれている）場合は何もしない
            if (disposed)
            {
                return;
            }

            // disposeメソッドでリソースを解放
            if (disposing)
            {
                // マネージリソースの解放（自動）
                handle.Dispose();
            }

            // ここにアンマネージリソースの処理を書く（明示的）


            // 解放されたらdisposedのステータスをtrueに変更
            disposed = true;
        }
    }


    /// <summary>
    /// 外部リソースを使う処理を内包したクラス
    /// </summary>
    public class DisposeDemo : IDisposable
    {
        /// <summary>
        /// マネージリソース
        /// 外部リソースを扱うクラス
        /// </summary>
        private readonly MemoryStream stream;

        /// <summary>
        /// privateなDispose()メソッドが何回呼ばれても一回しか処理が走らないようするためのフラグ
        /// true：解放済／false：未解放
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DisposeDemo()
        {
            stream = new MemoryStream();
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        /// <remarks>
        /// 万が一Disposeを忘れた場合でも、ガベージコレクタによってDisposeを呼び出してもらう事で解放忘れを防ぐ
        /// </remarks>
        ~DisposeDemo()
        {
            Dispose(false);
        }

        /// <summary>
        /// 外部リソースをラップして適当な処理を実装した体の処理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Process(string value)
        {
            stream.Write(Encoding.UTF8.GetBytes(value));
            stream.Position = 0;
            var buffer = new byte[4096];
            var length = stream.Read(buffer, 0, (int)stream.Length);
            stream.Flush();
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// IDisposableによる実装メソッド
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize()はメモリが解放されるときにファイナライザを呼び出さないようにするメソッド
            // デストラクタでDiposeしているのはDisposeを忘れたときにガベージコレクトされるときに解放してもらうのが目的である。
            // このメソッドが実行されるということは明示的に解放していることを意味しているので、
            // 明示的に解放している以上、それ以上解放する必要がないため、処理を抑制している。
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose処理の本体
        /// </summary>
        /// <param name="isDisposing">
        /// usingによるDisposeが実行された後、ガベージコレクタによるDisposeの実行を回避するフラグ。
        /// </param>
        /// <remarks>
        /// protected virtualにすることによって、このクラスを継承したクラスが破棄処理を書けるようにしている。
        /// 継承先のクラスから、このDispose()を呼ぶのを忘れてはならない。
        /// </remarks>
        protected virtual void Dispose(bool isDisposing)
        {
            // メモリ解放済みであれば何もしない
            if (disposed)
            {
                return;
            }
            // usingによるDisposeが実行された後、ガベージコレクタによる実行を回避する
            // フラグによる制御がない場合、this.streamはnullになっているのでエラーとなってしまう。
            if (isDisposing)
            {
                if (stream != null)
                {
                    Console.Write("Closing and Disposing");
                    stream.Dispose();
                }
            }

            disposed = true;
        }
    }
}
