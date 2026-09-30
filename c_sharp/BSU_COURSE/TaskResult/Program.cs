using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskResult
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1 = 1, n2 = 2;
            Task<int> sumTask = new Task<int>(() => {
                Thread.Sleep(1000);
                return n1 + n2;
                });
            sumTask.Start();

            int result = sumTask.Result;
            Console.WriteLine($"{n1} + {n2} = {result}");
        }
    }
}
