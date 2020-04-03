using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://tomosoft.jp/design/?p=3526
namespace CallBack2
{
    class Button
    {
        //イベント
        //(クラス定義内でしかeventは使えない)
        public event DemoDelegate DemoEvent = delegate (string name) { };
        //イベントの発生
        public void Clicked(string name)
        {
            Console.WriteLine("<Button Click>");
            //登録した関数を呼び出す
            //(直接にはButtonクラス内でしか実行できない)
            DemoEvent(name);
        }
    }
    class Class2
    {
        static void StaticHello(string name)
        {
            Console.WriteLine("(Static) : Check {0}!", name);
        }
        public static void Start()
        {
            Button btn = new Button();

            //関数の登録(アタッチ)
            btn.DemoEvent += new Foo().Hello;
            btn.DemoEvent += StaticHello;
            btn.DemoEvent += (string name) =>
            {
                Console.WriteLine("(lambda) : Check {0}!", name);
            };

            //イベント発生
            btn.Clicked("Event");
        }
    }
}
