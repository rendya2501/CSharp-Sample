using System;

namespace BidirectionalLinkedList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var aa = new BidirectionalLinkedList<string>();
            aa.InsertFirst("Node1");
            aa.InsertLast("Node2");
            //aa.InsertAfter(aa[2], "asdasd");
            //aa.InsertAfter(aa.Last, "InsertAfter");
            //aa.InsertAfter(aa.Last, "InsertAfter");
            //aa.InsertAfter(aa.Last, "InsertAfter");


            foreach (var item in aa)
            {
                Console.WriteLine(item);
            }
        }
    }
}
