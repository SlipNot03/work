using System;

namespace FirstProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Bob";
            Console.WriteLine("Введите год рождения:");
            string year = Console.ReadLine();
            int age = DateTime.Today.Year - Convert.ToInt32(year);
            Console.WriteLine(name);
            Console.WriteLine($"Hello World, {name}!");
            Console.WriteLine($"Возраст: {age}");
        }
    }
}
