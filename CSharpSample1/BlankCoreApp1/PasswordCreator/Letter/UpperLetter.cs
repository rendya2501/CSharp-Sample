using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// OとIは除外する
/// </summary>
namespace PasswordCreator {
    class UpperLetter : Letter {
        public UpperLetter(Random random) : base(random) {   }

        //LINQを使用したコード
        public override string GetLetter() {
            string[] uppers = Enumerable.Range(65, 26).Where(x => x != 73 && x != 79).Select(x => ((char)x).ToString()).ToArray();
            int n = random.Next(uppers.Length);
            return uppers[n];
        }

        //配列を使用したコード
        //public override string GetLetter() {
        //    string[] uppers = { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L",
        //                "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        //    int n = random.Next(uppers.Length);
        //    return uppers[n];
        //}

        //繰り返しを使用したコード
        //public override string GetLetter() {
        //    int n;
        //    do {
        //        n = random.Next(65, 91);
        //    } while (n == 73 || n == 79);
        //    char c = (char)n;
        //    return c.ToString();
        //}
    }
}

