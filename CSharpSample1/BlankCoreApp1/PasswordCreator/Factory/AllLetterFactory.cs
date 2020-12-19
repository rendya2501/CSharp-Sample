using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCreator {
    public class AllLetterFactory  : ILetterFactory{
        public Letter Create(Random random, int c) {    //cはカウンター変数
            int n = c % 4;
            if (n == 0) return new NumLetter(random);
            else if (n == 1) return new LowerLetter(random);
            else if (n == 2) return new UpperLetter(random);
            else if (n == 3) return new MarkLetter(random);
            else return null;         //条件分岐は全ての場合を考慮する
        }
    }
}
