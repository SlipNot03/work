using System;
using System.Threading;

namespace CreateThreads
{
    class Program
    {
        static void Main(string[] args)
        {
        //public delegate void ThreadStart();
            Thread thread1 = new Thread(Print);
            Thread thread2 = new Thread(new ThreadStart(Print));
            Thread thread3 = new Thread(()=>Console.WriteLine("Hello Threads"));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            void Print() => Console.WriteLine("Thread");

        }


    }
}
