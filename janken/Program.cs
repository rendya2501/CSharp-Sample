using System;
using System.Linq;

namespace janken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new StructuredJanken().Execute();
            new ObjectiveJanken().Execute();
        }
    }
}
