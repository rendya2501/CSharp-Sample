using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallBack1
{
    public class SampleMain
    {

        public void Start()
        {
            SampleSub sub = new SampleSub();
            sub.hoge(Callback);//①Callback関数をhogeメソッドの引数として渡す。

            sub.hoge((string msg) => {//ラムダ式で再現した場合はこうなるか？
                Console.WriteLine("Callback : " + msg);
            });
        }

        public void Callback(string msg)//③SampleSubクラスのhogeメソッドから実行される
        {
            Console.WriteLine("Callback : " + msg);
        }
    }

    public class SampleSub
    {
        public delegate void onComplete(string msg);

        public void hoge(onComplete callback)//②引数としてonComplete(string msg)の形の関数を受け取る。
        {
            callback("owari");
        }
    }
}
