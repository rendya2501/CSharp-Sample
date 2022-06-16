using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SandBox1
{
    class Other2
    {
        static void Execute()
        {
            // A + B * C
            // A + ( B * C )
            // A + D
            // A = True

            if (int.TryParse("6", out int i) && (i >= 5))
            {
                Console.WriteLine("Hello World");
            }

            var tmpList = new List<string>
            {
                "aa",
                "bb"
            };
            Test(tmpList);


            var classA = new ClassA();
        }


        static void Test<T>(T n) //where T : List<T>, new()
        {
            //if (typeof(List<>).IsAssignableFrom(n.GetType().GetGenericTypeDefinition()))
            //if (typeof(T).IsGenericType)
            //if (n is IList a)
            //if (n.GetType().IsGenericType)

            if (n.GetType().IsGenericType && n.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                IEnumerable a = (IEnumerable)n;
                foreach (var e in a)
                {
                    Test(e);
                }
            }
            else
            {
                Console.WriteLine(n.ToString());
            }
        }
    }

    public class ClassA
    {
        public ClassB B { get; }

        public ClassA()
        {
            this.B = new ClassB();
        }
    }
    public class ClassB
    {
        public List<ClassC> Cs { get; }
        public ClassB()
        {
            this.Cs = new List<ClassC>
            {
                new ClassC(),
                new ClassC(),
                new ClassC(),
            };
        }
    }

    public class ClassC
    {
        static int counter = 0;
        public int Int { get; set; }

        public ClassC()
        {
            counter++;
            this.Int = counter;
        }
    }
    //public class MasterClass
    //{
    //    public virtual void Say()
    //    {
    //        Console.WriteLine("Use the Force");
    //    }
    //}

    //public class PadawanClass : MasterClass
    //{
    //    public override void Say()
    //    {
    //        Console.WriteLine("Yes, Master");
    //    }
    //}

    //public class JediMaster
    //{
    //    static void Main()
    //    {
    //        Type masterType = Type.GetType("MasterClass");
    //        MasterClass obiwan = (MasterClass)Activator.CreateInstance(masterType);
    //        obiwan.Say(); // 出力：Use the Force

    //        Type padawanType = Type.GetType("PadawanClass");
    //        MasterClass anakin = (MasterClass)Activator.CreateInstance(padawanType);
    //        anakin.Say(); // 出力：Yes, Master
    //    }
    //}
}
