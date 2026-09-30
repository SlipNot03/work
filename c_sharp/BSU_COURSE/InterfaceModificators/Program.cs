using System;

namespace InterfaceModificators
{
    public interface IMovable
    {
        internal void Move();
        protected internal string Name { get; }
        delegate void MoveHandler();
        protected internal event MoveHandler MoveEvent;
    }

    class Person : IMovable
    {
        string name;

        string IMovable.Name  { get => name; }

        IMovable.MoveHandler moveEvent;

        event IMovable.MoveHandler IMovable.MoveEvent
        {
            add
            {
                moveEvent += value;
            }

            remove
            {
                moveEvent -= value;
            }
        }

        void IMovable.Move()
        {
            Console.WriteLine($"{name} is walking");
            moveEvent.Invoke();
        }

        public Person(string name)
        {
            this.name = name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IMovable p = new Person("Tom");
            p.MoveEvent += () => Console.WriteLine($"{p.Name} Moving");
            p.Move();
        }
    }
}
