using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCreator {
    public class PasswordGenerator {
        public static int count;
        private Random random;
        public PasswordGenerator(Random random) {  //コンストラクター
            this.random = random;　       //引数のrandomをprivate変数のrandomに格納
            count++;
        }
        public string MakePassword(int count, ILetterFactory factory) {
            string password = string.Empty;
            for(int i = 0; i< count; i++) {
                Letter letter = factory.Create(random, i);
                password += letter.GetLetter();
            }
            return ArrangeEnd.Execute(Shuffle.Execute(password));
        }
    }
}
