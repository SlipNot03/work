using System;

namespace TestDlegates4
{
    class Program
    {
        delegate int Operation(int x, int y);
        static void Main(string[] args)
        {
            DoOperation(5, 4, Add);         // 9
            DoOperation(5, 4, Subtract);    // 1
            DoOperation(5, 4, Multiply);    // 20

            void DoOperation(int a, int b, Operation op)
            {
                Console.WriteLine(op(a, b));
            }
            
            int Add(int x, int y) => x + y;
            int Subtract(int x, int y) => x - y;
            int Multiply(int x, int y) => x * y;
            
        }
    }
}
