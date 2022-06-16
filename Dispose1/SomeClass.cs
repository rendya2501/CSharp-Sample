using System;
using System.IO;

namespace Dispose1
{
    public class SomeClass : IDisposable
    {
        private IntPtr unmanagedResource;

        private StreamWriter managedResource;

        private bool disposed = false;

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~SomeClass()
        {
            this.Dispose(false);
        }

        public void Hoge()
        {
            _ = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!this.disposed)
            {
                this.Free(this.unmanagedResource);

                if (isDisposing)
                {
                    if (this.managedResource != null)
                    {
                        this.managedResource.Dispose();
                    }
                }

                this.disposed = true;
            }
        }

        private void Free(IntPtr unmanagedResource)
        {
            // 省略
        }
    }
}
