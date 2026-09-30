using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarLib
{
    class Car
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }
        private bool isDead;

        public Car() { MaxSpeed = 100; }
        public Car(string name, int maxSp, int currSp)
        {
            this.MaxSpeed = maxSp;
            this.PetName = name;
            this.CurrentSpeed = currSp;
        }

        public delegate void CarEngineHadler(string msg4Caller);
        private CarEngineHadler listOfHandlers;
        public void RegisterWithCarEngine(CarEngineHadler method)
        {
            listOfHandlers += method;
        }

        public void Accelerate(int delta)
        {
            if (isDead)
            {
                if (listOfHandlers != null)
                    listOfHandlers("Car is dead");
            }
            else
            {
                CurrentSpeed += delta;
                if ((MaxSpeed - CurrentSpeed) == 10 && listOfHandlers != null)
                {
                    listOfHandlers("Be Carefull");
                }
            }

            if (CurrentSpeed >= MaxSpeed)
                isDead = true;
            else
                Console.WriteLine("CurrentSpeed={0}", CurrentSpeed);
        }

    }

/*    class Program
    {

        static void Main(string[] args)
        {
            Car car = new Car("TestCar", maxSp: 100, currSp: 10);

            car.RegisterWithCarEngine(OnCarEngineEvent);

            for (int i = 0; i < 6; i++)
            {
                car.Accelerate(20);
            }

            Console.ReadKey();
        }

        public static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("Message from Car");
            Console.WriteLine("=>{0}", msg);
            Console.WriteLine("+++++++++++++++++++++\n");
        }

    }*/

}