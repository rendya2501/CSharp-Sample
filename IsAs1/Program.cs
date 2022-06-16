using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsAs1
{
    /// <summary>
    /// 基底クラス
    /// </summary>
    public class Base
    {
        public virtual void Method()
        {
            Console.WriteLine("This Method is BaseMethod.");
        }
    }

    /// <summary>
    /// 派生クラス
    /// </summary>
    public class Derived : Base
    {
        public override void Method()
        {
            Console.WriteLine("This Method is Derived BaseMethod");
        }
    }

    class Program
    {
        /// <summary>
        /// エントリーポイント
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // is演算子の例
            IsOperatorExample();
            // as演算子の例
            AsOperatorExample();
        }

        /// <summary>
        /// is演算子の例
        /// </summary>
        private static void IsOperatorExample()
        {
            object b = new Base();
            Console.WriteLine(b is Base);
            Console.WriteLine(b is Derived);

            object d = new Derived();
            Console.WriteLine(d is Base);
            Console.WriteLine(d is Derived);

            if(b is Base b2)
            {
                b2.Method();
            }
            if(d is Base b3)
            {
                b3.Method();
            }
        }

        /// <summary>
        /// as演算子の例
        /// </summary>
        private static void AsOperatorExample()
        {
            object b = new Base();
            if (b is Base)
            {
                var b2 = b as Base;
                b2.Method();
            }
            object d = new Derived();
            if (d is Base)
            {
                var b2 = b as Base;
                b2.Method();
            }
        }
    }
}
