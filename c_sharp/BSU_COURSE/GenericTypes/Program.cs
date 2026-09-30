using System;

namespace GenericTypes
{
    class Person<T, K>
    {
        public T Id { get; set; }
        public K Password { get; set; }
        public string Name { get; set; }
        public Person(T id, K password, string name)
        {
            Id = id;
            Password = password;
            Name = name;
        }

        public static T Code { get; set; }

        public static void Swap<T>(ref T x, ref T y) 
        {
            T t = x;
            x = y;
            y = t;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person1 = new Person<int, int>(1, 123, "Tom");
            var person2 = new Person<string, int>("2", 234,  "Bob");
            Person<int, int>.Code = 1;
            int x = 10;
            int y = 20;
            Person<int, int>.Swap<int>(ref x, ref y);

            Console.WriteLine($"x={x} y={y}");
        }
    }
}
