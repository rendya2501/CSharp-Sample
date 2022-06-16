using System;

namespace UpCastDownCast1
{
    /// <summary>
    /// https://www.kakistamp.com/entry/2018/04/29/010258
    /// そもそもアップキャストとダウンキャストがどうなのかわかってない
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        //親クラス
        class ParentClass { }
        //子クラス
        class ChildClass : ParentClass { }

        private static void Test()
        {
            //===========================
            //      アップキャスト
            //===========================
            //子クラスのインスタンスを生成
            ChildClass child01 = new ChildClass();
            //子クラスのインスタンスを、親クラスに代入可能。
            ParentClass parent01 = child01;

            //===========================
            //  ダウンキャスト（エラー）
            //===========================
            ChildClass child02;
            //親クラスのインスタンスを生成
            ParentClass parent02 = new ParentClass();
            //キャストできるかどうか確認（falseになります）
            if (parent02 is ChildClass)
            {
                //キャスト時にエラーが発生する
                child02 = (ChildClass)parent02;
            }
            //asを使えば キャストできない場合 nullが入る
            child02 = parent02 as ChildClass;

            //===========================
            //     ダウンキャスト
            //===========================
            //親クラスのインスタンスを、子クラスで生成
            ParentClass parent03 = new ChildClass();
            //ダウンキャスト可。
            ChildClass child03 = (ChildClass)parent03;
        }
    }
}
