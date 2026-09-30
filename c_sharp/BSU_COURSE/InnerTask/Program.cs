using System;
using System.Threading.Tasks;
using System.Threading;

namespace InnerTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskOuter = Task.Factory.StartNew(() => {
                Console.WriteLine("Outer task");
                var innerTask = Task.Factory.StartNew(()=> {
                    Console.WriteLine("Inner task start");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task end");
                }, TaskCreationOptions.AttachedToParent);
            });
            taskOuter.Wait();
            Console.WriteLine("Main end");
        }
    }
}
