using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAPPattern
{
    class Program
    {
        //TAP - Task-based Asynchronous Pattern
        //Метод должен иметь несколько параметров (ref и out не использовать)
        //Метод должен возвращать значение, если есть в этом смысл (Task или Task<T>)
        //Название должно отображать поведение метода (c добавлением Async)
        //Общие и ожидаемые ошибки должны быть частью возвращаемого типа
        //Пример public static IPHostEntry GetHostEntry(string hostNameOrAddress)

        private void LookupHostName()
        {
            Task<IPAddress[]> ipAdrPromise = Dns.GetHostAddressesAsync("yandex.ru");
            //что делать при завершении асинхронного метода
            ipAdrPromise.ContinueWith(_ =>
            {
                IPAddress[] ipAddrs = ipAdrPromise.Result;
                foreach (var ip in ipAddrs)
                    Console.WriteLine(ip);
            }
            );
        }

        public static Task<int> MyLongComputation(int a, int b)
        {
            Thread.Sleep(1000);
            var result = a + b;
            return Task.Run(() => result);
        }

        public static Task Delay(int msec)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Timer timer = new Timer(_ => tcs.SetResult(null), state: null, dueTime: msec, period: Timeout.Infinite);
            tcs.Task.ContinueWith(delegate { timer.Dispose(); });
            return tcs.Task;
        }

        public static async void RunTasks(Task<int[]> allTask)
        {
            int[] results = await allTask;
            foreach (var i in results)
            {
                Console.WriteLine(i);
            }
        }


        public static async void RunAnyTasks(Task<Task<int>> anyTask)
        {
            Task<int> winner = await anyTask;
            int result = await winner;
            Console.WriteLine(result);
        }


        public static Task<int> ExecuteAsync(CancellationToken cToken)
        {
            int result = 0;
            return Task.Run(() =>
            {
                for (int i = 0; i < Int32.MaxValue; i++)
                {
                    cToken.ThrowIfCancellationRequested();
                    result += 1;
                    //Console.Write(".");
                }
                return result;
            }
            );
        }

        public static async void RunWithCancellationToken()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.WriteLine("Cancelling task");
            cts.Cancel();
            int result = await ExecuteAsync(cts.Token);
/*            Console.WriteLine("Cancelling task");
            cts.Cancel();*/
        }

        //офрмление задачи с задержкой времени перед выполнением
        public static async Task<T> WithTimeout<T>(Task<T> task, int time)
        {
            Task delayTask = Task.Delay(time);
            //Добавляем время задержки
            Task firstToFinish = await Task.WhenAny(task, delayTask);

            if (firstToFinish == delayTask)
            {
                task.ContinueWith(HandleException);
                throw new TimeoutException();
            }

            //Если дошли сюда, значит задача task уже завершилась
            return await task;
        }

        public static void HandleException<T>(Task<T> task)
        {
            if (task.Exception != null)
            {
                Console.WriteLine(task.Exception);
            }
        }

        public static Task<int> RunTaskWithProgress(int[] data, CancellationToken cToken, IProgress<int> progress)
        {

            
            /*        int processCount = await Task.Run<int>(() =>
                    {
                        Console.WriteLine("Начало работы RunTaskWithProgress");
                        int tempCount = 0;
                        int totalcount = data.Length;
                        foreach (var i in data)
                        {
                            //Thread.Sleep(500);
                            if (cToken.IsCancellationRequested)
                            {
                                Console.WriteLine("Прервано");
                                return tempCount;
                            }
                            tempCount++;
                            if (progress != null)
                            {
                                progress.Report((tempCount * 100 / totalcount));
                            }
                        }
                        return tempCount;
                    }
        );
                    return processCount;*/
            return new Task<int>(() => {
                Console.WriteLine("Начало работы RunTaskWithProgress");
                int tempCount = 0;
                int totalcount = data.Length;
                foreach (var i in data)
                {
                    Thread.Sleep(500);
                    if (cToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Прервано");
                        return tempCount;
                    }
                    tempCount++;
                    if (progress != null)
                    {
                        progress.Report((tempCount * 100 / totalcount));
                    }
                }
                return tempCount;
            });
        }

         public static void RunTaskWithProgress()
         {
             var indicator = new Progress<int>(delegate (int a) { 
                 Console.Write("\r{0} ", a); 
             });
             var data = new int[100];
             for (int i = 0; i < data.Length; i++)
             {
                 data[i] = i;
             }
             var cts = new CancellationTokenSource();
            //var result = await RunTaskWithProgress(data, cts.Token, indicator);
            //cts.Cancel();
            Console.WriteLine("введите N для отмены");
            var task = RunTaskWithProgress(data, cts.Token, indicator);
            task.Start();
            //cts.Cancel();
            var s = Console.ReadLine();
            if (s == "N")
            {
                cts.Cancel();
            }

        }

        static void Main(string[] args)
         {

            //new Program().LookupHostName();

            /*            Console.WriteLine("Start t1");
                        //Запуск задачи в другом потоке из пула
                        Task t1 = Task.Run(() =>
                        {
                            Thread.Sleep(10000);
                            Console.WriteLine("End t1");
                        }

                        ) ;*/
            //Console.WriteLine("End t1");

            //Для больше контроля можем использовать так
            /*            Task t2 = Task.Factory.StartNew(() => Dns.GetHostAddressesAsync("ya.ru"), 
                                                cancellationToken,
                                                TaskCreationOptions.LongRunning,
                                                taskScheduler
                                                );*/


            //-------------Работа с группой задач----------------
            //Ожидание всех задач Task.WhenAll(IEnumerable<Task> tasks)

            //Создаем список задач
            List<Task<int>> tasks = new List<Task<int>>();
            int[] numbers = new int[] { 1, 2, 3, 4, 5 };
            //Console.WriteLine(string.Join(" ", numbers));


            for (int i = 0; i < numbers.Length - 1; i++)
            {
                tasks.Add(MyLongComputation(numbers[i], numbers[i + 1]));
                //cделать через LINQ
            }

            //Задача будет закончена когда все задачи будут окончены
            /*            Console.WriteLine("AllTasks");
                        Task<int[]> allTask = Task.WhenAll(tasks);


                        RunTasks(allTask);*/


            //Если должна завершиться какая либо одна задача
            Console.WriteLine("AnyTasks");
            Task<Task<int>> anyTask = Task.WhenAny(tasks);
            RunAnyTasks(anyTask);


            //
            /*            Console.WriteLine("Tasks withTimeout");
                        for (int i = 0; i < numbers.Length - 1; i++)
                        {
                            tasks.Add(WithTimeout(MyLongComputation(numbers[i], numbers[i + 1]), 1000 * i));
                        }
                        RunTasks(Task.WhenAll(tasks));*/

            /*            Console.WriteLine("CancellationToken");
                        RunWithCancellationToken();*/

            //Console.WriteLine("Run with progress");
            //RunTaskWithProgress();
            //Console.WriteLine();

            Console.WriteLine("Press any key...");
            Console.ReadKey();

        }
    }
}