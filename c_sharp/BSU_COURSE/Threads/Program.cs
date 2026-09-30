using System;
using System.Threading;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread currentThread = Thread.CurrentThread;
            currentThread.Name = "Main";
            Console.WriteLine($"{currentThread.Name}");
            Console.WriteLine($"{currentThread.IsAlive}");
            Console.WriteLine($"{currentThread.Priority}");
            Console.WriteLine($"{currentThread.ThreadState}");

            Console.WriteLine($"{Thread.GetDomain()}");

            for(int i=0; i< 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(i);
            }
        }
    }
}
