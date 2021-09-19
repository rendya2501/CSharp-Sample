﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Sort_Heap1
{
    class Program
    {
        /// <summary>
        /// ヒープソートはヒープと呼ばれる木構造を用いたソート
        /// ヒープは親が最大値となるので、ヒープを作り、親を決定してもらい、親を抜き、
        /// またヒープを作り、親を決定してもらい、親を抜くってのを繰り返してソートしていく
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CSHeapSort1.Execute();
            JavaHeapSort.Execute();

            // TupleのList
            var tt = new List<(int, string)>
            {
                (3, "first"),
                (6, "second")
            };
            // ValueTupleのListその１
            List<(int example, string descrpt)> list = Enumerable.Range(0, 10)
                .Select(i => (example: i, descrpt: $"{i}"))
                .ToList();
            // ValueTupleのListその2
            var tupleList = new List<(int Index, string Name)>
            {
                (1, "cow"),
                (5, "chickens"),
                (1, "airplane")
            };
        }
    }
}
