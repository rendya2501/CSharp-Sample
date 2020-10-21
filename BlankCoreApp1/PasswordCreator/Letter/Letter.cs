using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 他のプロジェクトで使用するにはpublicが必要になる
/// </summary>
namespace PasswordCreator {
    public class Letter {
        protected Random random;    //子クラスでも使用できるようprotected変数を用意
        public Letter(Random random) {      //コンストラクター
            this.random = random;           //引数の値をprotected変数に格納
        }
        public virtual string GetLetter() { //このメソッドは子クラスでも定義する
            return string.Empty;            //とりあえず戻り値を設定しておく
        }
    }
}
