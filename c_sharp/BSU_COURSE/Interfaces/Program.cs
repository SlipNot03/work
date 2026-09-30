using System;

namespace Interfaces
{
    internal interface IMovable
    {
        public void Move()
        {
            Console.WriteLine("Interface Move");
        }

        int Speed { get { return 0; } }
    }

    public interface IInfo
    {
        public string Name { get; set; }
        public void Print();
    }

    public class Car : IMovable
    {
        public int Speed { get; set; }
    }

    public class Person : IMovable, IInfo
    {
        public string Name { get; set; }

        public void Print() => Console.WriteLine($"Person Print");

        void IMovable.Move() => Console.WriteLine("Person Move");
    }

    class Student: Person, IInfo
    {
        public new string Name { 
            get { return "Student"; }
        }
        void IInfo.Print() => Console.WriteLine($"Student Print");
    }

    

    class Program
    {
        static void DoAction(IMovable movable) => movable.Move();

        static void Main(string[] args)
        {
        /*    var person = new Person();
            var student = new Student();

            person.Print();
            //((IInfo)student).Print();
            //student.Print();*/

            var car = new Car() { Speed = 10};
            var person = new Person();

            DoAction(car);
            DoAction(person);


            (car as IMovable).Move();
            Console.WriteLine(car.Speed);
        }
    }
}
