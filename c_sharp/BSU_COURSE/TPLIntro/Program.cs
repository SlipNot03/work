using System;
using System.Threading.Tasks;

namespace TPLIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = new Task(()=> Console.WriteLine("Task!"));
            //task.Start();
            //task.RunSynchronously();

            //task.Wait();

            Task task1 = Task.Factory.StartNew(() => Console.WriteLine("Task Factory!"));
            Task task2 = Task.Run(() => Console.WriteLine("Task Run!"));

            task1.Wait();
            task2.Wait();
            Console.WriteLine("Main end");
        }

    }
}
