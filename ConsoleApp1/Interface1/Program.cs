using System;

namespace Interface1
{
    class Program
    {
        /// <summary>
        /// インターフェース内の修飾子
        /// https://www.infoq.com/jp/articles/default-interface-methods-cs8/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IDefaultInterfaceMethod anyClass = new AnyClass();
            anyClass.DefaultMethod();

            IOverrideDefaultInterfaceMethod anyClassOverridden = new AnyClass();
            anyClassOverridden.DefaultMethod();
        }
    }

    interface IDefaultInterfaceMethod
    {
        /// <summary>
        /// 既定では、このメソッドはvirtualである。virtualキーワードを明示的に使うこともできる。
        /// </summary>
        virtual void DefaultMethod()
        {
            Console.WriteLine("I am a default method in the interface!");
        }

        /// <summary>
        /// 既定では、このメソッドはabstractである。abstractキーワードを明示的に使うこともできる。
        /// </summary>
        abstract void Sum();
    }

    interface IOverrideDefaultInterfaceMethod : IDefaultInterfaceMethod
    {
        //public などアクセス修飾子はつけられない
        void IDefaultInterfaceMethod.DefaultMethod()
        {
            Console.WriteLine("I am an overridden default method!");
        }

        //public void IDefaultInterfaceMethod.DefaultMethod()
        //{
        //    Console.WriteLine("I am an overridden default method!");
        //}
    }

    class AnyClass : IDefaultInterfaceMethod, IOverrideDefaultInterfaceMethod
    {
        public void Sum() { }
    }
}
