using System;
using System.Threading.Tasks;
using System.Threading;


namespace ContTask
{
    class Program
    {
        static void Main(string[] args)
        {
            /*                        Task task1 = new Task(() => Console.WriteLine($"Task Id {Task.CurrentId}"));

                                    Task task2 = task1.ContinueWith((Task t) => {
                                        Console.WriteLine($"{Task.CurrentId}");
                                        Console.WriteLine($"{t.Id}");
                                        Thread.Sleep(2000);
                                    });

                                    task1.Start();

                                    task2.Wait();*/

            Task<int> sumTask = new Task<int>(() => Sum(1, 2));
            Task printTask = sumTask.ContinueWith(task => PrintResult(task.Result));

            sumTask.Start();
            printTask.Wait();

            int Sum(int a, int b) { return a + b; };

            void PrintResult(int sum) => Console.WriteLine($"{sum}");

            Console.WriteLine("Main end");
        }
    }
}
