using System;

namespace TestDelegates2
{
    class Program
    {
        delegate int Operation(int x, int y);
        delegate void Message();
        static void Main(string[] args)
        {
            //Operation operation = Add;      // делегат указывает на метод Add
            Operation operation = new Operation(Add);
            int result = operation(4, 5);   // фактически Add(4, 5)
            Console.WriteLine(result);      // 9

            operation = Multiply;           // теперь делегат указывает на метод Multiply
            result = operation(4, 5);       // фактически Multiply(4, 5)
            Console.WriteLine(result);      // 20

            int Add(int x, int y) => x + y;

            int Multiply(int x, int y) => x * y;

            //----------------------------
            Operation operation2 = Multiply;
            operation2 += Add;
            int result2 = operation2(3, 4);
            Console.WriteLine($"result2 = {result2}");

            //Добавление методов через +=

            void Hello() => Console.WriteLine("Hello");
            void Name() => Console.WriteLine("Bob");
            Message? message = Hello;
            message += Name;
            message();

            message -= Name;
            message();

            //объединение делегатов
            Message message2 = Name;
            message = message + message2;
            message();

            //Вызов через Invoke
            message.Invoke();

            //Список вызовов пуст
            message -= Name;
            message -= Hello;
            message?.Invoke();
        }
    }
}
