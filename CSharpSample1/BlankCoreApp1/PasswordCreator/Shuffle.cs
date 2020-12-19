using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCreator {
    class Shuffle {
        public static string Execute(string password) {  //staticをつけると静的メソッドになる
            char x = ' ';   //選択済みの文字（半角スペース）
            string res = string.Empty;
            int c = 0;
            char[] pws = password.ToArray();  //文字列を配列に変換
            Random random = new Random();
            while(c < password.Length) {
                int n = random.Next(password.Length);
                if(pws[n] != x) {                     //未選択ならば
                    res += pws[n];
                    pws[n] = x;                       //選択済みの設定
                    c++;                              //インクリメント
                }
            }
            return res;
        }
    }
}
