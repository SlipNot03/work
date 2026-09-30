using System;

namespace Indexer
{
    class Person
    {
        public string Name { get; set; }
        public Person(string name)
        {
            Name = name;
        }
    }

    class Company
    {
        Person[] personal;
        public Company(Person[] people)
        {
            personal = people;
        }

        public Person this[int index]
        {
            get => personal[index];
            set => personal[index] = value;
        }

        public Person this[string name]
        {
            get { 
                foreach(var person in personal)
                {
                    if (person.Name == name) return person;
                }
                throw new Exception("Unknown name");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var microsft = new Company(new[] {  
                new Person("Tom"), 
                new Person("Sam"),
                new Person("Alice")
            });
            Person fPerson = microsft[0];
            Console.WriteLine(fPerson.Name);

            microsft[0] = new Person("Mike");
            Console.WriteLine(microsft[0].Name);

            Console.WriteLine(microsft["Sam"].Name);

            Console.WriteLine(microsft["Test"].Name);


        }
    }
}
