using System;

namespace Inherit
{
    class Person
    {
        private string _name = "";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Person(string name)
        {
            Name = name;
        }

        public Person()
        {
            Name = "Tom";
        }

        public virtual void Print()
        {
            Console.WriteLine(Name);
        }
    }

    class Employee : Person
    {
        public string Company { get; set; }
        public Employee(string name, string company): base(name)
        {
            Company = company;
        }

        //Запечатанный метод
        public override sealed void Print()
        {
            Console.WriteLine($"{Name} {Company}");
        }
    }

    class Client : Person
    {
        public string Bank { get; set; }
        public Client(string name, string bank) : base(name)
        {
            Bank = bank;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee obj = new Employee("Tom", "Comp");
            obj.Print();
            
        }
    }
}
