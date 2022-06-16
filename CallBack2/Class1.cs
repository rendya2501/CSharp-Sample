using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://tomosoft.jp/design/?p=3526
namespace CallBack2
{
    delegate void DemoDelegate(string name);

    class Foo
    {
        public void Hello(string name)
        {
            Console.WriteLine("(method) : Check {0}!", name);
        }
    }

    class Class1
    {
        static void StaticHello(string name)
        {
            Console.WriteLine("(static) : Check {0}!", name);
        }

        public static void Start()
        {
            Foo foo = new Foo();
            //delegateの変数
            //初期化した後に実際に呼び出したい関数を格納します。
            //呼び出したときには何もしない関数Delegate(string name){} も実行されます。
            DemoDelegate funcs = delegate (string name) { };

            funcs += foo.Hello; //メソッドの格納
            funcs += StaticHello;   //static関数の格納
            funcs += (string name) =>   //ラムダの格納
            {
                Console.WriteLine("(lambda) : Check {0}!", name);
            };

            //格納した関数の呼び出し
            funcs("CallBack");
        }
    }
}
