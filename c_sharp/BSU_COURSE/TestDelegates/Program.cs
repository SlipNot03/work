using System;

namespace TestDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Message message1 = Welcome.Print;
            Message message2 = new Hello().Display;
            message1(); // Welcome
            message2(); // Привет
        }
    }

    delegate void Message();

    class Welcome
    {
        public static void Print() => Console.WriteLine("Welcome");
        //public static void Print() { Console.WriteLine("Welcome"); }
    }
    class Hello
    {
        public void Display() => Console.WriteLine("Привет");
    }

}
