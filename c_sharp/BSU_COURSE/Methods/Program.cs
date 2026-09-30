using System;

namespace Methods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int a, b;
            TestMethods(out a, out b);
            TestMethods2("1", 2, DateTime.Now, a, b);
            //Console.WriteLine($"{a} {b}");
        }

        static void TestMethods(out int a, out int b)
        {
            a = 1;
            b = 2;
            return;
        }

        static void TestMethods2(params object[] args)
        {
            foreach(var a in args)
            {
                Console.WriteLine($"{a.GetType()}: {a}");//object
            }
            return;
        }
    }
}
