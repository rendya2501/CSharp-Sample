using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// l エルを除外する
/// </summary>
namespace PasswordCreator {
    class LowerLetter : Letter {
        public LowerLetter(Random random) : base(random) {    }

        //LINQを使用したコード
        public override string GetLetter() {
            string[] lowers = Enumerable.Range(97, 26).Where(x => x != 108)
                                        .Select(x => ((char)x).ToString()).ToArray();
            int n = random.Next(lowers.Length);
            return lowers[n];
        }

        //配列を使用したコード
        //public override string GetLetter() {
        //    string[] lowers = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "m",
        //                            "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        //    int n = random.Next(lowers.Length);
        //    return lowers[n];
        //}

        //繰り返しを使用したコード
        //public override string GetLetter() {
        //    int n;
        //    do {
        //        n = random.Next(97, 123);
        //    } while(n == 108);
        //    char c = (char)n;
        //    return c.ToString();
        //}
    }
}

