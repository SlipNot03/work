using System;
using System.Threading;

namespace SyncTheads
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            object loker = new object();

            AutoResetEvent handler = new AutoResetEvent(true);

            Mutex mutex = new Mutex();

            Semaphore semaphore = new Semaphore(2, 3);

            for(int i =0; i< 6; i++)
            {
                Thread thread = new Thread(Print);
                thread.Name = $"Thread{i}";
                thread.Start();
            }
            
            void Print()
            {
                /*                lock (loker)
                                {
                                    x = 1;
                                    for (int i = 0; i < 6; i++)
                                    {
                                        Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                                        x++;
                                        Thread.Sleep(100);
                                    }
                                }*/
                /*                bool acLock = false;
                                try
                                {
                                    Monitor.Enter(loker, ref acLock);
                                    x = 1;
                                    for (int i = 0; i < 6; i++)
                                    {
                                        Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                                        x++;
                                        Thread.Sleep(100);
                                    }
                                }
                                finally
                                {
                                    if (acLock)
                                        Monitor.Exit(loker);
                                }*/
                /*handler.WaitOne();
                //AutoResetEvent.WaitAll(new WaitHandle[] { handler });
                x = 1;
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
                handler.Set();*/
                /*                mutex.WaitOne();
                                x = 1;
                                for (int i = 0; i < 6; i++)
                                {
                                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                                    x++;
                                    Thread.Sleep(100);
                                }
                                mutex.ReleaseMutex();*/

                semaphore.WaitOne();
                x = 1;
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
                semaphore.Release();
            }
        }


    }
}
