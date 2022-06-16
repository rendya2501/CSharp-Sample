using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract_Interface3
{
    interface IBell
    {
        static readonly string sound = "ting";
        //only abstract methods allowed in interface
        public void Ring();
        public void IncreaseVolume();
        public void DecreaseVolume();
    }

    abstract class AbstractBell : IBell
    {
        public void Ring()
        {
            Console.WriteLine(IBell.sound);
        }

        public void IncreaseVolume()
        {
            Console.WriteLine("Increasing Volume");
        }

        public void DecreaseVolume()
        {
            Console.WriteLine("Decreasing Volume");
        }
    }

    class SchoolBell : AbstractBell
    {
        //public void Ring()
        //{
        //    Console.WriteLine("Ringing the School bell: ");
        //}
    }

    class aa
    {
        
        private void aaaaa()
        {
            SchoolBell schoolBell = new();
            schoolBell.Ring();
        }
    }
}
