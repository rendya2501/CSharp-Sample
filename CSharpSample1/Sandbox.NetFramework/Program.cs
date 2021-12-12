using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.NetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<(int Data1, int Data2)>
            {
                (11, 20),
                (12, 20),
                (10, 22),
                (10, 21),
                (10, 20),
            };

            //// こちらはコーディング段階でエラーにはならないが実行するとエラーになる。
            //// System.InvalidOperationException: 'Failed to compare two elements in the array.'
            //_ = list.OrderBy(o => new { o.Data1, o.Data2 }).ToList();

            //// なんと行ける
            //// data1=10, data2=20
            //// data1=10, data2=21
            //// data1=10, data2=22
            //// data1=11, data2=20
            //// data1=12, data2=20
            //var a2 = order_list3.OrderBy(o => (o.Data1, o.Data2));
            //// data1=10, data2=20
            //// data1=11, data2=20
            //// data1=12, data2=20
            //// data1=10, data2=21
            //// data1=10, data2=22
            //var a2 = order_list3.OrderBy(o => (o.Data2, o.Data1));
            //// data1=10, data2=20
            //// data1=10, data2=21
            //// data1=10, data2=22
            //// data1=11, data2=20
            //// data1=12, data2=20
            //var order_list3 = list.OrderBy(d => d.Data1).ThenBy(d => d.Data2);
            //// data1=10, data2=20
            //// data1=11, data2=20
            //// data1=12, data2=20
            //// data1=10, data2=21
            //// data1=10, data2=22
            //var order_list33 = list.OrderBy(d => d.Data2).ThenBy(d => d.Data1);

            var list2 = new List<(int Large, int Middle, int small)>
            {
                (1, 1, 1),
                (1, 2, 2),
                (1, 3, 3),
                (2, 1, 4),
                (2, 2, 5),
                (2, 3, 6),
                (3, 1, 7),
                (3, 2, 8),
                (3, 3, 9),
            };

            var aa = list2.OrderBy(o => false ? (o.Large, o.Middle) : (o.Middle, o.Large)).ToList();


            var list3 = new List<(int i, string str)>
            {
                (1, "a"),
                (1, "b"),
                (2, "c"),
                (2, "d"),
            };
        }
    }
}
