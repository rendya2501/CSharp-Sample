using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCreator {
    public class Mark2LetterFactory : ILetterFactory {
        public Letter Create(Random random, int c) {    //cはカウンター変数を受け取る
            if (c == 0) return new MarkLetter(random);
            else if (c == 1) return new MarkLetter(random);
            else if (c >= 2) {
                NonMarkLetterFactory factory = new NonMarkLetterFactory();
                return factory.Create(random, c);
            }
            else return null;         //条件分岐は全ての場合を考慮すること
        }
    }
}
