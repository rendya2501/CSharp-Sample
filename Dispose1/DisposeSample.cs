// セーフハンドルを使用するために必要なusing
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

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
}
