using System;

namespace OverrideOperator
{
    class Counter
    {
        public int Value { get; set; }
        public static Counter operator +(Counter counter1, Counter counter2)
        {
            return new Counter { Value = counter1.Value + counter2.Value };
        }
        public static bool operator >(Counter counter1, Counter counter2)
        {
            return counter1.Value > counter2.Value;
        }
        public static bool operator <(Counter counter1, Counter counter2)
        {
            return counter1.Value < counter2.Value;
        }

        public static Counter operator ++(Counter counter1)
        {
            return new Counter { Value = counter1.Value + 10 };
        }

        public static bool operator true(Counter counter1)
        {
            return counter1.Value != 0;
        }
        public static bool operator false(Counter counter1)
        {
            return counter1.Value == 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Counter counter1 = new Counter { Value = 23 };
            Counter counter2 = new Counter { Value = 45 };
            bool result = counter1 > counter2;
            Console.WriteLine(result); // false

            Counter counter3 = counter1 + counter2;
            Console.WriteLine(counter3.Value);  // 23 + 45 = 68

            var counter4 = ++counter1;
            Console.WriteLine($"++counter1 {counter1.Value}"); //33
            Console.WriteLine($"++counter4 {counter4.Value}");

            var counter5 = counter2++;
            Console.WriteLine($"counter1++ {counter2.Value}"); //55
            Console.WriteLine($"++counter5 {counter5.Value}");

            if (counter1)
            {
                Console.WriteLine(true);
            } else
            {
                Console.WriteLine(false);
            }
        }
    }
}
