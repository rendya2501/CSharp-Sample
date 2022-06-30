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
            new ObjectiveJanken().Execute();
            // new BasicInputJanken().Execute();
        }
    }
}
