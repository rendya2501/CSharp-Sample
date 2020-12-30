using System;

namespace IdealProcessingSystem1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 日次帳票印刷API生成において、理想の形は何か色々考えた結果、行きついたとりあえずの形。
            // 理想の処理系
            // 単一責務の法則に則り、印刷データを生成するだけのクラスを作って、処理を任せたほうがいいのではないかと思ったのでこうした。
            // モデルの中でやるにはいささか大きすぎると思ったので。
            // 休み明けに早速、この形にしてみよう。
        }
    }
}
