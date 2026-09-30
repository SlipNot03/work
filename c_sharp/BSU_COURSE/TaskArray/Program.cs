using System;
using System.Threading.Tasks;
using System.Threading;

namespace TaskArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Task[] taskArray = new Task[3];
            var k = 1;
            for (var i=0; i<3; i++)
            {
                //k += 1;
                taskArray[i] = new Task(()=> {
                    Thread.Sleep(1000);
                    k += 1;
                    Console.WriteLine($"Task {i} finished");
                });
                taskArray[i].Start();
            }
            Task.WaitAll(taskArray);
            //Task.WaitAny(taskArray);
            Console.WriteLine("Main End!");
        }
    }
}
