using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace TPLParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parallel.Invoke(Print, () => Square(5));
            
            void Print()
            {
                Console.WriteLine($"Task ID = {Task.CurrentId}");
                Thread.Sleep(1000);
            }

            //void Square(int n, ParallelLoopState pls)
           void Square(int n)
            {
                //if (n == 3)
                //    pls.Break();

                Console.WriteLine("Square");
                Thread.Sleep(2000);
                Console.WriteLine($"n*n={n*n}");

            }

            //void Square(int n, ParallelLoopState pls)
            //{
            //    if (n == 3)
            //    /    pls.Break();
            //    Console.WriteLine($"Square {n} {Task.CurrentId}");
            //    Console.WriteLine($"n*n={n * n}");
            //    Thread.Sleep(2000);
            //}

              Parallel.For(1, 5, Square);

            //ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() {1,2,3,4,5}, Square);
            //Console.WriteLine(result.IsCompleted);
            //Console.WriteLine(result.LowestBreakIteration);

        }
    }
}
