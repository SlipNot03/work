using System;

namespace AbstractClass
{
    abstract class Transport
    {
        public string Name { get; }
        public abstract void Move();

        public abstract int Speed { get; set; }

        public Transport(string Name)
        {
            this.Name = Name;
        }

        
    }

    // класс корабля
    class Ship : Transport 
    {
        public Ship(string name) : base(name) { }

        public override void Move()
        {
            Console.WriteLine($"Move {Name}");
        }

        public override int Speed { get;set; }

    }
    // класс самолета
    /*class Aircraft : Transport 
    {
        public Aircraft(string name) : base(name) { }
    }
    // класс машины
    class Car : Transport 
    {
        public Car(string Name) : base(Name) { }
    }*/

    class Program
    {
        static void Main(string[] args)
        {
            Transport ship = new Ship("Ship") { Speed = 100};
            //Transport car = new Car("Car");
            //Transport aircraft = new Aircraft("Aircraft");

            ship.Move();
            //car.Move();
            //aircraft.Move();
        }
    }
}
