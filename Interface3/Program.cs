using System;

namespace Interface3
{
    class Program
    {
        static void Main(string[] args)
        {
            IDefaultInterfaceWithThis defaultMethodWithThis = new DefaultMethodWithThis();
            defaultMethodWithThis.CallDefaultThis(2);
            _ = defaultMethodWithThis[2];
           
        }
    }

    public interface IDefaultInterfaceWithThis
    {
        internal int this[int x]
        {
            get
            {
                Console.WriteLine(x);
                return x;
            }
            set => Console.WriteLine("SetX");
        }

        void CallDefaultThis(int x)
        {
            this[0] = x;
        }
    }

    class DefaultMethodWithThis : IDefaultInterfaceWithThis
    {
    }
}
