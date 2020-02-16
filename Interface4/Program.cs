using System;
using System.Collections.Generic;

namespace Interface4
{
    /// <summary>
    /// https://www.infoq.com/jp/articles/default-interface-methods-cs8/
    /// 全体的に後半のサンプル
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Logger
            ILogger consoleLogger = new ConsoleLogger();
            consoleLogger.WriteWarning("Cool no code duplication!");

            ILogger traceLogger = new TraceLogger();
            traceLogger.WriteInformation("Cool no code duplication!");


            // Generic
            IGenericFilter<int> genericFilter = new GenericFilterExample();
            var result = genericFilter.ApplyFilter(new List<int>() { 1, 2, 3 }, x => x > 1);

            IDummyFilter<int> dummyFilter = new GenericFilterExample();
            var emptyResult = dummyFilter.ApplyFilter(new List<int>() { 1, 2, 3 }, x => x > 1);

        }
    }
}