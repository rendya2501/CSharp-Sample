using System;

namespace CS8._0_.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Person person = null;
            DisplayPersonAge(ref person);

            string s = null;
            Console.WriteLine(s.Length);
        }

        static void DisplayPersonAge(ref Person? person)
        {
            Console.WriteLine(person.age);
            if (person != null)
            {
                Console.WriteLine(person.age);
            }

            int age = person?.age ?? 0;
            Console.WriteLine(age);
        }
    }

    class Person
    {
        public int age;
    }
}
