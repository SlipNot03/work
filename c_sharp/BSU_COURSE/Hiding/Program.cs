using System;

namespace Hiding
{
class Person
    {
        public string Name { get; set; }
        public Person(string name)
        {
            Name = name;
        }
    }
    class Employee : Person
    {
        // скрываем свойство Name базового класса
        public new string Name
        {
            get => $"Mr./Ms. {base.Name}";
            set => base.Name = value;

        }
        public string Company { get; set; }
        public Employee(string name, string company)
                    : base(name)
        {
            Company = company;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Person bob = new Person("Bob");
            Console.WriteLine(bob.Name);    // Bob

            Employee tom = new Employee("Tom", "Microsoft");
            Console.WriteLine(tom.Name);    // Mr./Ms. Tom

        }
    }
}
