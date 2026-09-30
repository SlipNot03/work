using System;
using System.Threading;

namespace ParamThreads
{
    class Program
    {
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public void Print()
            {
                Console.WriteLine($"Name ={Name}");
                Console.WriteLine($"Age ={Age}");
            }
        }

        static void Main(string[] args)
        {

            //public delegate void ParameterizedThreadStart(object? obj);
            void Print(object obj) {
                //if (obj is Person p)
                //{
                var p = (Person)obj;

                //}
            };

            var tom = new Person() { Name = "Tom", Age = 17 };
            //Thread thread1 = new Thread(new ParameterizedThreadStart(Print));
            Thread thread1 = new Thread(tom.Print);

            //thread1.Start(tom);
            //thread1.Start("Hello World!");
            thread1.Start();

        }
    }
}
