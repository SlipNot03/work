using System;

namespace TupleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var tuple = (5, 10);
            Console.WriteLine(tuple.Item1);
            Console.WriteLine(tuple.Item2);

            tuple.Item1 += 26;
            Console.WriteLine(tuple.Item1);

            var tuple2 = (name: "tom", sum: 10, value: 25.1);
            var (name, age) = (tuple2.name, tuple2.sum);
            Console.WriteLine($"{name} {age}");

            var (val1, val2) = (1, 2);
            (val1, val2) = (val2, val1);
            Console.WriteLine($"{val1} {val2}");

            Console.WriteLine(GetValues());

            Print(("Tom", 15));

            (int val1, int val2) GetValues()
            {
                var result = (1, 2);
                return result;
            }

            void Print((string name, int age) person)
            {
                Console.WriteLine($"{person.name} {person.age}");
            }
            
        }
    }
}
