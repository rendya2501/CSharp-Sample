using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SandBox1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const bool flag = true;
            new Action<int, string>(flag
                ? (Action<int, string>)((id, name) => Console.WriteLine($"{id} {name} 様"))
                : (id, name) => Console.WriteLine($"{id} {name} さん")).Invoke(1, "2");
        }

        public void Button_SaveStudent()
        {
            Student student = new Student()
            {
                StudentId = "1",
                StudentName = "Cnillincy"
            };
            new StudentManager().Create(student);
        }
    }

    class TETETE : Student
    {

    }

    public interface ICRUD<T>
    {
        string Create(T obj);
        T Read(string key);
        void Update(T obj);
        void Delete(string key);
    }

    public class Student
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
    }

    public class StudentManager : ICRUD<Student>
    {
        public string Create(Student obj)
        {
            // inserts record in the DB via DAL
            throw new NotImplementedException();
        }

        public Student Read(string userId)
        {
            // retrieveds record from the DB via DAL
            throw new NotImplementedException();
        }

        public void Update(Student obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string userId)
        {
            // deletes record from the DB
        }
    }

}



namespace aa
{

    class aaa
    {


        static private void Button_Click()
        {
            //Task.Delay(2000).GetAwaiter().GetResult();
            //Console.WriteLine("owata1");
            Console.WriteLine($"1:Before await. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine($"2:In task run. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            }).GetAwaiter().GetResult();
            Console.WriteLine($"3:After await. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        static private async void Button_Click_1()
        {
            Console.WriteLine($"1:Before await. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() => Console.WriteLine($"2:In task run. Thread Id: {Thread.CurrentThread.ManagedThreadId}"));
            Console.WriteLine($"3:After await. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            //await Task.Delay(2000);
            //Console.WriteLine("owata2");
        }
    }
}

