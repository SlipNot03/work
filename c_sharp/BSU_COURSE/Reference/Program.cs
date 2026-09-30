using System;

namespace Reference
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            ref int xRef = ref x;

            Console.WriteLine(x);
            xRef = 125;

            Console.WriteLine(x);

            int[] numbers = { 1, 2, 3, 4, 5 };
            ref int numRef = ref Find(4, numbers);
            numRef = 9;
            Console.WriteLine(numbers[3]);


            ref int Find(int number, int[] numbers)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == number)
                    {
                        return ref numbers[i];
                    }
                }
                throw new IndexOutOfRangeException("Число не найдено");
            }

        }
        
    }
}
