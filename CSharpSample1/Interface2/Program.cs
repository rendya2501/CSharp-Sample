using System;

namespace Interface2
{
    class D : B,C
    {
        ///// <summary>
        ///// ダイアモンド問題
        ///// これではいけませんね
        ///// </summary>
        ///// <param name="args"></param>
        //static void Main(string[] args)
        //{
        //    C c = new D();
        //    c.m();
        //}


        /// <summary>
        /// 定義してあげないといけない
        /// </summary>
        void A.m()
        {
            Console.WriteLine("I am in class D");
        }

        static void Main()
        {
            A a = new D();
            a.m();
        }
    }

    interface A
    {
        void m();
    }

    interface B : A
    {
        void A.m() { Console.WriteLine("interface B"); }
    }

    interface C : A
    {
        void A.m() { Console.WriteLine("interface C"); }
    }
}
