using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Task task = new Task(() => 
                { 
                    for (int i=0; i<10; i++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine("Operation cancelled");
                            token.ThrowIfCancellationRequested();
                            //return;
                        }
                        Console.WriteLine($"i*i={i*i}");
                        Thread.Sleep(1000);
                    }
                }, token);

            try
            {
                task.Start();

                Thread.Sleep(3000);
                cancelTokenSource.Cancel();
                Thread.Sleep(1000);

                Console.WriteLine($"Status {task.Status}");
                task.Wait();
            } catch (AggregateException  ae) 
            { 
                foreach(Exception e in ae.InnerExceptions)
                {
                    if(e is TaskCanceledException)
                    {
                        Console.WriteLine("operation cancelled");
                    } 
                else
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            finally {
                cancelTokenSource.Dispose();
            }
        }
    }
}
