using System;
using System.Threading;

namespace ThreadParam
{
    class Program
    {
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public void Print()
            {
                Console.WriteLine($"{Name} {Age}");
            }
        }

        static void Main(string[] args)
        {
            void Print(object? message) => Console.WriteLine(message);

            Thread thread1 = new Thread(new ParameterizedThreadStart(Print));
            Thread thread2 = new Thread(new ParameterizedThreadStart(Print));

            var person = new Person() { Name="Tom", Age= 20 };
            Thread thread3 = new Thread(new ParameterizedThreadStart(person.Print));

            thread1.Start("Hello");
            thread2.Start("Привет");
        }


    }
}
