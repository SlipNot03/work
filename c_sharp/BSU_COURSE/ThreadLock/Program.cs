using System;
using System.Threading;

namespace ThreadLock
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;

            // запускаем пять потоков
            for (int i = 1; i < 6; i++)
            {
                Thread myThread = new Thread(Print);
                myThread.Name = $"Поток {i}";   // устанавливаем имя для каждого потока
                myThread.Start();
            }

            void Print()
            {
                x = 1;
                for (int i = 1; i < 6; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
        }
    }
}
