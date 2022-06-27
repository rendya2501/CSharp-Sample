using System;
using System.Linq;
using System.Text;

namespace janken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //new StructuredJanken().Execute();
            //new ObjectiveJanken().Execute();
            new BasicInputJanken().Execute();
        }

        public static int Gcd(int a, int b)
        {
            int gcd(int x, int y)
            {
                return y == 0
                    ? x
                    : gcd(y, x % y);
            }
            return a > b
                ? gcd(a, b)
                : gcd(b, a);
        }
    }
}
