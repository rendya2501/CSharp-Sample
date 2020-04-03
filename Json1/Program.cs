using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace Json1
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person
            {
                Name = "Kato Jun",
                Age = 31
            };

            var list = new List<IStart>{
                { new JsonSample1(p) },
                { new JsonSample2(p) },
                { new JsonSample3() }
            };

            //foreach (IStart IF in list){IF.Start();}


            new Sample2().Start();
        }
    }


}
